using System;

namespace Shooting.Planes.Story
{
	internal class Story_SSSEx_02 : BaseStory_SSS
	{
		public Story_SSSEx_02(StageDataPackage StageData)
			: base(StageData)
		{
			string name = base.MyPlane.Name;
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
								this.LoadConversation(".\\Story\\Ex-2D.csv");
							}
						}
						else
						{
							this.LoadConversation(".\\Story\\Ex-2C.csv");
						}
					}
					else
					{
						this.LoadConversation(".\\Story\\Ex-2B.csv");
					}
				}
				else
				{
					this.LoadConversation2(".\\Story\\Ex-2A.csv");
				}
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == this.Conv.Count)
			{
				this.SBox.Dispose();
				base.Story = null;
				new ClearBonus(base.StageData, 70000000);
				base.StageData.CurrentStageName = "ExAll";
				base.StageData.RepInfo.LastStage = base.StageData.CurrentStageName;
				if (!base.StageData.OnReplay)
				{
					base.StageData.C_History = base.StageData.PData.C_History.Find(new Predicate<ClearHistory>(base.StageData.FindClear));
					base.StageData.C_History.ClearTimes++;
					if (base.StageData.ContinueTimes == 0)
					{
						base.StageData.C_History.NoContinueClearTimes++;
					}
				}
				if (base.StageData.ContinueTimes == 0 && !base.StageData.OnReplay)
				{
					new EndStage(base.StageData, "FinalReplaySaveMenu", true);
				}
				else
				{
					new EndStage(base.StageData, "GameSetMenu", true);
				}
			}
		}
	}
}
