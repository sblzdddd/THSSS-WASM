using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Thsss.Browser
{
	public static class ContentLoader
	{
		private static readonly HashSet<string> readyFiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		private static readonly object readyLock = new object();

		private static int backgroundLeft;

		private static Task backgroundTask;

		private static readonly byte[] TinyPng = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mP8z8BQDwAEhQGAhKmMIQAAAABJRU5ErkJggg==");

		public static bool BackgroundIdle
		{
			get
			{
				return Volatile.Read(ref backgroundLeft) == 0;
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
			ThsssJs.SetStatus("Fetching content list…");
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(ThsssJs.PageBase());
			string manifest = await client.GetStringAsync("assets/manifest.txt");
			string[] lines = manifest.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			List<string> priority = new List<string>();
			List<string> rest = new List<string>();
			foreach (string line in lines)
			{
				string rel = line.Trim().Replace('\\', '/');
				if (rel.Length == 0 || rel == "manifest.txt" || rel.StartsWith("BGM/", StringComparison.OrdinalIgnoreCase))
				{
					continue;
				}
				if (IsSound(rel))
				{
					Touch(rel);
					continue;
				}
				if (IsPriority(rel))
				{
					priority.Add(rel);
					continue;
				}
				if (IsImage(rel))
				{
					Touch(rel);
				}
				rest.Add(rel);
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
			await FetchGroup(client, priority, true);
			OverlaySaves();
			if (!File.Exists("Setting.INI"))
			{
				File.WriteAllText("Setting.INI", DefaultIni);
			}
			if (rest.Count == 0)
			{
				client.Dispose();
			}
			else
			{
				Interlocked.Exchange(ref backgroundLeft, rest.Count);
				backgroundTask = FetchRest(client, rest);
			}
			ThsssJs.SetStatus("Content ready.");
		}

		private static async Task FetchRest(HttpClient client, List<string> rest)
		{
			try
			{
				await FetchGroup(client, rest, false);
			}
			finally
			{
				client.Dispose();
			}
		}

		private static async Task FetchGroup(HttpClient client, List<string> files, bool required)
		{
			using (SemaphoreSlim gate = new SemaphoreSlim(8))
			{
				List<Task> tasks = new List<Task>();
				foreach (string rel in files)
				{
					tasks.Add(FetchOne(client, gate, rel, required));
				}
				await Task.WhenAll(tasks);
			}
		}

		private static async Task FetchOne(HttpClient client, SemaphoreSlim gate, string rel, bool required)
		{
			await gate.WaitAsync();
			try
			{
				byte[] bytes = null;
				try
				{
					bytes = await client.GetByteArrayAsync("assets/" + rel);
				}
				catch (Exception ex)
				{
					if (required)
					{
						throw new FileNotFoundException("Missing content file assets/" + rel, ex);
					}
					Console.WriteLine("Missing content file assets/" + rel + " " + ex.Message);
					if (IsImage(rel))
					{
						bytes = TinyPng;
					}
				}
				if (bytes != null)
				{
					WriteFile(rel, bytes);
					if (IsImage(rel))
					{
						MarkReady(rel);
					}
				}
			}
			finally
			{
				if (!required)
				{
					Interlocked.Decrement(ref backgroundLeft);
				}
				gate.Release();
			}
		}

		private static bool IsSound(string rel)
		{
			return rel.StartsWith("Sound/", StringComparison.OrdinalIgnoreCase);
		}

		private static bool IsImage(string rel)
		{
			return rel.StartsWith("Image/", StringComparison.OrdinalIgnoreCase);
		}

		private static bool IsPriority(string rel)
		{
			return rel.StartsWith("Image/NowLoading/", StringComparison.OrdinalIgnoreCase) || rel.Equals("FX/Blur.fx", StringComparison.OrdinalIgnoreCase) || rel.Equals("FX/Warp.fx", StringComparison.OrdinalIgnoreCase) || rel.Equals("FX/Filter.fx", StringComparison.OrdinalIgnoreCase) || rel.Equals("Score.dat", StringComparison.OrdinalIgnoreCase) || rel.StartsWith("Replay/", StringComparison.OrdinalIgnoreCase);
		}

		private static void MarkReady(string rel)
		{
			lock (readyLock)
			{
				readyFiles.Add(rel);
			}
		}

		private static void Touch(string rel)
		{
			WriteFile(rel, Array.Empty<byte>());
		}

		private static void WriteFile(string rel, byte[] bytes)
		{
			string path = rel.Replace('/', Path.DirectorySeparatorChar);
			string dir = Path.GetDirectoryName(path);
			if (!string.IsNullOrEmpty(dir))
			{
				Directory.CreateDirectory(dir);
			}
			File.WriteAllBytes(path, bytes);
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
			Dictionary<string, string> map = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
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

		private const string DefaultIni = "[DirectX]\r\nFullWindow=0\r\nAntiAlias=0\r\nVerticalSync=0\r\n[DirectInput]\r\nUseDirectInput=0\r\n[Mode]\r\nClear=no\r\nSingleThreaded=1\r\nFixedFPS=0\r\nDelayTolerance=10\r\nWindowSize=0\r\nPresentInterval=1\r\n[Key]\r\nLeft=Left\r\nRight=Right\r\nUp=Up\r\nDown=Down\r\nSlow=ShiftKey\r\nShoot=Z\r\nBomb=X\r\n[JoyStick]\r\nSkip=0\r\nPause=0\r\nSlow=0\r\nShoot=0\r\nBomb=0\r\nC=0\r\n[Volume]\r\nBGMVolume=80\r\nSEVolume=80\r\n[Game]\r\nFontType=Arial\r\n";
	}
}
