using System;
using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class Story_SSS02_01C : BaseStory_SSS
	{
		public Story_SSS02_01C(StageDataPackage StageData)
			: base(StageData)
		{
			this.CharR = new CharacterR_Touhou(StageData, "FaceRakuki_no")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF((float)(base.BoundRect.Width - 60), 380f)
			};
			this.CharL = new CharacterL_Touhou(StageData, "FaceSanae_an")
			{
				TxtureObject2 = base.TextureObjectDictionary[" "],
				OriginalPosition = new PointF(70f, 380f)
			};
			this.SBox = new StoryBox(StageData);
			this.Conv.Add(new Conversation("前方的区域禁止进入", false, true, "FaceSanae_an", "FaceRakuki_no"));
			this.Conv.Add(new Conversation("你是谁 为什么要阻拦我？", true, false, "FaceSanae_an", "FaceRakuki_no"));
			this.Conv.Add(new Conversation("我叫洛熙", false, true, null, "FaceRakuki_an2"));
			this.Conv.Add(new Conversation("你是人类？回去吧 这些与你无关", false, true, null, "FaceRakuki_no"));
			this.Conv.Add(new Conversation("果然你们在谋划些什么", true, false, "FaceSanae_no", null));
			this.Conv.Add(new Conversation("我是现人神 可不能坐视不管", true, false, "FaceSanae_no2", null));
			this.Conv.Add(new Conversation("现人神也是一样", false, true, null, "FaceRakuki_an2"));
			this.Conv.Add(new Conversation("只要争取到足够的时间的话", false, true, null, "FaceRakuki_dp"));
			this.Conv.Add(new Conversation("我们自然会离开这里", false, true, null, "FaceRakuki_no2"));
			this.Conv.Add(new Conversation("这怎么可以！", true, false, "FaceSanae_sw", null));
			this.Conv.Add(new Conversation("再放任这些魔力的流星不管的话", true, false, "FaceSanae_sw", null));
			this.Conv.Add(new Conversation("各路妖怪们都会暴动的", true, false, "FaceSanae_sw", null));
			this.Conv.Add(new Conversation("妖怪会如何与我人类何干？", false, true, null, "FaceRakuki_no"));
			this.Conv.Add(new Conversation("看来只能强行突破了", true, false, "FaceSanae_an", null));
			this.Conv.Add(new Conversation("来吧 弹幕对战！", true, false, "FaceSanae_an2", null));
			this.Conv.Add(new Conversation("天真！", false, true, null, "FaceRakuki_an"));
			this.Conv.Add(new Conversation("你真以为你能逃过我的箭雨吗？", false, true, null, "FaceRakuki_an"));
			this.Conv.Add(new Conversation("在这把琴引山的神弓下", false, true, null, "FaceRakuki_dp"));
			this.Conv.Add(new Conversation("去死吧！", false, true, null, "FaceRakuki_bk"));
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 2 && this.LastTime < base.Time)
			{
				this.LastTime = base.Time;
				this.CharN = new CharacterName(base.StageData, "ename_Rakuki");
				new StoryEmitterStar(base.StageData, this.CharN.Position, 0f, 0.0);
			}
			if (base.Time == 17 && this.LastTime < base.Time)
			{
				base.StageData.ChangeBGM(".\\BGM\\Boss02.wav", 0, 0, 255, 327663, 3504627);
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
