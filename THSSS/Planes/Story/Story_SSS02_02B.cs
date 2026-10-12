using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS02_02B : BaseStory_SSS
	{
		public Story_SSS02_02B(StageDataPackage StageData)
			: base(StageData)
		{
			this.CharR = new CharacterR_Touhou(StageData, "FaceRakuki_lo")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF((float)(base.BoundRect.Width - 60), 380f)
			};
			this.CharL = new CharacterL_Touhou(StageData, "FaceMarisa_hp")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF(70f, 340f)
			};
			this.SBox = new StoryBox(StageData);
			this.Conv.Add(new Conversation("怎么可能…我竟然会败……", false, true, "FaceMarisa_hp", "FaceRakuki_lo"));
			this.Conv.Add(new Conversation("对不起…姐姐大人……", false, true, null, null));
			this.Conv.Add(new Conversation("呼~总算是通过了", true, false, "FaceMarisa_hp", null));
			this.Conv.Add(new Conversation("Lucky Lucky~", true, false, "FaceMarisa_hp2", null));
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
