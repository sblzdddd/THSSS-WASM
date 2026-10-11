using System;
using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS01_01 : BaseStory_SSS
	{
		public Story_SSS01_01(StageDataPackage StageData)
			: base(StageData)
		{
			this.CharR = new CharacterR_Touhou(StageData, "FaceAmi01")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF((float)(base.BoundRect.Width - 60), 380f)
			};
			this.CharL = new CharacterL_Touhou(StageData, "FaceAya_no")
			{
				TxtureObject2 = base.TextureObjectDictionary["FaceAya_base"]
			};
			this.SBox = new StoryBox(StageData);
			this.CharN = new CharacterName(StageData, "ename_Ami");
			new StoryEmitterStar(StageData, this.CharN.Position, 0f, 0.0);
			this.Conv.Add(new Conversation("。。。", false, true, "FaceAya_no", "FaceAmi01"));
			this.Conv.Add(new Conversation("。。。。。。", true, false, "FaceAya_no", null));
			this.Conv.Add(new Conversation("从前有座山，", false, true, null, "FaceAmi01"));
			this.Conv.Add(new Conversation("山上有座庙，", false, true, null, "FaceAmi01"));
			this.Conv.Add(new Conversation("庙里有只文文在卖报，", false, true, null, "FaceAmi01"));
			this.Conv.Add(new Conversation("。。。。。。", true, false, "FaceAya_sw", null));
			this.Conv.Add(new Conversation("报上写着，", false, true, null, "FaceAmi02"));
			this.Conv.Add(new Conversation("从前有座山，山上有座庙，庙里有只文文在卖报", false, true, null, "FaceAmi03"));
			this.Conv.Add(new Conversation("报上写着，", false, true, null, "FaceAmi04"));
			this.Conv.Add(new Conversation("从前有座山，山上有座庙，庙里有只文文在卖报", false, true, null, "FaceAmi05"));
			this.Conv.Add(new Conversation("报上写着，", false, true, null, "FaceAmi06"));
			this.Conv.Add(new Conversation("喂，有完没完了", true, false, "FaceAya_sw", null));
			this.Conv.Add(new Conversation("好了，开打吧", false, true, null, "FaceAmi07"));
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == this.Conv.Count)
			{
				base.Boss.Enabled = true;
				base.StageData.ChangeBGM(".\\BGM\\Boss01.wav", 0, 0, 255, 754110, 3294270);
				MusicTitle musicTitle = new MusicTitle(base.StageData, "喧闹吧！在这不眠之夜", new Point(base.BoundRect.Width, base.BoundRect.Height - 16));
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
