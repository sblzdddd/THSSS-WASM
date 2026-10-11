using System;
using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS03_01B : BaseStory_SSS
	{
		public Story_SSS03_01B(StageDataPackage StageData)
			: base(StageData)
		{
			this.CharR = new CharacterR_Touhou(StageData, "FaceSeiryuu_no")
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
			this.Conv.Add(new Conversation("碎片掉到这片密林里去了呢…", true, false, "FaceMarisa_sp", "FaceSeiryuu_sp"));
			this.Conv.Add(new Conversation("不过话说这附近的树木也太茂密了吧…不方便收集呢", true, false, "FaceMarisa_lo", null));
			this.Conv.Add(new Conversation("不赶紧到视野好一点的地方去的话……", true, false, "FaceMarisa_lo", null));
			this.Conv.Add(new Conversation("你是…人类？", false, true, null, "FaceSeiryuu_sp"));
			this.Conv.Add(new Conversation("暂时还是~", true, false, "FaceMarisa_hp", null));
			this.Conv.Add(new Conversation("人类为何要来这偏远的柳林呢？", false, true, null, "FaceSeiryuu_no"));
			this.Conv.Add(new Conversation("本来是顺着魔力来收集星星碎片的说…然后…", true, false, "FaceMarisa_no2", null));
			this.Conv.Add(new Conversation("迷路了~", true, false, "FaceMarisa_hp2", null));
			this.Conv.Add(new Conversation("哈？", false, true, null, "FaceSeiryuu_sw"));
			this.Conv.Add(new Conversation("那个 方便的话 可以指个路么ZE~", true, false, "FaceMarisa_hp", null));
			this.Conv.Add(new Conversation("诶嘿~可以的哟~", false, true, null, "FaceSeiryuu_no"));
			this.Conv.Add(new Conversation("不过作为交换", false, true, null, "FaceSeiryuu_hp"));
			this.Conv.Add(new Conversation("把你手中的流星碎片留下吧", false, true, null, "FaceSeiryuu_dp"));
			this.Conv.Add(new Conversation("只有这个不可以呢", true, false, "FaceMarisa_no", null));
			this.Conv.Add(new Conversation("你该不会一开始就看准了这个吧？", true, false, "FaceMarisa_sw", null));
			this.Conv.Add(new Conversation("柳树的妖怪？", true, false, "FaceMarisa_sw", null));
			this.Conv.Add(new Conversation("被识破了吗？", false, true, null, "FaceSeiryuu_no"));
			this.Conv.Add(new Conversation("你也不是简单的人类啊", false, true, null, "FaceSeiryuu_dp"));
			this.Conv.Add(new Conversation("人类少女怎么会孤零零地来这偏远的柳林", true, false, "FaceMarisa_no", null));
			this.Conv.Add(new Conversation("把你手中的八卦炉作为交换也是可以的哦~", false, true, null, "FaceSeiryuu_sp"));
			this.Conv.Add(new Conversation("这个也不行！", true, false, "FaceMarisa_an", null));
			this.Conv.Add(new Conversation("是吗？那么……", false, true, null, "FaceSeiryuu_dp"));
			this.Conv.Add(new Conversation("就把你的命留下吧！", false, true, null, "FaceSeiryuu_sw"));
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 16 && this.LastTime < base.Time)
			{
				this.CharN = new CharacterName(base.StageData, "ename_Seiryuu");
				new StoryEmitterStar(base.StageData, this.CharN.Position, 0f, 0.0);
				this.LastTime = base.Time;
			}
			else if (base.Time == 21 && this.LastTime < base.Time)
			{
				base.StageData.ChangeBGM(".\\BGM\\Boss03.wav", 0, 0, 255, 1286397, 6275430);
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
