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

		public EnvironmentSSS06(StageDataPackage StageData)
		{
			this.StageData = StageData;
			DeviceMain = StageData.DeviceMain;
			Velocity = 0.5f;
			CameraPosition = new Vector3(0f, 0f, 170f);
			CameraTarget = new Vector3(0f, 200f, 170f);
			CameraDirection = new Vector3(0f, 0f, 1f);
			FogStart = 0f;
			FogEnd = 200f;
			LightColor = Color.White;
			light = default(Light);
			light.Type = LightType.Directional;
		}

		public override void Ctrl()
		{
			Time++;
			if (Time > 50)
			{
				FogStart += 1f;
				if (FogStart > 10f)
				{
					FogStart = 10f;
				}
				FogEnd += 20f;
				if (FogEnd > 900f)
				{
					FogEnd = 900f;
				}
			}
			Velocity += Accelerate;
			if (Velocity < 0.5f)
			{
				Velocity = 0.5f;
			}
			CameraPosition.Y = CameraPosition.Y + Velocity;
			CameraTarget.Y = CameraTarget.Y + Velocity;
			CameraPosition.X = 40f * (float)Math.Sin((double)((float)Time / 360f));
			CameraTarget.X = 40f * (float)Math.Sin((double)((float)(Time - 60) / 360f));
			if (TimeMain == 3)
			{
				BaseParticle3D baseParticle3D = new BaseParticle3D(StageData, "BG01a", new PointF((float)(BoundRect.Width / 2), (float)(BoundRect.Height / 2)), 0f, 1.5707963267948966);
				baseParticle3D.TransparentVelocityDictionary.Add(500, -2f);
				Background3D.BackgroundParticleList.Add(baseParticle3D);
				Background3DObject background3DObject = new Background3DObject(StageData, "祭坛", new PointF(0f, 0f), false);
				background3DObject.Scale = 10f;
				Velocity = 0f;
				CameraPosition = new Vector3(0f, -550f, 280f);
				CameraTarget = new Vector3(0f, 0f, 100f);
				CameraDirection = new Vector3(0f, 0f, 1f);
			}
			if (TimeMain > 0 && TimeMain < 800)
			{
				CameraTarget.Z = CameraTarget.Z + 1.5f;
				CameraPosition.Z = CameraPosition.Z + 1.5f;
			}
			else if (TimeMain == 800)
			{
				PosY2 -= 4000;
				Background3D.BackgroundList.Clear();
				while ((float)PosY2 < CameraPosition.Y + 6132f)
				{
					Background3DObject background3DObject2 = new Background3DObject(StageData, "云（亮层）", new PointF(0f, (float)PosY2), true);
					background3DObject2.Scale = 3f;
					background3DObject2.Mirrored = false;
					background3DObject2.OriginalPositionZ = CameraPosition.Z - 200f;
					background3DObject2.Angle3DX = -3.1415927f;
					background3DObject2.TransparentValueF = 0f;
					background3DObject2.MaxTransparent = 250;
					background3DObject2.TransparentVelocity = 2.5f;
					background3DObject2.Velocity = 0f;
					Background3DObject bo = new Background3DObject(StageData, "Cloud01", new PointF(0f, (float)(PosY2 + 250)), false)
					{
						Scale = 3f,
						Mirrored = true,
						OriginalPositionZ = CameraPosition.Z - 200f + 20f,
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
							bo.TransparentVelocity = -0.62831855f * (float)Math.Sin((double)((float)(Time - 100) / 300f) * 3.141592653589793 * 2.0);
						}
					};
					PosY2 += 1314;
				}
			}
			else if (TimeMain > 800)
			{
				if (TimeMain < 1000)
				{
					CameraPosition.Z = CameraPosition.Z + 1f;
				}
				if (TimeMain < 8000)
				{
					if (Time % 10 == 0)
					{
						PointF pointF = new PointF((float)Ran.Next(-400, 400), CameraPos.Y + (float)Ran.Next(100, 500));
						Background3DObject background3DObject3 = new Background3DObject(StageData, "591-1", pointF, false);
						background3DObject3.Scale = (float)Ran.Next(80, 100) / 100f;
						background3DObject3.ScaleVelocity = -0.004f;
						background3DObject3.Velocity = (float)Ran.Next(-10, 10) / 20f;
						background3DObject3.DirectionDegree = (double)Ran.Next(360);
						background3DObject3.VelocityZ = (float)Ran.Next(6, 15) / 20f;
						background3DObject3.LifeTime = 200;
						background3DObject3.Active = true;
						background3DObject3.Angle3DX = -1.5707964f + StageData.Background3D.Envi.CameraAngle;
						background3DObject3.TransparentValueF = 0f;
						background3DObject3.TransparentVelocity = 10f;
						background3DObject3.OriginalPositionZ = CameraPosition.Z - 200f;
					}
				}
				if (8000 < TimeMain && TimeMain < 8200)
				{
					CameraTarget.Z = CameraTarget.Z + 1.5f;
				}
			}
			if (TimeMain == 8200)
			{
				BaseParticle3D baseParticle3D = new BaseParticle3D(StageData, "夜空（暗）", new PointF((float)(BoundRect.Width / 2), (float)(BoundRect.Height / 2)), 0f, 1.5707963267948966);
				baseParticle3D.TransparentValueF = 0f;
				baseParticle3D.TransparentVelocity = 3f;
				Background3D.BackgroundParticleList.Add(baseParticle3D);
			}
			if (TimeMain == 8401)
			{
				if (Background3D.BackgroundParticleList.Count == 0)
				{
					BaseParticle3D baseParticle3D = new BaseParticle3D(StageData, "夜空（暗）", new PointF((float)(BoundRect.Width / 2), (float)(BoundRect.Height / 2)), 0f, 1.5707963267948966);
					Background3D.BackgroundParticleList.Add(baseParticle3D);
				}
			}
			for (int i = Background3D.BackgroundList.Count - 1; i >= 0; i--)
			{
				if (Background3D.BackgroundList[i].OriginalPosition.Y < CameraPosition.Y - 1000f)
				{
					Background3D.BackgroundList.RemoveAt(i);
				}
			}
			light.Diffuse = LightColor;
			light.Ambient = LightColor;
			light.Direction = new Vector3(0f, 0f, -1f);
		}

		public void SetRenderPara()
		{
			DeviceMain.Clear(ClearFlags.ZBuffer | ClearFlags.Target, FogColor, 1f, 0);
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			SpriteMain.End();
			DeviceMain.SetRenderState(RenderState.ZEnable, TimeMain < 800);
			DeviceMain.SetRenderState(RenderState.CullMode, true);
			DeviceMain.SetRenderState(RenderState.AlphaBlendEnable, true);
			DeviceMain.SetRenderState<Blend>(RenderState.SourceBlend, Blend.SourceAlpha);
			DeviceMain.SetRenderState<Blend>(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
			DeviceMain.SetTextureStageState(0, TextureStage.AlphaArg1, TextureArgument.Texture);
			DeviceMain.SetTextureStageState(0, TextureStage.AlphaOperation, TextureOperation.Modulate);
			DeviceMain.SetTextureStageState(0, TextureStage.AlphaArg2, TextureArgument.Diffuse);
			DeviceMain.SetTransform(TransformState.View, Matrix.LookAtLH(CameraPosition, CameraTarget, CameraDirection));
			DeviceMain.SetTransform(TransformState.Projection, Matrix.PerspectiveFovLH(0.7853982f, 1f, 1f, FogEnd + 200f));
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

		private int PosY = -1000;

		private int PosY2 = -1000;

		private int PosY3 = -1000;
	}
}
