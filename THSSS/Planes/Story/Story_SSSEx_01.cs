using System;
using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSSEx_01 : BaseStory_SSS
	{
		public Story_SSSEx_01(StageDataPackage StageData)
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
								this.LoadConversation(".\\Story\\Ex-1D.csv");
							}
						}
						else
						{
							this.LoadConversation(".\\Story\\Ex-1C.csv");
						}
					}
					else
					{
						this.LoadConversation(".\\Story\\Ex-1B.csv");
					}
				}
				else
				{
					this.LoadConversation2(".\\Story\\Ex-1A.csv");
				}
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == this.NameTime && this.LastTime < base.Time)
			{
				this.CharN = new CharacterName(base.StageData, "ename_Rika");
				this.LastTime = base.Time;
			}
			else if (base.Time == this.MusicTime && this.LastTime < base.Time)
			{
				base.StageData.ChangeBGM(".\\BGM\\BossEx.ogg", 0, 0, 255, 1125873, 10783332);
				this.LastTime = base.Time;
			}
			if (base.Time == this.Conv.Count)
			{
				MusicTitle musicTitle = new MusicTitle(base.StageData, "风中花，雪中月", new Point(base.BoundRect.Width, base.BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)base.BoundRect.Width, (float)(base.BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				base.Boss.Enabled = true;
				this.SBox.Dispose();
				base.Story = null;
				new EmitterBossFire(base.StageData, base.Boss.OriginalPosition, Color.Yellow);
				MagicCircle magicCircle = new MagicCircle(base.StageData, "MagicCircleSix");
				magicCircle.Scale = 1.5f;
				base.Background3D.WarpEnabled = true;
			}
		}
	}
}
