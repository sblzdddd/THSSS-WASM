using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SlimDX.Direct3D9;

namespace Shooting
{
	internal class BaseGameState : UserControl, IGameState
	{
		public GlobalDataPackage GlobalData { get; set; }

		public StageDataPackage StageData { get; set; }

		public string StageName { get; set; }

		public StateSwitchDataPackage StateSwitchData
		{
			get
			{
				return this.StageData.StateSwitchData;
			}
			set
			{
				this.StageData.StateSwitchData = value;
			}
		}

		public MyRandom Ran
		{
			get
			{
				return this.StageData.Ran;
			}
			set
			{
				this.StageData.Ran = value;
			}
		}

		public MySprite SpriteMain
		{
			get
			{
				return this.StageData.SpriteMain;
			}
		}

		public Rectangle BoundRect
		{
			get
			{
				return this.StageData.BoundRect;
			}
			set
			{
				this.StageData.BoundRect = value;
			}
		}

		public BaseMyPlane MyPlane
		{
			get
			{
				return this.StageData.MyPlane;
			}
			set
			{
				this.StageData.MyPlane = value;
			}
		}

		public List<BaseBullet_Touhou> BulletList
		{
			get
			{
				return this.StageData.BulletList;
			}
			set
			{
				this.StageData.BulletList = value;
			}
		}

		public List<BaseObject> MyBulletList
		{
			get
			{
				return this.StageData.MyBulletList;
			}
			set
			{
				this.StageData.MyBulletList = value;
			}
		}

		public List<BaseObject> SpellList
		{
			get
			{
				return this.StageData.SpellList;
			}
			set
			{
				this.StageData.SpellList = value;
			}
		}

		public List<BaseEnemyPlane> EnemyPlaneList
		{
			get
			{
				return this.StageData.EnemyPlaneList;
			}
			set
			{
				this.StageData.EnemyPlaneList = value;
			}
		}

		public List<BaseEffect> EffectList
		{
			get
			{
				return this.StageData.EffectList;
			}
			set
			{
				this.StageData.EffectList = value;
			}
		}

		public List<BaseItem> ItemList
		{
			get
			{
				return this.StageData.ItemList;
			}
			set
			{
				this.StageData.ItemList = value;
			}
		}

		public List<BaseObject> InterfaceList
		{
			get
			{
				return this.StageData.InterfaceList;
			}
			set
			{
				this.StageData.InterfaceList = value;
			}
		}

		public BaseStory Story
		{
			get
			{
				return this.StageData.Story;
			}
			set
			{
				this.StageData.Story = value;
			}
		}

		public BackgroundManager Background
		{
			get
			{
				return this.StageData.Background;
			}
			set
			{
				this.StageData.Background = value;
			}
		}

		public BackgroundManager Background2
		{
			get
			{
				return this.StageData.Background2;
			}
			set
			{
				this.StageData.Background2 = value;
			}
		}

		public BackgroundManager3D Background3D
		{
			get
			{
				return this.StageData.Background3D;
			}
			set
			{
				this.StageData.Background3D = value;
			}
		}

		public ParticleManager3D Particle3D
		{
			get
			{
				return this.StageData.Particle3D;
			}
			set
			{
				this.StageData.Particle3D = value;
			}
		}

		public BaseBossTouhou Boss
		{
			get
			{
				return this.StageData.Boss;
			}
			set
			{
				this.StageData.Boss = value;
			}
		}

		public List<XAudio2_Player> SoundPlayList
		{
			get
			{
				return this.StageData.SoundPlayList;
			}
			set
			{
				this.StageData.SoundPlayList = value;
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
		}

		public KeyClass KClass
		{
			get
			{
				return this.GlobalData.KClass;
			}
		}

		public Device DeviceMain
		{
			get
			{
				return this.GlobalData.DeviceMain;
			}
		}

		public double FPS { get; set; }

		public int TimeMain
		{
			get
			{
				return this.StageData.TimeMain;
			}
			set
			{
				this.StageData.TimeMain = value;
			}
		}

		private long TimeCount { get; set; }

		public SlimDX.Direct3D9.Font DXFont { get; set; }

		public BaseGameState()
		{
		}

		public BaseGameState(GlobalDataPackage GlobalData)
		{
			this.GlobalData = GlobalData;
		}

		public virtual void Init()
		{
		}

		public virtual void UpdateData()
		{
			if (this.TimeMain % 30 == 0)
			{
				long timestamp = Stopwatch.GetTimestamp();
				this.FPS = (double)(30f * (float)Stopwatch.Frequency / (float)(timestamp - this.TimeCount));
				if (this.TimeMain != 0)
				{
					this.StageData.RealTimeTotal += timestamp - this.TimeCount;
					this.StageData.TimeTotal += 30L;
				}
				this.TimeCount = timestamp;
			}
			this.TimeMain++;
			this.SoundPlayList.Clear();
		}

		public void TimeSycn()
		{
			this.TimeCount = Stopwatch.GetTimestamp();
		}

		public virtual void Render()
		{
		}

		public void DrawText(string text, Point Pos)
		{
			int num = 0;
			char[] array = text.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				PointF pointF = new PointF((float)(Pos.X + 5 + num), (float)(Pos.Y + 9));
				this.SpriteMain.Draw2D(this.TextureObjectDictionary["ScoreNum_" + array[i].ToString()], 1f, 0f, pointF, Color.White);
				num += 16;
				if (array[i] == '.')
				{
					num -= 6;
				}
			}
		}

		public virtual void BGM_ON()
		{
		}

		public virtual void BGM_Resume()
		{
			this.GlobalData.BGM_Resume();
		}

		public virtual void BGM_Pause()
		{
			this.GlobalData.BGM_Pause();
		}

		public new void Dispose()
		{
			if (this.StageData != null)
			{
				this.StageData.Dispose();
			}
			if (this.DXFont != null)
			{
				this.DXFont.Dispose();
			}
		}
	}
}
