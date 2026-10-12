namespace Shooting.Planes.Story
{
	internal class Story_SSStaff : BaseStory_SSS
	{
		public Story_SSStaff(StageDataPackage StageData)
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
								this.LoadConversation(".\\Story\\Staff-1D.csv");
							}
						}
						else
						{
							this.LoadConversation(".\\Story\\Staff-1C.csv");
						}
					}
					else
					{
						this.LoadConversation(".\\Story\\Staff-1B.csv");
					}
				}
				else
				{
					this.LoadConversation(".\\Story\\Staff-1A.csv");
				}
			}
			this.SBox.MaxTransparent = 0;
			this.Cg = new CG(StageData, null);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			this.CharL.TxtureObject = null;
			this.CharR.TxtureObject = null;
			this.Cg.Ctrl();
			if (base.Time < this.Conv.Count)
			{
				this.Cg.TxtureObject = base.TextureObjectDictionary[this.Conv[base.Time].CharL];
			}
			if (base.Time == this.Conv.Count)
			{
				this.SBox.Dispose();
				base.Story = null;
			}
		}

		public override void Show()
		{
			this.Cg.Show();
			base.Show();
		}

		private CG Cg;
	}
}
