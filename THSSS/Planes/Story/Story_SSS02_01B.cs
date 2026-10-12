using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS02_01B : BaseStory_SSS
	{
		public Story_SSS02_01B(StageDataPackage StageData)
			: base(StageData)
		{
			CharR = new CharacterR_Touhou(StageData, "FaceRakuki_an2")
			{
				TxtureObject2 = TextureObjectDictionary[" "],
				OriginalPosition = new PointF((float)(BoundRect.Width - 60), 380f)
			};
			CharL = new CharacterL_Touhou(StageData, "FaceMarisa_sp")
			{
				TxtureObject2 = TextureObjectDictionary[" "],
				OriginalPosition = new PointF(70f, 340f)
			};
			SBox = new StoryBox(StageData);
			Conv.Add(new Conversation("到此为止了 魔女小姐！", false, true, "FaceMarisa_sp", "FaceRakuki_an2"));
			Conv.Add(new Conversation("诶 出现了一个奇怪的人类呢", true, false, "FaceMarisa_sp", "FaceRakuki_an2"));
			Conv.Add(new Conversation("才不是什么奇怪的人类呢", false, true, null, "FaceRakuki_an"));
			Conv.Add(new Conversation("我叫洛熙", false, true, null, "FaceRakuki_an"));
			Conv.Add(new Conversation("罕见的弓箭使呢", true, false, "FaceMarisa_no", null));
			Conv.Add(new Conversation("你也是来妨碍我的吗？", true, false, "FaceMarisa_sw", null));
			Conv.Add(new Conversation("因为和姐姐约定好了……", false, true, null, "FaceRakuki_dp"));
			Conv.Add(new Conversation("总之你不能继续往前的哦", false, true, null, "FaceRakuki_an2"));
			Conv.Add(new Conversation("前面有些什么我并不感兴趣", true, false, "FaceMarisa_no", null));
			Conv.Add(new Conversation("我只是来收集流星碎片的说~", true, false, "FaceMarisa_hp", null));
			Conv.Add(new Conversation("就让我过去吧 如何？", true, false, "FaceMarisa_hp2", null));
			Conv.Add(new Conversation("那你可要问问我手里这张弓了！", false, true, null, "FaceRakuki_dp"));
			Conv.Add(new Conversation("只不过是弓箭而已", true, false, "FaceMarisa_hp", null));
			Conv.Add(new Conversation("追的上我的魔法话就来试试吧~", true, false, "FaceMarisa_hp2", null));
			Conv.Add(new Conversation("真是目光短浅！", false, true, null, "FaceRakuki_an"));
			Conv.Add(new Conversation("那种三脚猫魔法在我面前毫无用处", false, true, null, "FaceRakuki_an"));
			Conv.Add(new Conversation("就让你带着恐惧和后悔", false, true, null, "FaceRakuki_dp"));
			Conv.Add(new Conversation("去死吧！", false, true, null, "FaceRakuki_bk"));
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == 3 && LastTime < Time)
			{
				LastTime = Time;
				CharN = new CharacterName(StageData, "ename_Rakuki");
				new StoryEmitterStar(StageData, CharN.Position, 0f, 0.0);
			}
			if (Time == 17 && LastTime < Time)
			{
				StageData.ChangeBGM(".\\BGM\\Boss02.ogg", 0, 0, 255, 327663, 3504627);
				LastTime = Time;
			}
			if (Time == Conv.Count)
			{
				MusicTitle musicTitle = new MusicTitle(StageData, "疾风闪电", new Point(BoundRect.Width, BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)BoundRect.Width, (float)(BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				Boss.Enabled = true;
				SBox.Dispose();
				Story = null;
				new EmitterBossFire(StageData, Boss.OriginalPosition, Color.FromArgb(144, 255, 120));
				MagicCircle magicCircle = new MagicCircle(StageData, "MagicCircleSix");
				magicCircle.Scale = 1.5f;
				Background3D.WarpEnabled = true;
			}
		}
	}
}
