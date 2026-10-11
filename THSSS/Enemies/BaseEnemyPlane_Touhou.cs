using System;
using System.Collections.Generic;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	// Token: 0x0200006F RID: 111
	public class BaseEnemyPlane_Touhou : BaseEnemyPlane
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0001AA2C File Offset: 0x00018C2C
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x0001AA43 File Offset: 0x00018C43
		private TextureObject[,] TxtureObjects { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0001AA4C File Offset: 0x00018C4C
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x0001AA63 File Offset: 0x00018C63
		private float IndexX { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0001AA6C File Offset: 0x00018C6C
		// (set) Token: 0x060003DB RID: 987 RVA: 0x0001AA83 File Offset: 0x00018C83
		private int IndexY { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0001AA8C File Offset: 0x00018C8C
		// (set) Token: 0x060003DD RID: 989 RVA: 0x0001AAA3 File Offset: 0x00018CA3
		public byte ColorType { get; set; }

		// Token: 0x17000112 RID: 274
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0001AAAC File Offset: 0x00018CAC
		public int ItemCount
		{
			set
			{
				this.RedCount = value;
				this.BlueCount = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0001AAC0 File Offset: 0x00018CC0
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0001AAD7 File Offset: 0x00018CD7
		public int RedCount { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0001AAE0 File Offset: 0x00018CE0
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0001AAF7 File Offset: 0x00018CF7
		public int BlueCount { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0001AB00 File Offset: 0x00018D00
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0001AB17 File Offset: 0x00018D17
		public int GreenCount { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0001AB20 File Offset: 0x00018D20
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0001AB37 File Offset: 0x00018D37
		public int ClearRadius { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0001AB40 File Offset: 0x00018D40
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0001AB57 File Offset: 0x00018D57
		public bool StarFall { get; set; }

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001AB60 File Offset: 0x00018D60
		public BaseEnemyPlane_Touhou()
		{
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001AB6B File Offset: 0x00018D6B
		public BaseEnemyPlane_Touhou(StageDataPackage StageData, string EnemyName, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: this(StageData, EnemyName, OriginalPosition, Velocity, Direction)
		{
			this.ColorType = ColorType;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001AB88 File Offset: 0x00018D88
		public BaseEnemyPlane_Touhou(StageDataPackage StageData, string EnemyName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, null, OriginalPosition, Velocity, Direction)
		{
			base.OriginalPosition = OriginalPosition;
			if (EnemyName != null)
			{
				TextureObject[,] array = new TextureObject[2, 8];
				array[0, 0] = base.TextureObjectDictionary[EnemyName + "_0"];
				array[0, 1] = base.TextureObjectDictionary[EnemyName + "_1"];
				array[0, 2] = base.TextureObjectDictionary[EnemyName + "_2"];
				array[0, 3] = base.TextureObjectDictionary[EnemyName + "_3"];
				array[0, 4] = base.TextureObjectDictionary[EnemyName + "_0"];
				array[0, 5] = base.TextureObjectDictionary[EnemyName + "_1"];
				array[0, 6] = base.TextureObjectDictionary[EnemyName + "_2"];
				array[0, 7] = base.TextureObjectDictionary[EnemyName + "_3"];
				array[1, 0] = base.TextureObjectDictionary[EnemyName + "_4"];
				array[1, 1] = base.TextureObjectDictionary[EnemyName + "_5"];
				array[1, 2] = base.TextureObjectDictionary[EnemyName + "_6"];
				array[1, 3] = base.TextureObjectDictionary[EnemyName + "_7"];
				array[1, 4] = base.TextureObjectDictionary[EnemyName + "_8"];
				array[1, 5] = base.TextureObjectDictionary[EnemyName + "_9"];
				array[1, 6] = base.TextureObjectDictionary[EnemyName + "_A"];
				array[1, 7] = base.TextureObjectDictionary[EnemyName + "_B"];
				this.TxtureObjects = array;
			}
			base.EventGroupList = new List<EventGroup>();
			base.EventsExecutionList = new List<Execution>();
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001ADC2 File Offset: 0x00018FC2
		public override void Ctrl()
		{
			base.Ctrl();
			this.TextureCtrl();
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001ADD4 File Offset: 0x00018FD4
		public virtual void TextureCtrl()
		{
			base.AngleWithDirection = false;
			base.Angle = 1.5707963267948966;
			int num = (base.Mirrored ? this.IndexY : (-this.IndexY));
			if (base.Vx < -0.5f)
			{
				base.Mirrored = true;
				this.IndexY = 1;
				if (this.IndexX < 3f)
				{
					this.IndexX += 0.16666667f;
				}
				else
				{
					this.IndexX = (float)(3 + base.Time % 24 / 6);
				}
			}
			else if (base.Vx > 0.5f)
			{
				base.Mirrored = false;
				this.IndexY = 1;
				if (this.IndexX < 3f)
				{
					this.IndexX += 0.16666667f;
				}
				else
				{
					this.IndexX = (float)(3 + base.Time % 24 / 6);
				}
			}
			else
			{
				base.Mirrored = false;
				this.IndexY = 0;
				this.IndexX = (float)(base.Time % 48 / 6);
			}
			if (num != (base.Mirrored ? this.IndexY : (-this.IndexY)))
			{
				this.IndexX = 0f;
			}
			if (this.IndexX > 7f)
			{
				this.IndexX = 7f;
			}
			base.TxtureObject = this.TxtureObjects[this.IndexY, (int)this.IndexX];
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001AF7B File Offset: 0x0001917B
		public override void Shoot()
		{
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001AF80 File Offset: 0x00019180
		public override void GiveEndEffect()
		{
			Color color;
			switch (this.ColorType)
			{
			case 0:
			{
				color = Color.Red;
				EndEnemy endEnemy = new EndEnemy(base.StageData, "EndEnemy0", base.Position, 0f, 0.0);
				endEnemy.Scale = 0.2f;
				break;
			}
			case 1:
			{
				color = Color.CornflowerBlue;
				EndEnemy endEnemy2 = new EndEnemy(base.StageData, "EndEnemy1", base.Position, 0f, 0.0);
				endEnemy2.Scale = 0.2f;
				break;
			}
			case 2:
			{
				color = Color.Green;
				EndEnemy endEnemy3 = new EndEnemy(base.StageData, "EndEnemy2", base.Position, 0f, 0.0);
				endEnemy3.Scale = 0.2f;
				break;
			}
			case 3:
			{
				color = Color.Yellow;
				EndEnemy endEnemy4 = new EndEnemy(base.StageData, "EndEnemy3", base.Position, 0f, 0.0);
				endEnemy4.Scale = 0.2f;
				break;
			}
			default:
			{
				color = Color.White;
				EndEnemy endEnemy5 = new EndEnemy(base.StageData, "EndEnemy2", base.Position, 0f, 0.0);
				endEnemy5.Scale = 0.2f;
				EndEnemy endEnemy6 = new EndEnemy(base.StageData, "EndEnemy3", base.Position, 0f, 0.0);
				endEnemy6.Scale = 0.2f;
				break;
			}
			}
			for (int i = 0; i < 10; i++)
			{
				Particle3D3 particle3D = new Particle3D3(base.StageData, "Effect-0", base.OriginalPosition, (float)base.Ran.Next(40) / 10f, (double)((float)base.Ran.Next(360) / 180f * 3.1415927f), 40);
				particle3D.Angle = (double)((float)base.StageData.Ran.Next(0, 360) * 3.1415927f / 180f);
				particle3D.AngularVelocity3D = (float)base.StageData.Ran.Next(10, 30) / 100f;
				particle3D.Scale = (float)base.StageData.Ran.Next(12, 18) / 10f;
				particle3D.RotatingAxis = new Vector3((float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100));
				particle3D.ColorValue = color;
				particle3D.MaxTransparent = 200;
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001B260 File Offset: 0x00019460
		public override void GiveItems()
		{
			for (int i = 0; i < this.RedCount; i++)
			{
				PointF pointF = new PointF(base.OriginalPosition.X + (float)base.Ran.Next(-this.RedCount * 5, this.RedCount * 5), base.OriginalPosition.Y + (float)base.Ran.Next(-this.RedCount * 5, this.RedCount * 5));
				new PowerItem_Touhou(base.StageData, pointF);
			}
			for (int i = 0; i < this.BlueCount; i++)
			{
				PointF pointF = new PointF(base.OriginalPosition.X + (float)base.Ran.Next(-this.BlueCount * 5, this.BlueCount * 5), base.OriginalPosition.Y + (float)base.Ran.Next(-this.BlueCount * 5, this.BlueCount * 5));
				new ScoreItem_Touhou(base.StageData, pointF);
			}
			for (int i = 0; i < this.GreenCount; i++)
			{
				PointF pointF = new PointF(base.OriginalPosition.X + (float)base.Ran.Next(-this.GreenCount * 5, this.GreenCount * 5), base.OriginalPosition.Y + (float)base.Ran.Next(-this.GreenCount * 5, this.GreenCount * 5));
				new BeliefItem_Touhou(base.StageData, pointF);
			}
			if (this.ClearRadius != 0)
			{
				BulletRemover_Small bulletRemover_Small = new BulletRemover_Small(base.StageData, base.OriginalPosition);
				bulletRemover_Small.Region = this.ClearRadius;
				bulletRemover_Small.LifeTime = 60;
			}
			if (this.StarFall)
			{
				BaseEffect baseEffect = new BaseEffect(base.StageData, "StarShardBS", base.Position, 8f, -1.5707963267948966);
				baseEffect.GhostingCount = 40;
				baseEffect.AngularVelocityDegree = 5f;
				new ShootingStarShard(base.StageData, new PointF(base.OriginalPosition.X, 0f));
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001B4D0 File Offset: 0x000196D0
		public override void EventCtrl()
		{
			base.EventGroupList.ForEach(delegate(EventGroup a)
			{
				a.Update(this);
			});
			base.EventsExecutionList.ForEach(delegate(Execution a)
			{
				a.Update(this);
			});
		}
	}
}
