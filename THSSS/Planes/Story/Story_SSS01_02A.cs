namespace Shooting.Planes.Story
{
	internal class Story_SSS01_02A : BaseStory_SSS
	{
		public Story_SSS01_02A(StageDataPackage StageData)
			: base(StageData)
		{
			this.LoadConversation2(".\\Story\\1-2A.csv");
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == this.Conv.Count)
			{
				this.SBox.Dispose();
				base.Story = null;
				new EndStage(base.StageData, "St2", false);
				new ClearBonus(base.StageData, 10000000);
			}
		}
	}
}
