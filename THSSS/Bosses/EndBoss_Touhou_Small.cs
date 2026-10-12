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
			if (Time == 1)
			{
				StageData.SoundPlay("se_enep01.wav", OriginalPosition.X / (float)BoundRect.Width);
				for (var i = 0; i < 40; i++)
				{
					var particle3D = new Particle3D2(StageData, "Effect-1", OriginalPosition, (float)Ran.Next(50, 70) / 10f, (double)((float)Ran.Next(360) / 180f * 3.1415927f), 30 + StageData.Ran.Next(50));
					particle3D.Direction3D = (double)((float)StageData.Ran.Next(0, 90) * 3.1415927f / 180f);
					particle3D.Angle = (double)((float)StageData.Ran.Next(0, 360) * 3.1415927f / 180f);
					particle3D.AngularVelocity3D = (float)StageData.Ran.Next(10, 18) / 100f;
					particle3D.Scale = (float)StageData.Ran.Next(30, 40) / 10f;
					particle3D.RotatingAxis = new Vector3((float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100));
					particle3D.ColorValue = Color.FromArgb(Ran.Next(R1, R2), Ran.Next(G1, G2), Ran.Next(B1, B2));
				}
				StageData.VibrateStart(90);
				Boss = null;
			}
		}

		public override void Show()
		{
		}
	}
}
