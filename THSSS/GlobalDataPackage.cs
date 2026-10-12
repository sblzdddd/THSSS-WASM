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
			this.TextureObjectDictionary = new Dictionary<string, TextureObject>();
			try
			{
				this.DeviceXaudio2 = new XAudio2();
				this.MasteringVoice = new MasteringVoice(this.DeviceXaudio2);
				this.SoundInitSuccess = true;
				this.BGM_Player = new Wave_Player(this.DeviceXaudio2);
			}
			catch
			{
				MessageBox.Show("音频设备故障", "Sound Initial Error");
			}
		}

		public void LoadResources1()
		{
			this.ScreenTexMan = new ScreenTexManager(this.DeviceMain);
			while (!this.LoadEffect())
			{
			}
			this.LoadingList = new List<string>();
			this.LoadFolderTextureList(".\\Image");
		}

		public void LoadResources()
		{
			this.PData = new PlayerData();
			this.LoadSound();
			this.LoadBulletPic();
			while (this.LoadingList.Count > 0)
			{
				Thread.Sleep(1);
			}
			Hash.Init();
		}

		public void LoadFolderTextureList(string Path)
		{
#if BROWSER
			string folder = Path.Replace((char)92, '/');
			if (!folder.EndsWith("/Image/NowLoading"))
#else
			if (!(Path == ".\\Image\\NowLoading"))
#endif
			{
				string[] files = Directory.GetFiles(Path, "*.png");
				foreach (string text in files)
				{
					FileInfo fileInfo = new FileInfo(text);
					this.LoadingList.Add(fileInfo.FullName);
				}
				string[] directories = Directory.GetDirectories(Path);
				foreach (string text in directories)
				{
					this.LoadFolderTextureList(text);
				}
			}
		}

		private bool LoadEffect()
		{
			this.EffectDictionary = new Dictionary<string, Effect>();
			bool flag;
			try
			{
				this.EffectDictionary.Add("Blur", Effect.FromFile(this.DeviceMain, ".\\FX\\Blur.fx", ShaderFlags.None));
				this.EffectDictionary.Add("Warp", Effect.FromFile(this.DeviceMain, ".\\FX\\Warp.fx", ShaderFlags.None));
				this.EffectDictionary.Add("Filter", Effect.FromFile(this.DeviceMain, ".\\FX\\Filter.fx", ShaderFlags.None));
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
			this.ModelDictionary = new Dictionary<string, IModel>();
			this.LoadModel("Petal00", 0);
			this.LoadModel("Petal01", 0);
			this.LoadModel("Petal02", 0);
			this.LoadModel("Petal03", 0);
			this.LoadModel("Petal10", 0);
			this.LoadModel("Petal11", 0);
			this.LoadModel("Petal12", 0);
			this.LoadModel("Petal13", 0);
			this.LoadModel("Petal20", 0);
			this.LoadModel("Petal21", 0);
			this.LoadModel("Petal22", 0);
			this.LoadModel("Petal23", 0);
			this.LoadModel("Petal30", 0);
			this.LoadModel("Petal31", 0);
			this.LoadModel("Petal32", 0);
			this.LoadModel("Petal33", 0);
			this.LoadModel("Effect-0", 0);
			this.LoadModel("Effect-1", 0);
			this.LoadModel("BG02d", 0);
			this.LoadModel("liba", 0);
			this.LoadModel("zhu", 0);
			this.LoadModel("柳树", 0);
			this.LoadModel("草地", 0);
			this.LoadModel("地砖", 0);
			this.LoadModel("黑暗滤镜", 0);
			this.LoadModel("极光素材", 0);
			this.LoadModel("万花镜素材", 0);
			this.LoadModel("草地ex", 0);
			this.LoadModel("Tree1", 0);
			this.LoadModel("Tree2", 0);
			this.LoadModel("591-1", 0);
			this.LoadModel("591-1R", 0);
			this.LoadModel("Leaf", 0);
			this.LoadModel("Tree", 0);
			this.LoadModel("Cloud01", 0);
			this.LoadModel("Cloud02", 0);
			this.LoadModel("夜空（暗）", 0);
			this.LoadModel("夜空（亮）", 0);
			this.LoadModel("云（暗层）", 0);
			this.LoadModel("云（亮层）", 0);
			this.LoadModel("BG01a", 0);
			this.LoadModel("Stair", 2);
			this.LoadModel("祭坛", 2);
		}

		private void LoadModel(string Name, int ModelType)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < 20)
			{
				try
				{
					switch (ModelType)
					{
					case 0:
					{
						IModel model = new Model(this.DeviceMain, this.TextureObjectDictionary[Name], Name);
						this.ModelDictionary.Add(Name, model);
						break;
					}
					case 1:
					{
						IModel model = new Model_Circle(this.DeviceMain, this.TextureObjectDictionary[Name], Name);
						this.ModelDictionary.Add(Name, model);
						break;
					}
					case 2:
					{
						IModel model = new Model_Mesh(this.DeviceMain, Name)
						{
							GlobalData = this
						};
						this.ModelDictionary.Add(model.Name, model);
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
				this.BulletPicDictionary = new Dictionary<string, bool[,]>();
				for (int i = 0; i < Directory.GetFiles(".\\BPic", "*.png").Length; i++)
				{
					FileInfo fileInfo = new FileInfo(Directory.GetFiles(".\\BPic", "*.png")[i]);
					Bitmap bitmap = (Bitmap)Image.FromFile(fileInfo.FullName);
					bool[,] array = new bool[bitmap.Width, bitmap.Height];
					for (int j = 0; j < bitmap.Width; j++)
					{
						for (int k = 0; k < bitmap.Height; k++)
						{
							array[j, k] = bitmap.GetPixel(j, k).G < 128;
						}
					}
					this.BulletPicDictionary.Add(fileInfo.Name, array);
				}
			}
			catch
			{
				MessageBox.Show("图像读取错误", "LoadBPic Error");
			}
		}

		public void LoadSound()
		{
			if (this.SoundInitSuccess)
			{
				this.SoundDictionary = new Dictionary<string, XAudio2_Player>();
				string[] files = Directory.GetFiles(".\\Sound", "*.wav");
				for (int i = 0; i < files.Length; i++)
				{
					FileInfo fileInfo = new FileInfo(files[i]);
					this.SoundDictionary.Add(fileInfo.Name, new XAudio2_Player(fileInfo.FullName, this.DeviceXaudio2)
					{
						Volume = this.SEVolume
					});
					if (!this.SoundDictionary[fileInfo.Name].loadSuccess)
					{
						MessageBox.Show(fileInfo.Name + "读取错误", "LoadSound Error");
					}
				}
			}
		}

		public void TextureObjectLoader(string imageFileName)
		{
			this.TextureObjectLoader(imageFileName, 0);
		}

		public void TextureObjectLoader(string imageFileName, int colorKey)
		{
			if (File.Exists(imageFileName))
			{
				int num = 0;
				Texture texture;
				while (!this.SetTexture(imageFileName, colorKey, new Rectangle(0, 0, 1, 1), out texture) && num++ < 20)
				{
				}
				if (texture == null)
				{
					MessageBox.Show(imageFileName + "纹理读取错误", "LoadTexture Error");
					return;
				}
				this.RegisterLoadedTexture(imageFileName, texture);
			}
		}

		public void RegisterLoadedTexture(string imageFileName, Texture texture)
		{
			Rectangle rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
			FileInfo fileInfo = new FileInfo(imageFileName);
			string text = fileInfo.FullName.Replace(fileInfo.Extension, ".txt");
			if (File.Exists(text))
			{
				char[] array = new char[] { '\t' };
				string[] array2 = File.ReadAllLines(text, Encoding.Default);
				foreach (string text2 in array2)
				{
					string[] array4 = text2.Split(array);
					TextureObject textureObject = new TextureObject();
					textureObject.TXTure = texture;
					textureObject.PosRect = new Rectangle(Convert.ToInt32(array4[1]), Convert.ToInt32(array4[2]), Convert.ToInt32(array4[3]), Convert.ToInt32(array4[4]));
					textureObject.OffsetX = Convert.ToInt32(array4[5]);
					textureObject.OffsetY = Convert.ToInt32(array4[6]);
					textureObject.SrcWidth = rectangle.Width;
					textureObject.SrcHeight = rectangle.Height;
					this.TextureObjectDictionary.Add(array4[0], textureObject);
				}
			}
			else
			{
				TextureObject textureObject = new TextureObject();
				textureObject.TXTure = texture;
				textureObject.PosRect = new Rectangle(0, 0, rectangle.Width, rectangle.Height);
				textureObject.SrcWidth = rectangle.Width;
				textureObject.SrcHeight = rectangle.Height;
				this.TextureObjectDictionary.Add(fileInfo.Name.Replace(".png", ""), textureObject);
			}
		}

		private bool SetTexture(string imageFileName, int colorKey, Rectangle Rect, out Texture texture)
		{
			bool flag;
			try
			{
				texture = Texture.FromFile(this.DeviceMain, imageFileName, Rect.Width, Rect.Height, 0, Usage.None, Format.Unknown, Pool.Managed, Filter.Linear, Filter.None, colorKey);
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
				int bgmvolume = this.BGMVolume;
				this.BGM_Player.FileName = FileName;
				this.BGM_Player.Volume = bgmvolume;
				this.BGM_Player.Play();
			}
			catch
			{
			}
		}

		public void BGM_OFF()
		{
			this.BGM_Player.Stop();
		}

		public void BGM_Pause()
		{
			this.BGM_Player.Puase();
		}

		public void BGM_Resume()
		{
			if (this.BGM_Player != null)
			{
				this.BGM_Player.Resume();
			}
		}

		public void OnLostDevice()
		{
			if (this.SpriteMain != null)
			{
				this.SpriteMain.sprite.OnLostDevice();
			}
			if (this.ScreenTexMan != null)
			{
				this.ScreenTexMan.Dispose();
			}
			if (this.EffectDictionary != null)
			{
				foreach (Effect effect in this.EffectDictionary.Values)
				{
					effect.OnLostDevice();
				}
			}
		}

		public void OnResetDevice()
		{
			this.SpriteMain.sprite.OnResetDevice();
			this.DeviceMain.Reset(new PresentParameters[] { this.presentParams });
			if (this.ScreenTexMan != null)
			{
				this.ScreenTexMan.Reset();
			}
			if (this.EffectDictionary != null)
			{
				foreach (Effect effect in this.EffectDictionary.Values)
				{
					effect.OnResetDevice();
				}
			}
		}

		public void Dispose()
		{
			this.DeviceMain.Direct3D.Dispose();
			this.SpriteMain.Dispose();
			if (this.TextureObjectDictionary != null)
			{
				foreach (TextureObject textureObject in this.TextureObjectDictionary.Values)
				{
					textureObject.Dispose();
				}
			}
			if (this.SoundDictionary != null)
			{
				foreach (XAudio2_Player xaudio2_Player in this.SoundDictionary.Values)
				{
					xaudio2_Player.Dispose();
				}
			}
			if (this.BGM_Player != null)
			{
				this.BGM_Player.Dispose();
			}
			if (this.ScreenTexMan != null)
			{
				this.ScreenTexMan.Dispose();
			}
			if (this.EffectDictionary != null)
			{
				foreach (Effect effect in this.EffectDictionary.Values)
				{
					effect.Dispose();
				}
			}
			if (this.ModelDictionary != null)
			{
				foreach (IModel model in this.ModelDictionary.Values)
				{
					Model model2 = (Model)model;
					model2.Dispose();
				}
			}
			if (this.DeviceXaudio2 != null)
			{
				this.DeviceXaudio2.Dispose();
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
