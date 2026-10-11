using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	internal class Background3DObject : BaseObject
	{
		public IModel model { get; set; }

		public float Angle3DX { get; set; }

		public float Angle3DY { get; set; }

		public float Height { get; set; }

		public float OriginalPositionZ { get; set; }

		public float VelocityZ { get; set; }

		public Device DeviceMain
		{
			get
			{
				return base.StageData.DeviceMain;
			}
		}

		public Background3DObject(StageDataPackage StageData, string modelName, PointF OriginalPosition, bool InsertAtFirst)
			: base(StageData, null, new PointF(0f, 0f), 0f, 0.0)
		{
			base.OriginalPosition = OriginalPosition;
			if (modelName != null)
			{
				this.model = StageData.ModelDictionary[modelName];
			}
			if (InsertAtFirst)
			{
				base.Background3D.BackgroundList.Insert(0, this);
			}
			else
			{
				base.Background3D.BackgroundList.Add(this);
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			this.OriginalPositionZ += this.VelocityZ;
		}

		public override bool OutBoundary()
		{
			return base.Time > base.LifeTime && base.LifeTime > 0;
		}

		private void SetupMatrices()
		{
			Matrix matrix = Matrix.Identity;
			matrix *= Matrix.RotationZ((float)base.Angle);
			matrix *= Matrix.Scaling(new Vector3(base.Scale, base.Scale, base.Scale));
			if (base.Mirrored)
			{
				matrix *= Matrix.RotationY(3.1415927f);
			}
			matrix *= Matrix.RotationY(this.Angle3DY);
			matrix *= Matrix.RotationX(this.Angle3DX);
			matrix *= Matrix.Translation(base.OriginalPosition.X, base.OriginalPosition.Y, this.OriginalPositionZ + this.Height);
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
			this.model.SetModel();
			this.model.Draw();
		}

		public override void Dispose()
		{
		}
	}
}
