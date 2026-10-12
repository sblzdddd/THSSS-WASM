using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS02_01 : BaseStory_SSS
	{
		public Story_SSS02_01(StageDataPackage StageData)
			: base(StageData)
		{
			this.CharR = new CharacterR_Touhou(StageData, "FaceRakuki_no")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF((float)(base.BoundRect.Width - 60), 380f)
			};
			this.CharL = new CharacterL_Touhou(StageData, "FaceAya_no")
			{
				TxtureObject2 = base.TextureObjectDictionary["FaceAya_base"]
			};
			this.SBox = new StoryBox(StageData);
			this.CharN = new CharacterName(StageData, "ename_Rakuki");
			new StoryEmitterStar(StageData, this.CharN.Position, 0f, 0.0);
			this.Conv.Add(new Conversation("。。。", false, true, "FaceAya_no", "FaceRakuki_no"));
			this.Conv.Add(new Conversation("。。。。。。", true, false, "FaceAya_no", "FaceRakuki_no"));
			this.Conv.Add(new Conversation("四只老鼠在吹牛：", false, true, null, "FaceRakuki_no2"));
			this.Conv.Add(new Conversation("甲：我每天都拿鼠药当糖吃；", false, true, null, "FaceRakuki_hp"));
			this.Conv.Add(new Conversation("乙：我一天不踩老鼠夹脚发痒；", false, true, null, "FaceRakuki_no"));
			this.Conv.Add(new Conversation("丙：我每天不过几次大街不踏实；", false, true, null, "FaceRakuki_an"));
			this.Conv.Add(new Conversation("丁：时间不早了，回家抱猫去咯。", false, true, null, "FaceRakuki_bk"));
			this.Conv.Add(new Conversation("@画师，我需要一个泪奔的表情", true, false, "FaceAya_lo", null));
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == this.Conv.Count)
			{
				base.Boss.Enabled = true;
				base.StageData.ChangeBGM(".\\BGM\\Boss02.ogg", 0, 0, 255, 327663, 3504627);
				MusicTitle musicTitle = new MusicTitle(base.StageData, "疾风闪电", new Point(base.BoundRect.Width, base.BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)base.BoundRect.Width, (float)(base.BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				this.SBox.Dispose();
				base.Story = null;
				new EmitterBossFire(base.StageData, base.Boss.OriginalPosition, Color.FromArgb(144, 255, 120));
				MagicCircle magicCircle = new MagicCircle(base.StageData, "MagicCircleSix");
				magicCircle.Scale = 1.5f;
				base.Background3D.WarpEnabled = true;
			}
		}
	}
}
