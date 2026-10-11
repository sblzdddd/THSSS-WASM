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
	// Token: 0x0200018C RID: 396
	public class StageDataPackage
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0005AA14 File Offset: 0x00058C14
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x0005AA2B File Offset: 0x00058C2B
		public ClearHistory C_History { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0005AA34 File Offset: 0x00058C34
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x0005AA4B File Offset: 0x00058C4B
		public List<ScoreHistory> S_History { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0005AA54 File Offset: 0x00058C54
		// (set) Token: 0x06000962 RID: 2402 RVA: 0x0005AA6B File Offset: 0x00058C6B
		public GlobalDataPackage GlobalData { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x0005AA74 File Offset: 0x00058C74
		public Device DeviceMain
		{
			get
			{
				return this.GlobalData.DeviceMain;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0005AA94 File Offset: 0x00058C94
		public Dictionary<string, IModel> ModelDictionary
		{
			get
			{
				return this.GlobalData.ModelDictionary;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0005AAB4 File Offset: 0x00058CB4
		public Dictionary<string, TextureObject> TextureObjectDictionary
		{
			get
			{
				return this.GlobalData.TextureObjectDictionary;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0005AAD4 File Offset: 0x00058CD4
		public Dictionary<string, XAudio2_Player> SoundDictionary
		{
			get
			{
				return this.GlobalData.SoundDictionary;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0005AAF4 File Offset: 0x00058CF4
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x0005AB11 File Offset: 0x00058D11
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

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0005AB20 File Offset: 0x00058D20
		public KeyClass KClass
		{
			get
			{
				return this.GlobalData.KClass;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0005AB40 File Offset: 0x00058D40
		public MySprite SpriteMain
		{
			get
			{
				return this.GlobalData.SpriteMain;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0005AB60 File Offset: 0x00058D60
		public Replay Rep
		{
			get
			{
				return this.GlobalData.Rep;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0005AB80 File Offset: 0x00058D80
		public ScreenTexManager ScreenTexMan
		{
			get
			{
				return this.GlobalData.ScreenTexMan;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0005ABA0 File Offset: 0x00058DA0
		// (set) Token: 0x0600096E RID: 2414 RVA: 0x0005ABBD File Offset: 0x00058DBD
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

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x0005ABCC File Offset: 0x00058DCC
		public Dictionary<string, Effect> EffectDictionary
		{
			get
			{
				return this.GlobalData.EffectDictionary;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x0005ABEC File Offset: 0x00058DEC
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x0005AC09 File Offset: 0x00058E09
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

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x0005AC18 File Offset: 0x00058E18
		public string SlowRate
		{
			get
			{
				float num = 60f * (float)this.RealTimeTotal / (float)Stopwatch.Frequency;
				num = (num - (float)this.TimeTotal) * 100f / num;
				return ((num < 0f) ? 0f : num).ToString("F1") + "%";
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0005AC78 File Offset: 0x00058E78
		public StageDataPackage(GlobalDataPackage GlobalData)
		{
			this.GlobalData = GlobalData;
			this.Init();
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0005ACEC File Offset: 0x00058EEC
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

		// Token: 0x06000975 RID: 2421 RVA: 0x0005ADB4 File Offset: 0x00058FB4
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

		// Token: 0x06000976 RID: 2422 RVA: 0x0005AE94 File Offset: 0x00059094
		public void VibrateStart(int CuntinueTime)
		{
			if (this.VibrateTime < this.TimeMain)
			{
				this.VibrateRectSave = this.BoundRect;
			}
			this.VibrateTime = this.TimeMain + CuntinueTime;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0005AED4 File Offset: 0x000590D4
		public void VibrateStop()
		{
			if (this.VibrateTime >= this.TimeMain)
			{
				this.VibrateTime = -1;
				this.BoundRect = this.VibrateRectSave;
			}
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0005AF10 File Offset: 0x00059110
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

		// Token: 0x06000979 RID: 2425 RVA: 0x0005B078 File Offset: 0x00059278
		public void SoundPlay(string MusicName)
		{
			this.SoundPlay(MusicName, 0.5f);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0005B088 File Offset: 0x00059288
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

		// Token: 0x0600097B RID: 2427 RVA: 0x0005B130 File Offset: 0x00059330
		public void ChangeBGM(string MusicName, int PlayBegin, int PlayLength, int LoopCount, int LoopBegin, int LoopEnd)
		{
			int bgmvolume = this.GlobalData.BGMVolume;
			this.BGM_Player.FileName = MusicName;
			this.BGM_Player.Volume = bgmvolume;
			this.BGM_Player.PlayRepeat(PlayBegin, PlayLength, LoopCount, LoopBegin, LoopEnd);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0005B178 File Offset: 0x00059378
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

		// Token: 0x0600097D RID: 2429 RVA: 0x0005B1B4 File Offset: 0x000593B4
		public void SetReplayInfo(string StartStage)
		{
			this.RepInfo.MyPlaneName = this.MyPlane.Name;
			this.RepInfo.Rank = this.Difficulty;
			this.RepInfo.WeaponType = this.MyPlane.WeaponType;
			this.RepInfo.StartStage = StartStage;
			this.SetReplayStageInfo();
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0005B214 File Offset: 0x00059414
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

		// Token: 0x0600097F RID: 2431 RVA: 0x0005B3F0 File Offset: 0x000595F0
		public bool FindScore(ScoreHistory SH)
		{
			return SH.Rank == this.Difficulty && SH.MyPlaneFullName == this.MyPlane.FullName;
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0005B42C File Offset: 0x0005962C
		public bool FindClear(ClearHistory CH)
		{
			return CH.Rank == this.Difficulty && CH.MyPlaneFullName == this.MyPlane.FullName;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0005B468 File Offset: 0x00059668
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

		// Token: 0x06000982 RID: 2434 RVA: 0x0005B501 File Offset: 0x00059701
		public void ClearCSCathe()
		{
			this.CS_Cathe.Clear();
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0005B510 File Offset: 0x00059710
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

		// Token: 0x06000984 RID: 2436 RVA: 0x0005B558 File Offset: 0x00059758
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

		// Token: 0x06000985 RID: 2437 RVA: 0x0005B638 File Offset: 0x00059838
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

		// Token: 0x0400037D RID: 893
		public StateSwitchDataPackage StateSwitchData;

		// Token: 0x0400037E RID: 894
		public ReplayInfo RepInfo;

		// Token: 0x0400037F RID: 895
		public long TimeTotal;

		// Token: 0x04000380 RID: 896
		public long RealTimeTotal;

		// Token: 0x04000381 RID: 897
		public int TimeMain;

		// Token: 0x04000382 RID: 898
		public Rectangle BoundRect;

		// Token: 0x04000383 RID: 899
		public BaseMyPlane MyPlane;

		// Token: 0x04000384 RID: 900
		public BaseBossTouhou Boss;

		// Token: 0x04000385 RID: 901
		public List<BaseBullet_Touhou> BulletList;

		// Token: 0x04000386 RID: 902
		public List<BaseObject> MyBulletList;

		// Token: 0x04000387 RID: 903
		public List<BaseEnemyPlane> EnemyPlaneList;

		// Token: 0x04000388 RID: 904
		public List<BaseEffect> EffectList;

		// Token: 0x04000389 RID: 905
		public List<BaseObject> SpellList;

		// Token: 0x0400038A RID: 906
		public List<BaseObject> InterfaceList;

		// Token: 0x0400038B RID: 907
		public List<BaseItem> ItemList;

		// Token: 0x0400038C RID: 908
		public BaseStory Story;

		// Token: 0x0400038D RID: 909
		public BackgroundManager Background;

		// Token: 0x0400038E RID: 910
		public BackgroundManager Background2;

		// Token: 0x0400038F RID: 911
		public BackgroundManager3D Background3D;

		// Token: 0x04000390 RID: 912
		public ParticleManager3D Particle3D;

		// Token: 0x04000391 RID: 913
		public List<XAudio2_Player> SoundPlayList;

		// Token: 0x04000392 RID: 914
		public List<BaseMenuGroup> MenuGroupList;

		// Token: 0x04000393 RID: 915
		public MyRandom Ran = new MyRandom();

		// Token: 0x04000394 RID: 916
		public bool C_Check = false;

		// Token: 0x04000395 RID: 917
		public bool OnReplay = false;

		// Token: 0x04000396 RID: 918
		public bool OnPractice = false;

		// Token: 0x04000397 RID: 919
		public DifficultLevel Difficulty;

		// Token: 0x04000398 RID: 920
		public int ContinueTimes = 0;

		// Token: 0x04000399 RID: 921
		private int VibrateTime = -1;

		// Token: 0x0400039A RID: 922
		private Rectangle VibrateRectSave;

		// Token: 0x0400039B RID: 923
		public int SlowMode = 0;

		// Token: 0x0400039C RID: 924
		public string CurrentStageName = null;

		// Token: 0x0400039D RID: 925
		public string SCBstring = null;

		// Token: 0x0400039E RID: 926
		public Dictionary<string, CS_Data> CS_Cathe = new Dictionary<string, CS_Data>();
	}
}
