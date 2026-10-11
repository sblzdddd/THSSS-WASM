using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x020000A8 RID: 168
	public class EnvironmentSSS05 : BaseObject, IEnvironment
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00027310 File Offset: 0x00025510
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x00027327 File Offset: 0x00025527
		public Device DeviceMain { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00027330 File Offset: 0x00025530
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x00027347 File Offset: 0x00025547
		public bool OnBossAttack { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00027350 File Offset: 0x00025550
		public Vector3 CameraPos
		{
			get
			{
				return this.CameraPosition;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00027368 File Offset: 0x00025568
		public float CameraAngle
		{
			get
			{
				return (float)Math.Atan((double)((this.CameraTarget.Z - this.CameraPosition.Z) / (this.CameraTarget.Y - this.CameraPosition.Y)));
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000273B0 File Offset: 0x000255B0
		public EnvironmentSSS05(StageDataPackage StageData)
		{
			base.StageData = StageData;
			this.DeviceMain = StageData.DeviceMain;
			base.Velocity = 1f;
			this.CameraPosition = new Vector3(400f, -460f, 210f);
			this.CameraTarget = new Vector3(400f, 0f, 0f);
			this.CameraDirection = new Vector3(0f, 0f, 1f);
			this.FogStart = 0f;
			this.FogEnd = 0f;
			this.light = default(Light);
			this.light.Type = LightType.Directional;
			new Background3DObject(StageData, "Stair", new PointF(0f, 0f), false);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00027480 File Offset: 0x00025680
		public override void Ctrl()
		{
			base.Time++;
			base.Velocity += base.Accelerate;
			this.CameraPosition.Y = this.CameraPosition.Y + base.Velocity;
			this.CameraTarget.Y = this.CameraTarget.Y + base.Velocity;
			this.CameraPosition.Z = this.CameraPosition.Z + base.Velocity;
			this.CameraTarget.Z = this.CameraTarget.Z + base.Velocity;
			if (this.CameraPosition.Y > -100f)
			{
				this.CameraPosition.Y = this.CameraPosition.Y - 150f;
				this.CameraTarget.Y = this.CameraTarget.Y - 150f;
				this.CameraPosition.Z = this.CameraPosition.Z - 150f;
				this.CameraTarget.Z = this.CameraTarget.Z - 150f;
			}
			this.CameraPosition.X = this.CameraPosition.X + 0.055555556f * (float)Math.Cos((double)((float)base.Time / 180f));
			this.CameraTarget.X = this.CameraTarget.X + 0.055555556f * (float)Math.Cos((double)((float)base.Time / 180f));
			if (base.Time > 100 && base.Time < 5050)
			{
				this.FogEnd += 1f;
				if (this.FogEnd > 700f)
				{
					this.FogEnd = 700f;
				}
			}
			if (base.TimeMain > 400 && base.TimeMain < 700)
			{
				this.CameraPosition.X = this.CameraPosition.X - 1f;
				this.CameraTarget.X = this.CameraTarget.X - 1f;
			}
			else if (base.TimeMain >= 700 && base.TimeMain < 800)
			{
				this.CameraPosition.X = this.CameraPosition.X - 0.5f;
				this.CameraTarget.X = this.CameraTarget.X - 0.5f;
			}
			else if (base.TimeMain >= 800 && base.TimeMain < 850)
			{
				this.CameraPosition.X = this.CameraPosition.X - 0.2f;
				this.CameraTarget.X = this.CameraTarget.X - 0.2f;
			}
			if (base.TimeMain > 200 && base.TimeMain < 300)
			{
				this.CameraPosition.Y = this.CameraPosition.Y + 0.5f;
				this.CameraPosition.Z = this.CameraPosition.Z - 0.5f;
			}
			else if (base.TimeMain > 1100 && base.TimeMain < 1400)
			{
				this.CameraPosition.X = this.CameraPosition.X + 0.1f;
				this.CameraTarget.X = this.CameraTarget.X + 0.13f;
				this.CameraPosition.Z = this.CameraPosition.Z - 0.1f;
				if (this.CameraDirection.X < 0.03f)
				{
					this.CameraDirection.X = this.CameraDirection.X + 0.0001f;
				}
			}
			else if (base.TimeMain > 1800 && base.TimeMain < 2100)
			{
				if (this.CameraPosition.X > 30f)
				{
					this.CameraPosition.X = this.CameraPosition.X - 0.35f;
				}
				if (this.CameraTarget.X < 200f)
				{
					this.CameraTarget.X = this.CameraTarget.X + 0.25f;
				}
				this.CameraPosition.Z = this.CameraPosition.Z - 0.3f;
				this.CameraDirection.X = this.CameraDirection.X - 0.0001f;
			}
			if (base.TimeMain > 8200 && base.TimeMain < 8250)
			{
				BaseParticle3D baseParticle3D = new BaseParticle3D(base.StageData, "云（亮层）", new PointF((float)(base.BoundRect.Width / 2 + base.Ran.Next(-20, 20)), (float)(240 + base.Ran.Next(-20, 20))), 0f, 0.0)
				{
					TransparentValueF = 0f,
					TransparentVelocity = 0.2f,
					Scale = 2f,
					ColorValue = Color.Gray
				};
				baseParticle3D.TransparentVelocityDictionary.Add(300, -0.2f);
				baseParticle3D.AngularVelocityDegree = (float)base.Ran.Next(-50, 50) / 20f;
				baseParticle3D.Depth = (float)(base.TimeMain - 8200) / 10f;
				baseParticle3D.LifeTime = 1500;
				base.Background3D.BackgroundParticleList.Add(baseParticle3D);
			}
			if (base.TimeMain == 8601)
			{
				base.Background3D.BackgroundList.Clear();
				Background3DObject background3DObject = new Background3DObject(base.StageData, "祭坛", new PointF(0f, 0f), false);
				background3DObject.Scale = 10f;
				base.Velocity = 0f;
				this.CameraPosition = new Vector3(0f, -480f, 480f);
				this.CameraTarget = new Vector3(0f, 0f, 300f);
				this.CameraDirection = new Vector3(0f, 0f, 1f);
				this.FogEnd = 700f;
				this.FogStart = 200f;
			}
			if (base.TimeMain > 8600)
			{
				this.CameraPosition.X = this.CameraPosition.X + 0.1f * (float)Math.Cos((double)((float)base.Time / 180f));
				this.CameraTarget.X = this.CameraTarget.X + 0.083333336f * (float)Math.Cos((double)((float)base.Time / 180f));
				this.CameraPosition.Y = this.CameraPosition.Y - 0.05882353f * (float)Math.Cos((double)((float)base.Time / 180f));
				this.CameraTarget.Y = this.CameraTarget.Y - 0.07692308f * (float)Math.Cos((double)((float)base.Time / 180f));
			}
			this.LightColor = Color.White;
			this.light.Diffuse = this.LightColor;
			this.light.Ambient = this.LightColor;
			this.light.Direction = new Vector3(0f, 0f, -1f);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00027BFC File Offset: 0x00025DFC
		public void SetRenderPara()
		{
			this.DeviceMain.Clear(ClearFlags.ZBuffer | ClearFlags.Target, this.FogColor, 1f, 0);
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.SpriteMain.Draw2D(base.StageData.TextureObjectDictionary["夜空底色"], 1f, 0f, new PointF((float)(base.BoundRect.Left + 205), (float)(base.BoundRect.Top + 240)), Color.White);
			base.SpriteMain.Draw2D(base.StageData.TextureObjectDictionary["星星1"], 1f, 0f, new PointF((float)(base.BoundRect.Left + 205), (float)(base.BoundRect.Top + 240)), Color.FromArgb((int)(128.0 + 127.0 * Math.Sin((double)((float)base.Time / 200f * 2f) * 3.141592653589793)), Color.White));
			base.SpriteMain.Draw2D(base.StageData.TextureObjectDictionary["星星2"], 1f, 0f, new PointF((float)(base.BoundRect.Left + 205), (float)(base.BoundRect.Top + 240)), Color.FromArgb((int)(128.0 - 127.0 * Math.Sin((double)((float)base.Time / 200f * 2f) * 3.141592653589793)), Color.White));
			base.SpriteMain.End();
			this.DeviceMain.SetRenderState(RenderState.ZEnable, true);
			this.DeviceMain.SetRenderState(RenderState.ZWriteEnable, true);
			this.DeviceMain.SetRenderState(RenderState.CullMode, true);
			this.DeviceMain.SetRenderState(RenderState.AlphaBlendEnable, true);
			this.DeviceMain.SetRenderState<Blend>(RenderState.SourceBlend, Blend.SourceAlpha);
			this.DeviceMain.SetRenderState<Blend>(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
			this.DeviceMain.SetTextureStageState(0, TextureStage.AlphaArg1, TextureArgument.Texture);
			this.DeviceMain.SetTextureStageState(0, TextureStage.AlphaOperation, TextureOperation.Modulate);
			this.DeviceMain.SetTextureStageState(0, TextureStage.AlphaArg2, TextureArgument.Diffuse);
			this.DeviceMain.SetTransform(TransformState.View, Matrix.LookAtLH(this.CameraPosition, this.CameraTarget, this.CameraDirection));
			this.DeviceMain.SetTransform(TransformState.Projection, Matrix.PerspectiveFovLH(0.7853982f, 1f, 1f, this.FogEnd + 400f));
			this.DeviceMain.SetRenderState(RenderState.FogEnable, true);
			this.DeviceMain.SetRenderState<FogMode>(RenderState.FogTableMode, FogMode.Linear);
			this.DeviceMain.SetRenderState(RenderState.FogColor, this.FogColor.ToArgb());
			this.DeviceMain.SetRenderState(RenderState.FogStart, this.FogStart);
			this.DeviceMain.SetRenderState(RenderState.FogEnd, this.FogEnd);
			this.DeviceMain.SetLight(0, this.light);
			this.DeviceMain.SetRenderState(RenderState.Lighting, true);
			this.DeviceMain.EnableLight(0, true);
		}

		// Token: 0x04000193 RID: 403
		private Light light;

		// Token: 0x04000194 RID: 404
		private Color LightColor;

		// Token: 0x04000195 RID: 405
		private Color FogColor;

		// Token: 0x04000196 RID: 406
		private float FogStart;

		// Token: 0x04000197 RID: 407
		private float FogEnd;

		// Token: 0x04000198 RID: 408
		public Vector3 CameraPosition;

		// Token: 0x04000199 RID: 409
		public Vector3 CameraTarget;

		// Token: 0x0400019A RID: 410
		public Vector3 CameraDirection;
	}
}
