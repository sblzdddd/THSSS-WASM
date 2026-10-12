using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Thsss.Browser
{
	public static class ContentLoader
	{
		private static readonly HashSet<string> readyFiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		private static readonly object readyLock = new object();

		public static bool BackgroundIdle
		{
			get
			{
				return true;
			}
		}

		public static bool IsFileReady(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return false;
			}
			string norm = path.Replace('\\', '/');
			int image = norm.IndexOf("Image/", StringComparison.OrdinalIgnoreCase);
			if (image < 0)
			{
				return true;
			}
			string rel = norm.Substring(image);
			lock (readyLock)
			{
				return readyFiles.Contains(rel);
			}
		}

		public static async Task LoadAsync()
		{
			string root = "/content";
			try
			{
				Directory.CreateDirectory(root);
				Directory.SetCurrentDirectory(root);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Using default directory: " + ex.Message);
			}
			ThsssJs.SetStatus("Fetching content…");
			byte[] hosted = Array.Empty<byte>();
			try
			{
				hosted = ThsssJs.TakeArchive() ?? Array.Empty<byte>();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			if (hosted.Length == 0)
			{
				throw new InvalidDataException("Content archive was not provided.");
			}
			using (MemoryStream stream = new MemoryStream(hosted, false))
			{
				await UnpackAsync(stream);
			}
			EnsureDir("Sound");
			EnsureDir("BPic");
			EnsureDir("ScreenShot");
			EnsureDir("BGM");
			EnsureDir("Replay");
			EnsureDir("CS");
			EnsureDir("Image");
			EnsureDir("Model");
			EnsureDir("Story");
			OverlaySaves();
			if (!File.Exists("Setting.INI"))
			{
				File.WriteAllText("Setting.INI", DefaultIni);
			}
			ThsssJs.SetStatus("Content ready.");
		}

		private static async Task UnpackAsync(Stream input)
		{
			byte[] magic = new byte[4];
			await ReadExactAsync(input, magic, 4);
			if (magic[0] != (byte)'T' || magic[1] != (byte)'H' || magic[2] != (byte)'D' || magic[3] != (byte)'A')
			{
				throw new InvalidDataException("Content archive is not a THDAT file.");
			}
			int version = await ReadU16Async(input);
			await ReadU16Async(input);
			if (version != 1)
			{
				throw new InvalidDataException("Unsupported content archive version.");
			}
			int count = await ReadU32Async(input);
			List<ArchiveEntry> entries = new List<ArchiveEntry>(count);
			int position = 12;
			for (int i = 0; i < count; i++)
			{
				int offset = await ReadU32Async(input);
				int size = await ReadU32Async(input);
				int nameLen = await ReadU16Async(input);
				byte[] nameBytes = new byte[nameLen];
				await ReadExactAsync(input, nameBytes, nameLen);
				position += 10 + nameLen;
				string name = Encoding.UTF8.GetString(nameBytes).Replace('\\', '/');
				if (!IsSafePath(name))
				{
					throw new InvalidDataException("Content archive path is not allowed.");
				}
				entries.Add(new ArchiveEntry
				{
					Name = name,
					Offset = offset,
					Size = size
				});
			}
			ThsssJs.SetStatus("Unpacking content…");
			foreach (ArchiveEntry entry in entries)
			{
				if (entry.Offset != position)
				{
					throw new InvalidDataException("Content archive entry is out of order.");
				}
				await WriteEntryAsync(input, entry);
				position += entry.Size;
				if (IsImage(entry.Name))
				{
					MarkReady(entry.Name);
				}
			}
		}

		private static async Task WriteEntryAsync(Stream input, ArchiveEntry entry)
		{
			string path = entry.Name.Replace('/', Path.DirectorySeparatorChar);
			string dir = Path.GetDirectoryName(path);
			if (!string.IsNullOrEmpty(dir))
			{
				Directory.CreateDirectory(dir);
			}
			byte[] audio = IsRuntimeAudio(entry.Name) && entry.Size > 0 ? new byte[entry.Size] : null;
			int audioAt = 0;
			using (FileStream output = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				byte[] buffer = new byte[81920];
				int left = entry.Size;
				while (left > 0)
				{
					int want = left < buffer.Length ? left : buffer.Length;
					int read = await input.ReadAsync(buffer, 0, want);
					if (read <= 0)
					{
						throw new EndOfStreamException("Truncated content archive.");
					}
					await output.WriteAsync(buffer, 0, read);
					if (audio != null)
					{
						Buffer.BlockCopy(buffer, 0, audio, audioAt, read);
						audioAt += read;
					}
					left -= read;
				}
			}
			if (audio != null)
			{
				ThsssJs.PutAudioFile("/" + entry.Name, audio);
			}
		}

		private static async Task ReadExactAsync(Stream input, byte[] buffer, int count)
		{
			int offset = 0;
			while (offset < count)
			{
				int read = await input.ReadAsync(buffer, offset, count - offset);
				if (read <= 0)
				{
					throw new EndOfStreamException("Truncated content archive.");
				}
				offset += read;
			}
		}

		private static async Task<int> ReadU16Async(Stream input)
		{
			byte[] buffer = new byte[2];
			await ReadExactAsync(input, buffer, 2);
			return buffer[0] | (buffer[1] << 8);
		}

		private static async Task<int> ReadU32Async(Stream input)
		{
			byte[] buffer = new byte[4];
			await ReadExactAsync(input, buffer, 4);
			return buffer[0] | (buffer[1] << 8) | (buffer[2] << 16) | (buffer[3] << 24);
		}

		private static bool IsSafePath(string rel)
		{
			if (string.IsNullOrEmpty(rel) || rel.StartsWith("/", StringComparison.Ordinal) || rel.IndexOf(':') >= 0)
			{
				return false;
			}
			string[] parts = rel.Split('/');
			foreach (string part in parts)
			{
				if (part.Length == 0 || part == "." || part == "..")
				{
					return false;
				}
			}
			return true;
		}

		private static bool IsImage(string rel)
		{
			return rel.StartsWith("Image/", StringComparison.OrdinalIgnoreCase);
		}

		private static bool IsRuntimeAudio(string rel)
		{
			return rel.StartsWith("Sound/", StringComparison.OrdinalIgnoreCase)
				&& (rel.EndsWith(".wav", StringComparison.OrdinalIgnoreCase) || rel.EndsWith(".ogg", StringComparison.OrdinalIgnoreCase));
		}

		private static void MarkReady(string rel)
		{
			lock (readyLock)
			{
				readyFiles.Add(rel);
			}
		}

		private static void EnsureDir(string name)
		{
			if (!Directory.Exists(name))
			{
				Directory.CreateDirectory(name);
			}
		}

		private static void OverlaySaves()
		{
			string json = ThsssJs.ReadSaves();
			if (string.IsNullOrWhiteSpace(json) || json == "{}")
			{
				return;
			}
			Dictionary<string, string> map = JsonSerializer.Deserialize(json, SaveJsonContext.Default.DictionaryStringString);
			if (map == null)
			{
				return;
			}
			foreach (KeyValuePair<string, string> pair in map)
			{
				if (string.IsNullOrEmpty(pair.Value))
				{
					continue;
				}
				byte[] bytes = Convert.FromBase64String(pair.Value);
				string path = pair.Key.Replace('/', Path.DirectorySeparatorChar);
				string dir = Path.GetDirectoryName(path);
				if (!string.IsNullOrEmpty(dir))
				{
					Directory.CreateDirectory(dir);
				}
				File.WriteAllBytes(path, bytes);
			}
		}

		private sealed class ArchiveEntry
		{
			public string Name;

			public int Offset;

			public int Size;
		}

		private const string DefaultIni = "[DirectX]\r\nFullWindow=0\r\nAntiAlias=0\r\nVerticalSync=0\r\n[DirectInput]\r\nUseDirectInput=0\r\n[Mode]\r\nClear=no\r\nSingleThreaded=1\r\nFixedFPS=0\r\nUnlockFPS=0\r\nDelayTolerance=10\r\nWindowSize=0\r\nPresentInterval=1\r\n[Key]\r\nLeft=Left\r\nRight=Right\r\nUp=Up\r\nDown=Down\r\nSlow=ShiftKey\r\nShoot=Z\r\nBomb=X\r\n[JoyStick]\r\nSkip=0\r\nPause=0\r\nSlow=0\r\nShoot=0\r\nBomb=0\r\nC=0\r\n[Volume]\r\nBGMVolume=80\r\nSEVolume=80\r\n[Game]\r\nFontType=Arial\r\n";
	}
}
