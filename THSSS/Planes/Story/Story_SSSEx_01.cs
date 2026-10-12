using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSSEx_01 : BaseStory_SSS
	{
		public Story_SSSEx_01(StageDataPackage StageData)
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
								LoadConversation(".\\Story\\Ex-1D.csv");
							}
						}
						else
						{
							LoadConversation(".\\Story\\Ex-1C.csv");
						}
					}
					else
					{
						LoadConversation(".\\Story\\Ex-1B.csv");
					}
				}
				else
				{
					LoadConversation2(".\\Story\\Ex-1A.csv");
				}
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == NameTime && LastTime < Time)
			{
				CharN = new CharacterName(StageData, "ename_Rika");
				LastTime = Time;
			}
			else if (Time == MusicTime && LastTime < Time)
			{
				StageData.ChangeBGM(".\\BGM\\BossEx.ogg", 0, 0, 255, 1125873, 10783332);
				LastTime = Time;
			}
			if (Time == Conv.Count)
			{
				var musicTitle = new MusicTitle(StageData, "风中花，雪中月", new Point(BoundRect.Width, BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)BoundRect.Width, (float)(BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				Boss.Enabled = true;
				SBox.Dispose();
				Story = null;
				new EmitterBossFire(StageData, Boss.OriginalPosition, Color.Yellow);
				var magicCircle = new MagicCircle(StageData, "MagicCircleSix");
				magicCircle.Scale = 1.5f;
				Background3D.WarpEnabled = true;
			}
		}
	}
}
