using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS02_02X : BaseStory_SSS
	{
		public Story_SSS02_02X(StageDataPackage StageData)
			: base(StageData)
		{
			this.CharR = new CharacterR_Touhou(StageData, "FaceRakuki_lo")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF((float)(base.BoundRect.Width - 60), 380f)
			};
			this.CharL = new CharacterL_Touhou(StageData, "Myplane")
			{
				TxtureObject2 = base.TextureObjectDictionary["Myplane"]
			};
			this.SBox = new StoryBox(StageData);
			this.Conv.Add(new Conversation("。。。", false, true, "Myplane", "FaceRakuki_lo"));
			this.Conv.Add(new Conversation("这是意外。", true, false, "Myplane", "FaceRakuki_lo"));
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
