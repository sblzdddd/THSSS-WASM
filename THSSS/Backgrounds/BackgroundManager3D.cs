using System;
using System.Collections.Generic;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x0200013F RID: 319
	public class BackgroundManager3D
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x0004CEC8 File Offset: 0x0004B0C8
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x0004CEDF File Offset: 0x0004B0DF
		public bool Enabled3D { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0004CEE8 File Offset: 0x0004B0E8
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x0004CEFF File Offset: 0x0004B0FF
		public bool WarpEnabled { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x0004CF08 File Offset: 0x0004B108
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x0004CF1F File Offset: 0x0004B11F
		public int WarpColorKey { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x0004CF28 File Offset: 0x0004B128
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x0004CF3F File Offset: 0x0004B13F
		public float WarpRadius { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x0004CF48 File Offset: 0x0004B148
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x0004CF5F File Offset: 0x0004B15F
		public List<BaseObject> BackgroundList { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x0004CF68 File Offset: 0x0004B168
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x0004CF7F File Offset: 0x0004B17F
		public List<BaseParticle3D> BackgroundParticleList { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0004CF88 File Offset: 0x0004B188
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x0004CF9F File Offset: 0x0004B19F
		private VertexDeclaration vertexDecl { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0004CFA8 File Offset: 0x0004B1A8
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x0004CFBF File Offset: 0x0004B1BF
		public StageDataPackage StageData { get; private set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x0004CFC8 File Offset: 0x0004B1C8
		public Device DeviceMain
		{
			get
			{
				return this.StageData.DeviceMain;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x0004CFE8 File Offset: 0x0004B1E8
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x0004CFFF File Offset: 0x0004B1FF
		public IEnvironment Envi { get; set; }

		// Token: 0x06000862 RID: 2146 RVA: 0x0004D008 File Offset: 0x0004B208
		public BackgroundManager3D(StageDataPackage StageData)
		{
			this.BackgroundList = new List<BaseObject>();
			this.BackgroundParticleList = new List<BaseParticle3D>();
			this.StageData = StageData;
			this.Enabled3D = true;
			this.vertexDecl = new VertexDeclaration(this.DeviceMain, TexturedVertex.VertexElements);
			this.SetupLight();
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0004D064 File Offset: 0x0004B264
		private void SetupLight()
		{
			this.light = default(Light);
			this.light.Type = LightType.Directional;
			this.light.Diffuse = Color.White;
			this.light.Ambient = Color.White;
			this.light.Direction = new Vector3(0f, 0f, -1f);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0004D150 File Offset: 0x0004B350
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

		// Token: 0x06000865 RID: 2149 RVA: 0x0004D278 File Offset: 0x0004B478
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

		// Token: 0x06000866 RID: 2150 RVA: 0x0004D474 File Offset: 0x0004B674
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

		// Token: 0x06000867 RID: 2151 RVA: 0x0004D948 File Offset: 0x0004BB48
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

		// Token: 0x06000868 RID: 2152 RVA: 0x0004D9C7 File Offset: 0x0004BBC7
		public void Clear()
		{
			this.BackgroundList.Clear();
			this.BackgroundParticleList.Clear();
		}

		// Token: 0x040002F5 RID: 757
		private Light light;
	}
}
