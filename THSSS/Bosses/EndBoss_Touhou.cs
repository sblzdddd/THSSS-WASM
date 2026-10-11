using System;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	// Token: 0x0200001B RID: 27
	internal class EndBoss_Touhou : BaseEffect
	{
		// Token: 0x06000179 RID: 377 RVA: 0x00008A14 File Offset: 0x00006C14
		public EndBoss_Touhou(StageDataPackage StageData, PointF OriginalPosition, Color Color1, Color Color2)
			: this(StageData)
		{
			base.OriginalPosition = OriginalPosition;
			this.R2 = (int)Math.Max(Color1.R, Color2.R);
			this.R1 = (int)Math.Min(Color1.R, Color2.R);
			this.G2 = (int)Math.Max(Color1.G, Color2.G);
			this.G1 = (int)Math.Min(Color1.G, Color2.G);
			this.B2 = (int)Math.Max(Color1.B, Color2.B);
			this.B1 = (int)Math.Min(Color1.B, Color2.B);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008AC9 File Offset: 0x00006CC9
		public EndBoss_Touhou(StageDataPackage StageData)
			: base(StageData)
		{
			base.LifeTime = 250;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00008AE4 File Offset: 0x00006CE4
		public override void Move()
		{
			if (base.Boss != null)
			{
				base.Position = base.Boss.Position;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00008B14 File Offset: 0x00006D14
		public override void Shoot()
		{
			if (base.Time == 1)
			{
				base.StageData.SoundPlay("se_enep01.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
				for (int i = 0; i < 20; i++)
				{
					Particle3D2 particle3D = new Particle3D2(base.StageData, "Effect-1", base.OriginalPosition, (float)base.Ran.Next(10, 80) / 10f, (double)((float)base.Ran.Next(360) / 180f * 3.1415927f), 30 + base.StageData.Ran.Next(50));
					particle3D.Direction3D = (double)((float)base.StageData.Ran.Next(0, 30) * 3.1415927f / 180f);
					particle3D.Angle = (double)((float)base.StageData.Ran.Next(0, 360) * 3.1415927f / 180f);
					particle3D.AngularVelocity3D = (float)base.StageData.Ran.Next(10, 18) / 100f;
					particle3D.Scale = (float)base.StageData.Ran.Next(30, 40) / 10f;
					particle3D.RotatingAxis = new Vector3((float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100));
					particle3D.ColorValue = Color.FromArgb(base.Ran.Next(this.R1, this.R2), base.Ran.Next(this.G1, this.G2), base.Ran.Next(this.B1, this.B2));
				}
				base.StageData.VibrateStart(50);
			}
			else if (base.Time == 100)
			{
				base.StageData.SoundPlay("se_enep01.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
				for (int i = 0; i < 80; i++)
				{
					Particle3D2 particle3D2 = new Particle3D2(base.StageData, "Effect-1", base.OriginalPosition, (float)base.Ran.Next(10, 140) / 10f, (double)((float)base.Ran.Next(360) / 180f * 3.1415927f), 30 + base.StageData.Ran.Next(50));
					particle3D2.Direction3D = (double)((float)base.StageData.Ran.Next(0, 30) * 3.1415927f / 180f);
					particle3D2.Angle = (double)((float)base.StageData.Ran.Next(0, 360) * 3.1415927f / 180f);
					particle3D2.AngularVelocity3D = (float)base.StageData.Ran.Next(10, 18) / 100f;
					particle3D2.Scale = (float)base.StageData.Ran.Next(30, 40) / 10f;
					particle3D2.RotatingAxis = new Vector3((float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100));
					particle3D2.ColorValue = Color.FromArgb(base.Ran.Next(this.R1, this.R2), base.Ran.Next(this.G1, this.G2), base.Ran.Next(this.B1, this.B2));
				}
				base.StageData.VibrateStart(90);
				base.Boss = null;
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00008F37 File Offset: 0x00007137
		public override void Show()
		{
		}

		// Token: 0x04000078 RID: 120
		public int R1;

		// Token: 0x04000079 RID: 121
		public int R2;

		// Token: 0x0400007A RID: 122
		public int G1;

		// Token: 0x0400007B RID: 123
		public int G2;

		// Token: 0x0400007C RID: 124
		public int B1;

		// Token: 0x0400007D RID: 125
		public int B2;
	}
}
