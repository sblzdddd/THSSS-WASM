using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS01_01C : BaseStory_SSS
	{
		public Story_SSS01_01C(StageDataPackage StageData)
			: base(StageData)
		{
			this.CharR = new CharacterR_Touhou(StageData, "FaceAmi01")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF((float)(base.BoundRect.Width - 60), 380f)
			};
			this.CharL = new CharacterL_Touhou(StageData, "FaceSanae_hp")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF(70f, 380f)
			};
			this.SBox = new StoryBox(StageData);
			this.Conv.Add(new Conversation("那边的人类 请等一下！", false, true, "FaceSanae_hp", "FaceAmi01"));
			this.Conv.Add(new Conversation("哇 是妖精呢~", true, false, "FaceSanae_hp", null));
			this.Conv.Add(new Conversation("有什么事情吗？", true, false, "FaceSanae_no2", null));
			this.Conv.Add(new Conversation("我们来开party吧！", false, true, null, "FaceAmi05"));
			this.Conv.Add(new Conversation("party？现在？", true, false, "FaceSanae_sw", null));
			this.Conv.Add(new Conversation("是啊是啊 我还要邀请很多人呢", false, true, null, "FaceAmi07"));
			this.Conv.Add(new Conversation("你也一起来吧~", false, true, null, "FaceAmi02"));
			this.Conv.Add(new Conversation("真是有活力呢 妖精晚上都不睡觉的吗……", true, false, "FaceSanae_sw", null));
			this.Conv.Add(new Conversation("真遗憾呢，我还有更重要的事情要调查才行", true, false, "FaceSanae_no", null));
			this.Conv.Add(new Conversation("咦？忙着赶路吗？", false, true, null, "FaceAmi01"));
			this.Conv.Add(new Conversation("那也没关系", false, true, null, "FaceAmi04"));
			this.Conv.Add(new Conversation("party现在开始也是可以的哦！", false, true, null, "FaceAmi05"));
			this.Conv.Add(new Conversation("妨碍的话我会揍你的哦？", true, false, "FaceSanae_sw", null));
			this.Conv.Add(new Conversation("在这充满魔力的流星的夜晚", false, true, null, "FaceAmi04"));
			this.Conv.Add(new Conversation("让我们用弹幕", false, true, null, "FaceAmi01"));
			this.Conv.Add(new Conversation("喧闹起来吧！", false, true, null, "FaceAmi05"));
			this.Conv.Add(new Conversation("这可不行呢，看来有必要退治一下了！", true, false, "FaceSanae_an2", null));
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 3 && this.LastTime < base.Time)
			{
				this.LastTime = base.Time;
				this.CharN = new CharacterName(base.StageData, "ename_Ami");
				new StoryEmitterStar(base.StageData, this.CharN.Position, 0f, 0.0);
			}
			if (base.Time == 15 && this.LastTime < base.Time)
			{
				this.LastTime = base.Time;
				base.StageData.ChangeBGM(".\\BGM\\Boss01.ogg", 0, 0, 255, 754110, 3294270);
			}
			if (base.Time == this.Conv.Count)
			{
				MusicTitle musicTitle = new MusicTitle(base.StageData, "喧闹吧！在这不眠之夜", new Point(base.BoundRect.Width, base.BoundRect.Height - 16));
				musicTitle.OriginalPosition = new PointF((float)base.BoundRect.Width, (float)(base.BoundRect.Height + 100));
				musicTitle.Scale = 0.5f;
				base.Boss.Enabled = true;
				this.SBox.Dispose();
				base.Story = null;
				new EmitterBossFire(base.StageData, base.Boss.OriginalPosition, Color.FromArgb(40, 255, 20));
				MagicCircle magicCircle = new MagicCircle(base.StageData, "MagicCircleSix");
				magicCircle.Scale = 1.5f;
				base.Background3D.WarpEnabled = true;
			}
		}
	}
}
