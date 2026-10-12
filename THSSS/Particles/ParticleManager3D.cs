using System.Collections.Generic;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class ParticleManager3D
	{
		public List<BaseParticle3D> ParticleList { get; set; }

		public VertexDeclaration vertexDecl { get; set; }

		public StageDataPackage StageData { get; private set; }

		public Device DeviceMain
		{
			get
			{
				return this.StageData.DeviceMain;
			}
		}

		public ParticleManager3D(StageDataPackage StageData)
		{
			this.ParticleList = new List<BaseParticle3D>();
			this.StageData = StageData;
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
			for (int i = this.ParticleList.Count - 1; i >= 0; i--)
			{
				BaseParticle3D particle = this.ParticleList[i];
				if (particle.OutBoundary())
				{
					particle.Dispose();
					this.ParticleList.RemoveAt(i);
					continue;
				}
				particle.Ctrl();
			}
			this.ParticleList.Sort(depthComparer);
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
			this.DeviceMain.Viewport = new Viewport
			{
				X = this.StageData.BoundRect.Left - 10,
				Y = this.StageData.BoundRect.Top - 10,
				Width = this.StageData.BoundRect.Width + 20,
				Height = this.StageData.BoundRect.Height + 20
			};
			this.SetParticleRenderPara();
			string ModelName = null;
			for (int i = 0; i < this.ParticleList.Count; i++)
			{
				BaseParticle3D x = this.ParticleList[i];
				if (x.model != null)
				{
					if (ModelName != x.model.Name)
					{
						x.model.SetModel();
					}
					ModelName = x.model.Name;
				}
				else
				{
					ModelName = null;
				}
				x.Show();
			}
			this.StageData.ScreenTexMan.End();
			this.DeviceMain.Viewport = viewport;
			this.StageData.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			this.DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			this.DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			this.StageData.SpriteMain.Draw2D(this.StageData.ScreenTexMan.RenderTexture, new Point(0, 0), 0f, new PointF(0f, 0f), Color.White);
			this.StageData.SpriteMain.End();
		}

		public void Dispose()
		{
			this.ParticleList.ForEach(delegate(BaseParticle3D x)
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
			this.ParticleList.Clear();
		}

		private Light light;

		private static readonly CompareDepth depthComparer = new CompareDepth();
	}
}
