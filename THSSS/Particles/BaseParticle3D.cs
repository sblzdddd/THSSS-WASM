using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class BaseParticle3D : BaseObject
	{
		public IModel model { get; set; }

		public Device DeviceMain
		{
			get
			{
				return StageData.DeviceMain;
			}
		}

		public float Depth { get; set; }

		public float MaxDepth { get; set; }

		public double Direction3D { get; set; }

		public Vector3 RotatingAxis { get; set; }

		public float Angle3D { get; set; }

		public float AngularVelocity3D { get; set; }

		public BaseParticle3D(StageDataPackage StageData, string modelName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, modelName, OriginalPosition, Velocity, Direction)
		{
			this.OriginalPosition = OriginalPosition;
			model = StageData.ModelDictionary[modelName];
			MaxDepth = 80f;
		}

		public BaseParticle3D()
		{
		}

		public override void Move()
		{
			bool flag;
			if (DestPoint != new PointF(0f, 0f))
			{
				var destPoint = DestPoint;
				flag = 1 == 0;
			}
			else
			{
				flag = true;
			}
			if (!flag)
			{
				MovePoint();
			}
			var num = Velocity * (float)Math.Cos(Direction3D);
			Position = new PointF((float)((double)Position.X + (double)num * Math.Cos(Direction)), (float)((double)Position.Y + (double)num * Math.Sin(Direction)));
			Depth += Velocity * (float)Math.Sin(Direction3D);
			if (Depth > MaxDepth)
			{
				Depth = MaxDepth;
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			Angle3D += AngularVelocity3D;
			if (Velocity < 0f)
			{
				Velocity = 0f;
			}
		}

		public virtual void SetupMatrices()
		{
			var matrix = Matrix.Identity;
			if (Mirrored)
			{
				matrix *= Matrix.RotationY(3.1415927f);
			}
			matrix *= Matrix.Scaling(new Vector3(Scale, Scale, Scale));
			matrix *= Matrix.RotationZ((float)Angle);
			matrix *= Matrix.RotationAxis(RotatingAxis, Mirrored ? (-Angle3D) : Angle3D);
			matrix *= Matrix.Translation(OriginalPosition.X - (float)(StageData.BoundRect.Width / 2), OriginalPosition.Y - (float)(StageData.BoundRect.Height / 2), Depth);
			DeviceMain.SetTransform(TransformState.World, matrix);
		}

		public override void Show()
		{
			SetupMatrices();
			model.TransparentValue = TransparentValue;
			model.ColorValue = ColorValue;
			if (Active)
			{
				DeviceMain.SetRenderState<Blend>(RenderState.DestinationBlend, Blend.One);
			}
			else
			{
				DeviceMain.SetRenderState<Blend>(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
			}
			model.Draw();
		}

		public override void Dispose()
		{
		}
	}
}
