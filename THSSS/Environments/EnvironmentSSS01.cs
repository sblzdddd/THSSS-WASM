using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class EnvironmentSSS01 : BaseObject, IEnvironment
	{
		public Device DeviceMain { get; set; }

		public bool OnBossAttack { get; set; }

		public Vector3 CameraPos
		{
			get
			{
				return this.CameraPosition;
			}
		}

		public float CameraAngle
		{
			get
			{
				return (float)Math.Atan((double)((this.CameraTarget.Z - this.CameraPosition.Z) / (this.CameraTarget.Y - this.CameraPosition.Y)));
			}
		}

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

		private Light light;

		private Color LightColor;

		private Color FogColor;

		private float FogStart;

		private float FogEnd;

		public Vector3 CameraPosition;

		public Vector3 CameraTarget;

		public Vector3 CameraDirection;
	}
}
