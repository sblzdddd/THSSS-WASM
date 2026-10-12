using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS02_01X : BaseStory_SSS
	{
		public Story_SSS02_01X(StageDataPackage StageData)
			: base(StageData)
		{
			CharR = new CharacterR_Touhou(StageData, "FaceRakuki_no")
			{
				TxtureObject2 = TextureObjectDictionary[" "],
				OriginalPosition = new PointF((float)(BoundRect.Width - 60), 380f)
			};
			CharL = new CharacterL_Touhou(StageData, "Myplane")
			{
				TxtureObject2 = TextureObjectDictionary["Myplane"]
			};
			SBox = new StoryBox(StageData);
			CharN = new CharacterName(StageData, "ename_Rakuki");
			new StoryEmitterStar(StageData, CharN.Position, 0f, 0.0);
			Conv.Add(new Conversation("。。。", false, true, "Myplane", "FaceRakuki_no"));
			Conv.Add(new Conversation("。。。。。。", true, false, "Myplane", "FaceRakuki_no"));
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == Conv.Count)
			{
				Boss.Enabled = true;
				StageData.ChangeBGM(".\\BGM\\Boss02.ogg", 0, 0, 255, 327663, 3504627);
				var musicTitle = new MusicTitle(StageData, "疾风闪电", new Point(BoundRect.Width, BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)BoundRect.Width, (float)(BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				SBox.Dispose();
				Story = null;
				new EmitterBossFire(StageData, Boss.OriginalPosition, Color.FromArgb(144, 255, 120));
				var magicCircle = new MagicCircle(StageData, "MagicCircleSix");
				magicCircle.Scale = 1.5f;
				Background3D.WarpEnabled = true;
			}
		}
	}
}
