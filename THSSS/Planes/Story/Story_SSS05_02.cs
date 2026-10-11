using System;
using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS05_02 : BaseStory_SSS
	{
		public Story_SSS05_02(StageDataPackage StageData)
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
								this.LoadConversation(".\\Story\\5-2D.csv");
							}
						}
						else
						{
							this.LoadConversation(".\\Story\\5-2C.csv");
						}
					}
					else
					{
						this.LoadConversation(".\\Story\\5-2B.csv");
					}
				}
				else
				{
					this.LoadConversation2(".\\Story\\5-2A.csv");
				}
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == this.NameTime && this.LastTime < base.Time)
			{
				this.LastTime = base.Time;
				this.CharN = new CharacterName(base.StageData, "ename_Rakukun");
				new StoryEmitterStar(base.StageData, this.CharN.Position, 0f, 0.0);
			}
			else if (base.Time == this.MusicTime && this.LastTime < base.Time)
			{
				base.StageData.ChangeBGM(".\\BGM\\Boss05.ogg", 0, 0, 255, 1000056, 6599345);
				this.LastTime = base.Time;
			}
			if (base.Time == this.Conv.Count)
			{
				MusicTitle musicTitle = new MusicTitle(base.StageData, "引燃夜空的星火", new Point(base.BoundRect.Width, base.BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)base.BoundRect.Width, (float)(base.BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
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
	}
}
