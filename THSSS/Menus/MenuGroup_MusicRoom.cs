using System;
using System.Collections.Generic;
using System.Drawing;
using Shooting.Planes.Story;

namespace Shooting
{
	// Token: 0x020000B8 RID: 184
	internal class MenuGroup_MusicRoom : BaseMenuGroup
	{
		// Token: 0x06000546 RID: 1350 RVA: 0x0002CC14 File Offset: 0x0002AE14
		public MenuGroup_MusicRoom(StageDataPackage StageData)
			: base(StageData)
		{
			base.MenuSelectIndex = 0;
			base.MenuItemList = new List<BaseMenuItem>
			{
				new BaseMenuItem(StageData, "被遗忘的久远星辉"),
				new BaseMenuItem(StageData, "宁静夏夜的微风"),
				new BaseMenuItem(StageData, "喧闹吧！在这不眠之夜"),
				new BaseMenuItem(StageData, "灯火竹林"),
				new BaseMenuItem(StageData, "疾风闪电"),
				new BaseMenuItem(StageData, "木灵们的夏夜祭"),
				new BaseMenuItem(StageData, "青柳传说"),
				new BaseMenuItem(StageData, "万花世界的光与影"),
				new BaseMenuItem(StageData, "镜中的幻象"),
				new BaseMenuItem(StageData, "记忆中遥远的星星"),
				new BaseMenuItem(StageData, "引燃夜空的星火"),
				new BaseMenuItem(StageData, "银河的彼方"),
				new BaseMenuItem(StageData, "？？？？？"),
				new BaseMenuItem(StageData, "闪耀在世界尽头"),
				new BaseMenuItem(StageData, "晚星之梦"),
				new BaseMenuItem(StageData, "璀璨无比的星空"),
				new BaseMenuItem(StageData, "来自仙界的新风"),
				new BaseMenuItem(StageData, "风中花，雪中月")
			};
			int num = 100;
			int num2 = 100;
			foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
			{
				baseMenuItem.Position = new PointF((float)num, 90f);
				baseMenuItem.DestPoint = new PointF((float)num, (float)num2);
				baseMenuItem.MaxVelocity = 100f;
				num2 += 30;
			}
			base.MenuItemList[base.MenuSelectIndex].Selected = true;
			this.MenuTitlePos1 = new PointF(140f, -30f);
			this.MenuTitlePos2 = new PointF(140f, -150f);
			base.MenuTilte = new BaseMenuItem(StageData, "MenuTitle_MusicRoom")
			{
				Position = this.MenuTitlePos2
			};
			base.TransparentValueF = 0f;
			base.TransparentVelocity = 5f;
			base.TxtureObject = base.TextureObjectDictionary["MenuBackground"];
			base.Scale = 0.625f;
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2));
			base.AngleDegree = 90.0;
			base.ColorValue = Color.SlateBlue;
			this.DescriptionBox = new DescriptionBox(StageData)
			{
				MaxTransparent = 0,
				OffsetX = 50
			};
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0002CF18 File Offset: 0x0002B118
		public override void SelectItemChanged()
		{
			base.SelectItemChanged();
			int num = 100;
			int num2 = 100;
			int num3 = (base.MenuSelectIndex - 6) * 30;
			if (num3 < 0)
			{
				num3 = 0;
			}
			foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
			{
				baseMenuItem.DestPoint = new PointF((float)num, (float)(num2 - num3));
				num2 += 30;
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0002CFB0 File Offset: 0x0002B1B0
		public override void Ctrl()
		{
			base.Ctrl();
			this.DescriptionBox.Ctrl();
			foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
			{
				if (baseMenuItem.DestPoint.Y < 80f || baseMenuItem.DestPoint.Y > 296f)
				{
					baseMenuItem.MaxTransparent -= 15;
				}
				else
				{
					baseMenuItem.MaxTransparent += 15;
				}
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0002D074 File Offset: 0x0002B274
		public override void ProcessKeys()
		{
			base.ProcessKeys();
			if (base.KClass.Key_Z && this.LastZ == 0)
			{
				StageDataPackage stageDataPackage = new StageDataPackage(base.StageData.GlobalData);
				string name = base.MenuItemList[base.MenuSelectIndex].Name;
				switch (name)
				{
				case "被遗忘的久远星辉":
				{
					this.DescriptionBox.Text = "东方惯例的标题曲。  ※作曲：文件";
					DescriptionBox descriptionBox = this.DescriptionBox;
					descriptionBox.Text = descriptionBox.Text + '\n' + "在东方标题曲特有的旋律上进行了改动，";
					DescriptionBox descriptionBox2 = this.DescriptionBox;
					descriptionBox2.Text = descriptionBox2.Text + '\n' + "不过这样一来旋律似乎就不明显了。";
					DescriptionBox descriptionBox3 = this.DescriptionBox;
					descriptionBox3.Text = descriptionBox3.Text + '\n' + "整体风格略为阴暗，似乎和积极向上（？）的主题不符？";
					base.StageData.ChangeBGM(".\\BGM\\OP.wav", 0, 0, 255, 604611, 6047874);
					goto IL_0E6D;
				}
				case "宁静夏夜的微风":
				{
					this.DescriptionBox.Text = "1面的主题曲。      ※作曲：11";
					DescriptionBox descriptionBox4 = this.DescriptionBox;
					descriptionBox4.Text = descriptionBox4.Text + '\n' + "因为是夏夜的微风，所以开头做成了格外静谧的感觉。";
					DescriptionBox descriptionBox5 = this.DescriptionBox;
					descriptionBox5.Text = descriptionBox5.Text + '\n' + "这样的开始之后，才是渐渐地进入了游戏。";
					DescriptionBox descriptionBox6 = this.DescriptionBox;
					descriptionBox6.Text = descriptionBox6.Text + '\n' + "旋律是怎么写出来的，其实我也不知道。仅仅是在脑中";
					DescriptionBox descriptionBox7 = this.DescriptionBox;
					descriptionBox7.Text = descriptionBox7.Text + '\n' + "突然出现了这段后赶紧记录下来罢了，效果却意外的好呢。";
					base.StageData.ChangeBGM(".\\BGM\\Stage01.wav", 88200, 0, 255, 0, 0);
					goto IL_0E6D;
				}
				case "喧闹吧！在这不眠之夜":
				{
					this.DescriptionBox.Text = "娅米的主题曲。      ※作曲：文件";
					DescriptionBox descriptionBox8 = this.DescriptionBox;
					descriptionBox8.Text = descriptionBox8.Text + '\n' + "按照1面的感觉做出的曲子。";
					DescriptionBox descriptionBox9 = this.DescriptionBox;
					descriptionBox9.Text = descriptionBox9.Text + '\n' + "虽然连贯性不错，但是听起来并不像1面Boss？";
					DescriptionBox descriptionBox10 = this.DescriptionBox;
					descriptionBox10.Text = descriptionBox10.Text + '\n' + "妖精好萌。";
					base.StageData.ChangeBGM(".\\BGM\\Boss01.wav", 44100, 0, 255, 754110, 3294270);
					goto IL_0E6D;
				}
				case "灯火竹林":
				{
					this.DescriptionBox.Text = "2面的主题曲。      ※作曲：11";
					DescriptionBox descriptionBox11 = this.DescriptionBox;
					descriptionBox11.Text = descriptionBox11.Text + '\n' + "一首自始至终都很悠闲自由的曲子，尤其是在旋律上";
					DescriptionBox descriptionBox12 = this.DescriptionBox;
					descriptionBox12.Text = descriptionBox12.Text + '\n' + "随心所欲的发挥。在悠闲之余，还延续了前一面的静谧。";
					DescriptionBox descriptionBox13 = this.DescriptionBox;
					descriptionBox13.Text = descriptionBox13.Text + '\n' + "整首曲子莫名其妙的就做成了钢琴曲。";
					DescriptionBox descriptionBox14 = this.DescriptionBox;
					descriptionBox14.Text = descriptionBox14.Text + '\n' + "不过好像怎么样都不会让人联想到竹林啊。";
					base.StageData.ChangeBGM(".\\BGM\\Stage02.wav", 88200, 0, 255, 1194228, 6866370);
					goto IL_0E6D;
				}
				case "疾风闪电":
				{
					this.DescriptionBox.Text = "桃雨 洛熙的主题曲。 ※作曲：文件";
					DescriptionBox descriptionBox15 = this.DescriptionBox;
					descriptionBox15.Text = descriptionBox15.Text + '\n' + "悠闲的2面之后接着就是这样紧迫的曲子，真的没问题吗。";
					DescriptionBox descriptionBox16 = this.DescriptionBox;
					descriptionBox16.Text = descriptionBox16.Text + '\n' + "虽然的确有Boss的风范，但是听起来并不像2面Boss？";
					DescriptionBox descriptionBox17 = this.DescriptionBox;
					descriptionBox17.Text = descriptionBox17.Text + '\n' + "不过弹幕倒是和曲子一样紧迫。";
					base.StageData.ChangeBGM(".\\BGM\\Boss02.wav", 44100, 0, 255, 327663, 3504627);
					goto IL_0E6D;
				}
				case "木灵们的夏夜祭":
				{
					this.DescriptionBox.Text = "3面的主题曲。      ※作曲：11";
					DescriptionBox descriptionBox18 = this.DescriptionBox;
					descriptionBox18.Text = descriptionBox18.Text + '\n' + "进入三面，游戏也要逐渐热闹起来了。";
					DescriptionBox descriptionBox19 = this.DescriptionBox;
					descriptionBox19.Text = descriptionBox19.Text + '\n' + "在旋律上下了不少功夫，试着把钢琴做成了行云流水的样子";
					DescriptionBox descriptionBox20 = this.DescriptionBox;
					descriptionBox20.Text = descriptionBox20.Text + '\n' + "在后面的风琴上，也尽量往旋律优美的方向去走。";
					DescriptionBox descriptionBox21 = this.DescriptionBox;
					descriptionBox21.Text = descriptionBox21.Text + '\n' + "才刚刚到三面而已啊，所以压制了一下过于激烈的感觉。";
					base.StageData.ChangeBGM(".\\BGM\\Stage03.wav", 88200, 0, 255, 88200, 5606874);
					goto IL_0E6D;
				}
				case "青柳传说":
				{
					this.DescriptionBox.Text = "夏 青柳的主题曲。   ※作曲：11";
					DescriptionBox descriptionBox22 = this.DescriptionBox;
					descriptionBox22.Text = descriptionBox22.Text + '\n' + "突出了远离尘世、不沾俗气的纯净风格。气势也做的很大。";
					DescriptionBox descriptionBox23 = this.DescriptionBox;
					descriptionBox23.Text = descriptionBox23.Text + '\n' + "试着使用了古风的元素，不过不是纯粹的古风。";
					DescriptionBox descriptionBox24 = this.DescriptionBox;
					descriptionBox24.Text = descriptionBox24.Text + '\n' + "面对这样一个设定，怎么能不倾注进感情来作曲呢？";
					DescriptionBox descriptionBox25 = this.DescriptionBox;
					descriptionBox25.Text = descriptionBox25.Text + '\n' + "于是，努力把这首曲子编成了更加有故事、有情感的曲子。";
					base.StageData.ChangeBGM(".\\BGM\\Boss03.wav", 44100, 0, 255, 1585395, 6571341);
					goto IL_0E6D;
				}
				case "万花世界的光与影":
				{
					base.StageData.ChangeBGM(".\\BGM\\Stage04.wav", 88200, 0, 255, 575990, 7775976);
					this.DescriptionBox.Text = "４面的主题曲。      ※作曲：文件";
					DescriptionBox descriptionBox26 = this.DescriptionBox;
					descriptionBox26.Text = descriptionBox26.Text + '\n' + "按照地灵殿的风格做的曲子（并没有说像地灵殿）。";
					DescriptionBox descriptionBox27 = this.DescriptionBox;
					descriptionBox27.Text = descriptionBox27.Text + '\n' + "虽然被多人吐槽说跑调（？）。";
					DescriptionBox descriptionBox28 = this.DescriptionBox;
					descriptionBox28.Text = descriptionBox28.Text + '\n' + "我不写半音了jojo！";
					goto IL_0E6D;
				}
				case "镜中的幻象":
				{
					base.StageData.ChangeBGM(".\\BGM\\Boss04.wav", 44100, 0, 255, 463491, 7541982);
					this.DescriptionBox.Text = "魅明魔 影的主题曲。 ※作曲：11";
					DescriptionBox descriptionBox29 = this.DescriptionBox;
					descriptionBox29.Text = descriptionBox29.Text + '\n' + "要做这首曲子的时候，首先在我脑中出现的是阴沉和激昂的";
					DescriptionBox descriptionBox30 = this.DescriptionBox;
					descriptionBox30.Text = descriptionBox30.Text + '\n' + "两种风格，于是直接混在一起了。";
					DescriptionBox descriptionBox31 = this.DescriptionBox;
					descriptionBox31.Text = descriptionBox31.Text + '\n' + "似乎加一些优美的旋律会被鬼畜旋律衬托得更优美一点？";
					DescriptionBox descriptionBox32 = this.DescriptionBox;
					descriptionBox32.Text = descriptionBox32.Text + '\n' + "三拍子太难了。";
					goto IL_0E6D;
				}
				case "记忆中遥远的星星":
				{
					base.StageData.ChangeBGM(".\\BGM\\Stage05.wav", 44100, 0, 255, 66150, 0);
					this.DescriptionBox.Text = "５面的主题曲。      ※作曲：文件";
					DescriptionBox descriptionBox33 = this.DescriptionBox;
					descriptionBox33.Text = descriptionBox33.Text + '\n' + "有好音源却用辣鸡音源是一种什么样的心态？";
					DescriptionBox descriptionBox34 = this.DescriptionBox;
					descriptionBox34.Text = descriptionBox34.Text + '\n' + "其实最后的贝斯部分是在偷懒。";
					goto IL_0E6D;
				}
				case "引燃夜空的星火":
				{
					base.StageData.ChangeBGM(".\\BGM\\Boss05.wav", 44100, 0, 255, 1000056, 6599345);
					this.DescriptionBox.Text = "桃雨 落薰的主题曲。 ※作曲：文件";
					DescriptionBox descriptionBox35 = this.DescriptionBox;
					descriptionBox35.Text = descriptionBox35.Text + '\n' + "历经三个版本若干小时才弄出来的曲子。";
					DescriptionBox descriptionBox36 = this.DescriptionBox;
					descriptionBox36.Text = descriptionBox36.Text + '\n' + " 然而时间和质量却成反比。";
					DescriptionBox descriptionBox37 = this.DescriptionBox;
					descriptionBox37.Text = descriptionBox37.Text + '\n' + "这一定是杰拉鲁星人的阴谋（确信）。";
					goto IL_0E6D;
				}
				case "银河的彼方":
				{
					base.StageData.ChangeBGM(".\\BGM\\Stage06.wav", 88200, 0, 255, 0, 6220746);
					this.DescriptionBox.Text = "６面的主题曲。      ※作曲：11";
					DescriptionBox descriptionBox38 = this.DescriptionBox;
					descriptionBox38.Text = descriptionBox38.Text + '\n' + "渲染星空的宁静的气氛的一首曲子。";
					DescriptionBox descriptionBox39 = this.DescriptionBox;
					descriptionBox39.Text = descriptionBox39.Text + '\n' + "刚开始是阴沉的曲调，之后瞬间展开的感觉。";
					DescriptionBox descriptionBox40 = this.DescriptionBox;
					descriptionBox40.Text = descriptionBox40.Text + '\n' + "有一段旋律居然用了四个调，真是神奇。";
					DescriptionBox descriptionBox41 = this.DescriptionBox;
					descriptionBox41.Text = descriptionBox41.Text + '\n' + "会让人想起流星雨的吧？激光流星雨什么的……";
					goto IL_0E6D;
				}
				case "？？？？？":
				{
					base.StageData.ChangeBGM(".\\BGM\\Boss06.wav", 44100, 0, 255, 1697409, 10789065);
					this.DescriptionBox.Text = "梦璃夜 天星的主题曲 ※作曲：11";
					DescriptionBox descriptionBox42 = this.DescriptionBox;
					descriptionBox42.Text = descriptionBox42.Text + '\n' + "想要做成有在梦中游荡的感觉的曲子，";
					DescriptionBox descriptionBox43 = this.DescriptionBox;
					descriptionBox43.Text = descriptionBox43.Text + '\n' + "毕竟，星空似乎常常和梦幻联系在一起吧？";
					DescriptionBox descriptionBox44 = this.DescriptionBox;
					descriptionBox44.Text = descriptionBox44.Text + '\n' + "据说是透露着哀思的旋律呢……";
					DescriptionBox descriptionBox45 = this.DescriptionBox;
					descriptionBox45.Text = descriptionBox45.Text + '\n' + "最终Boss的激烈感与星空的静谧与梦幻融合在一起的感觉。";
					goto IL_0E6D;
				}
				case "闪耀在世界尽头":
				{
					base.StageData.ChangeBGM(".\\BGM\\Boss06FSC.wav", 88200, 0, 255, 0, 0);
					this.DescriptionBox.Text = "梦璃夜 天星的FSC曲  ※作曲：11";
					DescriptionBox descriptionBox46 = this.DescriptionBox;
					descriptionBox46.Text = descriptionBox46.Text + '\n' + "Final SpellCard的激烈的曲子。";
					DescriptionBox descriptionBox47 = this.DescriptionBox;
					descriptionBox47.Text = descriptionBox47.Text + '\n' + "开头之后，是不断的反复和升调。";
					DescriptionBox descriptionBox48 = this.DescriptionBox;
					descriptionBox48.Text = descriptionBox48.Text + '\n' + "啊啊啊，战斗就这样结束了吗？";
					goto IL_0E6D;
				}
				case "来自仙界的新风":
				{
					base.StageData.ChangeBGM(".\\BGM\\StageEx.wav", 44100, 0, 255, 1341963, 7621362);
					this.DescriptionBox.Text = "Extra关卡的主题曲。 ※作曲：11";
					DescriptionBox descriptionBox49 = this.DescriptionBox;
					descriptionBox49.Text = descriptionBox49.Text + '\n' + "与东方传统的Ex曲子相比，是更为轻松的一首曲子，少了一";
					DescriptionBox descriptionBox50 = this.DescriptionBox;
					descriptionBox50.Text = descriptionBox50.Text + '\n' + "些紧迫感。但是旋律也丝毫没有松懈下来，曲速也不慢。";
					DescriptionBox descriptionBox51 = this.DescriptionBox;
					descriptionBox51.Text = descriptionBox51.Text + '\n' + "在主线关卡的激烈战斗之后，偶尔用这首曲子来轻松一下也";
					DescriptionBox descriptionBox52 = this.DescriptionBox;
					descriptionBox52.Text = descriptionBox52.Text + '\n' + "不是坏事吧。但是不知道游戏中的弹幕允不允许呢？";
					goto IL_0E6D;
				}
				case "风中花，雪中月":
				{
					base.StageData.ChangeBGM(".\\BGM\\BossEx.wav", 44100, 0, 255, 1125873, 10783332);
					this.DescriptionBox.Text = "雾隐 梨花的主题曲。 ※作曲：11";
					DescriptionBox descriptionBox53 = this.DescriptionBox;
					descriptionBox53.Text = descriptionBox53.Text + '\n' + "试着做成一首充满着“东之国”的风格的曲子。";
					DescriptionBox descriptionBox54 = this.DescriptionBox;
					descriptionBox54.Text = descriptionBox54.Text + '\n' + "与青柳相似的曲风，但这首要更加欢快一些。想要让曲子更";
					DescriptionBox descriptionBox55 = this.DescriptionBox;
					descriptionBox55.Text = descriptionBox55.Text + '\n' + "能打动人心，但似乎很难感受到来自ExBoss的压力呢。";
					DescriptionBox descriptionBox56 = this.DescriptionBox;
					descriptionBox56.Text = descriptionBox56.Text + '\n' + "一定是书生的错。";
					goto IL_0E6D;
				}
				case "晚星之梦":
				{
					base.StageData.ChangeBGM(".\\BGM\\ED.wav", 44100, 0, 0, 0, 0);
					this.DescriptionBox.Text = "Ending画面的主题曲  ※作曲：11";
					DescriptionBox descriptionBox57 = this.DescriptionBox;
					descriptionBox57.Text = descriptionBox57.Text + '\n' + "东方惯例的ED曲，东方惯例的音色。";
					DescriptionBox descriptionBox58 = this.DescriptionBox;
					descriptionBox58.Text = descriptionBox58.Text + '\n' + "十分安静的曲子。";
					DescriptionBox descriptionBox59 = this.DescriptionBox;
					descriptionBox59.Text = descriptionBox59.Text + '\n' + "然后这就完全不是东方风了吧？";
					goto IL_0E6D;
				}
				case "璀璨无比的星空":
				{
					base.StageData.ChangeBGM(".\\BGM\\Staff.wav", 44100, 0, 0, 0, 0);
					this.DescriptionBox.Text = "Staff画面的主题曲。 ※作曲：文件";
					DescriptionBox descriptionBox60 = this.DescriptionBox;
					descriptionBox60.Text = descriptionBox60.Text + '\n' + "本来是想加上1~6boss全部旋律的solo部分，然而并不能做";
					DescriptionBox descriptionBox61 = this.DescriptionBox;
					descriptionBox61.Text = descriptionBox61.Text + '\n' + "到，惊了。于是最后只加入了1~3boss要素。";
					DescriptionBox descriptionBox62 = this.DescriptionBox;
					descriptionBox62.Text = descriptionBox62.Text + '\n' + "一定是我太菜了，接下来人像/狂人的姿势/余烬会很有用。";
					goto IL_0E6D;
				}
				}
				base.StageData.StateSwitchData = new StateSwitchDataPackage
				{
					NextState = "此功能尚未开通",
					NeedInit = true,
					SDPswitch = base.StageData
				};
				IL_0E6D:;
			}
			if ((base.KClass.Key_X || base.KClass.Key_ESC) && this.LastX == 0)
			{
				this.OnRemoveMenu = base.TimeMain + 20;
				base.TransparentVelocity = -15f;
				this.DescriptionBox.TransparentVelocity = -15f;
				foreach (BaseMenuItem baseMenuItem in base.MenuItemList)
				{
					baseMenuItem.DestPoint = new PointF(100f, 60f);
					baseMenuItem.OnRemove = true;
				}
				base.StageData.SoundPlay("se_cancel00.wav");
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0002DFC4 File Offset: 0x0002C1C4
		public override void Show()
		{
			base.Show();
			this.DescriptionBox.Show();
		}

		// Token: 0x040001C8 RID: 456
		private DescriptionBox DescriptionBox;
	}
}
