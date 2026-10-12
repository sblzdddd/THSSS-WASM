using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS03_02B : BaseStory_SSS
	{
		public Story_SSS03_02B(StageDataPackage StageData)
			: base(StageData)
		{
			this.CharR = new CharacterR_Touhou(StageData, "FaceSeiryuu_lo")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF((float)(base.BoundRect.Width - 60), 380f)
			};
			this.CharL = new CharacterL_Touhou(StageData, "FaceMarisa_hp2")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF(70f, 340f)
			};
			this.SBox = new StoryBox(StageData);
			this.Conv.Add(new Conversation("呜~被打败啦", false, true, "FaceMarisa_hp2", "FaceSeiryuu_lo"));
			this.Conv.Add(new Conversation("好不甘心呀", false, true, null, null));
			this.Conv.Add(new Conversation("那么 你的那份星之碎片就给我保管咯~", true, false, "FaceMarisa_hp2", "FaceSeiryuu_lo"));
			this.Conv.Add(new Conversation("前方侦测到高能量魔力反应 会是什么呢？", true, false, null, null));
			this.Conv.Add(new Conversation("前进前进~", true, false, "FaceMarisa_hp", null));
			this.Conv.Add(new Conversation("小心好奇害死猫哦", false, true, null, "FaceSeiryuu_lo"));
			this.Conv.Add(new Conversation("猫也是有九条命的呢", true, false, "FaceMarisa_hp2", null));
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == this.Conv.Count)
			{
				this.SBox.Dispose();
				base.Story = null;
				new EndStage(base.StageData, "St4", false);
				new ClearBonus(base.StageData, 30000000);
			}
		}
	}
}
