using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x02000103 RID: 259
	public class EnvironmentSSSEx : BaseObject, IEnvironment
	{
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00040114 File Offset: 0x0003E314
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x0004012B File Offset: 0x0003E32B
		public Device DeviceMain { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00040134 File Offset: 0x0003E334
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x0004014B File Offset: 0x0003E34B
		public bool OnBossAttack { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00040154 File Offset: 0x0003E354
		public Vector3 CameraPos
		{
			get
			{
				return this.CameraPosition;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0004016C File Offset: 0x0003E36C
		public float CameraAngle
		{
			get
			{
				return (float)Math.Atan((double)((this.CameraTarget.Z - this.CameraPosition.Z) / (this.CameraTarget.Y - this.CameraPosition.Y)));
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000401B4 File Offset: 0x0003E3B4
		public EnvironmentSSSEx(StageDataPackage StageData)
		{
			base.StageData = StageData;
			this.DeviceMain = StageData.DeviceMain;
			base.Velocity = 1f;
			this.CameraPosition = new Vector3(0f, 0f, 250f);
			this.CameraTarget = new Vector3(0f, 200f, 170f);
			this.CameraDirection = new Vector3(0f, 0f, 1f);
			this.FogColor = Color.FromArgb(80, 165, 255);
			this.FogStart = 0f;
			this.FogEnd = 200f;
			this.LightColor = Color.White;
			this.light = default(Light);
			this.light.Type = LightType.Directional;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x000402A0 File Offset: 0x0003E4A0
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
				Background3DObject background3DObject = new Background3DObject(base.StageData, "Tree2", new PointF(300f, (float)this.PosY3), true);
				background3DObject.Angle3DX = -1.5707964f + this.CameraAngle;
				background3DObject.Height = 233f * (float)Math.Cos((double)this.CameraAngle);
				Background3DObject background3DObject2 = new Background3DObject(base.StageData, "Tree2", new PointF(-300f, (float)this.PosY3), true);
				background3DObject2.Angle3DX = -1.5707964f + this.CameraAngle;
				background3DObject2.Height = 233f * (float)Math.Cos((double)this.CameraAngle);
				background3DObject2.Mirrored = true;
				Background3DObject background3DObject3 = new Background3DObject(base.StageData, "Tree1", new PointF(300f, (float)(this.PosY3 + 200)), true);
				background3DObject3.Angle3DX = -1.5707964f + this.CameraAngle;
				background3DObject3.Height = 225f * (float)Math.Cos((double)this.CameraAngle);
				background3DObject3.Mirrored = true;
				Background3DObject background3DObject4 = new Background3DObject(base.StageData, "Tree1", new PointF(-300f, (float)(this.PosY3 + 200)), true);
				background3DObject4.Angle3DX = -1.5707964f + this.CameraAngle;
				background3DObject4.Height = 225f * (float)Math.Cos((double)this.CameraAngle);
				this.PosY3 += 400;
			}
			while ((float)this.PosY2 < this.CameraPosition.Y + 1950f)
			{
				Background3DObject background3DObject5 = new Background3DObject(base.StageData, "草地ex", new PointF(0f, (float)this.PosY2), true);
				background3DObject5.Angle3DX = -3.1415927f;
				background3DObject5.Scale = 2f;
				this.PosY2 += 486;
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

		// Token: 0x06000720 RID: 1824 RVA: 0x000406D4 File Offset: 0x0003E8D4
		public void SetRenderPara()
		{
			this.DeviceMain.Clear(ClearFlags.Target, this.FogColor, 0f, 0);
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.SpriteMain.Draw2D(base.StageData.TextureObjectDictionary["天色"], 1f, 0f, new PointF((float)(base.BoundRect.Left + 204), (float)(base.BoundRect.Top + 240)), Color.White);
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
			this.DeviceMain.SetTransform(TransformState.Projection, Matrix.PerspectiveFovLH(1.5707964f, 1f, 1f, 1000f));
			this.DeviceMain.SetRenderState(RenderState.FogEnable, true);
			this.DeviceMain.SetRenderState<FogMode>(RenderState.FogTableMode, FogMode.Linear);
			this.DeviceMain.SetRenderState(RenderState.FogColor, this.FogColor.ToArgb());
			this.DeviceMain.SetRenderState(RenderState.FogStart, this.FogStart);
			this.DeviceMain.SetRenderState(RenderState.FogEnd, this.FogEnd);
			this.DeviceMain.SetLight(0, this.light);
			this.DeviceMain.SetRenderState(RenderState.Lighting, true);
			this.DeviceMain.EnableLight(0, true);
		}

		// Token: 0x0400025F RID: 607
		private int PosY2 = -1000;

		// Token: 0x04000260 RID: 608
		private int PosY3 = -1000;

		// Token: 0x04000261 RID: 609
		private Light light;

		// Token: 0x04000262 RID: 610
		private Color LightColor;

		// Token: 0x04000263 RID: 611
		private Color FogColor;

		// Token: 0x04000264 RID: 612
		private float FogStart;

		// Token: 0x04000265 RID: 613
		private float FogEnd;

		// Token: 0x04000266 RID: 614
		public Vector3 CameraPosition;

		// Token: 0x04000267 RID: 615
		public Vector3 CameraTarget;

		// Token: 0x04000268 RID: 616
		public Vector3 CameraDirection;
	}
}
