using System;
using System.Drawing;

namespace Shooting
{
	internal class GameState_GameSetMenu : GameState_PauseMenu
	{
		public GameState_GameSetMenu(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
		}

		public override void Init()
		{
			base.StageData.MenuGroupList.Add(new MenuGroup_GameSet(base.StageData)
			{
				OriginalPosition = new PointF(36f, 16f)
			});
			base.KClass.Hex2Key(0);
		}
	}
}
