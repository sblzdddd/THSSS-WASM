using System;
using System.Collections.Generic;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class BackgroundManager3D
	{
		public bool Enabled3D { get; set; }

		public bool WarpEnabled { get; set; }

		public int WarpColorKey { get; set; }

		public float WarpRadius { get; set; }

		public List<BaseObject> BackgroundList { get; set; }

		public List<BaseParticle3D> BackgroundParticleList { get; set; }

		private VertexDeclaration vertexDecl { get; set; }

		public StageDataPackage StageData { get; private set; }

		public Device DeviceMain
		{
			get
			{
				return this.StageData.DeviceMain;
			}
		}

		public IEnvironment Envi { get; set; }

		public BackgroundManager3D(StageDataPackage StageData)
		{
			this.BackgroundList = new List<BaseObject>();
			this.BackgroundParticleList = new List<BaseParticle3D>();
			this.StageData = StageData;
			this.Enabled3D = true;
			this.vertexDecl = new VertexDeclaration(this.DeviceMain, TexturedVertex.VertexElements);
			this.SetupLight();
		}

		private void SetupLight()
		{
			this.light = default(Light);
			this.light.Type = LightType.Directional;
			this.light.Diffuse = Color.White;
			this.light.Ambient = Color.White;
			this.light.Direction = new Vector3(0f, 0f, -1f);
		}

		public void Ctrl()
		{
			if (this.Envi != null)
			{
				this.Envi.Ctrl();
			}
			this.BackgroundList.ForEach(delegate(BaseObject x)
			{
				if (x.OutBoundary())
				{
					x.Dispose();
					this.BackgroundList.Remove(x);
				}
			});
			this.BackgroundList.ForEach(delegate(BaseObject x)
			{
				x.Ctrl();
			});
			this.BackgroundParticleList.ForEach(delegate(BaseParticle3D x)
			{
				if (x.OutBoundary())
				{
					x.Dispose();
					this.BackgroundParticleList.Remove(x);
				}
			});
			this.BackgroundParticleList.ForEach(delegate(BaseParticle3D x)
			{
				x.Ctrl();
			});
			this.BackgroundParticleList.Sort(new CompareDepth());
			if (this.WarpEnabled)
			{
				if (this.WarpRadius < 128f)
				{
					this.WarpRadius += 4f;
				}
			}
			else if (this.WarpRadius > 0f)
			{
				this.WarpRadius -= 4f;
			}
		}

		private void SetParticleRenderPara()
		{
			this.DeviceMain.SetTransform(TransformState.View, Matrix.LookAtLH(new Vector3(0f, 0f, (float)(this.DeviceMain.Viewport.Height / 2)), new Vector3(0f, 0f, 0f), new Vector3(0f, -1f, 0f)));
			this.DeviceMain.SetTransform(TransformState.Projection, Matrix.PerspectiveFovLH(1.5707964f, (float)this.DeviceMain.Viewport.Width / (float)this.DeviceMain.Viewport.Height, 1f, 1000f));
			this.DeviceMain.SetRenderState(RenderState.ZEnable, false);
			this.DeviceMain.SetRenderState(RenderState.CullMode, true);
			this.DeviceMain.SetRenderState(RenderState.AlphaBlendEnable, true);
			this.DeviceMain.SetRenderState<Blend>(RenderState.SourceBlend, Blend.SourceAlpha);
			this.DeviceMain.SetRenderState(RenderState.FogEnable, false);
			this.DeviceMain.SetTextureStageState(0, TextureStage.AlphaArg1, TextureArgument.Texture);
			this.DeviceMain.SetTextureStageState(0, TextureStage.AlphaOperation, TextureOperation.Modulate);
			this.DeviceMain.SetTextureStageState(0, TextureStage.AlphaArg2, TextureArgument.Diffuse);
			this.DeviceMain.SetLight(0, this.light);
			this.DeviceMain.SetRenderState(RenderState.Lighting, true);
			this.DeviceMain.EnableLight(0, true);
			this.DeviceMain.VertexDeclaration = this.vertexDecl;
			this.DeviceMain.VertexFormat = TexturedVertex.Format;
		}

		public void Show()
		{
			this.StageData.ScreenTexMan.Begin();
			Viewport viewport = this.StageData.DeviceMain.Viewport;
			if (!this.StageData.BoundRect.Equals(new Rectangle(0, 0, 640, 480)))
			{
				this.DeviceMain.Viewport = new Viewport
				{
					X = this.StageData.BoundRect.Left - 10,
					Y = this.StageData.BoundRect.Top - 10,
					Width = this.StageData.BoundRect.Width + 20,
					Height = this.StageData.BoundRect.Height + 20,
					MinZ = 0f,
					MaxZ = 1f
				};
			}
			if (this.Envi != null)
			{
				this.Envi.SetRenderPara();
			}
			if (this.Enabled3D)
			{
				this.BackgroundList.ForEach(delegate(BaseObject x)
				{
					x.Show();
				});
			}
			this.SetParticleRenderPara();
			string ModelName = null;
			this.BackgroundParticleList.ForEach(delegate(BaseParticle3D x)
			{
				if (x.model != null)
				{
					if (ModelName != x.model.Name)
					{
						x.model.SetModel();
					}
					x.Show();
					ModelName = x.model.Name;
				}
			});
			this.DeviceMain.Viewport = viewport;
			this.StageData.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			this.StageData.Background.Show(false);
			this.StageData.SpriteMain.End();
			this.StageData.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			this.DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			this.DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			this.StageData.Background.Show(true);
			this.StageData.SpriteMain.End();
			this.StageData.ScreenTexMan.End();
			if (this.StageData.Boss != null)
			{
				this.StageData.SpriteMain.Begin(SpriteFlags.AlphaBlend);
				this.DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
				this.DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
				this.StageData.EffectMain = this.StageData.EffectDictionary["Warp"];
				this.StageData.EffectMain.Technique = "technique1";
				this.StageData.EffectMain.Begin();
				this.StageData.EffectMain.SetValue<float>("X", this.StageData.Boss.Position.X / 640f);
				this.StageData.EffectMain.SetValue<float>("Y", this.StageData.Boss.Position.Y / 480f);
				this.StageData.EffectMain.SetValue<float>("Radius", this.WarpRadius / 640f);
				this.StageData.EffectMain.SetValue<float>("Time", (float)this.StageData.TimeMain / 20f);
				this.StageData.EffectMain.SetValue<int>("ColorKey", this.WarpColorKey);
				this.StageData.EffectMain.BeginPass(0);
				this.StageData.SpriteMain.Draw2D(this.StageData.ScreenTexMan.RenderTexture, new Point(0, 0), 0f, new PointF(0f, 0f), Color.White);
				this.StageData.EffectMain.EndPass();
				this.StageData.SpriteMain.End();
				this.StageData.EffectMain.End();
			}
			else
			{
				this.StageData.SpriteMain.Begin(SpriteFlags.AlphaBlend);
				this.DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
				this.DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
				this.StageData.SpriteMain.Draw2D(this.StageData.ScreenTexMan.RenderTexture, new Point(0, 0), 0f, new PointF(0f, 0f), Color.White);
				this.StageData.SpriteMain.End();
			}
		}

		public void Dispose()
		{
			this.BackgroundList.ForEach(delegate(BaseObject x)
			{
				x.Dispose();
			});
			this.BackgroundParticleList.ForEach(delegate(BaseParticle3D x)
			{
				x.Dispose();
			});
			if (this.vertexDecl != null)
			{
				this.vertexDecl.Dispose();
			}
		}

		public void Clear()
		{
			this.BackgroundList.Clear();
			this.BackgroundParticleList.Clear();
		}

		private Light light;
	}
}
