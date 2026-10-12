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
				return StageData.DeviceMain;
			}
		}

		public Background3DObject(StageDataPackage StageData, string modelName, PointF OriginalPosition, bool InsertAtFirst)
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

		public override void Ctrl()
		{
			base.Ctrl();
			OriginalPositionZ += VelocityZ;
		}

		public override bool OutBoundary()
		{
			return Time > LifeTime && LifeTime > 0;
		}

		private void SetupMatrices()
		{
			var matrix = Matrix.Identity;
			matrix *= Matrix.RotationZ((float)Angle);
			matrix *= Matrix.Scaling(new Vector3(Scale, Scale, Scale));
			if (Mirrored)
			{
				matrix *= Matrix.RotationY(3.1415927f);
			}
			matrix *= Matrix.RotationY(Angle3DY);
			matrix *= Matrix.RotationX(Angle3DX);
			matrix *= Matrix.Translation(OriginalPosition.X, OriginalPosition.Y, OriginalPositionZ + Height);
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
