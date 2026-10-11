using System;
using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS03_02 : BaseStory_SSS
	{
		public Story_SSS03_02(StageDataPackage StageData)
			: base(StageData)
		{
			this.CharR = new CharacterR_Touhou(StageData, "FaceSeiryuu_lo")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF((float)(base.BoundRect.Width - 60), 380f)
			};
			this.CharL = new CharacterL_Touhou(StageData, "FaceAya_no")
			{
				TxtureObject2 = base.TextureObjectDictionary["FaceAya_base"]
			};
			this.SBox = new StoryBox(StageData);
			this.Conv.Add(new Conversation("。。。", false, true, "FaceAya_no", "FaceSeiryuu_no"));
			this.Conv.Add(new Conversation("。。。。。。", true, false, "FaceAya_no", "FaceSeiryuu_no"));
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
