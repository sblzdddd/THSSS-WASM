using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001EB RID: 491
	public class EnemyFactory
	{
		// Token: 0x06000B36 RID: 2870 RVA: 0x0006EB50 File Offset: 0x0006CD50
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

		// Token: 0x06000B37 RID: 2871 RVA: 0x0006ED28 File Offset: 0x0006CF28
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

		// Token: 0x06000B38 RID: 2872 RVA: 0x0006ED70 File Offset: 0x0006CF70
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

		// Token: 0x04000417 RID: 1047
		private readonly string[] EnemyType0 = new string[] { "EnemyA0", "EnemyA1", "EnemyA2", "EnemyA3", "EnemyA4", "EnemyA5", "EnemyA6", "EnemyA7", "EnemyC0", "EnemyC1" };

		// Token: 0x04000418 RID: 1048
		private readonly string[] EnemyType1 = new string[] { "EnemyA8", "EnemyB1", "EnemyC2", "EnemyC3" };

		// Token: 0x04000419 RID: 1049
		private readonly string[] EnemyType2 = new string[] { "EnemyGhost0", "EnemyGhost1", "EnemyGhost2", "EnemyGhost3" };

		// Token: 0x0400041A RID: 1050
		private readonly string[] EnemyType3 = new string[] { "EnemyMaoYu00", "EnemyMaoYu01", "EnemyMaoYu02", "EnemyMaoYu03" };

		// Token: 0x0400041B RID: 1051
		private readonly string[] EnemyType4 = new string[] { "EnemyYYY4", "EnemyYYY5", "EnemyYYY6", "EnemyYYY7" };

		// Token: 0x0400041C RID: 1052
		private int EnemyType = -1;

		// Token: 0x0400041D RID: 1053
		private string EnemyName;

		// Token: 0x0400041E RID: 1054
		public int HealthPoint;

		// Token: 0x0400041F RID: 1055
		public int RedCount;

		// Token: 0x04000420 RID: 1056
		public int BlueCount;

		// Token: 0x04000421 RID: 1057
		public int GreenCount;

		// Token: 0x04000422 RID: 1058
		public byte ColorType;

		// Token: 0x04000423 RID: 1059
		public bool BackFire;

		// Token: 0x04000424 RID: 1060
		public int ClearRadius;

		// Token: 0x04000425 RID: 1061
		public bool StarFall;
	}
}
