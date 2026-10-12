using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SlimDX.Direct3D9;
using SlimDX.XAudio2;

namespace Shooting
{
	public class GlobalDataPackage
	{
		public GlobalDataPackage()
		{
			TextureObjectDictionary = new Dictionary<string, TextureObject>();
			try
			{
				DeviceXaudio2 = new XAudio2();
				MasteringVoice = new MasteringVoice(DeviceXaudio2);
				SoundInitSuccess = true;
				BGM_Player = new Wave_Player(DeviceXaudio2);
			}
			catch
			{
				MessageBox.Show("音频设备故障", "Sound Initial Error");
			}
		}

		public void LoadResources1()
		{
			ScreenTexMan = new ScreenTexManager(DeviceMain);
			while (!LoadEffect())
			{
			}
			LoadingList = new List<string>();
			LoadFolderTextureList(".\\Image");
		}

		public void LoadResources()
		{
			PData = new PlayerData();
			LoadSound();
			LoadBulletPic();
			while (LoadingList.Count > 0)
			{
				Thread.Sleep(1);
			}
			Hash.Init();
		}

		public void LoadFolderTextureList(string Path)
		{
#if BROWSER
			var folder = Path.Replace((char)92, '/');
			if (!folder.EndsWith("/Image/NowLoading"))
#else
			if (!(Path == ".\\Image\\NowLoading"))
#endif
			{
				var files = Directory.GetFiles(Path, "*.png");
				foreach (var text in files)
				{
					var fileInfo = new FileInfo(text);
					LoadingList.Add(fileInfo.FullName);
				}
				var directories = Directory.GetDirectories(Path);
				foreach (var text in directories)
				{
					LoadFolderTextureList(text);
				}
			}
		}

