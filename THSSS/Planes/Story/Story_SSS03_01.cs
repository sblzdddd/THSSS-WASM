using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS03_01 : BaseStory_SSS
	{
		public Story_SSS03_01(StageDataPackage StageData)
			: base(StageData)
		{
			this.CharR = new CharacterR_Touhou(StageData, "FaceSeiryuu_no")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF((float)(base.BoundRect.Width - 60), 380f)
			};
			this.CharL = new CharacterL_Touhou(StageData, "FaceAya_no")
			{
				TxtureObject2 = base.TextureObjectDictionary["FaceAya_base"]
			};
			this.SBox = new StoryBox(StageData);
			this.CharN = new CharacterName(StageData, "ename_Seiryuu");
			new StoryEmitterStar(StageData, this.CharN.Position, 0f, 0.0);
			this.Conv.Add(new Conversation("。。。", false, true, "FaceAya_no", "FaceSeiryuu_no"));
			this.Conv.Add(new Conversation("。。。。。。", true, false, "FaceAya_no", "FaceSeiryuu_no"));
			this.Conv.Add(new Conversation("。。。", false, true, "FaceAya_no", "FaceSeiryuu_no"));
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == this.Conv.Count)
			{
				base.Boss.Enabled = true;
				base.StageData.ChangeBGM(".\\BGM\\Boss03.ogg", 0, 0, 255, 1286397, 6275430);
				MusicTitle musicTitle = new MusicTitle(base.StageData, "青柳传说", new Point(base.BoundRect.Width, base.BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)base.BoundRect.Width, (float)(base.BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
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
