using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS02_02C : BaseStory_SSS
	{
		public Story_SSS02_02C(StageDataPackage StageData)
			: base(StageData)
		{
			this.CharR = new CharacterR_Touhou(StageData, "FaceRakuki_lo")
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
			this.Conv.Add(new Conversation("怎么会！？", false, true, "FaceSanae_sw", "FaceRakuki_lo"));
			this.Conv.Add(new Conversation("姐姐大人对不起…", false, true, null, null));
			this.Conv.Add(new Conversation("总算是勉强通过了", true, false, null, null));
			this.Conv.Add(new Conversation("没想到会遇上如此强大的拦截", true, false, "FaceSanae_lo", null));
			this.Conv.Add(new Conversation("总觉得前面会很危险…", true, false, "FaceSanae_lo", null));
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == this.Conv.Count)
			{
				this.SBox.Dispose();
				base.Story = null;
				new EndStage(base.StageData, "St3", false);
				new ClearBonus(base.StageData, 20000000);
			}
		}
	}
}
