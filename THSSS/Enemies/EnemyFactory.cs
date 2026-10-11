using System;
using System.Drawing;

namespace Shooting
{
	public class EnemyFactory
	{
		public EnemyFactory(string EnemyName)
		{
			this.EnemyName = EnemyName;
			if (this.ContainName(this.EnemyType0, EnemyName))
			{
				this.EnemyType = 0;
			}
			else if (this.ContainName(this.EnemyType1, EnemyName))
			{
				this.EnemyType = 1;
			}
			else if (this.ContainName(this.EnemyType2, EnemyName))
			{
				this.EnemyType = 2;
			}
			else if (this.ContainName(this.EnemyType3, EnemyName))
			{
				this.EnemyType = 3;
			}
			else if (this.ContainName(this.EnemyType4, EnemyName))
			{
				this.EnemyType = 4;
			}
		}

		private bool ContainName(string[] EnemyType, string EnemyName)
		{
			foreach (string text in EnemyType)
			{
				if (text == EnemyName)
				{
					return true;
				}
			}
			return false;
		}

		public BaseEnemyPlane_Touhou GenerateEnemy(StageDataPackage StageData)
		{
			BaseEnemyPlane_Touhou baseEnemyPlane_Touhou;
			switch (this.EnemyType)
			{
			case 0:
			{
				string text = this.EnemyName;
				text = text.Replace("EnemyA", "");
				text = text.Replace("EnemyB", "");
				text = text.Replace("EnemyC", "");
				this.ColorType = byte.Parse(text);
				this.ColorType = (byte)((this.ColorType > 3) ? (this.ColorType - 4) : this.ColorType);
				baseEnemyPlane_Touhou = new EnemyPlane_TouhouSmall(StageData, this.EnemyName, new PointF(0f, 0f), 0f, 0.0, this.ColorType);
				if (this.BackFire)
				{
					((EnemyPlane_TouhouSmall)baseEnemyPlane_Touhou).SetBackFire();
				}
				break;
			}
			case 1:
				baseEnemyPlane_Touhou = new EnemyPlane_TouhouBig(StageData, this.EnemyName, new PointF(0f, 0f), 0f, 0.0, this.ColorType);
				break;
			case 2:
				this.ColorType = byte.Parse(this.EnemyName.Replace("EnemyGhost", ""));
				baseEnemyPlane_Touhou = new EnemyPlane_TouhouGhost(StageData, new PointF(0f, 0f), 0f, 0.0, this.ColorType);
				break;
			case 3:
				this.ColorType = byte.Parse(this.EnemyName.Replace("EnemyMaoYu0", ""));
				baseEnemyPlane_Touhou = new EnemyPlane_TouhouMaoYu(StageData, new PointF(0f, 0f), 0f, 0.0, this.ColorType);
				break;
			case 4:
				this.ColorType = (byte)(int.Parse(this.EnemyName.Replace("EnemyYYY", "")) - 4);
				baseEnemyPlane_Touhou = new EnemyPlane_TouhouYYY(StageData, new PointF(0f, 0f), 0f, 0.0, this.ColorType);
				break;
			default:
				baseEnemyPlane_Touhou = new EnemyPlane_TouhouNormal(StageData, this.EnemyName, new PointF(0f, 0f), 0f, 0.0, this.ColorType);
				break;
			}
			baseEnemyPlane_Touhou.HealthPoint = (float)this.HealthPoint;
			baseEnemyPlane_Touhou.RedCount = this.RedCount;
			baseEnemyPlane_Touhou.BlueCount = this.BlueCount;
			baseEnemyPlane_Touhou.GreenCount = this.GreenCount;
			baseEnemyPlane_Touhou.ClearRadius = this.ClearRadius;
			baseEnemyPlane_Touhou.StarFall = this.StarFall;
			StageData.EnemyPlaneList.Remove(baseEnemyPlane_Touhou);
			return baseEnemyPlane_Touhou;
		}

		private readonly string[] EnemyType0 = new string[] { "EnemyA0", "EnemyA1", "EnemyA2", "EnemyA3", "EnemyA4", "EnemyA5", "EnemyA6", "EnemyA7", "EnemyC0", "EnemyC1" };

		private readonly string[] EnemyType1 = new string[] { "EnemyA8", "EnemyB1", "EnemyC2", "EnemyC3" };

		private readonly string[] EnemyType2 = new string[] { "EnemyGhost0", "EnemyGhost1", "EnemyGhost2", "EnemyGhost3" };

		private readonly string[] EnemyType3 = new string[] { "EnemyMaoYu00", "EnemyMaoYu01", "EnemyMaoYu02", "EnemyMaoYu03" };

		private readonly string[] EnemyType4 = new string[] { "EnemyYYY4", "EnemyYYY5", "EnemyYYY6", "EnemyYYY7" };

		private int EnemyType = -1;

		private string EnemyName;

		public int HealthPoint;

		public int RedCount;

		public int BlueCount;

		public int GreenCount;

		public byte ColorType;

		public bool BackFire;

		public int ClearRadius;

		public bool StarFall;
	}
}
