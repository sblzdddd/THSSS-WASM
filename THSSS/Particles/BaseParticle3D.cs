using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x02000020 RID: 32
	public class BaseParticle3D : BaseObject
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000942C File Offset: 0x0000762C
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00009443 File Offset: 0x00007643
		public IModel model { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000944C File Offset: 0x0000764C
		public Device DeviceMain
		{
			get
			{
				return base.StageData.DeviceMain;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000946C File Offset: 0x0000766C
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00009483 File Offset: 0x00007683
		public float Depth { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0000948C File Offset: 0x0000768C
		// (set) Token: 0x0600018F RID: 399 RVA: 0x000094A3 File Offset: 0x000076A3
		public float MaxDepth { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000094AC File Offset: 0x000076AC
		// (set) Token: 0x06000191 RID: 401 RVA: 0x000094C3 File Offset: 0x000076C3
		public double Direction3D { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000094CC File Offset: 0x000076CC
		// (set) Token: 0x06000193 RID: 403 RVA: 0x000094E3 File Offset: 0x000076E3
		public Vector3 RotatingAxis { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000094EC File Offset: 0x000076EC
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00009503 File Offset: 0x00007703
		public float Angle3D { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000950C File Offset: 0x0000770C
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00009523 File Offset: 0x00007723
		public float AngularVelocity3D { get; set; }

		// Token: 0x06000198 RID: 408 RVA: 0x0000952C File Offset: 0x0000772C
		public BaseParticle3D(StageDataPackage StageData, string modelName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, modelName, OriginalPosition, Velocity, Direction)
		{
			base.OriginalPosition = OriginalPosition;
			this.model = StageData.ModelDictionary[modelName];
			this.MaxDepth = 80f;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00009565 File Offset: 0x00007765
		public BaseParticle3D()
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00009570 File Offset: 0x00007770
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

		// Token: 0x0600019B RID: 411 RVA: 0x00009658 File Offset: 0x00007858
		public override void Ctrl()
		{
			base.Ctrl();
			this.Angle3D += this.AngularVelocity3D;
			if (base.Velocity < 0f)
			{
				base.Velocity = 0f;
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000096A4 File Offset: 0x000078A4
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

		// Token: 0x0600019D RID: 413 RVA: 0x000097A0 File Offset: 0x000079A0
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

		// Token: 0x0600019E RID: 414 RVA: 0x00009816 File Offset: 0x00007A16
		public override void Dispose()
		{
		}
	}
}
