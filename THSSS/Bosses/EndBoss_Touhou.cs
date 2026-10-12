using System;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	internal class EndBoss_Touhou : BaseEffect
	{
		public EndBoss_Touhou(StageDataPackage StageData, PointF OriginalPosition, Color Color1, Color Color2)
			: this(StageData)
		{
			this.OriginalPosition = OriginalPosition;
			R2 = (int)Math.Max(Color1.R, Color2.R);
			R1 = (int)Math.Min(Color1.R, Color2.R);
			G2 = (int)Math.Max(Color1.G, Color2.G);
			G1 = (int)Math.Min(Color1.G, Color2.G);
			B2 = (int)Math.Max(Color1.B, Color2.B);
			B1 = (int)Math.Min(Color1.B, Color2.B);
		}

		public EndBoss_Touhou(StageDataPackage StageData)
			: base(StageData)
		{
			LifeTime = 250;
		}

		public override void Move()
		{
			if (Boss != null)
			{
				Position = Boss.Position;
			}
		}

		public override void Shoot()
		{
			if (Time == 1)
			{
				StageData.SoundPlay("se_enep01.wav", OriginalPosition.X / (float)BoundRect.Width);
				for (int i = 0; i < 20; i++)
				{
					Particle3D2 particle3D = new Particle3D2(StageData, "Effect-1", OriginalPosition, (float)Ran.Next(10, 80) / 10f, (double)((float)Ran.Next(360) / 180f * 3.1415927f), 30 + StageData.Ran.Next(50));
					particle3D.Direction3D = (double)((float)StageData.Ran.Next(0, 30) * 3.1415927f / 180f);
					particle3D.Angle = (double)((float)StageData.Ran.Next(0, 360) * 3.1415927f / 180f);
					particle3D.AngularVelocity3D = (float)StageData.Ran.Next(10, 18) / 100f;
					particle3D.Scale = (float)StageData.Ran.Next(30, 40) / 10f;
					particle3D.RotatingAxis = new Vector3((float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100));
					particle3D.ColorValue = Color.FromArgb(Ran.Next(R1, R2), Ran.Next(G1, G2), Ran.Next(B1, B2));
				}
				StageData.VibrateStart(50);
			}
			else if (Time == 100)
			{
				StageData.SoundPlay("se_enep01.wav", OriginalPosition.X / (float)BoundRect.Width);
				for (int i = 0; i < 80; i++)
				{
					Particle3D2 particle3D2 = new Particle3D2(StageData, "Effect-1", OriginalPosition, (float)Ran.Next(10, 140) / 10f, (double)((float)Ran.Next(360) / 180f * 3.1415927f), 30 + StageData.Ran.Next(50));
					particle3D2.Direction3D = (double)((float)StageData.Ran.Next(0, 30) * 3.1415927f / 180f);
					particle3D2.Angle = (double)((float)StageData.Ran.Next(0, 360) * 3.1415927f / 180f);
					particle3D2.AngularVelocity3D = (float)StageData.Ran.Next(10, 18) / 100f;
					particle3D2.Scale = (float)StageData.Ran.Next(30, 40) / 10f;
					particle3D2.RotatingAxis = new Vector3((float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100));
					particle3D2.ColorValue = Color.FromArgb(Ran.Next(R1, R2), Ran.Next(G1, G2), Ran.Next(B1, B2));
				}
				StageData.VibrateStart(90);
				Boss = null;
			}
		}

		public override void Show()
		{
		}

		public int R1;

		public int R2;

		public int G1;

		public int G2;

		public int B1;

		public int B2;
	}
}
