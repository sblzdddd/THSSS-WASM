using System;
using System.Collections.Generic;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x02000106 RID: 262
	public class ParticleManager3D
	{
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x000412F4 File Offset: 0x0003F4F4
		// (set) Token: 0x0600072D RID: 1837 RVA: 0x0004130B File Offset: 0x0003F50B
		public List<BaseParticle3D> ParticleList { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x00041314 File Offset: 0x0003F514
		// (set) Token: 0x0600072F RID: 1839 RVA: 0x0004132B File Offset: 0x0003F52B
		public VertexDeclaration vertexDecl { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x00041334 File Offset: 0x0003F534
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x0004134B File Offset: 0x0003F54B
		public StageDataPackage StageData { get; private set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x00041354 File Offset: 0x0003F554
		public Device DeviceMain
		{
			get
			{
				return this.StageData.DeviceMain;
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00041371 File Offset: 0x0003F571
		public ParticleManager3D(StageDataPackage StageData)
		{
			this.ParticleList = new List<BaseParticle3D>();
			this.StageData = StageData;
			this.vertexDecl = new VertexDeclaration(this.DeviceMain, TexturedVertex.VertexElements);
			this.SetupLight();
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x000413B0 File Offset: 0x0003F5B0
		private void SetupLight()
		{
			this.light = default(Light);
			this.light.Type = LightType.Directional;
			this.light.Diffuse = Color.White;
			this.light.Ambient = Color.White;
			this.light.Direction = new Vector3(0f, 0f, -1f);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00041460 File Offset: 0x0003F660
		public void Ctrl()
		{
			this.ParticleList.ForEach(delegate(BaseParticle3D x)
			{
				if (x.OutBoundary())
				{
					x.Dispose();
					this.ParticleList.Remove(x);
				}
			});
			this.ParticleList.ForEach(delegate(BaseParticle3D x)
			{
				x.Ctrl();
			});
			this.ParticleList.Sort(new CompareDepth());
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x000414C4 File Offset: 0x0003F6C4
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

		// Token: 0x06000737 RID: 1847 RVA: 0x000416BC File Offset: 0x0003F8BC
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
			this.ParticleList.ForEach(delegate(BaseParticle3D x)
			{
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
			});
			this.StageData.ScreenTexMan.End();
			this.DeviceMain.Viewport = viewport;
			this.StageData.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			this.DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			this.DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			this.StageData.SpriteMain.Draw2D(this.StageData.ScreenTexMan.RenderTexture, new Point(0, 0), 0f, new PointF(0f, 0f), Color.White);
			this.StageData.SpriteMain.End();
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0004184C File Offset: 0x0003FA4C
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

		// Token: 0x06000739 RID: 1849 RVA: 0x000418A0 File Offset: 0x0003FAA0
		public void Clear()
		{
			this.ParticleList.Clear();
		}

		// Token: 0x04000278 RID: 632
		private Light light;
	}
}
