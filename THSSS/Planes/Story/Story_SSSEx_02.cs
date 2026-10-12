using System;

namespace Shooting.Planes.Story
{
	internal class Story_SSSEx_02 : BaseStory_SSS
	{
		public Story_SSSEx_02(StageDataPackage StageData)
			: base(StageData)
		{
			var name = MyPlane.Name;
			if (name != null)
			{
				if (!(name == "Reimu"))
				{
					if (!(name == "Marisa"))
					{
						if (!(name == "Sanae"))
						{
							if (name == "Koishi")
							{
								LoadConversation(".\\Story\\Ex-2D.csv");
							}
						}
						else
						{
							LoadConversation(".\\Story\\Ex-2C.csv");
						}
					}
					else
					{
						LoadConversation(".\\Story\\Ex-2B.csv");
					}
				}
				else
				{
					LoadConversation2(".\\Story\\Ex-2A.csv");
				}
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == Conv.Count)
			{
				SBox.Dispose();
				Story = null;
				new ClearBonus(StageData, 70000000);
				StageData.CurrentStageName = "ExAll";
				StageData.RepInfo.LastStage = StageData.CurrentStageName;
				if (!StageData.OnReplay)
				{
					StageData.C_History = StageData.PData.C_History.Find(new Predicate<ClearHistory>(StageData.FindClear));
					StageData.C_History.ClearTimes++;
					if (StageData.ContinueTimes == 0)
					{
						StageData.C_History.NoContinueClearTimes++;
					}
				}
				if (StageData.ContinueTimes == 0 && !StageData.OnReplay)
				{
					new EndStage(StageData, "FinalReplaySaveMenu", true);
				}
				else
				{
					new EndStage(StageData, "GameSetMenu", true);
				}
			}
		}
	}
}
