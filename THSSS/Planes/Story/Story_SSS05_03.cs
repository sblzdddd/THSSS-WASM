namespace Shooting.Planes.Story
{
	internal class Story_SSS05_03 : BaseStory_SSS
	{
		public Story_SSS05_03(StageDataPackage StageData)
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
								LoadConversation(".\\Story\\5-3D.csv");
							}
						}
						else
						{
							LoadConversation(".\\Story\\5-3C.csv");
						}
					}
					else
					{
						LoadConversation(".\\Story\\5-3B.csv");
					}
				}
				else
				{
					LoadConversation2(".\\Story\\5-3A.csv");
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
				new EndStage(StageData, "St6", false);
				new ClearBonus(StageData, 50000000);
			}
		}
	}
}
