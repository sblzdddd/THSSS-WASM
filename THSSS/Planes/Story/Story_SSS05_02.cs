using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS05_02 : BaseStory_SSS
	{
		public Story_SSS05_02(StageDataPackage StageData)
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
								LoadConversation(".\\Story\\5-2D.csv");
							}
						}
						else
						{
							LoadConversation(".\\Story\\5-2C.csv");
						}
					}
					else
					{
						LoadConversation(".\\Story\\5-2B.csv");
					}
				}
				else
				{
					LoadConversation2(".\\Story\\5-2A.csv");
				}
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == NameTime && LastTime < Time)
			{
				LastTime = Time;
				CharN = new CharacterName(StageData, "ename_Rakukun");
				new StoryEmitterStar(StageData, CharN.Position, 0f, 0.0);
			}
			else if (Time == MusicTime && LastTime < Time)
			{
				StageData.ChangeBGM(".\\BGM\\Boss05.ogg", 0, 0, 255, 1000056, 6599345);
				LastTime = Time;
			}
			if (Time == Conv.Count)
			{
				var musicTitle = new MusicTitle(StageData, "引燃夜空的星火", new Point(BoundRect.Width, BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)BoundRect.Width, (float)(BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
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
	}
}
