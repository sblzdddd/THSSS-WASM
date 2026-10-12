namespace Shooting.Planes.Story
{
	internal class Story_SSS01_02D : BaseStory_SSS
	{
		public Story_SSS01_02D(StageDataPackage StageData)
			: base(StageData)
		{
			this.LoadConversation(".\\Story\\1-2D.csv");
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
