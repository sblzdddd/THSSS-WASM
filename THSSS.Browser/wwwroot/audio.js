export function createAudio() {
  let ctx = null;
  let nextId = 1;
  const buffers = new Map();
  const voices = new Map();
  const plays = [];

  function context() {
    if (!ctx) {
      const AC = window.AudioContext || window.webkitAudioContext;
      ctx = new AC();
    }
    return ctx;
  }

  function publish() {
    const playsNow = plays.slice(-12);
    window.__thsssAudio = {
      get state() {
        return ctx ? ctx.state : "none";
      },
      get currentTime() {
        return ctx ? ctx.currentTime : 0;
      },
      plays: playsNow
    };
  }

  function resume() {
    const audio = context();
    if (audio.state === "suspended") {
      audio.resume().then(publish).catch(() => {});
    }
    publish();
  }

  function assetUrl(path) {
    const text = String(path || "").replace(/\\/g, "/");
    const match = text.match(/(?:^|\/)(BGM|Sound)\/([^/]+)$/i);
    if (!match) return "";
    const folder = match[1].toLowerCase() === "bgm" ? "BGM" : "Sound";
    return new URL("assets/" + folder + "/" + match[2], document.baseURI).href;
  }

  function wavSampleRate(data) {
    if (!data || data.byteLength < 28) return 0;
    const view = new DataView(data);
    if (view.getUint32(0, false) !== 0x52494646 || view.getUint32(8, false) !== 0x57415645) return 0;
    let offset = 12;
    while (offset + 16 <= data.byteLength) {
      const id = view.getUint32(offset, false);
      const size = view.getUint32(offset + 4, true);
      if (id === 0x666d7420) {
        const rate = view.getUint32(offset + 12, true);
        return rate >= 8000 && rate <= 192000 ? rate : 0;
      }
      if (!size || size > data.byteLength) break;
      offset += 8 + size + (size & 1);
    }
    return 0;
  }

  function prepare(path) {
    const url = assetUrl(path);
    if (!url) return 0;
    let rec = buffers.get(url);
    if (rec) return rec.id;
    const audio = context();
    rec = { id: nextId++, url, audio: null, fileRate: 44100, failed: false, waiters: [] };
    buffers.set(url, rec);
    fetch(url)
      .then((response) => {
        if (!response.ok) throw new Error(String(response.status));
        return response.arrayBuffer();
      })
      .then((data) => {
        rec.fileRate = wavSampleRate(data) || 44100;
        return audio.decodeAudioData(data);
      })
      .then((decoded) => {
        rec.audio = decoded;
        const waiting = rec.waiters.splice(0);
        waiting.forEach((voice) => {
          if (voice.want) arm(voice);
        });
        publish();
      })
      .catch(() => {
        rec.failed = true;
        rec.waiters = [];
        publish();
      });
    return rec.id;
  }

  function bufferOf(id) {
    for (const rec of buffers.values()) {
      if (rec.id === id) return rec;
    }
    return null;
  }

  function connect(voice, source) {
    const audio = context();
    const gain = audio.createGain();
    const mid = (voice.left + voice.right) * 0.5;
    gain.gain.value = Math.max(0, voice.volume * mid);
    const filter = audio.createBiquadFilter();
    filter.type = "highpass";
    filter.frequency.value = voice.highpass && voice.frequency > 0 ? voice.frequency * (audio.sampleRate * 0.5) : 10;
    filter.Q.value = 0.5;
    source.connect(filter);
    filter.connect(gain);
    voice.gain = gain;
    voice.filterNode = filter;
    if (audio.createStereoPanner) {
      const panner = audio.createStereoPanner();
      const span = voice.left + voice.right;
      panner.pan.value = span <= 0.0001 ? 0 : Math.max(-1, Math.min(1, (voice.right - voice.left) / span));
      gain.connect(panner);
      panner.connect(audio.destination);
      voice.panner = panner;
    } else {
      gain.connect(audio.destination);
      voice.panner = null;
    }
  }

  function stopNode(voice) {
    if (!voice.node) return;
    try {
      voice.node.stop();
    } catch (err) {
    }
    voice.node.disconnect();
    voice.node = null;
  }

  function arm(voice) {
    const rec = bufferOf(voice.bufferId);
    if (!rec || !rec.audio) return;
    const audio = context();
    stopNode(voice);
    const source = audio.createBufferSource();
    source.buffer = rec.audio;
    const rate = rec.fileRate || 44100;
    const duration = rec.audio.duration || 0;
    const samplesToSeconds = (samples) => Math.max(0, samples || 0) / rate;
    const begin = samplesToSeconds(voice.playBegin);
    let loopStart = 0;
    let loopEnd = duration;
    if ((voice.loopLength || 0) > 0) {
      loopStart = samplesToSeconds(voice.loopBegin);
      loopEnd = samplesToSeconds((voice.loopBegin || 0) + voice.loopLength);
    } else if ((voice.playLength || 0) > 0) {
      loopStart = begin;
      loopEnd = begin + samplesToSeconds(voice.playLength);
    }
    if ((voice.loopCount || 0) > 0 && duration > 0) {
      source.loop = true;
      source.loopStart = Math.min(Math.max(0, loopStart), duration);
      source.loopEnd = Math.min(Math.max(loopEnd, source.loopStart), duration);
      if (source.loopEnd <= source.loopStart) {
        source.loopStart = 0;
        source.loopEnd = duration;
      }
    }
    connect(voice, source);
    const playLength = (voice.playLength || 0) > 0 && !source.loop ? samplesToSeconds(voice.playLength) : undefined;
    const offset = Math.min(begin, Math.max(0, duration - 0.001));
    source.start(0, offset, playLength);
    voice.node = source;
    voice.startedAt = audio.currentTime;
    voice.origin = voice.playBegin || 0;
    voice.rate = rate;
    plays.push({ url: rec.url, loop: !!source.loop, at: audio.currentTime });
    publish();
  }

  function voiceOf(id) {
    return voices.get(id | 0) || null;
  }

  function createVoice() {
    const id = nextId++;
    voices.set(id, {
      id,
      bufferId: 0,
      volume: 1,
      left: 1,
      right: 1,
      highpass: false,
      frequency: 0,
      playBegin: 0,
      playLength: 0,
      loopCount: 0,
      loopBegin: 0,
      loopLength: 0,
      want: false,
      node: null,
      startedAt: 0,
      origin: 0,
      rate: 44100
    });
    return id;
  }

  return {
    resume,
    prepare,
    createVoice,
    destroyVoice(id) {
      const voice = voiceOf(id);
      if (!voice) return;
      voice.want = false;
      stopNode(voice);
      voices.delete(id | 0);
    },
    submit(voiceId, bufferId, playBegin, playLength, loopCount, loopBegin, loopLength) {
      const voice = voiceOf(voiceId);
      if (!voice) return;
      voice.bufferId = bufferId | 0;
      voice.playBegin = playBegin | 0;
      voice.playLength = playLength | 0;
      voice.loopCount = loopCount | 0;
      voice.loopBegin = loopBegin | 0;
      voice.loopLength = loopLength | 0;
    },
    start(id) {
      const voice = voiceOf(id);
      if (!voice) return;
      resume();
      voice.want = true;
      const rec = bufferOf(voice.bufferId);
      if (!rec || rec.failed) return;
      if (!rec.audio) {
        if (rec.waiters.indexOf(voice) < 0) rec.waiters.push(voice);
        return;
      }
      arm(voice);
    },
    stop(id) {
      const voice = voiceOf(id);
      if (!voice) return;
      voice.want = false;
      if (voice.node && ctx) {
        const elapsed = ctx.currentTime - voice.startedAt;
        voice.origin += Math.max(0, Math.floor(elapsed * voice.rate));
      }
      stopNode(voice);
    },
    setVolume(id, volume) {
      const voice = voiceOf(id);
      if (!voice) return;
      voice.volume = volume;
      if (voice.gain) {
        voice.gain.gain.value = Math.max(0, volume * (voice.left + voice.right) * 0.5);
      }
    },
    setPan(id, left, right) {
      const voice = voiceOf(id);
      if (!voice) return;
      voice.left = left;
      voice.right = right;
      if (voice.gain) {
        voice.gain.gain.value = Math.max(0, voice.volume * (left + right) * 0.5);
      }
      if (voice.panner) {
        const span = left + right;
        voice.panner.pan.value = span <= 0.0001 ? 0 : Math.max(-1, Math.min(1, (right - left) / span));
      }
    },
    setFilter(id, highpass, frequency) {
      const voice = voiceOf(id);
      if (!voice) return;
      voice.highpass = !!highpass;
      voice.frequency = frequency;
      if (voice.filterNode && ctx) {
        voice.filterNode.frequency.value = voice.highpass && frequency > 0 ? frequency * (ctx.sampleRate * 0.5) : 10;
      }
    },
    samplesPlayed(id) {
      const voice = voiceOf(id);
      if (!voice || !ctx) return 0;
      if (!voice.node) return voice.origin | 0;
      const elapsed = ctx.currentTime - voice.startedAt;
      return (voice.origin + Math.max(0, Math.floor(elapsed * voice.rate))) | 0;
    },
    channels(bufferId) {
      const rec = bufferOf(bufferId);
      return rec && rec.audio ? rec.audio.numberOfChannels : 0;
    }
  };
}