		private bool LoadEffect()
		{
			EffectDictionary = new Dictionary<string, Effect>();
			bool flag;
			try
			{
				EffectDictionary.Add("Blur", Effect.FromFile(DeviceMain, ".\\FX\\Blur.fx", ShaderFlags.None));
				EffectDictionary.Add("Warp", Effect.FromFile(DeviceMain, ".\\FX\\Warp.fx", ShaderFlags.None));
				EffectDictionary.Add("Filter", Effect.FromFile(DeviceMain, ".\\FX\\Filter.fx", ShaderFlags.None));
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		public void LoadModelDictionary()
		{
			ModelDictionary = new Dictionary<string, IModel>();
			LoadModel("Petal00", 0);
			LoadModel("Petal01", 0);
			LoadModel("Petal02", 0);
			LoadModel("Petal03", 0);
			LoadModel("Petal10", 0);
			LoadModel("Petal11", 0);
			LoadModel("Petal12", 0);
			LoadModel("Petal13", 0);
			LoadModel("Petal20", 0);
			LoadModel("Petal21", 0);
			LoadModel("Petal22", 0);
			LoadModel("Petal23", 0);
			LoadModel("Petal30", 0);
			LoadModel("Petal31", 0);
			LoadModel("Petal32", 0);
			LoadModel("Petal33", 0);
			LoadModel("Effect-0", 0);
			LoadModel("Effect-1", 0);
			LoadModel("BG02d", 0);
			LoadModel("liba", 0);
			LoadModel("zhu", 0);
			LoadModel("柳树", 0);
			LoadModel("草地", 0);
			LoadModel("地砖", 0);
			LoadModel("黑暗滤镜", 0);
			LoadModel("极光素材", 0);
			LoadModel("万花镜素材", 0);
			LoadModel("草地ex", 0);
			LoadModel("Tree1", 0);
			LoadModel("Tree2", 0);
			LoadModel("591-1", 0);
			LoadModel("591-1R", 0);
			LoadModel("Leaf", 0);
			LoadModel("Tree", 0);
			LoadModel("Cloud01", 0);
			LoadModel("Cloud02", 0);
			LoadModel("夜空（暗）", 0);
			LoadModel("夜空（亮）", 0);
			LoadModel("云（暗层）", 0);
			LoadModel("云（亮层）", 0);
			LoadModel("BG01a", 0);
			LoadModel("Stair", 2);
			LoadModel("祭坛", 2);
		}

		private void LoadModel(string Name, int ModelType)
		{
			var flag = false;
			var num = 0;
			while (!flag && num < 20)
			{
				try
				{
					switch (ModelType)
					{
					case 0:
					{
						IModel model = new Model(DeviceMain, TextureObjectDictionary[Name], Name);
						ModelDictionary.Add(Name, model);
						break;
					}
					case 1:
					{
						IModel model = new Model_Circle(DeviceMain, TextureObjectDictionary[Name], Name);
						ModelDictionary.Add(Name, model);
						break;
					}
					case 2:
					{
						IModel model = new Model_Mesh(DeviceMain, Name)
						{
							GlobalData = this
						};
						ModelDictionary.Add(model.Name, model);
						break;
					}
					}
					flag = true;
				}
				catch
				{
					flag = false;
					num++;
				}
			}
			if (num == 20)
			{
				MessageBox.Show(Name + "模型生成时出错", "LoadModel Error");
			}
		}

		private void LoadBulletPic()
		{
			try
			{
				BulletPicDictionary = new Dictionary<string, bool[,]>();
				for (var i = 0; i < Directory.GetFiles(".\\BPic", "*.png").Length; i++)
				{
					var fileInfo = new FileInfo(Directory.GetFiles(".\\BPic", "*.png")[i]);
					var bitmap = (Bitmap)Image.FromFile(fileInfo.FullName);
					var array = new bool[bitmap.Width, bitmap.Height];
					for (var j = 0; j < bitmap.Width; j++)
					{
						for (var k = 0; k < bitmap.Height; k++)
						{
							array[j, k] = bitmap.GetPixel(j, k).G < 128;
						}
					}
					BulletPicDictionary.Add(fileInfo.Name, array);
				}
			}
			catch
			{
				MessageBox.Show("图像读取错误", "LoadBPic Error");
			}
		}

		public void LoadSound()
		{
			if (SoundInitSuccess)
			{
				SoundDictionary = new Dictionary<string, XAudio2_Player>();
				var files = Directory.GetFiles(".\\Sound", "*.wav");
				for (var i = 0; i < files.Length; i++)
				{
					var fileInfo = new FileInfo(files[i]);
					SoundDictionary.Add(fileInfo.Name, new XAudio2_Player(fileInfo.FullName, DeviceXaudio2)
					{
						Volume = SEVolume
					});
					if (!SoundDictionary[fileInfo.Name].loadSuccess)
					{
						MessageBox.Show(fileInfo.Name + "读取错误", "LoadSound Error");
					}
				}
			}
		}

		public void TextureObjectLoader(string imageFileName)
		{
			TextureObjectLoader(imageFileName, 0);
		}

		public void TextureObjectLoader(string imageFileName, int colorKey)
		{
			if (File.Exists(imageFileName))
			{
				var num = 0;
				Texture texture;
				while (!SetTexture(imageFileName, colorKey, new Rectangle(0, 0, 1, 1), out texture) && num++ < 20)
				{
				}
				if (texture == null)
				{
					MessageBox.Show(imageFileName + "纹理读取错误", "LoadTexture Error");
					return;
				}
				RegisterLoadedTexture(imageFileName, texture);
			}
		}

		public void RegisterLoadedTexture(string imageFileName, Texture texture)
		{
			var rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
			var fileInfo = new FileInfo(imageFileName);
			var text = fileInfo.FullName.Replace(fileInfo.Extension, ".txt");
			if (File.Exists(text))
			{
				var array = new char[] { '\t' };
				var array2 = File.ReadAllLines(text, Encoding.Default);
				foreach (var text2 in array2)
				{
					var array4 = text2.Split(array);
					var textureObject = new TextureObject();
					textureObject.TXTure = texture;
					textureObject.PosRect = new Rectangle(Convert.ToInt32(array4[1]), Convert.ToInt32(array4[2]), Convert.ToInt32(array4[3]), Convert.ToInt32(array4[4]));
					textureObject.OffsetX = Convert.ToInt32(array4[5]);
					textureObject.OffsetY = Convert.ToInt32(array4[6]);
					textureObject.SrcWidth = rectangle.Width;
					textureObject.SrcHeight = rectangle.Height;
					TextureObjectDictionary.Add(array4[0], textureObject);
				}
			}
			else
			{
				var textureObject = new TextureObject();
				textureObject.TXTure = texture;
				textureObject.PosRect = new Rectangle(0, 0, rectangle.Width, rectangle.Height);
				textureObject.SrcWidth = rectangle.Width;
				textureObject.SrcHeight = rectangle.Height;
				TextureObjectDictionary.Add(fileInfo.Name.Replace(".png", ""), textureObject);
			}
		}

		private bool SetTexture(string imageFileName, int colorKey, Rectangle Rect, out Texture texture)
		{
			bool flag;
			try
			{
				texture = Texture.FromFile(DeviceMain, imageFileName, Rect.Width, Rect.Height, 0, Usage.None, Format.Unknown, Pool.Managed, Filter.Linear, Filter.None, colorKey);
				flag = true;
			}
			catch
			{
				texture = null;
				flag = false;
			}
			return flag;
		}

		public void BGM_Play(string FileName)
		{
			try
			{
				var bgmvolume = BGMVolume;
				BGM_Player.FileName = FileName;
				BGM_Player.Volume = bgmvolume;
				BGM_Player.Play();
			}
			catch
			{
			}
		}

		public void BGM_OFF()
		{
			BGM_Player.Stop();
		}

		public void BGM_Pause()
		{
			BGM_Player.Puase();
		}

		public void BGM_Resume()
		{
			if (BGM_Player != null)
			{
				BGM_Player.Resume();
			}
		}

		public void OnLostDevice()
		{
			if (SpriteMain != null)
			{
				SpriteMain.sprite.OnLostDevice();
			}
			if (ScreenTexMan != null)
			{
				ScreenTexMan.Dispose();
			}
			if (EffectDictionary != null)
			{
				foreach (var effect in EffectDictionary.Values)
				{
					effect.OnLostDevice();
				}
			}
		}

		public void OnResetDevice()
		{
			SpriteMain.sprite.OnResetDevice();
			DeviceMain.Reset(new PresentParameters[] { presentParams });
			if (ScreenTexMan != null)
			{
				ScreenTexMan.Reset();
			}
			if (EffectDictionary != null)
			{
				foreach (var effect in EffectDictionary.Values)
				{
					effect.OnResetDevice();
				}
			}
		}

		public void Dispose()
		{
			DeviceMain.Direct3D.Dispose();
			SpriteMain.Dispose();
			if (TextureObjectDictionary != null)
			{
				foreach (var textureObject in TextureObjectDictionary.Values)
				{
					textureObject.Dispose();
				}
			}
			if (SoundDictionary != null)
			{
				foreach (var xaudio2_Player in SoundDictionary.Values)
				{
					xaudio2_Player.Dispose();
				}
			}
			if (BGM_Player != null)
			{
				BGM_Player.Dispose();
			}
			if (ScreenTexMan != null)
			{
				ScreenTexMan.Dispose();
			}
			if (EffectDictionary != null)
			{
				foreach (var effect in EffectDictionary.Values)
				{
					effect.Dispose();
				}
			}
			if (ModelDictionary != null)
			{
				foreach (var model in ModelDictionary.Values)
				{
					var model2 = (Model)model;
					model2.Dispose();
				}
			}
			if (DeviceXaudio2 != null)
			{
				DeviceXaudio2.Dispose();
			}
		}

		public const int WindowWidth = 640;

		public const int WindowHeight = 480;

		public XAudio2 DeviceXaudio2;

		public MasteringVoice MasteringVoice;

		public Device DeviceMain;

		public PresentParameters presentParams;

		public MySprite SpriteMain;

		public Effect EffectMain;

		public Dictionary<string, Effect> EffectDictionary;

		public Dictionary<string, TextureObject> TextureObjectDictionary;

		public Dictionary<string, XAudio2_Player> SoundDictionary;

		public Dictionary<string, IModel> ModelDictionary;

		public Dictionary<string, bool[,]> BulletPicDictionary;

		public Wave_Player BGM_Player;

		public List<string> LoadingList;

		public KeyClass KClass = new KeyClass();

		public bool Clear = false;

		public Replay Rep;

		public PlayerData PData;

		public ScreenTexManager ScreenTexMan;

		public IGameState LastState;

		public int BGMVolume = 100;

		public int SEVolume = 80;

		public string FontType = "宋体";

		public static float FontScale = 1f;

		private bool SoundInitSuccess;

#if !BROWSER
		public void TestFontScale()
		{
			global::System.Drawing.Font font = new global::System.Drawing.Font(this.FontType, 15f, FontStyle.Bold);
			Graphics graphics = Graphics.FromImage(new Bitmap(1, 1));
			float num = 832.4413f / graphics.MeasureString("国国国国国国国国国国国国国国国国国国国国国国国国国国国国国国国国国国国国国国国国", font).Width;
			font.Dispose();
			GlobalDataPackage.FontScale = num;
		}
#endif
	}
}
