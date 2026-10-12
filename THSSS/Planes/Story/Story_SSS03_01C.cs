using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS03_01C : BaseStory_SSS
	{
		public Story_SSS03_01C(StageDataPackage StageData)
			: base(StageData)
		{
			this.CharR = new CharacterR_Touhou(StageData, "FaceSeiryuu_no")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF((float)(base.BoundRect.Width - 60), 380f)
			};
			this.CharL = new CharacterL_Touhou(StageData, "FaceSanae_no")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF(70f, 380f)
			};
			this.SBox = new StoryBox(StageData);
			this.Conv.Add(new Conversation("不知不觉已经进入密林当中了吗？", true, false, "FaceSanae_no", "FaceSeiryuu_sp"));
			this.Conv.Add(new Conversation("呜……视线都被遮挡住了 这样就没法看到流星了呢", true, false, "FaceSanae_lo", null));
			this.Conv.Add(new Conversation("不赶紧飞上树林上方的话……", true, false, "FaceSanae_sw", null));
			this.Conv.Add(new Conversation("咦？是人类吗", false, true, null, "FaceSeiryuu_sp"));
			this.Conv.Add(new Conversation("人类为何会来这片偏远的柳林？", false, true, null, "FaceSeiryuu_hp"));
			this.Conv.Add(new Conversation("那个 我是来调查这些流星碎片的", true, false, "FaceSanae_no2", null));
			this.Conv.Add(new Conversation("总觉得要是收集到这份力量的话", true, false, "FaceSanae_no", null));
			this.Conv.Add(new Conversation("信仰也会增加的吧", true, false, "FaceSanae_no", null));
			this.Conv.Add(new Conversation("是这样啊~", false, true, null, "FaceSeiryuu_hp"));
			this.Conv.Add(new Conversation("的确呢 能够感受到这些星星里面包含着充足的魔力", false, true, null, "FaceSeiryuu_dp"));
			this.Conv.Add(new Conversation("你知道是谁在制造这些流星吗？", true, false, "FaceSanae_no", null));
			this.Conv.Add(new Conversation("不知道呢", false, true, null, "FaceSeiryuu_no"));
			this.Conv.Add(new Conversation("不过如果吸收掉的话", false, true, null, "FaceSeiryuu_hp"));
			this.Conv.Add(new Conversation("我的力量也会增长的吧", false, true, null, "FaceSeiryuu_sw"));
			this.Conv.Add(new Conversation("你是妖怪？", true, false, "FaceSanae_sp", null));
			this.Conv.Add(new Conversation("你觉得会有孤零零的人类少女来到这偏远的地方吗", false, true, null, "FaceSeiryuu_sw"));
			this.Conv.Add(new Conversation("你也在收集流星碎片吗？", true, false, "FaceSanae_an2", null));
			this.Conv.Add(new Conversation("原来是竞争对手呢", true, false, "FaceSanae_an", null));
			this.Conv.Add(new Conversation("区区人类也要与妖怪竞争吗？被小看了啊", false, true, null, "FaceSeiryuu_dp"));
			this.Conv.Add(new Conversation("怎么说我也是存在了千年的妖怪了", false, true, null, "FaceSeiryuu_dp"));
			this.Conv.Add(new Conversation("妖怪的厉害之处", false, true, null, "FaceSeiryuu_sw"));
			this.Conv.Add(new Conversation("就让你好好体会一下吧！", false, true, null, "FaceSeiryuu_sw"));
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 15 && this.LastTime < base.Time)
			{
				this.CharN = new CharacterName(base.StageData, "ename_Seiryuu");
				new StoryEmitterStar(base.StageData, this.CharN.Position, 0f, 0.0);
				this.LastTime = base.Time;
			}
			else if (base.Time == 20 && this.LastTime < base.Time)
			{
				base.StageData.ChangeBGM(".\\BGM\\Boss03.ogg", 0, 0, 255, 1286397, 6275430);
				this.LastTime = base.Time;
			}
			else if (base.Time == this.Conv.Count)
			{
				MusicTitle musicTitle = new MusicTitle(base.StageData, "青柳传说", new Point(base.BoundRect.Width, base.BoundRect.Height - 16));
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
