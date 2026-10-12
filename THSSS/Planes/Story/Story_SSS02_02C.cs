using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS02_02C : BaseStory_SSS
	{
		public Story_SSS02_02C(StageDataPackage StageData)
			: base(StageData)
		{
			CharR = new CharacterR_Touhou(StageData, "FaceRakuki_lo")
			{
				TxtureObject2 = TextureObjectDictionary[" "],
				OriginalPosition = new PointF((float)(BoundRect.Width - 60), 380f)
			};
			CharL = new CharacterL_Touhou(StageData, "FaceSanae_sw")
			{
				TxtureObject2 = TextureObjectDictionary[" "],
				OriginalPosition = new PointF(70f, 380f)
			};
			SBox = new StoryBox(StageData);
			Conv.Add(new Conversation("怎么会！？", false, true, "FaceSanae_sw", "FaceRakuki_lo"));
			Conv.Add(new Conversation("姐姐大人对不起…", false, true, null, null));
			Conv.Add(new Conversation("总算是勉强通过了", true, false, null, null));
			Conv.Add(new Conversation("没想到会遇上如此强大的拦截", true, false, "FaceSanae_lo", null));
			Conv.Add(new Conversation("总觉得前面会很危险…", true, false, "FaceSanae_lo", null));
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == Conv.Count)
			{
				SBox.Dispose();
				Story = null;
				new EndStage(StageData, "St3", false);
				new ClearBonus(StageData, 20000000);
			}
		}
	}
}
