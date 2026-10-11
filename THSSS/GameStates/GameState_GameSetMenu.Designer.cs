using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000160 RID: 352
	internal class GameState_GameSetMenu : GameState_PauseMenu
	{
		// Token: 0x060008E0 RID: 2272 RVA: 0x00054C44 File Offset: 0x00052E44
		public GameState_GameSetMenu(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00054C50 File Offset: 0x00052E50
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
