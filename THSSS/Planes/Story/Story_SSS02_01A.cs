using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS02_01A : BaseStory_SSS
	{
		public Story_SSS02_01A(StageDataPackage StageData)
			: base(StageData)
		{
			LoadConversation2(".\\Story\\2-1A.csv");
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == NameTime && LastTime < Time)
			{
				LastTime = Time;
				CharN = new CharacterName(StageData, "ename_Rakuki");
				new StoryEmitterStar(StageData, CharN.Position, 0f, 0.0);
			}
			if (Time == MusicTime && LastTime < Time)
			{
				StageData.ChangeBGM(".\\BGM\\Boss02.ogg", 0, 0, 255, 327663, 3504627);
				LastTime = Time;
			}
			if (Time == Conv.Count)
			{
				MusicTitle musicTitle = new MusicTitle(StageData, "疾风闪电", new Point(BoundRect.Width, BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)BoundRect.Width, (float)(BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				Boss.Enabled = true;
				SBox.Dispose();
				Story = null;
				new EmitterBossFire(StageData, Boss.OriginalPosition, Color.FromArgb(144, 255, 120));
				MagicCircle magicCircle = new MagicCircle(StageData, "MagicCircleSix");
				magicCircle.Scale = 1.5f;
				Background3D.WarpEnabled = true;
			}
		}
	}
}
