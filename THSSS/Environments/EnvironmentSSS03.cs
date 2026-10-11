using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x020000DF RID: 223
	public class EnvironmentSSS03 : BaseObject, IEnvironment
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00036054 File Offset: 0x00034254
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x0003606B File Offset: 0x0003426B
		public Device DeviceMain { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00036074 File Offset: 0x00034274
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x0003608B File Offset: 0x0003428B
		public bool OnBossAttack { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00036094 File Offset: 0x00034294
		public Vector3 CameraPos
		{
			get
			{
				return this.CameraPosition;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x000360AC File Offset: 0x000342AC
		public float CameraAngle
		{
			get
			{
				return (float)Math.Atan((double)((this.CameraTarget.Z - this.CameraPosition.Z) / (this.CameraTarget.Y - this.CameraPosition.Y)));
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x000360F4 File Offset: 0x000342F4
		public EnvironmentSSS03(StageDataPackage StageData)
		{
			base.StageData = StageData;
			this.DeviceMain = StageData.DeviceMain;
			base.Velocity = 1f;
			this.CameraPosition = new Vector3(0f, 0f, 170f);
			this.CameraTarget = new Vector3(0f, 200f, 170f);
			this.CameraDirection = new Vector3(0f, 0f, 1f);
			this.FogStart = 0f;
			this.FogEnd = 200f;
			this.LightColor = Color.White;
			this.light = default(Light);
			this.light.Type = LightType.Directional;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000361D4 File Offset: 0x000343D4
		public override void Ctrl()
		{
			base.Time++;
			base.Velocity += base.Accelerate;
			if (base.Velocity < 0.5f)
			{
				base.Velocity = 0.5f;
			}
			while ((float)this.PosY3 < this.CameraPosition.Y + 4200f)
			{
				Background3DObject background3DObject = new Background3DObject(base.StageData, "柳树", new PointF(300f, (float)this.PosY3), true);
				background3DObject.Angle3DX = -1.5707964f;
				background3DObject.Height = 400f;
				Background3DObject background3DObject2 = new Background3DObject(base.StageData, "柳树", new PointF(-300f, (float)this.PosY3), true);
				background3DObject2.Angle3DX = -1.5707964f;
				background3DObject2.Height = 400f;
				background3DObject2.Mirrored = true;
				Background3DObject background3DObject3 = new Background3DObject(base.StageData, "柳树", new PointF(300f, (float)(this.PosY3 + 200)), true);
				background3DObject3.Angle3DX = -1.5707964f;
				background3DObject3.Height = 400f;
				background3DObject3.Mirrored = true;
				Background3DObject background3DObject4 = new Background3DObject(base.StageData, "柳树", new PointF(-300f, (float)(this.PosY3 + 200)), true);
				background3DObject4.Angle3DX = -1.5707964f;
				background3DObject4.Height = 400f;
				this.PosY3 += 400;
			}
			while ((float)this.PosY < this.CameraPosition.Y + 3072f)
			{
				Background3DObject background3DObject5 = new Background3DObject(base.StageData, "地砖", new PointF(0f, (float)this.PosY), true);
				background3DObject5.Angle3DX = -3.1415927f;
				background3DObject5.Scale = 1f;
				this.PosY += 256;
			}
			while ((float)this.PosY2 < this.CameraPosition.Y + 1950f)
			{
				Background3DObject background3DObject6 = new Background3DObject(base.StageData, "草地", new PointF(0f, (float)this.PosY2), true);
				background3DObject6.Angle3DX = -3.1415927f;
				background3DObject6.Scale = 5f;
				this.PosY2 += 1625;
			}
			for (int i = base.Background3D.BackgroundList.Count - 1; i >= 0; i--)
			{
				if (base.Background3D.BackgroundList[i].OriginalPosition.Y < this.CameraPosition.Y - 1000f)
				{
					base.Background3D.BackgroundList.RemoveAt(i);
				}
			}
			this.CameraPosition.Y = this.CameraPosition.Y + base.Velocity;
			this.CameraTarget.Y = this.CameraTarget.Y + base.Velocity;
			this.CameraPosition.X = 40f * (float)Math.Sin((double)((float)base.Time / 360f));
			this.CameraTarget.X = 40f * (float)Math.Sin((double)((float)(base.Time - 60) / 360f));
			if (base.Time > 50)
			{
				this.FogStart += 1f;
				if (this.FogStart > 10f)
				{
					this.FogStart = 10f;
				}
				this.FogEnd += 20f;
				if (this.FogEnd > 900f)
				{
					this.FogEnd = 900f;
				}
			}
			this.light.Diffuse = this.LightColor;
			this.light.Ambient = this.LightColor;
			this.light.Direction = new Vector3(0f, 0f, -1f);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0003662C File Offset: 0x0003482C
		public void SetRenderPara()
		{
			this.DeviceMain.Clear(ClearFlags.Target, this.FogColor, 0f, 0);
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.SpriteMain.Draw2D(base.StageData.TextureObjectDictionary["BG01a"], 1f, 0f, new PointF((float)(base.BoundRect.Left + 256), (float)(base.BoundRect.Top + 256)), Color.White);
			base.SpriteMain.End();
			this.DeviceMain.SetRenderState(RenderState.ZEnable, false);
			this.DeviceMain.SetRenderState(RenderState.CullMode, true);
			this.DeviceMain.SetRenderState(RenderState.AlphaBlendEnable, true);
			this.DeviceMain.SetRenderState<Blend>(RenderState.SourceBlend, Blend.SourceAlpha);
			this.DeviceMain.SetRenderState<Blend>(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
			this.DeviceMain.SetTextureStageState(0, TextureStage.AlphaArg1, TextureArgument.Texture);
			this.DeviceMain.SetTextureStageState(0, TextureStage.AlphaOperation, TextureOperation.Modulate);
			this.DeviceMain.SetTextureStageState(0, TextureStage.AlphaArg2, TextureArgument.Diffuse);
			this.DeviceMain.SetTransform(TransformState.View, Matrix.LookAtLH(this.CameraPosition, this.CameraTarget, this.CameraDirection));
			this.DeviceMain.SetTransform(TransformState.Projection, Matrix.PerspectiveFovLH(1.5707964f, 1f, 1f, this.FogEnd + 200f));
			this.DeviceMain.SetRenderState(RenderState.FogEnable, true);
			this.DeviceMain.SetRenderState<FogMode>(RenderState.FogTableMode, FogMode.Linear);
			this.DeviceMain.SetRenderState(RenderState.FogColor, this.FogColor.ToArgb());
			this.DeviceMain.SetRenderState(RenderState.FogStart, this.FogStart);
			this.DeviceMain.SetRenderState(RenderState.FogEnd, this.FogEnd);
			this.DeviceMain.SetLight(0, this.light);
			this.DeviceMain.SetRenderState(RenderState.Lighting, true);
			this.DeviceMain.EnableLight(0, true);
		}

		// Token: 0x04000212 RID: 530
		private Light light;

		// Token: 0x04000213 RID: 531
		private Color LightColor;

		// Token: 0x04000214 RID: 532
		private Color FogColor;

		// Token: 0x04000215 RID: 533
		private float FogStart;

		// Token: 0x04000216 RID: 534
		private float FogEnd;

		// Token: 0x04000217 RID: 535
		public Vector3 CameraPosition;

		// Token: 0x04000218 RID: 536
		public Vector3 CameraTarget;

		// Token: 0x04000219 RID: 537
		public Vector3 CameraDirection;

		// Token: 0x0400021A RID: 538
		private int PosY = -1000;

		// Token: 0x0400021B RID: 539
		private int PosY2 = -1000;

		// Token: 0x0400021C RID: 540
		private int PosY3 = -1000;
	}
}
