using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x0200005C RID: 92
	internal class BaseGameState : UserControl, IGameState
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x000130E4 File Offset: 0x000112E4
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x000130FB File Offset: 0x000112FB
		public GlobalDataPackage GlobalData { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00013104 File Offset: 0x00011304
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0001311B File Offset: 0x0001131B
		public StageDataPackage StageData { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00013124 File Offset: 0x00011324
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x0001313B File Offset: 0x0001133B
		public string StageName { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00013144 File Offset: 0x00011344
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00013161 File Offset: 0x00011361
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

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00013170 File Offset: 0x00011370
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0001318D File Offset: 0x0001138D
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

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0001319C File Offset: 0x0001139C
		public MySprite SpriteMain
		{
			get
			{
				return this.StageData.SpriteMain;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002CF RID: 719 RVA: 0x000131BC File Offset: 0x000113BC
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x000131D9 File Offset: 0x000113D9
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

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x000131E8 File Offset: 0x000113E8
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x00013205 File Offset: 0x00011405
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

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00013214 File Offset: 0x00011414
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x00013231 File Offset: 0x00011431
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00013240 File Offset: 0x00011440
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0001325D File Offset: 0x0001145D
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0001326C File Offset: 0x0001146C
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x00013289 File Offset: 0x00011489
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

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00013298 File Offset: 0x00011498
		// (set) Token: 0x060002DA RID: 730 RVA: 0x000132B5 File Offset: 0x000114B5
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

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002DB RID: 731 RVA: 0x000132C4 File Offset: 0x000114C4
		// (set) Token: 0x060002DC RID: 732 RVA: 0x000132E1 File Offset: 0x000114E1
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

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002DD RID: 733 RVA: 0x000132F0 File Offset: 0x000114F0
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0001330D File Offset: 0x0001150D
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

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0001331C File Offset: 0x0001151C
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x00013339 File Offset: 0x00011539
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

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00013348 File Offset: 0x00011548
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x00013365 File Offset: 0x00011565
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

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00013374 File Offset: 0x00011574
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x00013391 File Offset: 0x00011591
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

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x000133A0 File Offset: 0x000115A0
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x000133BD File Offset: 0x000115BD
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

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x000133CC File Offset: 0x000115CC
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x000133E9 File Offset: 0x000115E9
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

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x000133F8 File Offset: 0x000115F8
		// (set) Token: 0x060002EA RID: 746 RVA: 0x00013415 File Offset: 0x00011615
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

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00013424 File Offset: 0x00011624
		// (set) Token: 0x060002EC RID: 748 RVA: 0x00013441 File Offset: 0x00011641
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

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00013450 File Offset: 0x00011650
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0001346D File Offset: 0x0001166D
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

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0001347C File Offset: 0x0001167C
		public Dictionary<string, TextureObject> TextureObjectDictionary
		{
			get
			{
				return this.GlobalData.TextureObjectDictionary;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0001349C File Offset: 0x0001169C
		public Dictionary<string, XAudio2_Player> SoundDictionary
		{
			get
			{
				return this.GlobalData.SoundDictionary;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x000134BC File Offset: 0x000116BC
		public Wave_Player BGM_Player
		{
			get
			{
				return this.GlobalData.BGM_Player;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x000134DC File Offset: 0x000116DC
		public KeyClass KClass
		{
			get
			{
				return this.GlobalData.KClass;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x000134FC File Offset: 0x000116FC
		public Device DeviceMain
		{
			get
			{
				return this.GlobalData.DeviceMain;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0001351C File Offset: 0x0001171C
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x00013533 File Offset: 0x00011733
		public double FPS { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0001353C File Offset: 0x0001173C
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x00013559 File Offset: 0x00011759
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

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00013568 File Offset: 0x00011768
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x0001357F File Offset: 0x0001177F
		private long TimeCount { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00013588 File Offset: 0x00011788
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0001359F File Offset: 0x0001179F
		public SlimDX.Direct3D9.Font DXFont { get; set; }

		// Token: 0x060002FC RID: 764 RVA: 0x000135A8 File Offset: 0x000117A8
		public BaseGameState()
		{
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000135B3 File Offset: 0x000117B3
		public BaseGameState(GlobalDataPackage GlobalData)
		{
			this.GlobalData = GlobalData;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000135C6 File Offset: 0x000117C6
		public virtual void Init()
		{
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000135CC File Offset: 0x000117CC
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

		// Token: 0x06000300 RID: 768 RVA: 0x00013674 File Offset: 0x00011874
		public void TimeSycn()
		{
			this.TimeCount = Stopwatch.GetTimestamp();
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00013683 File Offset: 0x00011883
		public virtual void Render()
		{
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00013688 File Offset: 0x00011888
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

		// Token: 0x06000303 RID: 771 RVA: 0x0001372F File Offset: 0x0001192F
		public virtual void BGM_ON()
		{
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00013732 File Offset: 0x00011932
		public virtual void BGM_Resume()
		{
			this.GlobalData.BGM_Resume();
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00013741 File Offset: 0x00011941
		public virtual void BGM_Pause()
		{
			this.GlobalData.BGM_Pause();
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00013750 File Offset: 0x00011950
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
