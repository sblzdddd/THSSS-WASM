using System;

namespace Shooting.Planes.Story
{
	internal class Story_SSS04_02 : BaseStory_SSS
	{
		public Story_SSS04_02(StageDataPackage StageData)
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
								this.LoadConversation(".\\Story\\4-2D.csv");
							}
						}
						else
						{
							this.LoadConversation(".\\Story\\4-2C.csv");
						}
					}
					else
					{
						this.LoadConversation(".\\Story\\4-2B.csv");
					}
				}
				else
				{
					this.LoadConversation2(".\\Story\\4-2A.csv");
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
				new EndStage(base.StageData, "St5", false);
				new ClearBonus(base.StageData, 40000000);
			}
		}
	}
}
