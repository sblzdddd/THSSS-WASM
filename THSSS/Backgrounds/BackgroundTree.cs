using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x020000E0 RID: 224
	internal class BackgroundTree : BaseObject
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0003682C File Offset: 0x00034A2C
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x00036843 File Offset: 0x00034A43
		public IModel model { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0003684C File Offset: 0x00034A4C
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x00036863 File Offset: 0x00034A63
		public float CameraAngle { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0003686C File Offset: 0x00034A6C
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x00036883 File Offset: 0x00034A83
		public int Height { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x0003688C File Offset: 0x00034A8C
		public Device DeviceMain
		{
			get
			{
				return base.StageData.DeviceMain;
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000368AC File Offset: 0x00034AAC
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

		// Token: 0x06000621 RID: 1569 RVA: 0x0003693C File Offset: 0x00034B3C
		public override bool OutBoundary()
		{
			return base.Time > base.LifeTime && base.LifeTime != 0;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00036974 File Offset: 0x00034B74
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

		// Token: 0x06000623 RID: 1571 RVA: 0x00036A88 File Offset: 0x00034C88
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

		// Token: 0x06000624 RID: 1572 RVA: 0x00036B0A File Offset: 0x00034D0A
		public override void Dispose()
		{
		}
	}
}
