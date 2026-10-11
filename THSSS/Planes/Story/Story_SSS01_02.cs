using System;
using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS01_02 : BaseStory_SSS
	{
		public Story_SSS01_02(StageDataPackage StageData)
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
			this.Conv.Add(new Conversation("。。。", true, false, "FaceAya_no", null));
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == this.Conv.Count)
			{
				this.SBox.Dispose();
				base.Story = null;
				new EndStage(base.StageData, "St2", false);
				new ClearBonus(base.StageData, 10000000);
			}
		}
	}
}
