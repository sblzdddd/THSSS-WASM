using System.Collections.Generic;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	public class BaseEnemyPlane_Touhou : BaseEnemyPlane
	{
		private TextureObject[,] TxtureObjects { get; set; }

		private float IndexX { get; set; }

		private int IndexY { get; set; }

		public byte ColorType { get; set; }

		public int ItemCount
		{
			set
			{
				this.RedCount = value;
				this.BlueCount = value;
			}
		}

		public int RedCount { get; set; }

		public int BlueCount { get; set; }

		public int GreenCount { get; set; }

		public int ClearRadius { get; set; }

		public bool StarFall { get; set; }

		public BaseEnemyPlane_Touhou()
		{
		}

		public BaseEnemyPlane_Touhou(StageDataPackage StageData, string EnemyName, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: this(StageData, EnemyName, OriginalPosition, Velocity, Direction)
		{
			this.ColorType = ColorType;
		}

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

		public override void Ctrl()
		{
			base.Ctrl();
			this.TextureCtrl();
		}

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

		public override void Shoot()
		{
		}

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
