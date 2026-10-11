using System;
using System.Drawing;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x02000060 RID: 96
	internal class GameState_ReplaySaveMenu : BaseMenu, IGameState
	{
		// Token: 0x06000335 RID: 821 RVA: 0x00016193 File Offset: 0x00014393
		public GameState_ReplaySaveMenu(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.BackgroundLight = 255;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000161AB File Offset: 0x000143AB
		public GameState_ReplaySaveMenu()
		{
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000161B8 File Offset: 0x000143B8
		public override void Init()
		{
			base.StageData.MenuGroupList.Add(new MenuGroup_ReplaySaver(base.StageData, new PointF(128f, 16f)));
			base.StageData.MenuGroupList.Add(new MenuGroup_ScoreSaver(base.StageData, new PointF(36f, 16f)));
			base.KClass.Hex2Key(0);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00016234 File Offset: 0x00014434
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

		// Token: 0x06000339 RID: 825 RVA: 0x000162E7 File Offset: 0x000144E7
		public override void BGM_Pause()
		{
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000162EA File Offset: 0x000144EA
		public override void BGM_Resume()
		{
		}
	}
}
