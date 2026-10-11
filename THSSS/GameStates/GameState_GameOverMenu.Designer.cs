using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000EA RID: 234
	internal class GameState_GameOverMenu : GameState_PauseMenu
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x000381F6 File Offset: 0x000363F6
		public GameState_GameOverMenu(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00038204 File Offset: 0x00036404
		public override void Init()
		{
			base.StageData.MenuGroupList.Add(new MenuGroup_GameOver(base.StageData)
			{
				OriginalPosition = new PointF(36f, 16f)
			});
			StageDataPackage stageData = base.StageData.GlobalData.LastState.StageData;
			if (!stageData.OnReplay && !stageData.OnPractice)
			{
				if (stageData.MyPlane.Score > stageData.S_History[9].Score)
				{
					base.StageData.MenuGroupList.Add(new MenuGroup_ScoreSaver(base.StageData, new PointF(36f, 16f)));
				}
			}
			base.KClass.Hex2Key(0);
		}
	}
}
