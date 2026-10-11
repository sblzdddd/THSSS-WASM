using System;
using System.Drawing;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x020000E9 RID: 233
	internal class GameState_PauseMenu : BaseMenu, IGameState
	{
		// Token: 0x06000651 RID: 1617 RVA: 0x00037EDD File Offset: 0x000360DD
		public GameState_PauseMenu(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.BackgroundLight = 255;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00037EF5 File Offset: 0x000360F5
		public GameState_PauseMenu()
		{
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00037F00 File Offset: 0x00036100
		public override void Init()
		{
			base.StageData.MenuGroupList.Add(new MenuGroup_Pause(base.StageData));
			base.KClass.Hex2Key(0);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00037F2C File Offset: 0x0003612C
		public override void UpdateData()
		{
			base.UpdateData();
			if (base.TimeMain < 50)
			{
				this.blurLevel = (float)base.TimeMain * 25f / 500000f;
			}
			else
			{
				this.blurLevel = 0.0025f;
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00037F90 File Offset: 0x00036190
		public override void Render()
		{
			base.GlobalData.ScreenTexMan.Begin();
			base.GlobalData.LastState.Render();
			base.GlobalData.ScreenTexMan.End();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.GlobalData.EffectMain = base.GlobalData.EffectDictionary["Blur"];
			base.GlobalData.EffectMain.Technique = "technique1";
			base.GlobalData.EffectMain.Begin();
			base.GlobalData.EffectMain.SetValue<float>("BlurLevel", this.blurLevel);
			base.GlobalData.EffectMain.BeginPass(0);
			base.GlobalData.SpriteMain.Draw2D(base.GlobalData.ScreenTexMan.RenderTexture, new Point(0, 0), 0f, new PointF(0f, 0f), Color.White);
			base.SpriteMain.End();
			base.GlobalData.EffectMain.EndPass();
			base.GlobalData.EffectMain.End();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.Background.Show(false);
			if (base.GlobalData.LastState is BaseStage)
			{
				((BaseStage)base.GlobalData.LastState).ShowInterface();
				((BaseStage)base.GlobalData.LastState).InterfaceList.ForEach(delegate(BaseObject x)
				{
					x.Show();
				});
			}
			base.StageData.MenuGroupList[base.StageData.MenuGroupList.Count - 1].Show();
			base.SpriteMain.End();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			base.DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			base.EffectList.ForEach(delegate(BaseEffect x)
			{
				x.Show();
			});
			base.SpriteMain.End();
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x000381F0 File Offset: 0x000363F0
		public override void BGM_Pause()
		{
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x000381F3 File Offset: 0x000363F3
		public override void BGM_Resume()
		{
		}

		// Token: 0x04000229 RID: 553
		private float blurLevel;
	}
}
