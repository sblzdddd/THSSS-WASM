using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x02000081 RID: 129
	internal class Background3DObject : BaseObject
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0001F0E0 File Offset: 0x0001D2E0
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x0001F0F7 File Offset: 0x0001D2F7
		public IModel model { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0001F100 File Offset: 0x0001D300
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x0001F117 File Offset: 0x0001D317
		public float Angle3DX { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0001F120 File Offset: 0x0001D320
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x0001F137 File Offset: 0x0001D337
		public float Angle3DY { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0001F140 File Offset: 0x0001D340
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x0001F157 File Offset: 0x0001D357
		public float Height { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0001F160 File Offset: 0x0001D360
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x0001F177 File Offset: 0x0001D377
		public float OriginalPositionZ { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0001F180 File Offset: 0x0001D380
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x0001F197 File Offset: 0x0001D397
		public float VelocityZ { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0001F1A0 File Offset: 0x0001D3A0
		public Device DeviceMain
		{
			get
			{
				return base.StageData.DeviceMain;
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0001F1C0 File Offset: 0x0001D3C0
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

		// Token: 0x06000448 RID: 1096 RVA: 0x0001F24E File Offset: 0x0001D44E
		public override void Ctrl()
		{
			base.Ctrl();
			this.OriginalPositionZ += this.VelocityZ;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001F26C File Offset: 0x0001D46C
		public override bool OutBoundary()
		{
			return base.Time > base.LifeTime && base.LifeTime > 0;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0001F2A8 File Offset: 0x0001D4A8
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

		// Token: 0x0600044B RID: 1099 RVA: 0x0001F380 File Offset: 0x0001D580
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

		// Token: 0x0600044C RID: 1100 RVA: 0x0001F402 File Offset: 0x0001D602
		public override void Dispose()
		{
		}
	}
}
