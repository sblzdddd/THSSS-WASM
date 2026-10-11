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
			using (HttpClient client = new HttpClient())
			{
				client.BaseAddress = new Uri(ThsssJs.PageBase());
				string manifest = await client.GetStringAsync("assets/manifest.txt");
				string[] lines = manifest.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
				int[] counter = new int[1];
				using (SemaphoreSlim gate = new SemaphoreSlim(8))
				{
					List<Task> tasks = new List<Task>();
					foreach (string line in lines)
					{
						string rel = line.Trim().Replace('\\', '/');
						if (rel.Length == 0 || rel == "manifest.txt" || rel.StartsWith("BGM/", StringComparison.OrdinalIgnoreCase))
						{
							continue;
						}
						tasks.Add(FetchOne(client, gate, rel, lines.Length, counter));
					}
					await Task.WhenAll(tasks);
				}
			}
			EnsureDir("Sound");
			EnsureDir("BPic");
			EnsureDir("ScreenShot");
			EnsureDir("BGM");
			EnsureDir("Replay");
			EnsureDir("CS");
			EnsureDir("Image");
			OverlaySaves();
			if (!File.Exists("Setting.INI"))
			{
				File.WriteAllText("Setting.INI", DefaultIni);
			}
			ThsssJs.SetStatus("Content ready.");
		}

		private static async Task FetchOne(HttpClient client, SemaphoreSlim gate, string rel, int total, int[] counter)
		{
			await gate.WaitAsync();
			try
			{
				byte[] bytes;
				try
				{
					bytes = await client.GetByteArrayAsync("assets/" + rel);
				}
				catch (Exception ex)
				{
					throw new FileNotFoundException("Missing content file assets/" + rel, ex);
				}
				string path = rel.Replace('/', Path.DirectorySeparatorChar);
				string dir = Path.GetDirectoryName(path);
				if (!string.IsNullOrEmpty(dir))
				{
					Directory.CreateDirectory(dir);
				}
				File.WriteAllBytes(path, bytes);
				int n = System.Threading.Interlocked.Increment(ref counter[0]);
				if (n % 40 == 0)
				{
					ThsssJs.SetStatus("Fetched " + n + " / " + total + " files…");
				}
			}
			finally
			{
				gate.Release();
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
