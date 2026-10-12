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
				return GlobalData.DeviceMain;
			}
		}

		public Dictionary<string, IModel> ModelDictionary
		{
			get
			{
				return GlobalData.ModelDictionary;
			}
		}

		public Dictionary<string, TextureObject> TextureObjectDictionary
		{
			get
			{
				return GlobalData.TextureObjectDictionary;
			}
		}

		public Dictionary<string, XAudio2_Player> SoundDictionary
		{
			get
			{
				return GlobalData.SoundDictionary;
			}
		}

		public Wave_Player BGM_Player
		{
			get
			{
				return GlobalData.BGM_Player;
			}
			set
			{
				GlobalData.BGM_Player = value;
			}
		}

		public KeyClass KClass
		{
			get
			{
				return GlobalData.KClass;
			}
		}

		public MySprite SpriteMain
		{
			get
			{
				return GlobalData.SpriteMain;
			}
		}

		public Replay Rep
		{
			get
			{
				return GlobalData.Rep;
			}
		}

		public ScreenTexManager ScreenTexMan
		{
			get
			{
				return GlobalData.ScreenTexMan;
			}
		}

		public Effect EffectMain
		{
			get
			{
				return GlobalData.EffectMain;
			}
			set
			{
				GlobalData.EffectMain = value;
			}
		}

		public Dictionary<string, Effect> EffectDictionary
		{
			get
			{
				return GlobalData.EffectDictionary;
			}
		}

		public PlayerData PData
		{
			get
			{
				return GlobalData.PData;
			}
			set
			{
				GlobalData.PData = value;
			}
		}

		public string SlowRate
		{
			get
			{
				var num = 60f * (float)RealTimeTotal / (float)Stopwatch.Frequency;
				num = (num - (float)TimeTotal) * 100f / num;
				return ((num < 0f) ? 0f : num).ToString("F1") + "%";
			}
		}

		public StageDataPackage(GlobalDataPackage GlobalData)
		{
			this.GlobalData = GlobalData;
			Init();
		}

		public void Init()
		{
			BoundRect = new Rectangle(0, 0, 640, 480);
			BulletList = new List<BaseBullet_Touhou>();
			EnemyPlaneList = new List<BaseEnemyPlane>();
			MyBulletList = new List<BaseObject>();
			SpellList = new List<BaseObject>();
			EffectList = new List<BaseEffect>();
			ItemList = new List<BaseItem>();
			InterfaceList = new List<BaseObject>();
			SoundPlayList = new List<XAudio2_Player>();
			Background = new BackgroundManager();
			Background2 = new BackgroundManager();
			Background3D = new BackgroundManager3D(this);
			Particle3D = new ParticleManager3D(this);
			MenuGroupList = new List<BaseMenuGroup>();
			TimeMain = 0;
			RepInfo = new ReplayInfo();
		}

		public void Vibrate()
		{
			if (VibrateTime > TimeMain)
			{
				int num;
				if (VibrateTime - TimeMain < 20)
				{
					num = (VibrateTime - TimeMain) / 2;
				}
				else
				{
					num = 10;
				}
				var num2 = Ran.Next(num) - num / 2;
				var num3 = Ran.Next(num) - num / 2;
				BoundRect = new Rectangle(VibrateRectSave.X + num2, VibrateRectSave.Y + num3, VibrateRectSave.Width, VibrateRectSave.Height);
			}
			else if (VibrateTime == TimeMain)
			{
				BoundRect = VibrateRectSave;
			}
		}

		public void VibrateStart(int CuntinueTime)
		{
			if (VibrateTime < TimeMain)
			{
				VibrateRectSave = BoundRect;
			}
			VibrateTime = TimeMain + CuntinueTime;
		}

		public void VibrateStop()
		{
			if (VibrateTime >= TimeMain)
			{
				VibrateTime = -1;
				BoundRect = VibrateRectSave;
			}
		}

		public void RemoveBullets()
		{
			foreach (var baseBullet_Touhou in BulletList)
			{
				baseBullet_Touhou.GiveEndEffect();
			}
			BulletList.Clear();
			foreach (var baseEnemyPlane in EnemyPlaneList)
			{
				baseEnemyPlane.GiveEndEffect();
				if (baseEnemyPlane.TxtureObject != null)
				{
					SoundPlay("se_enep00.wav");
				}
			}
			EnemyPlaneList.Clear();
			foreach (var baseObject in MyBulletList)
			{
				var particleSmaller = new ParticleSmaller(this, "光点", baseObject.Position, baseObject.Velocity, baseObject.Direction);
				particleSmaller.LifeTime = 30;
				particleSmaller.Scale = 0.5f;
			}
			MyBulletList.Clear();
		}

		public void SoundPlay(string MusicName)
		{
			SoundPlay(MusicName, 0.5f);
		}

		public void SoundPlay(string MusicName, float VoicePos)
		{
			if (MusicName != null)
			{
				if (SoundDictionary.ContainsKey(MusicName))
				{
					if (!SoundPlayList.Contains(SoundDictionary[MusicName]))
					{
						SoundDictionary[MusicName].VoicePos = VoicePos;
						SoundPlayList.Add(SoundDictionary[MusicName]);
					}
					else
					{
						SoundDictionary[MusicName].VoicePos = (VoicePos + SoundDictionary[MusicName].VoicePos) / 2f;
					}
				}
			}
		}

		public void ChangeBGM(string MusicName, int PlayBegin, int PlayLength, int LoopCount, int LoopBegin, int LoopEnd)
		{
			var bgmvolume = GlobalData.BGMVolume;
			BGM_Player.FileName = MusicName;
			BGM_Player.Volume = bgmvolume;
			BGM_Player.PlayRepeat(PlayBegin, PlayLength, LoopCount, LoopBegin, LoopEnd);
		}

		public void ChangeBGM(string MusicName, bool Repeat)
		{
			if (Repeat)
			{
				ChangeBGM(MusicName, 0, 0, 255, 0, 0);
			}
			else
			{
				ChangeBGM(MusicName, 0, 0, 0, 0, 0);
			}
		}

		public void SetReplayInfo(string StartStage)
		{
			RepInfo.MyPlaneName = MyPlane.Name;
			RepInfo.Rank = Difficulty;
			RepInfo.WeaponType = MyPlane.WeaponType;
			RepInfo.StartStage = StartStage;
			SetReplayStageInfo();
		}

		public void SetReplayStageInfo()
		{
			if (!OnReplay)
			{
				if (RepInfo.MyPlaneData.Count > 0)
				{
					if (RepInfo.MyPlaneData[RepInfo.MyPlaneData.Count - 1].DataPosition + 2L == Rep.DataPosition)
					{
						return;
					}
				}
				RepInfo.LastStage = CurrentStageName;
				RepInfo.SlowRate = SlowRate;
				RepInfo.MyPlaneData.Add(new MyPlaneInfo
				{
					Score = MyPlane.Score,
					Life = MyPlane.Life,
					Spell = MyPlane.Spell,
					Power = MyPlane.Power,
					Graze = MyPlane.Graze,
					PosX = MyPlane.OriginalPosition.X,
					PosY = MyPlane.OriginalPosition.Y,
					LifeChip = MyPlane.LifeChip,
					SpellChip = MyPlane.SpellChip,
					LifeUpCount = MyPlane.LifeUpCount,
					StarPoint = MyPlane.StarPoint,
					HighItemScore = MyPlane.HighItemScore,
					Rate = MyPlane.Rate,
					LastColor = MyPlane.LastColor,
					DataPosition = Rep.DataPosition
				});
				Rep.WriteKey(57358);
			}
		}

		public bool FindScore(ScoreHistory SH)
		{
			return SH.Rank == Difficulty && SH.MyPlaneFullName == MyPlane.FullName;
		}

		public bool FindClear(ClearHistory CH)
		{
			return CH.Rank == Difficulty && CH.MyPlaneFullName == MyPlane.FullName;
		}

		public void PreLoadCS(string Path)
		{
			var files = Directory.GetFiles(Path, "*.mbg");
			foreach (var text in files)
			{
				var fileInfo = new FileInfo(text);
				var cs_Data = new CS_Data(text);
				cs_Data.String2Data(this);
				CS_Cathe.Add(text, cs_Data);
			}
			var directories = Directory.GetDirectories(Path);
			foreach (var text in directories)
			{
				PreLoadCS(text);
			}
		}

		public void ClearCSCathe()
		{
			CS_Cathe.Clear();
		}

		public CS_Data LoadCS(string FileName)
		{
			CS_Data cs_Data;
			if (CS_Cathe.ContainsKey(FileName))
			{
				cs_Data = CS_Cathe[FileName];
			}
			else
			{
				var cs_Data2 = new CS_Data(FileName);
				cs_Data2.String2Data(this);
				cs_Data = cs_Data2;
			}
			return cs_Data;
		}

		public Texture DrawString(string text, System.Drawing.Font font, Brush brush, int width, int height)
		{
#if BROWSER
			return TextTextures.Get(text, font, brush, width, height);
#else
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
#endif
		}

		public void Dispose()
		{
			if (Background3D != null)
			{
				Background3D.Dispose();
			}
			if (Particle3D != null)
			{
				Particle3D.Dispose();
			}
			if (Rep != null)
			{
				Rep.Dispose();
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
