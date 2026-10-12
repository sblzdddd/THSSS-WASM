namespace Shooting.Planes.Story
{
	internal class Story_SSS02_02D : BaseStory_SSS
	{
		public Story_SSS02_02D(StageDataPackage StageData)
			: base(StageData)
		{
			this.LoadConversation(".\\Story\\2-2D.csv");
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
