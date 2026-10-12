using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS06_01 : BaseStory_SSS
	{
		public Story_SSS06_01(StageDataPackage StageData)
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
								LoadConversation(".\\Story\\6-1D.csv");
							}
						}
						else
						{
							LoadConversation(".\\Story\\6-1C.csv");
						}
					}
					else
					{
						LoadConversation(".\\Story\\6-1B.csv");
					}
				}
				else
				{
					LoadConversation2(".\\Story\\6-1A.csv");
				}
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == NameTime && LastTime < Time)
			{
				CharN = new CharacterName(StageData, "ename_Tensei");
				new StoryEmitterStar(StageData, CharN.Position, 0f, 0.0);
				LastTime = Time;
			}
			else if (Time == MusicTime && LastTime < Time)
			{
				StageData.ChangeBGM(".\\BGM\\Boss06.ogg", 0, 0, 255, 1697409, 10789065);
				LastTime = Time;
			}
			if (Time == Conv.Count)
			{
				MusicTitle musicTitle = new MusicTitle(StageData, "？？？？？", new Point(BoundRect.Width, BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)BoundRect.Width, (float)(BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				Boss.Enabled = true;
				SBox.Dispose();
				Story = null;
				new EmitterBossFire(StageData, Boss.OriginalPosition, Color.SkyBlue);
				MagicCircle magicCircle = new MagicCircle(StageData, "MagicCircleSix");
				magicCircle.Scale = 1.5f;
				Background3D.WarpEnabled = true;
			}
		}
	}
}
