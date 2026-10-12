using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS05_01 : BaseStory_SSS
	{
		public Story_SSS05_01(StageDataPackage StageData)
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
								LoadConversation(".\\Story\\5-1D.csv");
							}
						}
						else
						{
							LoadConversation(".\\Story\\5-1C.csv");
						}
					}
					else
					{
						LoadConversation(".\\Story\\5-1B.csv");
					}
				}
				else
				{
					LoadConversation2(".\\Story\\5-1A.csv");
				}
			}
		}

		public override void Ctrl()
		{
			tCount++;
			if (Time < tCount / circle)
			{
				Time++;
			}
			base.Ctrl();
			if (Time == NameTime && LastTime < Time)
			{
				LastTime = Time;
			}
			else if (Time == MusicTime && LastTime < Time)
			{
				LastTime = Time;
			}
			if (Time == Conv.Count)
			{
				Boss.Enabled = true;
				SBox.Dispose();
				Story = null;
				new EmitterBossFire(StageData, Boss.OriginalPosition, Color.FromArgb(144, 255, 120));
				var magicCircle = new MagicCircle(StageData, "MagicCircleSix");
				magicCircle.Scale = 1.5f;
				Background3D.WarpColorKey = 2;
				Background3D.WarpEnabled = true;
			}
		}

		private int circle = 120;

		private int tCount = 0;
	}
}
