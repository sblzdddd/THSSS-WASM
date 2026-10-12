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
				RedCount = value;
				BlueCount = value;
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
			this.OriginalPosition = OriginalPosition;
			if (EnemyName != null)
			{
				TextureObject[,] array = new TextureObject[2, 8];
				array[0, 0] = TextureObjectDictionary[EnemyName + "_0"];
				array[0, 1] = TextureObjectDictionary[EnemyName + "_1"];
				array[0, 2] = TextureObjectDictionary[EnemyName + "_2"];
				array[0, 3] = TextureObjectDictionary[EnemyName + "_3"];
				array[0, 4] = TextureObjectDictionary[EnemyName + "_0"];
				array[0, 5] = TextureObjectDictionary[EnemyName + "_1"];
				array[0, 6] = TextureObjectDictionary[EnemyName + "_2"];
				array[0, 7] = TextureObjectDictionary[EnemyName + "_3"];
				array[1, 0] = TextureObjectDictionary[EnemyName + "_4"];
				array[1, 1] = TextureObjectDictionary[EnemyName + "_5"];
				array[1, 2] = TextureObjectDictionary[EnemyName + "_6"];
				array[1, 3] = TextureObjectDictionary[EnemyName + "_7"];
				array[1, 4] = TextureObjectDictionary[EnemyName + "_8"];
				array[1, 5] = TextureObjectDictionary[EnemyName + "_9"];
				array[1, 6] = TextureObjectDictionary[EnemyName + "_A"];
				array[1, 7] = TextureObjectDictionary[EnemyName + "_B"];
				TxtureObjects = array;
			}
			EventGroupList = new List<EventGroup>();
			EventsExecutionList = new List<Execution>();
		}

		public override void Ctrl()
		{
			base.Ctrl();
			TextureCtrl();
		}

		public virtual void TextureCtrl()
		{
			AngleWithDirection = false;
			Angle = 1.5707963267948966;
			int num = (Mirrored ? IndexY : (-IndexY));
			if (Vx < -0.5f)
			{
				Mirrored = true;
				IndexY = 1;
				if (IndexX < 3f)
				{
					IndexX += 0.16666667f;
				}
				else
				{
					IndexX = (float)(3 + Time % 24 / 6);
				}
			}
			else if (Vx > 0.5f)
			{
				Mirrored = false;
				IndexY = 1;
				if (IndexX < 3f)
				{
					IndexX += 0.16666667f;
				}
				else
				{
					IndexX = (float)(3 + Time % 24 / 6);
				}
			}
			else
			{
				Mirrored = false;
				IndexY = 0;
				IndexX = (float)(Time % 48 / 6);
			}
			if (num != (Mirrored ? IndexY : (-IndexY)))
			{
				IndexX = 0f;
			}
			if (IndexX > 7f)
			{
				IndexX = 7f;
			}
			TxtureObject = TxtureObjects[IndexY, (int)IndexX];
		}

		public override void Shoot()
		{
		}

		public override void GiveEndEffect()
		{
			Color color;
			switch (ColorType)
			{
			case 0:
			{
				color = Color.Red;
				EndEnemy endEnemy = new EndEnemy(StageData, "EndEnemy0", Position, 0f, 0.0);
				endEnemy.Scale = 0.2f;
				break;
			}
			case 1:
			{
				color = Color.CornflowerBlue;
				EndEnemy endEnemy2 = new EndEnemy(StageData, "EndEnemy1", Position, 0f, 0.0);
				endEnemy2.Scale = 0.2f;
				break;
			}
			case 2:
			{
				color = Color.Green;
				EndEnemy endEnemy3 = new EndEnemy(StageData, "EndEnemy2", Position, 0f, 0.0);
				endEnemy3.Scale = 0.2f;
				break;
			}
			case 3:
			{
				color = Color.Yellow;
				EndEnemy endEnemy4 = new EndEnemy(StageData, "EndEnemy3", Position, 0f, 0.0);
				endEnemy4.Scale = 0.2f;
				break;
			}
			default:
			{
				color = Color.White;
				EndEnemy endEnemy5 = new EndEnemy(StageData, "EndEnemy2", Position, 0f, 0.0);
				endEnemy5.Scale = 0.2f;
				EndEnemy endEnemy6 = new EndEnemy(StageData, "EndEnemy3", Position, 0f, 0.0);
				endEnemy6.Scale = 0.2f;
				break;
			}
			}
			for (int i = 0; i < 10; i++)
			{
				Particle3D3 particle3D = new Particle3D3(StageData, "Effect-0", OriginalPosition, (float)Ran.Next(40) / 10f, (double)((float)Ran.Next(360) / 180f * 3.1415927f), 40);
				particle3D.Angle = (double)((float)StageData.Ran.Next(0, 360) * 3.1415927f / 180f);
				particle3D.AngularVelocity3D = (float)StageData.Ran.Next(10, 30) / 100f;
				particle3D.Scale = (float)StageData.Ran.Next(12, 18) / 10f;
				particle3D.RotatingAxis = new Vector3((float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100));
				particle3D.ColorValue = color;
				particle3D.MaxTransparent = 200;
			}
		}

		public override void GiveItems()
		{
			for (int i = 0; i < RedCount; i++)
			{
				PointF pointF = new PointF(OriginalPosition.X + (float)Ran.Next(-RedCount * 5, RedCount * 5), OriginalPosition.Y + (float)Ran.Next(-RedCount * 5, RedCount * 5));
				new PowerItem_Touhou(StageData, pointF);
			}
			for (int i = 0; i < BlueCount; i++)
			{
				PointF pointF = new PointF(OriginalPosition.X + (float)Ran.Next(-BlueCount * 5, BlueCount * 5), OriginalPosition.Y + (float)Ran.Next(-BlueCount * 5, BlueCount * 5));
				new ScoreItem_Touhou(StageData, pointF);
			}
			for (int i = 0; i < GreenCount; i++)
			{
				PointF pointF = new PointF(OriginalPosition.X + (float)Ran.Next(-GreenCount * 5, GreenCount * 5), OriginalPosition.Y + (float)Ran.Next(-GreenCount * 5, GreenCount * 5));
				new BeliefItem_Touhou(StageData, pointF);
			}
			if (ClearRadius != 0)
			{
				BulletRemover_Small bulletRemover_Small = new BulletRemover_Small(StageData, OriginalPosition);
				bulletRemover_Small.Region = ClearRadius;
				bulletRemover_Small.LifeTime = 60;
			}
			if (StarFall)
			{
				BaseEffect baseEffect = new BaseEffect(StageData, "StarShardBS", Position, 8f, -1.5707963267948966);
				baseEffect.GhostingCount = 40;
				baseEffect.AngularVelocityDegree = 5f;
				new ShootingStarShard(StageData, new PointF(OriginalPosition.X, 0f));
			}
		}

		public override void EventCtrl()
		{
			EventGroupList.ForEach(delegate(EventGroup a)
			{
				a.Update(this);
			});
			EventsExecutionList.ForEach(delegate(Execution a)
			{
				a.Update(this);
			});
		}
	}
}
