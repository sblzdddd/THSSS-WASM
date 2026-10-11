using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using SlimDX.Audio;
using SlimDX.DirectInput;
using SlimDX.Gl;
using Shooting;

namespace Thsss.Browser
{
	public static partial class GameHost
	{
		private static Game_Main game;

		private static int frame;

		private static bool unlockFps;

		private static readonly Dictionary<string, string> hashes = new Dictionary<string, string>();

		public static async System.Threading.Tasks.Task Start()
		{
			GlHost.Api = new JsGlApi();
			AudioHost.Api = new JsAudioApi();
			System.Drawing.GlyphRaster.Draw = delegate(string text, int width, int height, float sizePx, bool bold, bool italic, int argb, string family)
			{
				return ThsssJs.RasterTextBox(text, width, height, sizePx, bold, italic, argb, family ?? "sans-serif");
			};
			await ContentLoader.LoadAsync();
			ThsssJs.SetStatus("Creating the Direct3D device…");
			game = new Game_Main();
			if (!game.initSuccess)
			{
				throw new InvalidOperationException("Game_Main did not finish device init.");
			}
			FlushSaves();
			try
			{
				unlockFps = new INI_RW(".\\Setting.INI").IniReadValue("Mode", "UnlockFPS") == "1";
			}
			catch (Exception)
			{
				unlockFps = false;
			}
			ThsssJs.SetStatus("Device ready. Click the canvas. Arrows move, Z shoots, X bombs, Shift slows, Esc pauses.");
		}

		[System.Runtime.InteropServices.JavaScript.JSExport]
		public static int FpsUnlocked()
		{
			return unlockFps ? 1 : 0;
		}

		[System.Runtime.InteropServices.JavaScript.JSExport]
		public static void SetFpsUnlocked(int value)
		{
			unlockFps = value != 0;
			try
			{
				new INI_RW(".\\Setting.INI").IniWriteValue("Mode", "UnlockFPS", unlockFps ? "1" : "0");
			}
			catch (Exception)
			{
			}
		}

		[System.Runtime.InteropServices.JavaScript.JSExport]
		public static void Frame()
		{
			if (game == null || !game.initSuccess)
			{
				return;
			}
			try
			{
				game.MainProcess();
			}
			catch (Exception ex)
			{
				ThsssJs.SetStatus(ex.ToString());
				Console.WriteLine(ex);
				game = null;
				return;
			}
			frame++;
			if (frame % 30 == 0)
			{
				FlushSaves();
			}
		}

		[System.Runtime.InteropServices.JavaScript.JSExport]
		public static void OnKey(string name, int down)
		{
			if (game == null || game.Form_Main == null || string.IsNullOrEmpty(name))
			{
				return;
			}
			bool pressed = down != 0;
			if (pressed)
			{
				game.Form_Main.RaiseKeyDown(Keys.Parse(name));
			}
			else
			{
				game.Form_Main.RaiseKeyUp(Keys.Parse(name));
			}
			Key key;
			if (TryMap(name, out key))
			{
				if (pressed)
				{
					KeyboardStateMap.Down.Add(key);
				}
				else
				{
					KeyboardStateMap.Down.Remove(key);
				}
			}
		}

		private static bool TryMap(string name, out Key key)
		{
			switch (name)
			{
			case "Left": key = Key.LeftArrow; return true;
			case "Right": key = Key.RightArrow; return true;
			case "Up": key = Key.UpArrow; return true;
			case "Down": key = Key.DownArrow; return true;
			case "ShiftKey": key = Key.LeftShift; return true;
			case "ControlKey": key = Key.LeftControl; return true;
			case "Menu": key = Key.LeftAlt; return true;
			case "Return": key = Key.Return; return true;
			case "Escape": key = Key.Escape; return true;
			case "Space": key = Key.Space; return true;
			case "Z": key = Key.Z; return true;
			case "X": key = Key.X; return true;
			case "P": key = Key.P; return true;
			case "R": key = Key.R; return true;
			case "OemMinus": key = Key.Minus; return true;
			case "Oemplus": key = Key.Equals; return true;
			case "F8": key = Key.F8; return true;
			default:
				key = Key.Escape;
				return false;
			}
		}

		private static void FlushSaves()
		{
			Remember("Setting.INI");
			Remember("Score.dat");
			Remember("Log.txt");
			if (Directory.Exists("Replay"))
			{
				foreach (string file in Directory.GetFiles("Replay"))
				{
					Remember(file);
				}
			}
		}

		private static void Remember(string path)
		{
			if (!File.Exists(path))
			{
				return;
			}
			byte[] bytes = File.ReadAllBytes(path);
			string hash;
			using (SHA256 sha = SHA256.Create())
			{
				hash = Convert.ToBase64String(sha.ComputeHash(bytes));
			}
			string previous;
			if (hashes.TryGetValue(path, out previous) && previous == hash)
			{
				return;
			}
			hashes[path] = hash;
			ThsssJs.WriteSave(path.Replace('\\', '/'), Convert.ToBase64String(bytes));
		}
	}
}
