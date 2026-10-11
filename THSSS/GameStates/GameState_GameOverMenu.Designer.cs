using System;
using System.Drawing;

namespace Shooting
{
	internal class GameState_GameOverMenu : GameState_PauseMenu
	{
		public GameState_GameOverMenu(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
		}

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
