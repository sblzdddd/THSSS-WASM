using System.Drawing;
using System.IO;

namespace Shooting
{
	internal class MenuGroup_ReplayLoader : MenuGroup_ReplaySaver
	{
		public MenuGroup_ReplayLoader(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, OriginalPosition)
		{
			base.TransparentValueF = 0f;
			base.TransparentVelocity = 5f;
			base.TxtureObject = base.TextureObjectDictionary["MenuBackground"];
			base.Scale = 0.625f;
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2));
			base.AngleDegree = 90.0;
			base.ColorValue = Color.SkyBlue;
		}

		public override string SetDescroption(string fileName)
		{
			if (File.Exists(fileName))
			{
				try
				{
					ReplayInfo replayInfo = Replay.ReadTitle(fileName);
					if (replayInfo.Version != "ver 1.00")
					{
						return "------- -------- ----- ------- ------- --- ----";
					}
					string text = null;
					if (replayInfo != null)
					{
						text += ((replayInfo.PlayerName == null) ? "        " : replayInfo.PlayerName.PadRight(8));
						text += ((replayInfo.Date == null) ? "        " : replayInfo.Date.PadRight(9));
						text += ((replayInfo.Time == null) ? "        " : replayInfo.Time.PadRight(6));
						text += ((replayInfo.MyPlaneName == null) ? "               " : replayInfo.MyPlaneName.PadRight(8));
						text += replayInfo.Rank.ToString().PadRight(8);
						text += ((replayInfo.LastStage == null) ? "   " : replayInfo.LastStage.PadRight(4));
						text += ((replayInfo.SlowRate == null) ? "   " : replayInfo.SlowRate.PadRight(4));
					}
					return text;
				}
				catch
				{
					return "------- -------- ----- ------- ------- --- ----";
				}
			}
			return "------- -------- ----- ------- ------- --- ----";
		}

		public override void ProcessKeys2()
		{
			if (base.KClass.Key_Z && this.LastZ == 0)
			{
				base.MenuItemList[base.MenuSelectIndex].Click();
				this.OnChangeMenu = base.TimeMain + 1;
			}
			if ((base.KClass.Key_X || base.KClass.Key_ESC) && this.LastX == 0)
			{
				this.OnRemoveMenu = base.TimeMain + 20;
				base.TransparentVelocity = -15f;
				foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
				{
					baseMenuItem.DestPoint = new PointF(base.MenuItemList[0].OriginalPosition.X, base.MenuItemList[0].OriginalPosition.Y);
					baseMenuItem.OnRemove = true;
				}
				base.StageData.SoundPlay("se_cancel00.wav");
			}
		}

		public override void ProcessZ()
		{
			string description = ((DescriptionMenuItem)base.MenuItemList[base.MenuSelectIndex]).Description;
			if (description != "------- -------- ----- ------- ------- --- ----")
			{
				base.StageData.SoundPlay("se_ok00.wav");
				base.StageData.MenuGroupList.Add(new MenuGroup_ReplayStageSelect(base.StageData, new PointF(200f, 150f), (DescriptionMenuItem)base.MenuItemList[base.MenuSelectIndex]));
			}
		}

		private const string NullDescription = "------- -------- ----- ------- ------- --- ----";
	}
}
