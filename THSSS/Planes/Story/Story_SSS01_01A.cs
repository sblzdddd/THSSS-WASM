using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS01_01A : BaseStory_SSS
	{
		public Story_SSS01_01A(StageDataPackage StageData)
			: base(StageData)
		{
			this.LoadConversation2(".\\Story\\1-1A.csv");
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == this.NameTime && this.LastTime < base.Time)
			{
				this.LastTime = base.Time;
				this.CharN = new CharacterName(base.StageData, "ename_Ami");
				new StoryEmitterStar(base.StageData, this.CharN.Position, 0f, 0.0);
			}
			if (base.Time == this.MusicTime && this.LastTime < base.Time)
			{
				this.LastTime = base.Time;
				base.StageData.ChangeBGM(".\\BGM\\Boss01.ogg", 0, 0, 255, 754110, 3294270);
			}
			if (base.Time == this.Conv.Count)
			{
				MusicTitle musicTitle = new MusicTitle(base.StageData, "喧闹吧！在这不眠之夜", new Point(base.BoundRect.Width, base.BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)base.BoundRect.Width, (float)(base.BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				base.Boss.Enabled = true;
				this.SBox.Dispose();
				base.Story = null;
				new EmitterBossFire(base.StageData, base.Boss.OriginalPosition, Color.FromArgb(40, 255, 20));
				MagicCircle magicCircle = new MagicCircle(base.StageData, "MagicCircleSix");
				magicCircle.Scale = 1.5f;
				base.Background3D.WarpEnabled = true;
			}
		}
	}
}
