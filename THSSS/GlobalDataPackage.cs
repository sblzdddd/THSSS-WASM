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
	// Token: 0x02000131 RID: 305
	public class GlobalDataPackage
	{
		// Token: 0x06000819 RID: 2073 RVA: 0x00049DF0 File Offset: 0x00047FF0
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

		// Token: 0x0600081A RID: 2074 RVA: 0x00049E9C File Offset: 0x0004809C
		public void LoadResources1()
		{
			this.ScreenTexMan = new ScreenTexManager(this.DeviceMain);
			while (!this.LoadEffect())
			{
			}
			this.LoadingList = new List<string>();
			this.LoadFolderTextureList(".\\Image");
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00049EE4 File Offset: 0x000480E4
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

		// Token: 0x0600081C RID: 2076 RVA: 0x00049F30 File Offset: 0x00048130
		public void LoadFolderTextureList(string Path)
		{
			if (!(Path == ".\\Image\\NowLoading"))
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

		// Token: 0x0600081D RID: 2077 RVA: 0x00049FD4 File Offset: 0x000481D4
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

		// Token: 0x0600081E RID: 2078 RVA: 0x0004A070 File Offset: 0x00048270
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

		// Token: 0x0600081F RID: 2079 RVA: 0x0004A2B8 File Offset: 0x000484B8
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

		// Token: 0x06000820 RID: 2080 RVA: 0x0004A3C4 File Offset: 0x000485C4
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

		// Token: 0x06000821 RID: 2081 RVA: 0x0004A4E0 File Offset: 0x000486E0
		public void LoadSound()
		{
			if (this.SoundInitSuccess)
			{
				this.SoundDictionary = new Dictionary<string, XAudio2_Player>();
				for (int i = 0; i < Directory.GetFiles(".\\Sound", "*.wav").Length; i++)
				{
					FileInfo fileInfo = new FileInfo(Directory.GetFiles(".\\Sound", "*.wav")[i]);
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

		// Token: 0x06000822 RID: 2082 RVA: 0x0004A5B1 File Offset: 0x000487B1
		public void TextureObjectLoader(string imageFileName)
		{
			this.TextureObjectLoader(imageFileName, 0);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0004A5C0 File Offset: 0x000487C0
		public void TextureObjectLoader(string imageFileName, int colorKey)
		{
			if (File.Exists(imageFileName))
			{
				Bitmap bitmap = (Bitmap)Image.FromFile(imageFileName);
				Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
				bitmap.Dispose();
				int num = 0;
				Texture texture;
				while (!this.SetTexture(imageFileName, colorKey, rectangle, out texture) && num++ < 20)
				{
				}
				if (num >= 20)
				{
					MessageBox.Show(imageFileName + "纹理读取错误", "LoadTexture Error");
				}
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
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0004A7E4 File Offset: 0x000489E4
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

		// Token: 0x06000825 RID: 2085 RVA: 0x0004A838 File Offset: 0x00048A38
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

		// Token: 0x06000826 RID: 2086 RVA: 0x0004A890 File Offset: 0x00048A90
		public void BGM_OFF()
		{
			this.BGM_Player.Stop();
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0004A89F File Offset: 0x00048A9F
		public void BGM_Pause()
		{
			this.BGM_Player.Puase();
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0004A8B0 File Offset: 0x00048AB0
		public void BGM_Resume()
		{
			if (this.BGM_Player != null)
			{
				this.BGM_Player.Resume();
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0004A8DC File Offset: 0x00048ADC
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

		// Token: 0x0600082A RID: 2090 RVA: 0x0004A988 File Offset: 0x00048B88
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

		// Token: 0x0600082B RID: 2091 RVA: 0x0004AA40 File Offset: 0x00048C40
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

		// Token: 0x040002D2 RID: 722
		public const int WindowWidth = 640;

		// Token: 0x040002D3 RID: 723
		public const int WindowHeight = 480;

		// Token: 0x040002D4 RID: 724
		public XAudio2 DeviceXaudio2;

		// Token: 0x040002D5 RID: 725
		public MasteringVoice MasteringVoice;

		// Token: 0x040002D6 RID: 726
		public Device DeviceMain;

		// Token: 0x040002D7 RID: 727
		public PresentParameters presentParams;

		// Token: 0x040002D8 RID: 728
		public MySprite SpriteMain;

		// Token: 0x040002D9 RID: 729
		public Effect EffectMain;

		// Token: 0x040002DA RID: 730
		public Dictionary<string, Effect> EffectDictionary;

		// Token: 0x040002DB RID: 731
		public Dictionary<string, TextureObject> TextureObjectDictionary;

		// Token: 0x040002DC RID: 732
		public Dictionary<string, XAudio2_Player> SoundDictionary;

		// Token: 0x040002DD RID: 733
		public Dictionary<string, IModel> ModelDictionary;

		// Token: 0x040002DE RID: 734
		public Dictionary<string, bool[,]> BulletPicDictionary;

		// Token: 0x040002DF RID: 735
		public Wave_Player BGM_Player;

		// Token: 0x040002E0 RID: 736
		public List<string> LoadingList;

		// Token: 0x040002E1 RID: 737
		public KeyClass KClass = new KeyClass();

		// Token: 0x040002E2 RID: 738
		public bool Clear = false;

		// Token: 0x040002E3 RID: 739
		public Replay Rep;

		// Token: 0x040002E4 RID: 740
		public PlayerData PData;

		// Token: 0x040002E5 RID: 741
		public ScreenTexManager ScreenTexMan;

		// Token: 0x040002E6 RID: 742
		public IGameState LastState;

		// Token: 0x040002E7 RID: 743
		public int BGMVolume = 100;

		// Token: 0x040002E8 RID: 744
		public int SEVolume = 80;

		// Token: 0x040002E9 RID: 745
		public string FontType = "宋体";

		// Token: 0x040002EA RID: 746
		private bool SoundInitSuccess;
	}
}
