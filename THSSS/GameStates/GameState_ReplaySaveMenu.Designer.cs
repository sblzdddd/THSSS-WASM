using System;
using System.Drawing;
using SlimDX.Direct3D9;

namespace Shooting
{
	internal class GameState_ReplaySaveMenu : BaseMenu, IGameState
	{
		public GameState_ReplaySaveMenu(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.BackgroundLight = 255;
		}

		public GameState_ReplaySaveMenu()
		{
		}

		public override void Init()
		{
			base.StageData.MenuGroupList.Add(new MenuGroup_ReplaySaver(base.StageData, new PointF(128f, 16f)));
			base.StageData.MenuGroupList.Add(new MenuGroup_ScoreSaver(base.StageData, new PointF(36f, 16f)));
			base.KClass.Hex2Key(0);
		}

		public override void Render()
		{
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
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
	}
}
