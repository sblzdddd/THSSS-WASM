using System;
using System.Drawing;
using SlimDX.Direct3D9;

namespace Shooting
{
	internal class GameState_PauseMenu : BaseMenu, IGameState
	{
		public GameState_PauseMenu(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.BackgroundLight = 255;
		}

		public GameState_PauseMenu()
		{
		}

		public override void Init()
		{
			base.StageData.MenuGroupList.Add(new MenuGroup_Pause(base.StageData));
			base.KClass.Hex2Key(0);
		}

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

		public override void BGM_Pause()
		{
		}

		public override void BGM_Resume()
		{
		}

		private float blurLevel;
	}
}
