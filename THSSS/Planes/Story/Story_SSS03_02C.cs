using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS03_02C : BaseStory_SSS
	{
		public Story_SSS03_02C(StageDataPackage StageData)
			: base(StageData)
		{
			this.CharR = new CharacterR_Touhou(StageData, "FaceSeiryuu_lo")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF((float)(base.BoundRect.Width - 60), 380f)
			};
			this.CharL = new CharacterL_Touhou(StageData, "FaceSanae_sw")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF(70f, 380f)
			};
			this.SBox = new StoryBox(StageData);
			this.Conv.Add(new Conversation("呜~被打败啦", false, true, "FaceSanae_hp", "FaceSeiryuu_lo"));
			this.Conv.Add(new Conversation("好不甘心呀", false, true, "FaceSanae_hp", "FaceSeiryuu_lo"));
			this.Conv.Add(new Conversation("那么你的流星碎片我就收下啦", true, false, "FaceSanae_hp", "FaceSeiryuu_lo"));
			this.Conv.Add(new Conversation("这样我的信仰也会增加的吧？", true, false, "FaceSanae_hp", "FaceSeiryuu_lo"));
			this.Conv.Add(new Conversation("不过这样真的好吗？", false, true, null, "FaceSeiryuu_lo"));
			this.Conv.Add(new Conversation("你指什么？", true, false, "FaceSanae_sw", null));
			this.Conv.Add(new Conversation("你收集的流星碎片", false, true, null, "FaceSeiryuu_lo"));
			this.Conv.Add(new Conversation("那份光芒会让你迷失方向的哦？", false, true, null, "FaceSeiryuu_lo"));
			this.Conv.Add(new Conversation("净说什么瞎话", true, false, "FaceSanae_lo", null));
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
