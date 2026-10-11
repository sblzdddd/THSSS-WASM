using System;

namespace Shooting.Planes.Story
{
	internal class Story_SSS03_02D : BaseStory_SSS
	{
		public Story_SSS03_02D(StageDataPackage StageData)
			: base(StageData)
		{
			this.LoadConversation(".\\Story\\3-2D.csv");
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
