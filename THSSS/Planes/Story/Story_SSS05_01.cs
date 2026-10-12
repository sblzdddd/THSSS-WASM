using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS05_01 : BaseStory_SSS
	{
		public Story_SSS05_01(StageDataPackage StageData)
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
								this.LoadConversation(".\\Story\\5-1D.csv");
							}
						}
						else
						{
							this.LoadConversation(".\\Story\\5-1C.csv");
						}
					}
					else
					{
						this.LoadConversation(".\\Story\\5-1B.csv");
					}
				}
				else
				{
					this.LoadConversation2(".\\Story\\5-1A.csv");
				}
			}
		}

		public override void Ctrl()
		{
			this.tCount++;
			if (base.Time < this.tCount / this.circle)
			{
				base.Time++;
			}
			base.Ctrl();
			if (base.Time == this.NameTime && this.LastTime < base.Time)
			{
				this.LastTime = base.Time;
			}
			else if (base.Time == this.MusicTime && this.LastTime < base.Time)
			{
				this.LastTime = base.Time;
			}
			if (base.Time == this.Conv.Count)
			{
				base.Boss.Enabled = true;
				this.SBox.Dispose();
				base.Story = null;
				new EmitterBossFire(base.StageData, base.Boss.OriginalPosition, Color.FromArgb(144, 255, 120));
				MagicCircle magicCircle = new MagicCircle(base.StageData, "MagicCircleSix");
				magicCircle.Scale = 1.5f;
				base.Background3D.WarpColorKey = 2;
				base.Background3D.WarpEnabled = true;
			}
		}

		private int circle = 120;

		private int tCount = 0;
	}
}
