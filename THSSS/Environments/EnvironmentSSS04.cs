using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class EnvironmentSSS04 : BaseObject, IEnvironment
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

		public EnvironmentSSS04(StageDataPackage StageData)
		{
			this.StageData = StageData;
			DeviceMain = StageData.DeviceMain;
			Velocity = 1f;
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
			if (TimeMain < 400)
			{
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
				while ((float)PosY3 < CameraPosition.Y + 4200f)
				{
					Background3DObject background3DObject = new Background3DObject(StageData, "柳树", new PointF(300f, (float)PosY3), true);
					background3DObject.Angle3DX = -1.5707964f;
					background3DObject.Height = 400f;
					Background3DObject background3DObject2 = new Background3DObject(StageData, "柳树", new PointF(-300f, (float)PosY3), true);
					background3DObject2.Angle3DX = -1.5707964f;
					background3DObject2.Height = 400f;
					background3DObject2.Mirrored = true;
					Background3DObject background3DObject3 = new Background3DObject(StageData, "柳树", new PointF(300f, (float)(PosY3 + 200)), true);
					background3DObject3.Angle3DX = -1.5707964f;
					background3DObject3.Height = 400f;
					background3DObject3.Mirrored = true;
					Background3DObject background3DObject4 = new Background3DObject(StageData, "柳树", new PointF(-300f, (float)(PosY3 + 200)), true);
					background3DObject4.Angle3DX = -1.5707964f;
					background3DObject4.Height = 400f;
					PosY3 += 400;
				}
				while ((float)PosY < CameraPosition.Y + 3072f)
				{
					Background3DObject background3DObject5 = new Background3DObject(StageData, "地砖", new PointF(0f, (float)PosY), true);
					background3DObject5.Angle3DX = -3.1415927f;
					background3DObject5.Scale = 1f;
					PosY += 256;
				}
				while ((float)PosY2 < CameraPosition.Y + 1950f)
				{
					Background3DObject background3DObject6 = new Background3DObject(StageData, "草地", new PointF(0f, (float)PosY2), true);
					background3DObject6.Angle3DX = -3.1415927f;
					background3DObject6.Scale = 5f;
					PosY2 += 1625;
				}
				for (int i = Background3D.BackgroundList.Count - 1; i >= 0; i--)
				{
					if (Background3D.BackgroundList[i].OriginalPosition.Y < CameraPosition.Y - 1000f)
					{
						Background3D.BackgroundList.RemoveAt(i);
					}
				}
			}
			else if (TimeMain > 4000)
			{
				if (CameraTarget.Y < 500f)
				{
					CameraTarget.Y = CameraTarget.Y + 1f;
				}
			}
			if (TimeMain == 150)
			{
				BackgroundTransitionOut backgroundTransitionOut = new BackgroundTransitionOut(StageData, 150);
				backgroundTransitionOut.LifeTime = 400;
			}
			else if (TimeMain == 550)
			{
				BackgroundTransitionIn backgroundTransitionIn = new BackgroundTransitionIn(StageData);
				backgroundTransitionIn.Delay = 0;
				backgroundTransitionIn.LifeTime = 300;
				CameraPosition = new Vector3(0f, 0f, 250f);
				CameraTarget = new Vector3(0f, 0f, 0f);
				CameraDirection = new Vector3(0f, 1f, 1f);
				Background3D.BackgroundList.Clear();
				Background3DObject background3DObject7 = new Background3DObject(StageData, "万花镜素材", new PointF(0f, 0f), false);
				background3DObject7.AngularVelocityDegree = 0.1f;
				Background3DObject background3DObject8 = new Background3DObject(StageData, "黑暗滤镜", new PointF(0f, 0f), false)
				{
					Height = 10f,
					Scale = 1f
				};
				background3DObject8.TransparentVelocityDictionary.Add(3350, -5f);
				FogStart = 0f;
				FogEnd = 2000000f;
			}
			else if (TimeMain == 4300)
			{
				BaseParticle3D bk2 = new BaseParticle3D(StageData, "极光素材", new PointF((float)(BoundRect.Width / 2), 125f), 0f, 0.0)
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
				Background3D.BackgroundParticleList.Add(bk2);
			}
			if (TimeMain == 8701)
			{
				if (Background3D.BackgroundParticleList.Count == 0)
				{
					CameraPosition = new Vector3(0f, 0f, 250f);
					CameraTarget = new Vector3(0f, 500f, 0f);
					CameraDirection = new Vector3(0f, 1f, 1f);
					Background3DObject background3DObject9 = new Background3DObject(StageData, "万花镜素材", new PointF(0f, 0f), false);
					background3DObject9.AngularVelocityDegree = 0.1f;
					FogStart = 0f;
					FogEnd = 2000000f;
					BaseParticle3D bk = new BaseParticle3D(StageData, "极光素材", new PointF((float)(BoundRect.Width / 2), 125f), 0f, 0.0)
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
					Background3D.BackgroundParticleList.Add(bk);
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
			if (TimeMain < 400)
			{
				SpriteMain.Draw2D(StageData.TextureObjectDictionary["BG01a"], 1f, 0f, new PointF((float)(BoundRect.Left + 256), (float)(BoundRect.Top + 256)), Color.White);
			}
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

		private int PosY = -1000;

		private int PosY2 = -1000;

		private int PosY3 = -1000;
	}
}
