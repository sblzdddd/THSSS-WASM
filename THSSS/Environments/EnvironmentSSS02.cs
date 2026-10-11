using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class EnvironmentSSS02 : BaseObject, IEnvironment
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

		public EnvironmentSSS02(StageDataPackage StageData)
		{
			base.StageData = StageData;
			this.DeviceMain = StageData.DeviceMain;
			base.Velocity = 1f;
			this.CameraPosition = new Vector3(0f, 0f, 70f);
			this.CameraTarget = new Vector3(0f, 200f, 70f);
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
			base.Velocity += base.Accelerate;
			if (base.Velocity < 0.5f)
			{
				base.Velocity = 0.5f;
			}
			while ((float)this.PosY3 < this.CameraPosition.Y + 1600f)
			{
				Background3DObject background3DObject = new Background3DObject(base.StageData, "zhu", new PointF(200f, (float)this.PosY3), true);
				background3DObject.Angle3DX = -1.5707964f;
				background3DObject.Height = 256f;
				Background3DObject background3DObject2 = new Background3DObject(base.StageData, "zhu", new PointF(-200f, (float)this.PosY3), true);
				background3DObject2.Angle3DX = -1.5707964f;
				background3DObject2.Height = 256f;
				background3DObject2.Mirrored = true;
				Background3DObject background3DObject3 = new Background3DObject(base.StageData, "zhu", new PointF(200f, (float)(this.PosY3 + 100)), true);
				background3DObject3.Angle3DX = -1.5707964f;
				background3DObject3.Height = 256f;
				background3DObject3.Mirrored = true;
				Background3DObject background3DObject4 = new Background3DObject(base.StageData, "zhu", new PointF(-200f, (float)(this.PosY3 + 100)), true);
				background3DObject4.Angle3DX = -1.5707964f;
				background3DObject4.Height = 256f;
				this.PosY3 += 200;
			}
			while ((float)this.PosY2 < this.CameraPosition.Y + 1500f)
			{
				Background3DObject background3DObject5 = new Background3DObject(base.StageData, "BG02d", new PointF(0f, (float)this.PosY2), true);
				background3DObject5.Angle3DX = -3.1415927f;
				this.PosY2 += 310;
			}
			while ((float)this.PosY < this.CameraPosition.Y + 1500f)
			{
				Background3DObject background3DObject6 = new Background3DObject(base.StageData, "liba", new PointF(150f, (float)this.PosY), false);
				background3DObject6.Angle = 1.5707963705062866;
				background3DObject6.Angle3DY = -1.5707964f;
				background3DObject6.Height = 45f;
				Background3DObject background3DObject7 = new Background3DObject(base.StageData, "liba", new PointF(-150f, (float)this.PosY), false);
				background3DObject7.Angle = -1.5707963705062866;
				background3DObject7.Angle3DY = 1.5707964f;
				background3DObject7.Height = 45f;
				Background3DObject background3DObject8 = new Background3DObject(base.StageData, "591-1R", new PointF(150f, (float)this.PosY), false);
				background3DObject8.Scale = 1.5f;
				background3DObject8.Active = true;
				background3DObject8.Angle3DX = -1.5707964f + base.StageData.Background3D.Envi.CameraAngle;
				background3DObject8.TransparentValueF = 0f;
				background3DObject8.TransparentVelocity = 10f;
				background3DObject8.Height = 60f;
				Background3DObject background3DObject9 = new Background3DObject(base.StageData, "591-1R", new PointF(-150f, (float)this.PosY), false);
				background3DObject9.Scale = 1.5f;
				background3DObject9.Active = true;
				background3DObject9.Angle3DX = -1.5707964f + base.StageData.Background3D.Envi.CameraAngle;
				background3DObject9.TransparentValueF = 0f;
				background3DObject9.TransparentVelocity = 10f;
				background3DObject9.Height = 60f;
				this.PosY += 133;
			}
			for (int i = base.Background3D.BackgroundList.Count - 1; i >= 0; i--)
			{
				if (base.Background3D.BackgroundList[i].OriginalPosition.Y < this.CameraPosition.Y - 310f)
				{
					base.Background3D.BackgroundList.RemoveAt(i);
				}
			}
			this.CameraPosition.Y = this.CameraPosition.Y + base.Velocity;
			this.CameraTarget.Y = this.CameraTarget.Y + base.Velocity;
			this.CameraPosition.X = 40f * (float)Math.Sin((double)((float)base.Time / 360f));
			this.CameraTarget.X = 40f * (float)Math.Sin((double)((float)(base.Time - 60) / 360f));
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

		private int PosY = -1000;

		private int PosY2 = -1000;

		private int PosY3 = -1000;
	}
}
