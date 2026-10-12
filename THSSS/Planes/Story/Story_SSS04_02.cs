namespace Shooting.Planes.Story
{
	internal class Story_SSS04_02 : BaseStory_SSS
	{
		public Story_SSS04_02(StageDataPackage StageData)
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
								LoadConversation(".\\Story\\4-2D.csv");
							}
						}
						else
						{
							LoadConversation(".\\Story\\4-2C.csv");
						}
					}
					else
					{
						LoadConversation(".\\Story\\4-2B.csv");
					}
				}
				else
				{
					LoadConversation2(".\\Story\\4-2A.csv");
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
				new EndStage(StageData, "St5", false);
				new ClearBonus(StageData, 40000000);
			}
		}
	}
}
