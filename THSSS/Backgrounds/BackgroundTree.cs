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
				return StageData.DeviceMain;
			}
		}

		public BackgroundTree(StageDataPackage StageData, string modelName, PointF OriginalPosition, bool InsertAtFirst)
			: base(StageData, null, new PointF(0f, 0f), 0f, 0.0)
		{
			this.OriginalPosition = OriginalPosition;
			if (modelName != null)
			{
				model = StageData.ModelDictionary[modelName];
			}
			if (InsertAtFirst)
			{
				Background3D.BackgroundList.Insert(0, this);
			}
			else
			{
				Background3D.BackgroundList.Add(this);
			}
		}

		public override bool OutBoundary()
		{
			return Time > LifeTime && LifeTime != 0;
		}

		private void SetupMatrices()
		{
			CameraAngle = -1.5707964f + StageData.Background3D.Envi.CameraAngle;
			Matrix matrix = Matrix.Identity;
			matrix *= Matrix.RotationZ((float)Angle);
			matrix *= Matrix.Scaling(new Vector3(Scale, Scale, Scale));
			if (Mirrored)
			{
				matrix *= Matrix.RotationY(3.1415927f);
			}
			matrix *= Matrix.RotationX(CameraAngle);
			matrix *= Matrix.Translation(OriginalPosition.X, OriginalPosition.Y, (float)(model.TxtureObject.Height / 2 + Height) * Scale * (float)Math.Cos((double)(-(double)CameraAngle) - 1.5707963267948966));
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
			model.SetModel();
			model.Draw();
		}

		public override void Dispose()
		{
		}
	}
}
