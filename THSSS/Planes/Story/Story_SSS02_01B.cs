using System;
using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS02_01B : BaseStory_SSS
	{
		public Story_SSS02_01B(StageDataPackage StageData)
			: base(StageData)
		{
			this.CharR = new CharacterR_Touhou(StageData, "FaceRakuki_an2")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF((float)(base.BoundRect.Width - 60), 380f)
			};
			this.CharL = new CharacterL_Touhou(StageData, "FaceMarisa_sp")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF(70f, 340f)
			};
			this.SBox = new StoryBox(StageData);
			this.Conv.Add(new Conversation("到此为止了 魔女小姐！", false, true, "FaceMarisa_sp", "FaceRakuki_an2"));
			this.Conv.Add(new Conversation("诶 出现了一个奇怪的人类呢", true, false, "FaceMarisa_sp", "FaceRakuki_an2"));
			this.Conv.Add(new Conversation("才不是什么奇怪的人类呢", false, true, null, "FaceRakuki_an"));
			this.Conv.Add(new Conversation("我叫洛熙", false, true, null, "FaceRakuki_an"));
			this.Conv.Add(new Conversation("罕见的弓箭使呢", true, false, "FaceMarisa_no", null));
			this.Conv.Add(new Conversation("你也是来妨碍我的吗？", true, false, "FaceMarisa_sw", null));
			this.Conv.Add(new Conversation("因为和姐姐约定好了……", false, true, null, "FaceRakuki_dp"));
			this.Conv.Add(new Conversation("总之你不能继续往前的哦", false, true, null, "FaceRakuki_an2"));
			this.Conv.Add(new Conversation("前面有些什么我并不感兴趣", true, false, "FaceMarisa_no", null));
			this.Conv.Add(new Conversation("我只是来收集流星碎片的说~", true, false, "FaceMarisa_hp", null));
			this.Conv.Add(new Conversation("就让我过去吧 如何？", true, false, "FaceMarisa_hp2", null));
			this.Conv.Add(new Conversation("那你可要问问我手里这张弓了！", false, true, null, "FaceRakuki_dp"));
			this.Conv.Add(new Conversation("只不过是弓箭而已", true, false, "FaceMarisa_hp", null));
			this.Conv.Add(new Conversation("追的上我的魔法话就来试试吧~", true, false, "FaceMarisa_hp2", null));
			this.Conv.Add(new Conversation("真是目光短浅！", false, true, null, "FaceRakuki_an"));
			this.Conv.Add(new Conversation("那种三脚猫魔法在我面前毫无用处", false, true, null, "FaceRakuki_an"));
			this.Conv.Add(new Conversation("就让你带着恐惧和后悔", false, true, null, "FaceRakuki_dp"));
			this.Conv.Add(new Conversation("去死吧！", false, true, null, "FaceRakuki_bk"));
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 3 && this.LastTime < base.Time)
			{
				this.LastTime = base.Time;
				this.CharN = new CharacterName(base.StageData, "ename_Rakuki");
				new StoryEmitterStar(base.StageData, this.CharN.Position, 0f, 0.0);
			}
			if (base.Time == 17 && this.LastTime < base.Time)
			{
				base.StageData.ChangeBGM(".\\BGM\\Boss02.ogg", 0, 0, 255, 327663, 3504627);
				this.LastTime = base.Time;
			}
			if (base.Time == this.Conv.Count)
			{
				MusicTitle musicTitle = new MusicTitle(base.StageData, "疾风闪电", new Point(base.BoundRect.Width, base.BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)base.BoundRect.Width, (float)(base.BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				base.Boss.Enabled = true;
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
