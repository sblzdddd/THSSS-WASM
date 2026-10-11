using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class StageDataPackage
	{
		public ClearHistory C_History { get; set; }

		public List<ScoreHistory> S_History { get; set; }

		public GlobalDataPackage GlobalData { get; set; }

		public Device DeviceMain
		{
			get
			{
				return this.GlobalData.DeviceMain;
			}
		}

		public Dictionary<string, IModel> ModelDictionary
		{
			get
			{
				return this.GlobalData.ModelDictionary;
			}
		}

		public Dictionary<string, TextureObject> TextureObjectDictionary
		{
			get
			{
				return this.GlobalData.TextureObjectDictionary;
			}
		}

		public Dictionary<string, XAudio2_Player> SoundDictionary
		{
			get
			{
				return this.GlobalData.SoundDictionary;
			}
		}

		public Wave_Player BGM_Player
		{
			get
			{
				return this.GlobalData.BGM_Player;
			}
			set
			{
				this.GlobalData.BGM_Player = value;
			}
		}

		public KeyClass KClass
		{
			get
			{
				return this.GlobalData.KClass;
			}
		}

		public MySprite SpriteMain
		{
			get
			{
				return this.GlobalData.SpriteMain;
			}
		}

		public Replay Rep
		{
			get
			{
				return this.GlobalData.Rep;
			}
		}

		public ScreenTexManager ScreenTexMan
		{
			get
			{
				return this.GlobalData.ScreenTexMan;
			}
		}

		public Effect EffectMain
		{
			get
			{
				return this.GlobalData.EffectMain;
			}
			set
			{
				this.GlobalData.EffectMain = value;
			}
		}

		public Dictionary<string, Effect> EffectDictionary
		{
			get
			{
				return this.GlobalData.EffectDictionary;
			}
		}

		public PlayerData PData
		{
			get
			{
				return this.GlobalData.PData;
			}
			set
			{
				this.GlobalData.PData = value;
			}
		}

		public string SlowRate
		{
			get
			{
				float num = 60f * (float)this.RealTimeTotal / (float)Stopwatch.Frequency;
				num = (num - (float)this.TimeTotal) * 100f / num;
				return ((num < 0f) ? 0f : num).ToString("F1") + "%";
			}
		}

		public StageDataPackage(GlobalDataPackage GlobalData)
		{
			this.GlobalData = GlobalData;
			this.Init();
		}

		public void Init()
		{
			this.BoundRect = new Rectangle(0, 0, 640, 480);
			this.BulletList = new List<BaseBullet_Touhou>();
			this.EnemyPlaneList = new List<BaseEnemyPlane>();
			this.MyBulletList = new List<BaseObject>();
			this.SpellList = new List<BaseObject>();
			this.EffectList = new List<BaseEffect>();
			this.ItemList = new List<BaseItem>();
			this.InterfaceList = new List<BaseObject>();
			this.SoundPlayList = new List<XAudio2_Player>();
			this.Background = new BackgroundManager();
			this.Background2 = new BackgroundManager();
			this.Background3D = new BackgroundManager3D(this);
			this.Particle3D = new ParticleManager3D(this);
			this.MenuGroupList = new List<BaseMenuGroup>();
			this.TimeMain = 0;
			this.RepInfo = new ReplayInfo();
		}

		public void Vibrate()
		{
			if (this.VibrateTime > this.TimeMain)
			{
				int num;
				if (this.VibrateTime - this.TimeMain < 20)
				{
					num = (this.VibrateTime - this.TimeMain) / 2;
				}
				else
				{
					num = 10;
				}
				int num2 = this.Ran.Next(num) - num / 2;
				int num3 = this.Ran.Next(num) - num / 2;
				this.BoundRect = new Rectangle(this.VibrateRectSave.X + num2, this.VibrateRectSave.Y + num3, this.VibrateRectSave.Width, this.VibrateRectSave.Height);
			}
			else if (this.VibrateTime == this.TimeMain)
			{
				this.BoundRect = this.VibrateRectSave;
			}
		}

		public void VibrateStart(int CuntinueTime)
		{
			if (this.VibrateTime < this.TimeMain)
			{
				this.VibrateRectSave = this.BoundRect;
			}
			this.VibrateTime = this.TimeMain + CuntinueTime;
		}

		public void VibrateStop()
		{
			if (this.VibrateTime >= this.TimeMain)
			{
				this.VibrateTime = -1;
				this.BoundRect = this.VibrateRectSave;
			}
		}

		public void RemoveBullets()
		{
			foreach (BaseBullet_Touhou baseBullet_Touhou in this.BulletList)
			{
				baseBullet_Touhou.GiveEndEffect();
			}
			this.BulletList.Clear();
			foreach (BaseEnemyPlane baseEnemyPlane in this.EnemyPlaneList)
			{
				baseEnemyPlane.GiveEndEffect();
				if (baseEnemyPlane.TxtureObject != null)
				{
					this.SoundPlay("se_enep00.wav");
				}
			}
			this.EnemyPlaneList.Clear();
			foreach (BaseObject baseObject in this.MyBulletList)
			{
				ParticleSmaller particleSmaller = new ParticleSmaller(this, "光点", baseObject.Position, baseObject.Velocity, baseObject.Direction);
				particleSmaller.LifeTime = 30;
				particleSmaller.Scale = 0.5f;
			}
			this.MyBulletList.Clear();
		}

		public void SoundPlay(string MusicName)
		{
			this.SoundPlay(MusicName, 0.5f);
		}

		public void SoundPlay(string MusicName, float VoicePos)
		{
			if (MusicName != null)
			{
				if (this.SoundDictionary.ContainsKey(MusicName))
				{
					if (!this.SoundPlayList.Contains(this.SoundDictionary[MusicName]))
					{
						this.SoundDictionary[MusicName].VoicePos = VoicePos;
						this.SoundPlayList.Add(this.SoundDictionary[MusicName]);
					}
					else
					{
						this.SoundDictionary[MusicName].VoicePos = (VoicePos + this.SoundDictionary[MusicName].VoicePos) / 2f;
					}
				}
			}
		}

		public void ChangeBGM(string MusicName, int PlayBegin, int PlayLength, int LoopCount, int LoopBegin, int LoopEnd)
		{
			int bgmvolume = this.GlobalData.BGMVolume;
			this.BGM_Player.FileName = MusicName;
			this.BGM_Player.Volume = bgmvolume;
			this.BGM_Player.PlayRepeat(PlayBegin, PlayLength, LoopCount, LoopBegin, LoopEnd);
		}

		public void ChangeBGM(string MusicName, bool Repeat)
		{
			if (Repeat)
			{
				this.ChangeBGM(MusicName, 0, 0, 255, 0, 0);
			}
			else
			{
				this.ChangeBGM(MusicName, 0, 0, 0, 0, 0);
			}
		}

		public void SetReplayInfo(string StartStage)
		{
			this.RepInfo.MyPlaneName = this.MyPlane.Name;
			this.RepInfo.Rank = this.Difficulty;
			this.RepInfo.WeaponType = this.MyPlane.WeaponType;
			this.RepInfo.StartStage = StartStage;
			this.SetReplayStageInfo();
		}

		public void SetReplayStageInfo()
		{
			if (!this.OnReplay)
			{
				if (this.RepInfo.MyPlaneData.Count > 0)
				{
					if (this.RepInfo.MyPlaneData[this.RepInfo.MyPlaneData.Count - 1].DataPosition + 2L == this.Rep.DataPosition)
					{
						return;
					}
				}
				this.RepInfo.LastStage = this.CurrentStageName;
				this.RepInfo.SlowRate = this.SlowRate;
				this.RepInfo.MyPlaneData.Add(new MyPlaneInfo
				{
					Score = this.MyPlane.Score,
					Life = this.MyPlane.Life,
					Spell = this.MyPlane.Spell,
					Power = this.MyPlane.Power,
					Graze = this.MyPlane.Graze,
					PosX = this.MyPlane.OriginalPosition.X,
					PosY = this.MyPlane.OriginalPosition.Y,
					LifeChip = this.MyPlane.LifeChip,
					SpellChip = this.MyPlane.SpellChip,
					LifeUpCount = this.MyPlane.LifeUpCount,
					StarPoint = this.MyPlane.StarPoint,
					HighItemScore = this.MyPlane.HighItemScore,
					Rate = this.MyPlane.Rate,
					LastColor = this.MyPlane.LastColor,
					DataPosition = this.Rep.DataPosition
				});
				this.Rep.WriteKey(57358);
			}
		}

		public bool FindScore(ScoreHistory SH)
		{
			return SH.Rank == this.Difficulty && SH.MyPlaneFullName == this.MyPlane.FullName;
		}

		public bool FindClear(ClearHistory CH)
		{
			return CH.Rank == this.Difficulty && CH.MyPlaneFullName == this.MyPlane.FullName;
		}

		public void PreLoadCS(string Path)
		{
			string[] files = Directory.GetFiles(Path, "*.mbg");
			foreach (string text in files)
			{
				FileInfo fileInfo = new FileInfo(text);
				CS_Data cs_Data = new CS_Data(text);
				cs_Data.String2Data(this);
				this.CS_Cathe.Add(text, cs_Data);
			}
			string[] directories = Directory.GetDirectories(Path);
			foreach (string text in directories)
			{
				this.PreLoadCS(text);
			}
		}

		public void ClearCSCathe()
		{
			this.CS_Cathe.Clear();
		}

		public CS_Data LoadCS(string FileName)
		{
			CS_Data cs_Data;
			if (this.CS_Cathe.ContainsKey(FileName))
			{
				cs_Data = this.CS_Cathe[FileName];
			}
			else
			{
				CS_Data cs_Data2 = new CS_Data(FileName);
				cs_Data2.String2Data(this);
				cs_Data = cs_Data2;
			}
			return cs_Data;
		}

		public Texture DrawString(string text, global::System.Drawing.Font font, Brush brush, int width, int height)
		{
			Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
			graphics.DrawString(text, font, brush, 0f, 0f);
			graphics.Dispose();
			Texture texture = new Texture(this.DeviceMain, width, height, 1, Usage.None, Format.A8R8G8B8, Pool.Managed);
			SurfaceDescription levelDescription = texture.GetLevelDescription(0);
			DataRectangle dataRectangle = texture.LockRectangle(0, new Rectangle(0, 0, width, height), LockFlags.Discard);
			BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
			IntPtr scan = bitmapData.Scan0;
			dataRectangle.Data.WriteRange(scan, (long)(bitmapData.Width * bitmapData.Height * 4));
			bitmap.UnlockBits(bitmapData);
			texture.UnlockRectangle(0);
			bitmap.Dispose();
			return texture;
		}

		public void Dispose()
		{
			if (this.Background3D != null)
			{
				this.Background3D.Dispose();
			}
			if (this.Particle3D != null)
			{
				this.Particle3D.Dispose();
			}
			if (this.Rep != null)
			{
				this.Rep.Dispose();
			}
		}

		public StateSwitchDataPackage StateSwitchData;

		public ReplayInfo RepInfo;

		public long TimeTotal;

		public long RealTimeTotal;

		public int TimeMain;

		public Rectangle BoundRect;

		public BaseMyPlane MyPlane;

		public BaseBossTouhou Boss;

		public List<BaseBullet_Touhou> BulletList;

		public List<BaseObject> MyBulletList;

		public List<BaseEnemyPlane> EnemyPlaneList;

		public List<BaseEffect> EffectList;

		public List<BaseObject> SpellList;

		public List<BaseObject> InterfaceList;

		public List<BaseItem> ItemList;

		public BaseStory Story;

		public BackgroundManager Background;

		public BackgroundManager Background2;

		public BackgroundManager3D Background3D;

		public ParticleManager3D Particle3D;

		public List<XAudio2_Player> SoundPlayList;

		public List<BaseMenuGroup> MenuGroupList;

		public MyRandom Ran = new MyRandom();

		public bool C_Check = false;

		public bool OnReplay = false;

		public bool OnPractice = false;

		public DifficultLevel Difficulty;

		public int ContinueTimes = 0;

		private int VibrateTime = -1;

		private Rectangle VibrateRectSave;

		public int SlowMode = 0;

		public string CurrentStageName = null;

		public string SCBstring = null;

		public Dictionary<string, CS_Data> CS_Cathe = new Dictionary<string, CS_Data>();
	}
}
