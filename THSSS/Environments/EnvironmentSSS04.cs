using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x020000C4 RID: 196
	public class EnvironmentSSS04 : BaseObject, IEnvironment
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0003030C File Offset: 0x0002E50C
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x00030323 File Offset: 0x0002E523
		public Device DeviceMain { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0003032C File Offset: 0x0002E52C
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x00030343 File Offset: 0x0002E543
		public bool OnBossAttack { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0003034C File Offset: 0x0002E54C
		public Vector3 CameraPos
		{
			get
			{
				return this.CameraPosition;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00030364 File Offset: 0x0002E564
		public float CameraAngle
		{
			get
			{
				return (float)Math.Atan((double)((this.CameraTarget.Z - this.CameraPosition.Z) / (this.CameraTarget.Y - this.CameraPosition.Y)));
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000303AC File Offset: 0x0002E5AC
		public EnvironmentSSS04(StageDataPackage StageData)
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

		// Token: 0x06000572 RID: 1394 RVA: 0x000305EC File Offset: 0x0002E7EC
		public override void Ctrl()
		{
			base.Time++;
			if (base.TimeMain < 400)
			{
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
				base.Velocity += base.Accelerate;
				if (base.Velocity < 0.5f)
				{
					base.Velocity = 0.5f;
				}
				this.CameraPosition.Y = this.CameraPosition.Y + base.Velocity;
				this.CameraTarget.Y = this.CameraTarget.Y + base.Velocity;
				this.CameraPosition.X = 40f * (float)Math.Sin((double)((float)base.Time / 360f));
				this.CameraTarget.X = 40f * (float)Math.Sin((double)((float)(base.Time - 60) / 360f));
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
			}
			else if (base.TimeMain > 4000)
			{
				if (this.CameraTarget.Y < 500f)
				{
					this.CameraTarget.Y = this.CameraTarget.Y + 1f;
				}
			}
			if (base.TimeMain == 150)
			{
				BackgroundTransitionOut backgroundTransitionOut = new BackgroundTransitionOut(base.StageData, 150);
				backgroundTransitionOut.LifeTime = 400;
			}
			else if (base.TimeMain == 550)
			{
				BackgroundTransitionIn backgroundTransitionIn = new BackgroundTransitionIn(base.StageData);
				backgroundTransitionIn.Delay = 0;
				backgroundTransitionIn.LifeTime = 300;
				this.CameraPosition = new Vector3(0f, 0f, 250f);
				this.CameraTarget = new Vector3(0f, 0f, 0f);
				this.CameraDirection = new Vector3(0f, 1f, 1f);
				base.Background3D.BackgroundList.Clear();
				Background3DObject background3DObject7 = new Background3DObject(base.StageData, "万花镜素材", new PointF(0f, 0f), false);
				background3DObject7.AngularVelocityDegree = 0.1f;
				Background3DObject background3DObject8 = new Background3DObject(base.StageData, "黑暗滤镜", new PointF(0f, 0f), false)
				{
					Height = 10f,
					Scale = 1f
				};
				background3DObject8.TransparentVelocityDictionary.Add(3350, -5f);
				this.FogStart = 0f;
				this.FogEnd = 2000000f;
			}
			else if (base.TimeMain == 4300)
			{
				BaseParticle3D bk2 = new BaseParticle3D(base.StageData, "极光素材", new PointF((float)(base.BoundRect.Width / 2), 125f), 0f, 0.0)
				{
					TransparentValueF = 128f,
					Active = true,
					AngleDegree = 0.0,
					ColorValue = Color.Black
				};
				bk2.CtrlCall = delegate(string x)
				{
					if (bk2.Time * 2 < 255)
					{
						bk2.ColorValue = Color.FromArgb(bk2.Time * 2, bk2.Time * 2, bk2.Time * 2);
					}
					bk2.TransparentValueF = 128f + 48f * (float)Math.Sin((double)((float)bk2.Time / 200f) * 3.141592653589793 * 2.0);
				};
				base.Background3D.BackgroundParticleList.Add(bk2);
			}
			if (base.TimeMain == 8701)
			{
				if (base.Background3D.BackgroundParticleList.Count == 0)
				{
					this.CameraPosition = new Vector3(0f, 0f, 250f);
					this.CameraTarget = new Vector3(0f, 500f, 0f);
					this.CameraDirection = new Vector3(0f, 1f, 1f);
					Background3DObject background3DObject9 = new Background3DObject(base.StageData, "万花镜素材", new PointF(0f, 0f), false);
					background3DObject9.AngularVelocityDegree = 0.1f;
					this.FogStart = 0f;
					this.FogEnd = 2000000f;
					BaseParticle3D bk = new BaseParticle3D(base.StageData, "极光素材", new PointF((float)(base.BoundRect.Width / 2), 125f), 0f, 0.0)
					{
						TransparentValueF = 128f,
						Active = true,
						AngleDegree = 0.0,
						ColorValue = Color.Black
					};
					bk.CtrlCall = delegate(string x)
					{
						if (bk.Time * 2 < 255)
						{
							bk.ColorValue = Color.FromArgb(bk.Time * 2, bk.Time * 2, bk.Time * 2);
						}
						bk.TransparentValueF = 128f + 48f * (float)Math.Sin((double)((float)bk.Time / 200f) * 3.141592653589793 * 2.0);
					};
					base.Background3D.BackgroundParticleList.Add(bk);
				}
			}
			this.light.Diffuse = this.LightColor;
			this.light.Ambient = this.LightColor;
			this.light.Direction = new Vector3(0f, 0f, -1f);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00030E80 File Offset: 0x0002F080
		public void SetRenderPara()
		{
			this.DeviceMain.Clear(ClearFlags.ZBuffer | ClearFlags.Target, this.FogColor, 1f, 0);
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			if (base.TimeMain < 400)
			{
				base.SpriteMain.Draw2D(base.StageData.TextureObjectDictionary["BG01a"], 1f, 0f, new PointF((float)(base.BoundRect.Left + 256), (float)(base.BoundRect.Top + 256)), Color.White);
			}
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

		// Token: 0x040001D2 RID: 466
		private Light light;

		// Token: 0x040001D3 RID: 467
		private Color LightColor;

		// Token: 0x040001D4 RID: 468
		private Color FogColor;

		// Token: 0x040001D5 RID: 469
		private float FogStart;

		// Token: 0x040001D6 RID: 470
		private float FogEnd;

		// Token: 0x040001D7 RID: 471
		public Vector3 CameraPosition;

		// Token: 0x040001D8 RID: 472
		public Vector3 CameraTarget;

		// Token: 0x040001D9 RID: 473
		public Vector3 CameraDirection;

		// Token: 0x040001DA RID: 474
		private int PosY = -1000;

		// Token: 0x040001DB RID: 475
		private int PosY2 = -1000;

		// Token: 0x040001DC RID: 476
		private int PosY3 = -1000;
	}
}
