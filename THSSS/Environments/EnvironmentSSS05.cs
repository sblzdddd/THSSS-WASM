using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class EnvironmentSSS05 : BaseObject, IEnvironment
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

		public EnvironmentSSS05(StageDataPackage StageData)
		{
			this.StageData = StageData;
			DeviceMain = StageData.DeviceMain;
			Velocity = 1f;
			CameraPosition = new Vector3(400f, -460f, 210f);
			CameraTarget = new Vector3(400f, 0f, 0f);
			CameraDirection = new Vector3(0f, 0f, 1f);
			FogStart = 0f;
			FogEnd = 0f;
			light = default(Light);
			light.Type = LightType.Directional;
			new Background3DObject(StageData, "Stair", new PointF(0f, 0f), false);
		}

		public override void Ctrl()
		{
			Time++;
			Velocity += Accelerate;
			CameraPosition.Y = CameraPosition.Y + Velocity;
			CameraTarget.Y = CameraTarget.Y + Velocity;
			CameraPosition.Z = CameraPosition.Z + Velocity;
			CameraTarget.Z = CameraTarget.Z + Velocity;
			if (CameraPosition.Y > -100f)
			{
				CameraPosition.Y = CameraPosition.Y - 150f;
				CameraTarget.Y = CameraTarget.Y - 150f;
				CameraPosition.Z = CameraPosition.Z - 150f;
				CameraTarget.Z = CameraTarget.Z - 150f;
			}
			CameraPosition.X = CameraPosition.X + 0.055555556f * (float)Math.Cos((double)((float)Time / 180f));
			CameraTarget.X = CameraTarget.X + 0.055555556f * (float)Math.Cos((double)((float)Time / 180f));
			if (Time > 100 && Time < 5050)
			{
				FogEnd += 1f;
				if (FogEnd > 700f)
				{
					FogEnd = 700f;
				}
			}
			if (TimeMain > 400 && TimeMain < 700)
			{
				CameraPosition.X = CameraPosition.X - 1f;
				CameraTarget.X = CameraTarget.X - 1f;
			}
			else if (TimeMain >= 700 && TimeMain < 800)
			{
				CameraPosition.X = CameraPosition.X - 0.5f;
				CameraTarget.X = CameraTarget.X - 0.5f;
			}
			else if (TimeMain >= 800 && TimeMain < 850)
			{
				CameraPosition.X = CameraPosition.X - 0.2f;
				CameraTarget.X = CameraTarget.X - 0.2f;
			}
			if (TimeMain > 200 && TimeMain < 300)
			{
				CameraPosition.Y = CameraPosition.Y + 0.5f;
				CameraPosition.Z = CameraPosition.Z - 0.5f;
			}
			else if (TimeMain > 1100 && TimeMain < 1400)
			{
				CameraPosition.X = CameraPosition.X + 0.1f;
				CameraTarget.X = CameraTarget.X + 0.13f;
				CameraPosition.Z = CameraPosition.Z - 0.1f;
				if (CameraDirection.X < 0.03f)
				{
					CameraDirection.X = CameraDirection.X + 0.0001f;
				}
			}
			else if (TimeMain > 1800 && TimeMain < 2100)
			{
				if (CameraPosition.X > 30f)
				{
					CameraPosition.X = CameraPosition.X - 0.35f;
				}
				if (CameraTarget.X < 200f)
				{
					CameraTarget.X = CameraTarget.X + 0.25f;
				}
				CameraPosition.Z = CameraPosition.Z - 0.3f;
				CameraDirection.X = CameraDirection.X - 0.0001f;
			}
			if (TimeMain > 8200 && TimeMain < 8250)
			{
				BaseParticle3D baseParticle3D = new BaseParticle3D(StageData, "云（亮层）", new PointF((float)(BoundRect.Width / 2 + Ran.Next(-20, 20)), (float)(240 + Ran.Next(-20, 20))), 0f, 0.0)
				{
					TransparentValueF = 0f,
					TransparentVelocity = 0.2f,
					Scale = 2f,
					ColorValue = Color.Gray
				};
				baseParticle3D.TransparentVelocityDictionary.Add(300, -0.2f);
				baseParticle3D.AngularVelocityDegree = (float)Ran.Next(-50, 50) / 20f;
				baseParticle3D.Depth = (float)(TimeMain - 8200) / 10f;
				baseParticle3D.LifeTime = 1500;
				Background3D.BackgroundParticleList.Add(baseParticle3D);
			}
			if (TimeMain == 8601)
			{
				Background3D.BackgroundList.Clear();
				Background3DObject background3DObject = new Background3DObject(StageData, "祭坛", new PointF(0f, 0f), false);
				background3DObject.Scale = 10f;
				Velocity = 0f;
				CameraPosition = new Vector3(0f, -480f, 480f);
				CameraTarget = new Vector3(0f, 0f, 300f);
				CameraDirection = new Vector3(0f, 0f, 1f);
				FogEnd = 700f;
				FogStart = 200f;
			}
			if (TimeMain > 8600)
			{
				CameraPosition.X = CameraPosition.X + 0.1f * (float)Math.Cos((double)((float)Time / 180f));
				CameraTarget.X = CameraTarget.X + 0.083333336f * (float)Math.Cos((double)((float)Time / 180f));
				CameraPosition.Y = CameraPosition.Y - 0.05882353f * (float)Math.Cos((double)((float)Time / 180f));
				CameraTarget.Y = CameraTarget.Y - 0.07692308f * (float)Math.Cos((double)((float)Time / 180f));
			}
			LightColor = Color.White;
			light.Diffuse = LightColor;
			light.Ambient = LightColor;
			light.Direction = new Vector3(0f, 0f, -1f);
		}

		public void SetRenderPara()
		{
			DeviceMain.Clear(ClearFlags.ZBuffer | ClearFlags.Target, FogColor, 1f, 0);
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			SpriteMain.Draw2D(StageData.TextureObjectDictionary["夜空底色"], 1f, 0f, new PointF((float)(BoundRect.Left + 205), (float)(BoundRect.Top + 240)), Color.White);
			SpriteMain.Draw2D(StageData.TextureObjectDictionary["星星1"], 1f, 0f, new PointF((float)(BoundRect.Left + 205), (float)(BoundRect.Top + 240)), Color.FromArgb((int)(128.0 + 127.0 * Math.Sin((double)((float)Time / 200f * 2f) * 3.141592653589793)), Color.White));
			SpriteMain.Draw2D(StageData.TextureObjectDictionary["星星2"], 1f, 0f, new PointF((float)(BoundRect.Left + 205), (float)(BoundRect.Top + 240)), Color.FromArgb((int)(128.0 - 127.0 * Math.Sin((double)((float)Time / 200f * 2f) * 3.141592653589793)), Color.White));
			SpriteMain.End();
			DeviceMain.SetRenderState(RenderState.ZEnable, true);
			DeviceMain.SetRenderState(RenderState.ZWriteEnable, true);
			DeviceMain.SetRenderState(RenderState.CullMode, true);
			DeviceMain.SetRenderState(RenderState.AlphaBlendEnable, true);
			DeviceMain.SetRenderState<Blend>(RenderState.SourceBlend, Blend.SourceAlpha);
			DeviceMain.SetRenderState<Blend>(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
			DeviceMain.SetTextureStageState(0, TextureStage.AlphaArg1, TextureArgument.Texture);
			DeviceMain.SetTextureStageState(0, TextureStage.AlphaOperation, TextureOperation.Modulate);
			DeviceMain.SetTextureStageState(0, TextureStage.AlphaArg2, TextureArgument.Diffuse);
			DeviceMain.SetTransform(TransformState.View, Matrix.LookAtLH(CameraPosition, CameraTarget, CameraDirection));
			DeviceMain.SetTransform(TransformState.Projection, Matrix.PerspectiveFovLH(0.7853982f, 1f, 1f, FogEnd + 400f));
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
