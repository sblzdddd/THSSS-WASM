using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x02000120 RID: 288
	public class EnvironmentSSS01 : BaseObject, IEnvironment
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x000467DC File Offset: 0x000449DC
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x000467F3 File Offset: 0x000449F3
		public Device DeviceMain { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x000467FC File Offset: 0x000449FC
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x00046813 File Offset: 0x00044A13
		public bool OnBossAttack { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0004681C File Offset: 0x00044A1C
		public Vector3 CameraPos
		{
			get
			{
				return this.CameraPosition;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x00046834 File Offset: 0x00044A34
		public float CameraAngle
		{
			get
			{
				return (float)Math.Atan((double)((this.CameraTarget.Z - this.CameraPosition.Z) / (this.CameraTarget.Y - this.CameraPosition.Y)));
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0004687C File Offset: 0x00044A7C
		public EnvironmentSSS01(StageDataPackage StageData)
		{
			base.StageData = StageData;
			this.DeviceMain = StageData.DeviceMain;
			base.Velocity = 2f;
			this.CameraPosition = new Vector3(0f, 2200f, 130f);
			this.CameraTarget = new Vector3(0f, 2400f, 50f);
			this.FogStart = 0f;
			this.FogEnd = 50f;
			this.LightColor = Color.White;
			this.light = default(Light);
			this.light.Type = LightType.Directional;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00046A40 File Offset: 0x00044C40
		public override void Ctrl()
		{
			base.Time++;
			base.Velocity += base.Accelerate;
			if (base.Time % 2048 == 0)
			{
				base.Background3D.BackgroundList.ForEach(delegate(BaseObject x)
				{
					if (x is Background3DObject)
					{
						if (((Background3DObject)x).model.Name != "591-1")
						{
							x.OriginalPosition = new PointF(x.OriginalPosition.X - 1024f, x.OriginalPosition.Y);
						}
					}
				});
			}
			if (base.Time % 30 == 0)
			{
				PointF pointF = new PointF((float)base.Ran.Next(-200, 200), this.CameraPos.Y + (float)base.Ran.Next(100, 500));
				Background3DObject background3DObject = new Background3DObject(base.StageData, "591-1", pointF, false);
				background3DObject.Scale = (float)base.Ran.Next(40, 60) / 100f;
				background3DObject.ScaleVelocity = -0.004f;
				background3DObject.Velocity = (float)base.Ran.Next(-10, 10) / 20f;
				background3DObject.DirectionDegree = (double)base.Ran.Next(360);
				background3DObject.VelocityZ = (float)base.Ran.Next(6, 15) / 20f;
				background3DObject.LifeTime = 200;
				background3DObject.Active = true;
				background3DObject.Angle3DX = -1.5707964f + base.StageData.Background3D.Envi.CameraAngle;
				background3DObject.TransparentValueF = 0f;
				background3DObject.TransparentVelocity = 10f;
			}
			if (6000 == base.TimeMain)
			{
				this.OnBossAttack = true;
			}
			this.CameraPosition.Y = this.CameraPosition.Y + base.Velocity;
			this.CameraTarget.Y = this.CameraTarget.Y + base.Velocity;
			if (this.CameraPosition.Y > 3500f)
			{
				this.CameraPosition.Y = this.CameraPosition.Y - 2000f;
				this.CameraTarget.Y = this.CameraTarget.Y - 2000f;
				base.Background3D.BackgroundList.ForEach(delegate(BaseObject x)
				{
					if (x is Background3DObject)
					{
						if (((Background3DObject)x).model.Name == "591-1")
						{
							x.OriginalPosition = new PointF(x.OriginalPosition.X, x.OriginalPosition.Y - 2000f);
						}
					}
				});
			}
			this.CameraPosition.X = 40f * (float)Math.Sin((double)((float)base.Time / 180f));
			this.CameraTarget.X = 40f * (float)Math.Sin((double)((float)base.Time / 180f));
			this.CameraDirection = new Vector3(0.02f * (float)Math.Sin((double)((float)base.Time / 90f)), 1f, 0f);
			if (this.OnBossAttack)
			{
				if (this.CameraPosition.Z < 430f)
				{
					this.CameraPosition.Z = this.CameraPosition.Z + 1f;
					this.CameraTarget.Z = this.CameraTarget.Z + 1.05f;
				}
				base.Background3D.BackgroundList.ForEach(delegate(BaseObject x)
				{
					if (x is Background3DObject)
					{
						x.TransparentValueF -= 0.5f;
					}
				});
			}
			if (!this.OnBossAttack)
			{
				if (base.Time <= 980)
				{
					this.FogEnd += 2f;
					if (this.FogEnd > 400f)
					{
						this.FogEnd = 400f;
					}
				}
				if (base.Time > 980)
				{
					this.FogStart += 1f;
					if (this.FogStart > 130f)
					{
						this.FogStart = 130f;
					}
					this.FogEnd += 1f;
					if (this.FogEnd > 510f)
					{
						this.FogEnd = 510f;
					}
				}
			}
			else
			{
				this.FogStart += 1f;
				if (this.FogStart > 230f)
				{
					this.FogStart = 230f;
				}
				this.FogEnd += 1f;
				if (this.FogEnd > 1010f)
				{
					this.FogEnd = 1010f;
				}
			}
			this.light.Diffuse = this.LightColor;
			this.light.Ambient = this.LightColor;
			this.light.Direction = new Vector3(0f, 0f, -1f);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00046F3C File Offset: 0x0004513C
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

		// Token: 0x0400029F RID: 671
		private Light light;

		// Token: 0x040002A0 RID: 672
		private Color LightColor;

		// Token: 0x040002A1 RID: 673
		private Color FogColor;

		// Token: 0x040002A2 RID: 674
		private float FogStart;

		// Token: 0x040002A3 RID: 675
		private float FogEnd;

		// Token: 0x040002A4 RID: 676
		public Vector3 CameraPosition;

		// Token: 0x040002A5 RID: 677
		public Vector3 CameraTarget;

		// Token: 0x040002A6 RID: 678
		public Vector3 CameraDirection;
	}
}
