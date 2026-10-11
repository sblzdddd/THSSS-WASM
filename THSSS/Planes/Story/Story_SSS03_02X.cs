using System;
using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS03_02X : BaseStory_SSS
	{
		public Story_SSS03_02X(StageDataPackage StageData)
			: base(StageData)
		{
			this.CharR = new CharacterR_Touhou(StageData, "FaceSeiryuu_lo")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF((float)(base.BoundRect.Width - 60), 380f)
			};
			this.CharL = new CharacterL_Touhou(StageData, "Myplane")
			{
				TxtureObject2 = base.TextureObjectDictionary["Myplane"]
			};
			this.SBox = new StoryBox(StageData);
			this.Conv.Add(new Conversation("。。。", false, true, "Myplane", "FaceSeiryuu_lo"));
			this.Conv.Add(new Conversation("这是意外。", true, false, "Myplane", "FaceSeiryuu_lo"));
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
