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
				return CameraPosition;
			}
		}

		public float CameraAngle
		{
			get
			{
				return (float)Math.Atan((double)((CameraTarget.Z - CameraPosition.Z) / (CameraTarget.Y - CameraPosition.Y)));
			}
		}

		public EnvironmentSSS01(StageDataPackage StageData)
		{
			this.StageData = StageData;
			DeviceMain = StageData.DeviceMain;
			Velocity = 2f;
			CameraPosition = new Vector3(0f, 2200f, 130f);
			CameraTarget = new Vector3(0f, 2400f, 50f);
			FogStart = 0f;
			FogEnd = 50f;
			LightColor = Color.White;
			light = default(Light);
			light.Type = LightType.Directional;
		}

		public override void Ctrl()
		{
			Time++;
			Velocity += Accelerate;
			if (Time % 2048 == 0)
			{
				Background3D.BackgroundList.ForEach(delegate(BaseObject x)
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
			if (Time % 30 == 0)
			{
				PointF pointF = new PointF((float)Ran.Next(-200, 200), CameraPos.Y + (float)Ran.Next(100, 500));
				Background3DObject background3DObject = new Background3DObject(StageData, "591-1", pointF, false);
				background3DObject.Scale = (float)Ran.Next(40, 60) / 100f;
				background3DObject.ScaleVelocity = -0.004f;
				background3DObject.Velocity = (float)Ran.Next(-10, 10) / 20f;
				background3DObject.DirectionDegree = (double)Ran.Next(360);
				background3DObject.VelocityZ = (float)Ran.Next(6, 15) / 20f;
				background3DObject.LifeTime = 200;
				background3DObject.Active = true;
				background3DObject.Angle3DX = -1.5707964f + StageData.Background3D.Envi.CameraAngle;
				background3DObject.TransparentValueF = 0f;
				background3DObject.TransparentVelocity = 10f;
			}
			if (6000 == TimeMain)
			{
				OnBossAttack = true;
			}
			CameraPosition.Y = CameraPosition.Y + Velocity;
			CameraTarget.Y = CameraTarget.Y + Velocity;
			if (CameraPosition.Y > 3500f)
			{
				CameraPosition.Y = CameraPosition.Y - 2000f;
				CameraTarget.Y = CameraTarget.Y - 2000f;
				Background3D.BackgroundList.ForEach(delegate(BaseObject x)
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
			CameraPosition.X = 40f * (float)Math.Sin((double)((float)Time / 180f));
			CameraTarget.X = 40f * (float)Math.Sin((double)((float)Time / 180f));
			CameraDirection = new Vector3(0.02f * (float)Math.Sin((double)((float)Time / 90f)), 1f, 0f);
			if (OnBossAttack)
			{
				if (CameraPosition.Z < 430f)
				{
					CameraPosition.Z = CameraPosition.Z + 1f;
					CameraTarget.Z = CameraTarget.Z + 1.05f;
				}
				Background3D.BackgroundList.ForEach(delegate(BaseObject x)
				{
					if (x is Background3DObject)
					{
						x.TransparentValueF -= 0.5f;
					}
				});
			}
			if (!OnBossAttack)
			{
				if (Time <= 980)
				{
					FogEnd += 2f;
					if (FogEnd > 400f)
					{
						FogEnd = 400f;
					}
				}
				if (Time > 980)
				{
					FogStart += 1f;
					if (FogStart > 130f)
					{
						FogStart = 130f;
					}
					FogEnd += 1f;
					if (FogEnd > 510f)
					{
						FogEnd = 510f;
					}
				}
			}
			else
			{
				FogStart += 1f;
				if (FogStart > 230f)
				{
					FogStart = 230f;
				}
				FogEnd += 1f;
				if (FogEnd > 1010f)
				{
					FogEnd = 1010f;
				}
			}
			light.Diffuse = LightColor;
			light.Ambient = LightColor;
			light.Direction = new Vector3(0f, 0f, -1f);
		}

		public void SetRenderPara()
		{
			DeviceMain.Clear(ClearFlags.Target, FogColor, 0f, 0);
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			SpriteMain.Draw2D(StageData.TextureObjectDictionary["BG01a"], 1f, 0f, new PointF((float)(BoundRect.Left + 256), (float)(BoundRect.Top + 256)), Color.White);
			SpriteMain.End();
			DeviceMain.SetRenderState(RenderState.ZEnable, false);
			DeviceMain.SetRenderState(RenderState.CullMode, true);
			DeviceMain.SetRenderState(RenderState.AlphaBlendEnable, true);
			DeviceMain.SetRenderState<Blend>(RenderState.SourceBlend, Blend.SourceAlpha);
			DeviceMain.SetRenderState<Blend>(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
			DeviceMain.SetTextureStageState(0, TextureStage.AlphaArg1, TextureArgument.Texture);
			DeviceMain.SetTextureStageState(0, TextureStage.AlphaOperation, TextureOperation.Modulate);
			DeviceMain.SetTextureStageState(0, TextureStage.AlphaArg2, TextureArgument.Diffuse);
			DeviceMain.SetTransform(TransformState.View, Matrix.LookAtLH(CameraPosition, CameraTarget, CameraDirection));
			DeviceMain.SetTransform(TransformState.Projection, Matrix.PerspectiveFovLH(1.5707964f, 1f, 1f, FogEnd + 200f));
			DeviceMain.SetRenderState(RenderState.FogEnable, true);
			DeviceMain.SetRenderState<FogMode>(RenderState.FogTableMode, FogMode.Linear);
			DeviceMain.SetRenderState(RenderState.FogColor, FogColor.ToArgb());
			DeviceMain.SetRenderState(RenderState.FogStart, FogStart);
			DeviceMain.SetRenderState(RenderState.FogEnd, FogEnd);
			DeviceMain.SetLight(0, light);
			DeviceMain.SetRenderState(RenderState.Lighting, true);
			DeviceMain.EnableLight(0, true);
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
