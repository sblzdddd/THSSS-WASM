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
				return base.StageData.DeviceMain;
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
			base.OriginalPosition = OriginalPosition;
			this.model = StageData.ModelDictionary[modelName];
			this.MaxDepth = 80f;
		}

		public BaseParticle3D()
		{
		}

		public override void Move()
		{
			bool flag;
			if (base.DestPoint != new PointF(0f, 0f))
			{
				PointF destPoint = base.DestPoint;
				flag = 1 == 0;
			}
			else
			{
				flag = true;
			}
			if (!flag)
			{
				base.MovePoint();
			}
			float num = base.Velocity * (float)Math.Cos(this.Direction3D);
			base.Position = new PointF((float)((double)base.Position.X + (double)num * Math.Cos(base.Direction)), (float)((double)base.Position.Y + (double)num * Math.Sin(base.Direction)));
			this.Depth += base.Velocity * (float)Math.Sin(this.Direction3D);
			if (this.Depth > this.MaxDepth)
			{
				this.Depth = this.MaxDepth;
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			this.Angle3D += this.AngularVelocity3D;
			if (base.Velocity < 0f)
			{
				base.Velocity = 0f;
			}
		}

		public virtual void SetupMatrices()
		{
			Matrix matrix = Matrix.Identity;
			if (base.Mirrored)
			{
				matrix *= Matrix.RotationY(3.1415927f);
			}
			matrix *= Matrix.Scaling(new Vector3(base.Scale, base.Scale, base.Scale));
			matrix *= Matrix.RotationZ((float)base.Angle);
			matrix *= Matrix.RotationAxis(this.RotatingAxis, base.Mirrored ? (-this.Angle3D) : this.Angle3D);
			matrix *= Matrix.Translation(base.OriginalPosition.X - (float)(base.StageData.BoundRect.Width / 2), base.OriginalPosition.Y - (float)(base.StageData.BoundRect.Height / 2), this.Depth);
			this.DeviceMain.SetTransform(TransformState.World, matrix);
		}

		public override void Show()
		{
			this.SetupMatrices();
			this.model.TransparentValue = base.TransparentValue;
			this.model.ColorValue = base.ColorValue;
			if (base.Active)
			{
				this.DeviceMain.SetRenderState<Blend>(RenderState.DestinationBlend, Blend.One);
			}
			else
			{
				this.DeviceMain.SetRenderState<Blend>(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
			}
			this.model.Draw();
		}

		public override void Dispose()
		{
		}
	}
}
