using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Shooting
{
	// Token: 0x02000124 RID: 292
	public class PlayerData
	{
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00047610 File Offset: 0x00045810
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x00047627 File Offset: 0x00045827
		private FileStream ScoreFile { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00047630 File Offset: 0x00045830
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x00047647 File Offset: 0x00045847
		public bool PlaneAEnabled { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00047650 File Offset: 0x00045850
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x00047667 File Offset: 0x00045867
		public bool PlaneBEnabled { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00047670 File Offset: 0x00045870
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x00047687 File Offset: 0x00045887
		public bool UltraEnabled { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00047690 File Offset: 0x00045890
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x000476A7 File Offset: 0x000458A7
		public bool ExtraEnabled { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x000476B0 File Offset: 0x000458B0
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x000476C7 File Offset: 0x000458C7
		public string PlayerName { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x000476D0 File Offset: 0x000458D0
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x000476E7 File Offset: 0x000458E7
		public bool[] MusicState { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x000476F0 File Offset: 0x000458F0
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x00047707 File Offset: 0x00045907
		public List<ScoreHistory> S_History { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00047710 File Offset: 0x00045910
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x00047727 File Offset: 0x00045927
		public List<ClearHistory> C_History { get; set; }

		// Token: 0x060007C0 RID: 1984 RVA: 0x00047730 File Offset: 0x00045930
		public PlayerData()
		{
			this.CreatNewData();
			if (!File.Exists("Score.dat"))
			{
				this.SaveData();
			}
			else
			{
				try
				{
					this.LoadData();
				}
				catch
				{
					MessageBox.Show("读取分数记录错误。", "Score Error");
				}
			}
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0004779C File Offset: 0x0004599C
		private void CreatNewData()
		{
			this.PlayerName = "";
			this.MusicState = new bool[15];
			this.S_History = new List<ScoreHistory>();
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					for (int k = 0; k < 5; k++)
					{
						string text;
						switch (k)
						{
						case 0:
							text = "ReimuA";
							break;
						case 1:
							text = "MarisaA";
							break;
						case 2:
							text = "SanaeA";
							break;
						case 3:
							text = "KoishiA";
							break;
						case 4:
							text = "PlaneA";
							break;
						default:
							text = "AyaA";
							break;
						}
						ScoreHistory scoreHistory = new ScoreHistory
						{
							MyPlaneFullName = text,
							Rank = (DifficultLevel)i,
							PlayerName = "-------",
							Score = (long)((10 - j) * 100000),
							Date = "----/--/--",
							Time = "--:--",
							Stage = "---",
							SlowRate = "-.-%"
						};
						this.S_History.Add(scoreHistory);
					}
				}
			}
			this.C_History = new List<ClearHistory>();
			for (int i = 0; i < 6; i++)
			{
				for (int k = 0; k < 5; k++)
				{
					string text;
					switch (k)
					{
					case 0:
						text = "ReimuA";
						break;
					case 1:
						text = "MarisaA";
						break;
					case 2:
						text = "SanaeA";
						break;
					case 3:
						text = "KoishiA";
						break;
					case 4:
						text = "PlaneA";
						break;
					default:
						text = "AyaA";
						break;
					}
					ClearHistory clearHistory = new ClearHistory
					{
						MyPlaneFullName = text,
						Rank = (DifficultLevel)i,
						StartTimes = 0,
						PlayingTime = 0L,
						ClearTimes = 0,
						NoContinueClearTimes = 0,
						PracticeLevel = 1
					};
					this.C_History.Add(clearHistory);
				}
			}
			this.SC_History = new List<SpellCardHistory>();
			for (int k = 0; k < 5; k++)
			{
				int num = 1;
				string text;
				switch (k)
				{
				case 0:
					text = "ReimuA";
					break;
				case 1:
					text = "MarisaA";
					break;
				case 2:
					text = "SanaeA";
					break;
				case 3:
					text = "KoishiA";
					break;
				case 4:
					text = "PlaneA";
					break;
				default:
					text = "AyaA";
					break;
				}
				for (int i = 0; i < 6; i++)
				{
					if (i < 2)
					{
						for (int j = 0; j < PlayerData.SCNameEN.Length; j++)
						{
							SpellCardHistory spellCardHistory = new SpellCardHistory
							{
								MyPlaneFullName = text,
								Rank = (DifficultLevel)i,
								Name = PlayerData.SCNameEN[j],
								Index = num++,
								Recorded = 0,
								History = 0
							};
							this.SC_History.Add(spellCardHistory);
						}
					}
					else if (i < 5)
					{
						for (int j = 0; j < PlayerData.SCNameHL.Length; j++)
						{
							SpellCardHistory spellCardHistory = new SpellCardHistory
							{
								MyPlaneFullName = text,
								Rank = (DifficultLevel)i,
								Name = PlayerData.SCNameHL[j],
								Index = num++,
								Recorded = 0,
								History = 0
							};
							this.SC_History.Add(spellCardHistory);
						}
					}
					else
					{
						for (int j = 0; j < PlayerData.SCNameEx.Length; j++)
						{
							SpellCardHistory spellCardHistory = new SpellCardHistory
							{
								MyPlaneFullName = text,
								Rank = (DifficultLevel)i,
								Name = PlayerData.SCNameEx[j],
								Index = num++,
								Recorded = 0,
								History = 0
							};
							this.SC_History.Add(spellCardHistory);
						}
					}
				}
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00047C5C File Offset: 0x00045E5C
		public void SaveData()
		{
			this.ScoreFile = new FileStream("Score.dat", FileMode.Create);
			string tmp = null;
			tmp = tmp + this.PlayerName + "\r\n";
			this.SC_History.ForEach(delegate(SpellCardHistory x)
			{
				tmp += x.Data2String();
			});
			this.S_History.ForEach(delegate(ScoreHistory x)
			{
				tmp += x.Data2String();
			});
			this.C_History.ForEach(delegate(ClearHistory x)
			{
				tmp += x.Data2String();
			});
			tmp = Encryption.Encrypt(tmp);
			StreamWriter streamWriter = new StreamWriter(this.ScoreFile);
			streamWriter.Write(tmp);
			streamWriter.Close();
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00047DB8 File Offset: 0x00045FB8
		public void LoadData()
		{
			string[] array = File.ReadAllLines("Score.dat", Encoding.Default);
			array[0] = Encryption.Decrypt(array[0]);
			array[0] = array[0].Replace("\r\n", "\n");
			string[] stringList = array[0].Split(new char[] { '\n' });
			int i = 0;
			this.PlayerName = stringList[i++];
			this.SC_History.ForEach(delegate(SpellCardHistory x)
			{
				x.String2Data(stringList[i++]);
			});
			this.S_History.ForEach(delegate(ScoreHistory x)
			{
				x.String2Data(stringList[i++]);
			});
			this.C_History.ForEach(delegate(ClearHistory x)
			{
				x.String2Data(stringList[i++]);
			});
			this.CheckEnable();
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00047EB8 File Offset: 0x000460B8
		public void CheckEnable()
		{
			this.C_History.ForEach(delegate(ClearHistory x)
			{
				if (x.NoContinueClearTimes > 0)
				{
					this.ExtraEnabled = true;
				}
			});
		}

		// Token: 0x040002AF RID: 687
		private const string FileName = "Score.dat";

		// Token: 0x040002B0 RID: 688
		public List<SpellCardHistory> SC_History;

		// Token: 0x040002B1 RID: 689
		private static string[] SCNameEN = new string[]
		{
			"星符「夏夜的晚星」", "四叶「大自然之花环」", "魔矢「无限之箭」", "天诏「琴引山之诏琴」", "使魔「自动狩猎装置」", "天箭「星之箭」", "火符「逼近的火焰」", "火光「火精灵之灾」", "木灵「大屋津姬命」", "妖符「柳下妖蝉」",
			"绿符「柳叶风暴」", "魔镜「幻影之境」", "镜符「月季色红梦裂隙」", "阴阳「神鬼一击」", "辉针「闪耀万针剑」", "镜符「山吹色波纹裂隙」", "流光「幻影魔炮」", "星符「急速彗星」", "镜符「瑠璃色银河裂隙」", "岚符「大孔雀暴风」",
			"秘术「遗忘之封印」", "镜符「若竹色翠石裂隙」", "空想「恋之宣告」", "「古明地恋的消失」", "虹奥义「光幕结界」", "杀箭「新星爆炸箭」", "火舞「夏夜流火」", "机关「火焰发射装置」", "混沌「守护的心意」", "焰符「原始的烈焰」",
			"「星神降临仪式」", "许愿星「星火辉迹」", "耀星「白夜永昼之光」", "星雨「故乡之星」", "星图「宇宙棋盘」", "「弹幕之海」", "「流星神话」"
		};

		// Token: 0x040002B2 RID: 690
		private static string[] SCNameHL = new string[]
		{
			"微风「摇曳的三叶草」", "星符「夏夜的晚星」", "四叶「大自然之花环」", "夜风「轮回的幸运之星」", "魔矢「轮回的万箭」", "异器「琴引山之神弓」", "魔狩「星夜的狩猎舞曲」", "华箭「紫星之花」", "秘术「龙卷旋风之箭」", "火符「燃烧地狱火海」",
			"火光「午夜的大火灾」", "木魅「枛津姬命」", "妖符「柳下妖蝉」", "深绿「舞动的翠绿之风」", "叶符「德律阿得斯的圣枝」", "魔镜「虚无幽幻之境」", "镜符「月季色红梦裂隙」", "阴阳「神鬼一击」", "辉针「闪耀万针剑」", "镜符「山吹色波纹裂隙」",
			"流光「幻影魔炮」", "星符「急速彗星」", "镜符「瑠璃色银河裂隙」", "岚符「大孔雀暴风」", "秘术「遗忘之封印」", "镜符「若竹色翠石裂隙」", "空想「恋之宣告」", "「古明地恋的消失」", "虹奥义「光幕结界」", "境界「三原色」",
			"杀箭「新星爆炸箭」", "火舞「夏夜流火」", "焰机「星夜的火焰舞曲」", "混沌「守护之心」", "焰符「八百里的烈焰」", "「星神降临仪式」", "烈焰「焚秽净世」", "许愿星「星火辉迹」", "耀星「白夜永昼之光」", "星雨「故乡之星」",
			"星图「宇宙棋盘」", "「弹幕之海」", "「流星神话」", "第一个愿望「无尽之财」", "第二个愿望「无穷之力」", "第三个愿望「永生不灭」"
		};

		// Token: 0x040002B3 RID: 691
		private static string[] SCNameEx = new string[]
		{
			"青符「东之国度的树叶」", "柳符「新生的柳叶绿芽」", "蝶恋「千年之愿」", "风符「环形气流」", "速风「双龙卷」", "风切「狂风交错」", "舞风「阵风螺旋」", "飓风「台风眼」", "天岚「逆卷神风」", "「雾隐流剑技 ～ 暴风疾斩」",
			"绽放「爱意的玫瑰」", "幻花「仙晶玉莲」", "萱草「忘却之爱」", "荣符「生长与枯萎的循环」", "花咲「百花缭乱」", "花符「云上的花田」", "「雾隐流剑技 ～ 血染之樱」", "冬临「初雪之寒径」", "仙器「太乙冰凌」", "霜冻「寒霜封印」",
			"米雪「冷云雪晶」", "六花「冰结界的雪花阵」", "雪幕「冰雪流凌」", "「雾隐流剑技 ～ 落雪凝霜」", "迴梦「月色琉璃光」", "幽光「幻月蝶」", "倒映「天之月水之月」", "仙幻「月葬」", "净符「月光世界」", "皎符「星月炮」",
			"「雾隐流剑技 ～ 天地银蛇」", "仙境「风花雪月」", "仙蝶「无限的彩灵」", "「未定」", "「雾隐之蝶」"
		};
	}
}
