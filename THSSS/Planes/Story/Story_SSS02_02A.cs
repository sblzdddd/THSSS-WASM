using System;

namespace Shooting.Planes.Story
{
	internal class Story_SSS02_02A : BaseStory_SSS
	{
		public Story_SSS02_02A(StageDataPackage StageData)
			: base(StageData)
		{
			this.LoadConversation2(".\\Story\\2-2A.csv");
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == this.Conv.Count)
			{
				this.SBox.Dispose();
				base.Story = null;
				new EndStage(base.StageData, "St3", false);
				new ClearBonus(base.StageData, 20000000);
			}
		}
	}
}
