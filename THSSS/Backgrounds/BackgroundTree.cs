using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	internal class BackgroundTree : BaseObject
	{
		public IModel model { get; set; }

		public float CameraAngle { get; set; }

		public int Height { get; set; }

		public Device DeviceMain
		{
			get
			{
				return base.StageData.DeviceMain;
			}
		}

		public BackgroundTree(StageDataPackage StageData, string modelName, PointF OriginalPosition, bool InsertAtFirst)
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

		public override bool OutBoundary()
		{
			return base.Time > base.LifeTime && base.LifeTime != 0;
		}

		private void SetupMatrices()
		{
			this.CameraAngle = -1.5707964f + base.StageData.Background3D.Envi.CameraAngle;
			Matrix matrix = Matrix.Identity;
			matrix *= Matrix.RotationZ((float)base.Angle);
			matrix *= Matrix.Scaling(new Vector3(base.Scale, base.Scale, base.Scale));
			if (base.Mirrored)
			{
				matrix *= Matrix.RotationY(3.1415927f);
			}
			matrix *= Matrix.RotationX(this.CameraAngle);
			matrix *= Matrix.Translation(base.OriginalPosition.X, base.OriginalPosition.Y, (float)(this.model.TxtureObject.Height / 2 + this.Height) * base.Scale * (float)Math.Cos((double)(-(double)this.CameraAngle) - 1.5707963267948966));
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
