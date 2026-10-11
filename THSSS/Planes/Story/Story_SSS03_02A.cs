using System;

namespace Shooting.Planes.Story
{
	internal class Story_SSS03_02A : BaseStory_SSS
	{
		public Story_SSS03_02A(StageDataPackage StageData)
			: base(StageData)
		{
			this.LoadConversation2(".\\Story\\3-2A.csv");
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == this.Conv.Count)
			{
				this.SBox.Dispose();
				base.Story = null;
				new EndStage(base.StageData, "St4", false);
				new ClearBonus(base.StageData, 30000000);
			}
		}
	}
}
