using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class EnvironmentSSS06 : BaseObject, IEnvironment
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

		public EnvironmentSSS06(StageDataPackage StageData)
		{
			base.StageData = StageData;
			this.DeviceMain = StageData.DeviceMain;
			base.Velocity = 0.5f;
			this.CameraPosition = new Vector3(0f, 0f, 170f);
			this.CameraTarget = new Vector3(0f, 200f, 170f);
			this.CameraDirection = new Vector3(0f, 0f, 1f);
			this.FogStart = 0f;
			this.FogEnd = 200f;
			this.LightColor = Color.White;
			this.light = default(Light);
			this.light.Type = LightType.Directional;
		}

		public override void Ctrl()
		{
			base.Time++;
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
			if (base.TimeMain == 3)
			{
				BaseParticle3D baseParticle3D = new BaseParticle3D(base.StageData, "BG01a", new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2)), 0f, 1.5707963267948966);
				baseParticle3D.TransparentVelocityDictionary.Add(500, -2f);
				base.Background3D.BackgroundParticleList.Add(baseParticle3D);
				Background3DObject background3DObject = new Background3DObject(base.StageData, "祭坛", new PointF(0f, 0f), false);
				background3DObject.Scale = 10f;
				base.Velocity = 0f;
				this.CameraPosition = new Vector3(0f, -550f, 280f);
				this.CameraTarget = new Vector3(0f, 0f, 100f);
				this.CameraDirection = new Vector3(0f, 0f, 1f);
			}
			if (base.TimeMain > 0 && base.TimeMain < 800)
			{
				this.CameraTarget.Z = this.CameraTarget.Z + 1.5f;
				this.CameraPosition.Z = this.CameraPosition.Z + 1.5f;
			}
			else if (base.TimeMain == 800)
			{
				this.PosY2 -= 4000;
				base.Background3D.BackgroundList.Clear();
				while ((float)this.PosY2 < this.CameraPosition.Y + 6132f)
				{
					Background3DObject background3DObject2 = new Background3DObject(base.StageData, "云（亮层）", new PointF(0f, (float)this.PosY2), true);
					background3DObject2.Scale = 3f;
					background3DObject2.Mirrored = false;
					background3DObject2.OriginalPositionZ = this.CameraPosition.Z - 200f;
					background3DObject2.Angle3DX = -3.1415927f;
					background3DObject2.TransparentValueF = 0f;
					background3DObject2.MaxTransparent = 250;
					background3DObject2.TransparentVelocity = 2.5f;
					background3DObject2.Velocity = 0f;
					Background3DObject bo = new Background3DObject(base.StageData, "Cloud01", new PointF(0f, (float)(this.PosY2 + 250)), false)
					{
						Scale = 3f,
						Mirrored = true,
						OriginalPositionZ = this.CameraPosition.Z - 200f + 20f,
						Angle3DX = -3.1415927f,
						TransparentValueF = 0f,
						MaxTransparent = 50,
						TransparentVelocity = 0.5f,
						Velocity = 0.5f,
						Direction = -1.5707963267948966
					};
					bo.CtrlCall = delegate(string x)
					{
						if (bo.Time > 100)
						{
							bo.TransparentVelocity = -0.62831855f * (float)Math.Sin((double)((float)(this.Time - 100) / 300f) * 3.141592653589793 * 2.0);
						}
					};
					this.PosY2 += 1314;
				}
			}
			else if (base.TimeMain > 800)
			{
				if (base.TimeMain < 1000)
				{
					this.CameraPosition.Z = this.CameraPosition.Z + 1f;
				}
				if (base.TimeMain < 8000)
				{
					if (base.Time % 10 == 0)
					{
						PointF pointF = new PointF((float)base.Ran.Next(-400, 400), this.CameraPos.Y + (float)base.Ran.Next(100, 500));
						Background3DObject background3DObject3 = new Background3DObject(base.StageData, "591-1", pointF, false);
						background3DObject3.Scale = (float)base.Ran.Next(80, 100) / 100f;
						background3DObject3.ScaleVelocity = -0.004f;
						background3DObject3.Velocity = (float)base.Ran.Next(-10, 10) / 20f;
						background3DObject3.DirectionDegree = (double)base.Ran.Next(360);
						background3DObject3.VelocityZ = (float)base.Ran.Next(6, 15) / 20f;
						background3DObject3.LifeTime = 200;
						background3DObject3.Active = true;
						background3DObject3.Angle3DX = -1.5707964f + base.StageData.Background3D.Envi.CameraAngle;
						background3DObject3.TransparentValueF = 0f;
						background3DObject3.TransparentVelocity = 10f;
						background3DObject3.OriginalPositionZ = this.CameraPosition.Z - 200f;
					}
				}
				if (8000 < base.TimeMain && base.TimeMain < 8200)
				{
					this.CameraTarget.Z = this.CameraTarget.Z + 1.5f;
				}
			}
			if (base.TimeMain == 8200)
			{
				BaseParticle3D baseParticle3D = new BaseParticle3D(base.StageData, "夜空（暗）", new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2)), 0f, 1.5707963267948966);
				baseParticle3D.TransparentValueF = 0f;
				baseParticle3D.TransparentVelocity = 3f;
				base.Background3D.BackgroundParticleList.Add(baseParticle3D);
			}
			if (base.TimeMain == 8401)
			{
				if (base.Background3D.BackgroundParticleList.Count == 0)
				{
					BaseParticle3D baseParticle3D = new BaseParticle3D(base.StageData, "夜空（暗）", new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2)), 0f, 1.5707963267948966);
					base.Background3D.BackgroundParticleList.Add(baseParticle3D);
				}
			}
			for (int i = base.Background3D.BackgroundList.Count - 1; i >= 0; i--)
			{
				if (base.Background3D.BackgroundList[i].OriginalPosition.Y < this.CameraPosition.Y - 1000f)
				{
					base.Background3D.BackgroundList.RemoveAt(i);
				}
			}
			this.light.Diffuse = this.LightColor;
			this.light.Ambient = this.LightColor;
			this.light.Direction = new Vector3(0f, 0f, -1f);
		}

		public void SetRenderPara()
		{
			this.DeviceMain.Clear(ClearFlags.ZBuffer | ClearFlags.Target, this.FogColor, 1f, 0);
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.SpriteMain.End();
			this.DeviceMain.SetRenderState(RenderState.ZEnable, base.TimeMain < 800);
			this.DeviceMain.SetRenderState(RenderState.CullMode, true);
			this.DeviceMain.SetRenderState(RenderState.AlphaBlendEnable, true);
			this.DeviceMain.SetRenderState<Blend>(RenderState.SourceBlend, Blend.SourceAlpha);
			this.DeviceMain.SetRenderState<Blend>(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
			this.DeviceMain.SetTextureStageState(0, TextureStage.AlphaArg1, TextureArgument.Texture);
			this.DeviceMain.SetTextureStageState(0, TextureStage.AlphaOperation, TextureOperation.Modulate);
			this.DeviceMain.SetTextureStageState(0, TextureStage.AlphaArg2, TextureArgument.Diffuse);
			this.DeviceMain.SetTransform(TransformState.View, Matrix.LookAtLH(this.CameraPosition, this.CameraTarget, this.CameraDirection));
			this.DeviceMain.SetTransform(TransformState.Projection, Matrix.PerspectiveFovLH(0.7853982f, 1f, 1f, this.FogEnd + 200f));
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

		private int PosY = -1000;

		private int PosY2 = -1000;

		private int PosY3 = -1000;
	}
}
