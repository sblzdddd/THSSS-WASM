using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class EnvironmentSSSEx : BaseObject, IEnvironment
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

		public EnvironmentSSSEx(StageDataPackage StageData)
		{
			this.StageData = StageData;
			DeviceMain = StageData.DeviceMain;
			Velocity = 1f;
			CameraPosition = new Vector3(0f, 0f, 250f);
			CameraTarget = new Vector3(0f, 200f, 170f);
			CameraDirection = new Vector3(0f, 0f, 1f);
			FogColor = Color.FromArgb(80, 165, 255);
			FogStart = 0f;
			FogEnd = 200f;
			LightColor = Color.White;
			light = default(Light);
			light.Type = LightType.Directional;
		}

		public override void Ctrl()
		{
			Time++;
			Velocity += Accelerate;
			if (Velocity < 0.5f)
			{
				Velocity = 0.5f;
			}
			while ((float)PosY3 < CameraPosition.Y + 4200f)
			{
				Background3DObject background3DObject = new Background3DObject(StageData, "Tree2", new PointF(300f, (float)PosY3), true);
				background3DObject.Angle3DX = -1.5707964f + CameraAngle;
				background3DObject.Height = 233f * (float)Math.Cos((double)CameraAngle);
				Background3DObject background3DObject2 = new Background3DObject(StageData, "Tree2", new PointF(-300f, (float)PosY3), true);
				background3DObject2.Angle3DX = -1.5707964f + CameraAngle;
				background3DObject2.Height = 233f * (float)Math.Cos((double)CameraAngle);
				background3DObject2.Mirrored = true;
				Background3DObject background3DObject3 = new Background3DObject(StageData, "Tree1", new PointF(300f, (float)(PosY3 + 200)), true);
				background3DObject3.Angle3DX = -1.5707964f + CameraAngle;
				background3DObject3.Height = 225f * (float)Math.Cos((double)CameraAngle);
				background3DObject3.Mirrored = true;
				Background3DObject background3DObject4 = new Background3DObject(StageData, "Tree1", new PointF(-300f, (float)(PosY3 + 200)), true);
				background3DObject4.Angle3DX = -1.5707964f + CameraAngle;
				background3DObject4.Height = 225f * (float)Math.Cos((double)CameraAngle);
				PosY3 += 400;
			}
			while ((float)PosY2 < CameraPosition.Y + 1950f)
			{
				Background3DObject background3DObject5 = new Background3DObject(StageData, "草地ex", new PointF(0f, (float)PosY2), true);
				background3DObject5.Angle3DX = -3.1415927f;
				background3DObject5.Scale = 2f;
				PosY2 += 486;
			}
			for (int i = Background3D.BackgroundList.Count - 1; i >= 0; i--)
			{
				if (Background3D.BackgroundList[i].OriginalPosition.Y < CameraPosition.Y - 1000f)
				{
					Background3D.BackgroundList.RemoveAt(i);
				}
			}
			CameraPosition.Y = CameraPosition.Y + Velocity;
			CameraTarget.Y = CameraTarget.Y + Velocity;
			CameraPosition.X = 40f * (float)Math.Sin((double)((float)Time / 360f));
			CameraTarget.X = 40f * (float)Math.Sin((double)((float)(Time - 60) / 360f));
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
			light.Diffuse = LightColor;
			light.Ambient = LightColor;
			light.Direction = new Vector3(0f, 0f, -1f);
		}

		public void SetRenderPara()
		{
			DeviceMain.Clear(ClearFlags.Target, FogColor, 0f, 0);
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			SpriteMain.Draw2D(StageData.TextureObjectDictionary["天色"], 1f, 0f, new PointF((float)(BoundRect.Left + 204), (float)(BoundRect.Top + 240)), Color.White);
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
			DeviceMain.SetTransform(TransformState.Projection, Matrix.PerspectiveFovLH(1.5707964f, 1f, 1f, 1000f));
			DeviceMain.SetRenderState(RenderState.FogEnable, true);
			DeviceMain.SetRenderState<FogMode>(RenderState.FogTableMode, FogMode.Linear);
			DeviceMain.SetRenderState(RenderState.FogColor, FogColor.ToArgb());
			DeviceMain.SetRenderState(RenderState.FogStart, FogStart);
			DeviceMain.SetRenderState(RenderState.FogEnd, FogEnd);
			DeviceMain.SetLight(0, light);
			DeviceMain.SetRenderState(RenderState.Lighting, true);
			DeviceMain.EnableLight(0, true);
		}

		private int PosY2 = -1000;

		private int PosY3 = -1000;

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
