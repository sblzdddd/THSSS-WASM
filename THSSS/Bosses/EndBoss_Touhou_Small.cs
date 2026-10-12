using System.Drawing;
using SlimDX;

namespace Shooting
{
	internal class EndBoss_Touhou_Small : EndBoss_Touhou
	{
		public EndBoss_Touhou_Small(StageDataPackage StageData, PointF OriginalPosition, Color Color1, Color Color2)
			: base(StageData, OriginalPosition, Color1, Color2)
		{
		}

		public EndBoss_Touhou_Small(StageDataPackage StageData)
			: base(StageData)
		{
		}

		public override void Shoot()
		{
			if (base.Time == 1)
			{
				base.StageData.SoundPlay("se_enep01.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
				for (int i = 0; i < 40; i++)
				{
					Particle3D2 particle3D = new Particle3D2(base.StageData, "Effect-1", base.OriginalPosition, (float)base.Ran.Next(50, 70) / 10f, (double)((float)base.Ran.Next(360) / 180f * 3.1415927f), 30 + base.StageData.Ran.Next(50));
					particle3D.Direction3D = (double)((float)base.StageData.Ran.Next(0, 90) * 3.1415927f / 180f);
					particle3D.Angle = (double)((float)base.StageData.Ran.Next(0, 360) * 3.1415927f / 180f);
					particle3D.AngularVelocity3D = (float)base.StageData.Ran.Next(10, 18) / 100f;
					particle3D.Scale = (float)base.StageData.Ran.Next(30, 40) / 10f;
					particle3D.RotatingAxis = new Vector3((float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100));
					particle3D.ColorValue = Color.FromArgb(base.Ran.Next(this.R1, this.R2), base.Ran.Next(this.G1, this.G2), base.Ran.Next(this.B1, this.B2));
				}
				base.StageData.VibrateStart(90);
				base.Boss = null;
			}
		}

		public override void Show()
		{
		}
	}
}
