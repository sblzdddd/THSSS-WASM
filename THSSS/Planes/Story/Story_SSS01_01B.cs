using System;
using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS01_01B : BaseStory_SSS
	{
		public Story_SSS01_01B(StageDataPackage StageData)
			: base(StageData)
		{
			this.CharR = new CharacterR_Touhou(StageData, "FaceAmi05")
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
			this.Conv.Add(new Conversation("流星碎片的确是掉落在这附近了……", true, false, "FaceMarisa_sp", "FaceAmi05"));
			this.Conv.Add(new Conversation("没找到啊……", true, false, "FaceMarisa_lo", null));
			this.Conv.Add(new Conversation("出现了~可以邀请到Party的人类~", false, true, null, "FaceAmi05"));
			this.Conv.Add(new Conversation("哈？搞什么 原来是只妖精啊", true, false, "FaceMarisa_sw", null));
			this.Conv.Add(new Conversation("别飞那么快嘛 来参加热闹的Party吧！", false, true, null, "FaceAmi03"));
			this.Conv.Add(new Conversation("不好意思 我可是很忙的呐", true, false, "FaceMarisa_hp", null));
			this.Conv.Add(new Conversation("妨碍我的话会狠狠揍你的哦", true, false, "FaceMarisa_hp2", null));
			this.Conv.Add(new Conversation("你很忙吗？", false, true, null, "FaceAmi05"));
			this.Conv.Add(new Conversation("很忙的哟", true, false, "FaceMarisa_no", null));
			this.Conv.Add(new Conversation("啊啊…虽然有点早 不过现在就开始吧", false, true, null, "FaceAmi06"));
			this.Conv.Add(new Conversation("为大忙人准备的Party！", false, true, null, "FaceAmi04"));
			this.Conv.Add(new Conversation("呀咧呀咧…果然还是要揍一顿ZE~", true, false, "FaceMarisa_sw", null));
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
			if (base.Time == 10 && this.LastTime < base.Time)
			{
				this.LastTime = base.Time;
				base.StageData.ChangeBGM(".\\BGM\\Boss01.wav", 0, 0, 255, 754110, 3294270);
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
