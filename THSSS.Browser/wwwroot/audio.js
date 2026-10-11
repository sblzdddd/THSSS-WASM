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

  function prepare(path) {
    const url = assetUrl(path);
    if (!url) return 0;
    let rec = buffers.get(url);
    if (rec) return rec.id;
    const audio = context();
    rec = { id: nextId++, url, audio: null, failed: false, waiters: [] };
    buffers.set(url, rec);
    fetch(url)
      .then((response) => {
        if (!response.ok) throw new Error(String(response.status));
        return response.arrayBuffer();
      })
      .then((data) => audio.decodeAudioData(data))
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
    const rate = rec.audio.sampleRate || 44100;
    const duration = rec.audio.duration || 0;
    const begin = Math.max(0, voice.playBegin || 0) / rate;
    let loopEnd = duration;
    if ((voice.loopLength || 0) > 0) {
      loopEnd = ((voice.loopBegin || 0) + voice.loopLength) / rate;
    }
    if ((voice.loopCount || 0) > 0 && duration > 0) {
      source.loop = true;
      source.loopStart = Math.max(0, Math.min((voice.loopBegin || 0) / rate, duration));
      source.loopEnd = Math.max(source.loopStart, Math.min(loopEnd, duration));
    }
    connect(voice, source);
    const playLength = (voice.playLength || 0) > 0 && !source.loop ? voice.playLength / rate : undefined;
    source.start(0, Math.min(begin, Math.max(0, duration - 0.001)), playLength);
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
