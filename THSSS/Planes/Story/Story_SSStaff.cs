namespace Shooting.Planes.Story
{
	internal class Story_SSStaff : BaseStory_SSS
	{
		public Story_SSStaff(StageDataPackage StageData)
			: base(StageData)
		{
			string name = MyPlane.Name;
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
								LoadConversation(".\\Story\\Staff-1D.csv");
							}
						}
						else
						{
							LoadConversation(".\\Story\\Staff-1C.csv");
						}
					}
					else
					{
						LoadConversation(".\\Story\\Staff-1B.csv");
					}
				}
				else
				{
					LoadConversation(".\\Story\\Staff-1A.csv");
				}
			}
			SBox.MaxTransparent = 0;
			Cg = new CG(StageData, null);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			CharL.TxtureObject = null;
			CharR.TxtureObject = null;
			Cg.Ctrl();
			if (Time < Conv.Count)
			{
				Cg.TxtureObject = TextureObjectDictionary[Conv[Time].CharL];
			}
			if (Time == Conv.Count)
			{
				SBox.Dispose();
				Story = null;
			}
		}

		public override void Show()
		{
			Cg.Show();
			base.Show();
		}

		private CG Cg;
	}
}
