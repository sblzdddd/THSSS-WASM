using System;

namespace Shooting.Planes.Story
{
	internal class Story_SSS05_03 : BaseStory_SSS
	{
		public Story_SSS05_03(StageDataPackage StageData)
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
								this.LoadConversation(".\\Story\\5-3D.csv");
							}
						}
						else
						{
							this.LoadConversation(".\\Story\\5-3C.csv");
						}
					}
					else
					{
						this.LoadConversation(".\\Story\\5-3B.csv");
					}
				}
				else
				{
					this.LoadConversation2(".\\Story\\5-3A.csv");
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
				new EndStage(base.StageData, "St6", false);
				new ClearBonus(base.StageData, 50000000);
			}
		}
	}
}
