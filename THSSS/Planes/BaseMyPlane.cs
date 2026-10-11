using System;
using System.Collections.Generic;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	// Token: 0x02000066 RID: 102
	public class BaseMyPlane : BaseObject
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00016AEC File Offset: 0x00014CEC
		// (set) Token: 0x0600035D RID: 861 RVA: 0x00016B03 File Offset: 0x00014D03
		public string Name { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00016B0C File Offset: 0x00014D0C
		// (set) Token: 0x0600035F RID: 863 RVA: 0x00016B23 File Offset: 0x00014D23
		public string WeaponType { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00016B2C File Offset: 0x00014D2C
		public string FullName
		{
			get
			{
				return this.Name + this.WeaponType;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00016B50 File Offset: 0x00014D50
		// (set) Token: 0x06000362 RID: 866 RVA: 0x00016B67 File Offset: 0x00014D67
		public bool SlowMove { get; private set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00016B70 File Offset: 0x00014D70
		// (set) Token: 0x06000364 RID: 868 RVA: 0x00016B87 File Offset: 0x00014D87
		public float HighSpeed { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00016B90 File Offset: 0x00014D90
		// (set) Token: 0x06000366 RID: 870 RVA: 0x00016BA7 File Offset: 0x00014DA7
		public float LowSpeed { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00016BB0 File Offset: 0x00014DB0
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00016BC7 File Offset: 0x00014DC7
		public new float Vx { get; private set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00016BD0 File Offset: 0x00014DD0
		// (set) Token: 0x0600036A RID: 874 RVA: 0x00016BE7 File Offset: 0x00014DE7
		public new float Vy { get; private set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00016BF0 File Offset: 0x00014DF0
		// (set) Token: 0x0600036C RID: 876 RVA: 0x00016C07 File Offset: 0x00014E07
		public int DeadTime { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00016C10 File Offset: 0x00014E10
		// (set) Token: 0x0600036E RID: 878 RVA: 0x00016C28 File Offset: 0x00014E28
		public int Life
		{
			get
			{
				return this.life;
			}
			set
			{
				if (value > 8)
				{
					this.life = 8;
				}
				else
				{
					this.life = value;
				}
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00016C58 File Offset: 0x00014E58
		// (set) Token: 0x06000370 RID: 880 RVA: 0x00016C70 File Offset: 0x00014E70
		public int Spell
		{
			get
			{
				return this.spell;
			}
			set
			{
				if (value > 8)
				{
					this.spell = 8;
				}
				else if (value < 0)
				{
					this.spell = 0;
				}
				else
				{
					this.spell = value;
				}
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00016CB4 File Offset: 0x00014EB4
		// (set) Token: 0x06000372 RID: 882 RVA: 0x00016CCC File Offset: 0x00014ECC
		public int LifeChip
		{
			get
			{
				return this.lifeChip;
			}
			set
			{
				this.lifeChip = value;
				while (this.lifeChip >= this.nextLifeChip[this.lifeUpCount])
				{
					this.Extend();
					this.lifeChip -= this.nextLifeChip[this.lifeUpCount];
					this.lifeUpCount++;
				}
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00016D30 File Offset: 0x00014F30
		// (set) Token: 0x06000374 RID: 884 RVA: 0x00016D48 File Offset: 0x00014F48
		public int SpellChip
		{
			get
			{
				return this.spellChip;
			}
			set
			{
				this.spellChip = value;
				while (this.spellChip >= this.nextSpellChip)
				{
					this.SpellExtand();
					this.spellChip -= this.nextSpellChip;
				}
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00016D90 File Offset: 0x00014F90
		// (set) Token: 0x06000376 RID: 886 RVA: 0x00016DA8 File Offset: 0x00014FA8
		public int Power
		{
			get
			{
				return this.power;
			}
			set
			{
				if (value > 400)
				{
					this.power = 400;
				}
				else if (value < 100)
				{
					this.power = 100;
				}
				else
				{
					this.power = value;
				}
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00016DF8 File Offset: 0x00014FF8
		public int PowerLevel
		{
			get
			{
				return this.power / 100;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00016E14 File Offset: 0x00015014
		// (set) Token: 0x06000379 RID: 889 RVA: 0x00016E2C File Offset: 0x0001502C
		public long Score
		{
			get
			{
				return this.score;
			}
			set
			{
				if (value > 9999999999L)
				{
					this.score = 9999999999L;
				}
				else if (value < 0L)
				{
					this.score = 0L;
				}
				else
				{
					this.score = value;
				}
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00016E84 File Offset: 0x00015084
		// (set) Token: 0x0600037B RID: 891 RVA: 0x00016EBC File Offset: 0x000150BC
		public long HiScore
		{
			get
			{
				long num;
				if (this.score > this.hiScore)
				{
					num = this.score;
				}
				else
				{
					num = this.hiScore;
				}
				return num;
			}
			set
			{
				if (value > 9999999999L)
				{
					this.hiScore = 9999999999L;
				}
				else if (value < 0L)
				{
					this.hiScore = 0L;
				}
				else
				{
					this.hiScore = value;
				}
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00016F14 File Offset: 0x00015114
		// (set) Token: 0x0600037D RID: 893 RVA: 0x00016F2C File Offset: 0x0001512C
		public int BluePoint
		{
			get
			{
				return this.bluePoint;
			}
			set
			{
				this.bluePoint = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00016F38 File Offset: 0x00015138
		// (set) Token: 0x0600037F RID: 895 RVA: 0x00016F50 File Offset: 0x00015150
		public int GreenPoint
		{
			get
			{
				return this.greenPoint;
			}
			set
			{
				this.greenPoint = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00016F5C File Offset: 0x0001515C
		// (set) Token: 0x06000381 RID: 897 RVA: 0x00016F74 File Offset: 0x00015174
		public int NextBluePoint
		{
			get
			{
				return this.nextBluePoint;
			}
			set
			{
				this.nextBluePoint = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00016F80 File Offset: 0x00015180
		// (set) Token: 0x06000383 RID: 899 RVA: 0x00016F98 File Offset: 0x00015198
		public int NextGreenPoint
		{
			get
			{
				return this.nextGreenPoint;
			}
			set
			{
				this.nextGreenPoint = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00016FA4 File Offset: 0x000151A4
		// (set) Token: 0x06000385 RID: 901 RVA: 0x00016FBC File Offset: 0x000151BC
		public int StarPoint
		{
			get
			{
				return this.starPoint;
			}
			set
			{
				if (value >= this.maxStarPoint)
				{
					if (this.starPoint < this.maxStarPoint)
					{
						if (this.LastColor == EnchantmentType.Red)
						{
							this.ChangeEnchantment(EnchantmentType.Red);
						}
						else if (this.LastColor == EnchantmentType.Blue)
						{
							this.ChangeEnchantment(EnchantmentType.Blue);
						}
						else if (this.LastColor == EnchantmentType.Green)
						{
							this.ChangeEnchantment(EnchantmentType.Green);
						}
						base.StageData.SoundPlay("se_focusrot.wav");
						BaseBackground baseBackground = new BaseBackground(base.StageData, "pl02b", base.Position, 0f, 0.0);
						baseBackground.ScaleVelocity = 1f;
						baseBackground.LifeTime = 26;
						baseBackground.TransparentVelocity = -10f;
						baseBackground.Active = false;
						baseBackground.ColorValue = Color.Black;
						BaseBackground baseBackground2 = new BaseBackground(base.StageData, "pl02b", base.Position, 0f, 0.0);
						baseBackground2.ScaleVelocity = 1f;
						baseBackground2.LifeTime = 26;
						baseBackground2.TransparentVelocity = -10f;
						baseBackground2.Active = false;
						baseBackground2.ColorValue = Color.Black;
						BaseBackground baseBackground3 = new BaseBackground(base.StageData, "pl02b", base.Position, 0f, 0.0);
						baseBackground3.ScaleVelocity = 1f;
						baseBackground3.LifeTime = 26;
						baseBackground3.TransparentVelocity = -10f;
						baseBackground3.Active = false;
						baseBackground3.ColorValue = Color.Black;
						new EnchantmentMagicCircle(base.StageData, base.OriginalPosition);
					}
					this.starPoint = this.maxStarPoint;
				}
				else if (value < 0)
				{
					this.starPoint = 0;
				}
				else
				{
					this.starPoint = value;
				}
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000386 RID: 902 RVA: 0x000171AC File Offset: 0x000153AC
		// (set) Token: 0x06000387 RID: 903 RVA: 0x000171C4 File Offset: 0x000153C4
		public EnchantmentType EnchantmentState
		{
			get
			{
				return this.enchantmentState;
			}
			set
			{
				this.enchantmentState = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000388 RID: 904 RVA: 0x000171D0 File Offset: 0x000153D0
		// (set) Token: 0x06000389 RID: 905 RVA: 0x000171E8 File Offset: 0x000153E8
		public EnchantmentType LastColor
		{
			get
			{
				return this.lastColor;
			}
			set
			{
				this.lastColor = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600038A RID: 906 RVA: 0x000171F4 File Offset: 0x000153F4
		// (set) Token: 0x0600038B RID: 907 RVA: 0x0001720C File Offset: 0x0001540C
		public int EnchantmentTime
		{
			get
			{
				return this.enchantmentTime;
			}
			set
			{
				if (value > 0)
				{
					this.enchantmentTime = value;
				}
				else
				{
					if (this.enchantmentTime > 0)
					{
						if (this.enchantmentTime == 1)
						{
							this.EnchantmentCount++;
							this.Rate += 0.1f;
							long num = 0L;
							if (this.enchantmentState == EnchantmentType.Blue)
							{
								num = (long)(this.HighItemScore * 20);
							}
							else if (this.enchantmentState == EnchantmentType.Green)
							{
								num = 1000000L;
							}
							else if (this.enchantmentState == EnchantmentType.Red)
							{
								num = 5120000L;
							}
							this.score += num;
							new EnchantmentBonus(base.StageData, num);
							base.StageData.SoundPlay("se_cardget.wav");
						}
						else
						{
							BaseEffect baseEffect = new BaseEffect(base.StageData, "pl02b", base.Position, 0f, 0.0);
							baseEffect.ScaleVelocity = 1f;
							baseEffect.LifeTime = 26;
							baseEffect.TransparentVelocity = -10f;
							baseEffect.Active = false;
							baseEffect.ColorValue = Color.Black;
							BaseEffect baseEffect2 = new BaseEffect(base.StageData, "pl02b", base.Position, 0f, 0.0);
							baseEffect2.ScaleVelocity = 1f;
							baseEffect2.LifeTime = 26;
							baseEffect2.TransparentVelocity = -10f;
							baseEffect2.Active = false;
							baseEffect2.ColorValue = Color.Black;
							BaseEffect baseEffect3 = new BaseEffect(base.StageData, "pl02b", base.Position, 0f, 0.0);
							baseEffect3.ScaleVelocity = 1f;
							baseEffect3.LifeTime = 26;
							baseEffect3.TransparentVelocity = -10f;
							baseEffect3.Active = false;
							baseEffect3.ColorValue = Color.Black;
						}
						if (this.enchantmentState == EnchantmentType.Green)
						{
							base.Time = this.UnmatchedTime - 60;
							this.DeadTime = -1;
							new SpellDisable(base.StageData);
						}
						this.StarPoint = 0;
						base.StageData.SoundPlay("ice005.wav");
						TextureObject textureObject = base.TextureObjectDictionary["White"];
						int num2 = 16;
						int num3 = 8;
						Color color = Color.White;
						if (this.enchantmentState == EnchantmentType.Blue)
						{
							color = Color.Blue;
						}
						else if (this.enchantmentState == EnchantmentType.Green)
						{
							color = Color.Green;
						}
						else if (this.enchantmentState == EnchantmentType.Red)
						{
							color = Color.Red;
						}
						for (int i = 0; i < num2; i++)
						{
							for (int j = 0; j < num3; j++)
							{
								Model_Triangle model_Triangle = new Model_Triangle(base.StageData.DeviceMain, textureObject, i.ToString() + "_" + j.ToString(), new Vector2((float)i / (float)num2, (float)j / (float)num3), new Vector2((float)(i + 1) / (float)num2, (float)j / (float)num3), new Vector2((float)i / (float)num2, (float)(j + 1) / (float)num3), 640 / num2, 480 / num3);
								BaseParticle3D baseParticle3D = new BaseParticle3D();
								baseParticle3D.StageData = base.StageData;
								baseParticle3D.Position = new PointF((float)(i * 640 / num2), (float)(j * 480 / num3));
								baseParticle3D.model = model_Triangle;
								baseParticle3D.Velocity = (float)base.Ran.Next(20) / 10f;
								baseParticle3D.LifeTime = 30;
								baseParticle3D.DirectionDegree = (double)base.Ran.Next(360);
								baseParticle3D.RotatingAxis = new Vector3((float)base.Ran.Next(-100, 100), (float)base.Ran.Next(-100, 100), (float)base.Ran.Next(-100, 100));
								baseParticle3D.AngularVelocity3D = (float)base.Ran.Next(20) / 100f;
								baseParticle3D.TransparentVelocity = -6f;
								baseParticle3D.ScaleVelocity = -0.005f;
								baseParticle3D.ColorValue = color;
								baseParticle3D.TransparentValueF = 150f;
								base.StageData.Particle3D.ParticleList.Add(baseParticle3D);
							}
						}
						for (int i = 0; i < num2; i++)
						{
							for (int j = 0; j < num3; j++)
							{
								Model_Triangle model_Triangle = new Model_Triangle(base.StageData.DeviceMain, textureObject, i.ToString() + "_" + j.ToString(), new Vector2((float)(i + 1) / (float)num2, (float)(j + 1) / (float)num3), new Vector2((float)i / (float)num2, (float)(j + 1) / (float)num3), new Vector2((float)(i + 1) / (float)num2, (float)j / (float)num3), 640 / num2, 480 / num3);
								BaseParticle3D baseParticle3D = new BaseParticle3D();
								baseParticle3D.StageData = base.StageData;
								baseParticle3D.Position = new PointF((float)((i + 1) * 640 / num2), (float)((j + 1) * 480 / num3));
								baseParticle3D.model = model_Triangle;
								baseParticle3D.Angle = 3.141592653589793;
								baseParticle3D.Velocity = (float)base.Ran.Next(20) / 10f;
								baseParticle3D.LifeTime = 30;
								baseParticle3D.DirectionDegree = (double)base.Ran.Next(360);
								baseParticle3D.RotatingAxis = new Vector3((float)base.Ran.Next(-100, 100), (float)base.Ran.Next(-100, 100), (float)base.Ran.Next(-100, 100));
								baseParticle3D.AngularVelocity3D = (float)base.Ran.Next(20) / 100f;
								baseParticle3D.TransparentVelocity = -6f;
								baseParticle3D.ScaleVelocity = -0.005f;
								baseParticle3D.ColorValue = color;
								baseParticle3D.TransparentValueF = 150f;
								base.StageData.Particle3D.ParticleList.Add(baseParticle3D);
							}
						}
					}
					this.enchantmentTime = 0;
					this.enchantmentState = EnchantmentType.None;
				}
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600038C RID: 908 RVA: 0x000178D0 File Offset: 0x00015AD0
		// (set) Token: 0x0600038D RID: 909 RVA: 0x000178E8 File Offset: 0x00015AE8
		public int EnchantmentCount
		{
			get
			{
				return this.enchantmentCount;
			}
			set
			{
				this.enchantmentCount = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600038E RID: 910 RVA: 0x000178F4 File Offset: 0x00015AF4
		// (set) Token: 0x0600038F RID: 911 RVA: 0x0001790C File Offset: 0x00015B0C
		public int EnchantmentCountNeeded
		{
			get
			{
				return this.enchantmentCountNeeded;
			}
			set
			{
				this.enchantmentCountNeeded = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00017918 File Offset: 0x00015B18
		public bool FscEnabled
		{
			get
			{
				return this.EnchantmentCount >= this.EnchantmentCountNeeded && base.Difficulty > DifficultLevel.Normal;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00017944 File Offset: 0x00015B44
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0001795C File Offset: 0x00015B5C
		public float Rate
		{
			get
			{
				return this.rate;
			}
			set
			{
				this.rate = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00017968 File Offset: 0x00015B68
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0001797F File Offset: 0x00015B7F
		public int HighItemScore { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000395 RID: 917 RVA: 0x00017988 File Offset: 0x00015B88
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0001799F File Offset: 0x00015B9F
		public int Graze { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000397 RID: 919 RVA: 0x000179A8 File Offset: 0x00015BA8
		public int UnmatchedTime
		{
			get
			{
				return 300;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000398 RID: 920 RVA: 0x000179C0 File Offset: 0x00015BC0
		// (set) Token: 0x06000399 RID: 921 RVA: 0x000179D7 File Offset: 0x00015BD7
		public TextureObject[] TxtureObjects { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600039A RID: 922 RVA: 0x000179E0 File Offset: 0x00015BE0
		// (set) Token: 0x0600039B RID: 923 RVA: 0x000179F7 File Offset: 0x00015BF7
		public PointF[] SubPlanePoint { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00017A00 File Offset: 0x00015C00
		// (set) Token: 0x0600039D RID: 925 RVA: 0x00017A17 File Offset: 0x00015C17
		public List<BaseSubPlane> SubPlaneList { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00017A20 File Offset: 0x00015C20
		// (set) Token: 0x0600039F RID: 927 RVA: 0x00017A37 File Offset: 0x00015C37
		public bool SpellEnabled { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00017A40 File Offset: 0x00015C40
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x00017A58 File Offset: 0x00015C58
		public int LifeUpCount
		{
			get
			{
				return this.lifeUpCount;
			}
			set
			{
				this.lifeUpCount = value;
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00017A8C File Offset: 0x00015C8C
		public BaseMyPlane()
		{
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00017BA4 File Offset: 0x00015DA4
		public BaseMyPlane(StageDataPackage StageData, Point Position)
		{
			base.StageData = StageData;
			base.Position = Position;
			this.TxtureObjects = new TextureObject[72];
			for (int i = 0; i < 72; i++)
			{
				this.TxtureObjects[i] = base.TextureObjectDictionary["MyPlane" + i.ToString("D3")];
			}
			base.TxtureObject = this.TxtureObjects[0];
			this.SubPlanePoint = new PointF[4];
			this.HighSpeed = 4.5f;
			this.LowSpeed = 2f;
			base.Region = 1;
			base.Time = this.UnmatchedTime;
			this.DeadTime = -1;
			this.SubPlaneList = new List<BaseSubPlane>
			{
				new BaseSubPlane(StageData, "MySubPlane", Position),
				new BaseSubPlane(StageData, "MySubPlane", Position),
				new BaseSubPlane(StageData, "MySubPlane", Position),
				new BaseSubPlane(StageData, "MySubPlane", Position)
			};
			this.Name = "Plane";
			this.WeaponType = "A";
			this.SpellEnabled = true;
			this.HighItemScore = 100000;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00017DE8 File Offset: 0x00015FE8
		public virtual void Refresh()
		{
			base.Time = 300;
			this.EnchantmentCount = 0;
			this.SubPlaneList.ForEach(delegate(BaseSubPlane x)
			{
				x.Refresh();
			});
			this.SpellEnabled = true;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00017E40 File Offset: 0x00016040
		public override void Move()
		{
			this.Vx = 0f;
			this.Vy = 0f;
			float num = base.OriginalPosition.X;
			float num2 = base.OriginalPosition.Y;
			this.SlowMove = base.KClass.Key_Shift;
			if (!this.SlowMove)
			{
				if (base.KClass.ArrowRight)
				{
					this.Vx += this.HighSpeed;
				}
				if (base.KClass.ArrowLeft)
				{
					this.Vx -= this.HighSpeed;
				}
				if (base.KClass.ArrowUp)
				{
					this.Vy += this.HighSpeed;
				}
				if (base.KClass.ArrowDown)
				{
					this.Vy -= this.HighSpeed;
				}
			}
			else
			{
				if (base.KClass.ArrowRight)
				{
					this.Vx += this.LowSpeed;
				}
				if (base.KClass.ArrowLeft)
				{
					this.Vx -= this.LowSpeed;
				}
				if (base.KClass.ArrowUp)
				{
					this.Vy += this.LowSpeed;
				}
				if (base.KClass.ArrowDown)
				{
					this.Vy -= this.LowSpeed;
				}
			}
			if (this.Vx != 0f && this.Vy != 0f)
			{
				this.Vx = 0.71f * this.Vx;
				this.Vy = 0.71f * this.Vy;
			}
			num += this.Vx;
			num2 -= this.Vy;
			if (num < (float)(base.TxtureObject.Width / 2 - 6))
			{
				num = (float)(base.TxtureObject.Width / 2 - 6);
			}
			else if (num > (float)(base.BoundRect.Width - base.TxtureObject.Width / 2 + 6))
			{
				num = (float)(base.BoundRect.Width - base.TxtureObject.Width / 2 + 6);
			}
			if (num2 < (float)(base.TxtureObject.Height / 2))
			{
				num2 = (float)(base.TxtureObject.Height / 2);
			}
			else if (num2 >= (float)(base.BoundRect.Height - base.TxtureObject.Height / 2 + 8))
			{
				num2 = (float)(base.BoundRect.Height - base.TxtureObject.Height / 2 + 8);
			}
			base.OriginalPosition = new PointF(num, num2);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00018148 File Offset: 0x00016348
		public virtual void SubPlaneCtrl()
		{
			if (base.KClass.Key_Shift)
			{
				switch (this.PowerLevel)
				{
				case 1:
					this.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 30f);
					break;
				case 2:
					this.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 15f, base.OriginalPosition.Y - 30f);
					this.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 15f, base.OriginalPosition.Y - 30f);
					break;
				case 3:
					this.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 30f);
					this.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 20f, base.OriginalPosition.Y - 20f);
					this.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 20f, base.OriginalPosition.Y - 20f);
					break;
				case 4:
					this.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 15f, base.OriginalPosition.Y - 40f);
					this.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 15f, base.OriginalPosition.Y - 40f);
					this.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 30f, base.OriginalPosition.Y - 30f);
					this.SubPlanePoint[3] = new PointF(base.OriginalPosition.X + 30f, base.OriginalPosition.Y - 30f);
					break;
				}
			}
			else
			{
				switch (this.PowerLevel)
				{
				case 1:
					this.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 40f);
					break;
				case 2:
					this.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 25f, base.OriginalPosition.Y - 20f);
					this.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 25f, base.OriginalPosition.Y - 20f);
					break;
				case 3:
					this.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 40f);
					this.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 30f, base.OriginalPosition.Y - 20f);
					this.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 30f, base.OriginalPosition.Y - 20f);
					break;
				case 4:
					this.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 25f, base.OriginalPosition.Y - 0f);
					this.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 25f, base.OriginalPosition.Y - 0f);
					this.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 50f, base.OriginalPosition.Y + 5f);
					this.SubPlanePoint[3] = new PointF(base.OriginalPosition.X + 50f, base.OriginalPosition.Y + 5f);
					break;
				}
			}
			for (int i = 0; i < this.PowerLevel; i++)
			{
				this.SubPlaneList[i].Enabled = true;
				this.SubPlaneList[i].DestPoint = this.SubPlanePoint[i];
				this.SubPlaneList[i].Ctrl();
			}
			for (int i = this.PowerLevel; i < this.SubPlaneList.Count; i++)
			{
				this.SubPlaneList[i].Enabled = false;
				this.SubPlaneList[i].Position = base.Position;
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001875C File Offset: 0x0001695C
		public virtual void TextureCtrl()
		{
			if (this.Vx > 0.5f)
			{
				if (this.SlowMove)
				{
					this.OnRotate = false;
					if (0 <= this.Angle3D && this.Angle3D < 30)
					{
						this.Angle3D += 5;
					}
					else if (this.Angle3D < 0)
					{
						this.Angle3D = 0;
					}
				}
				else if (!this.OnRotate)
				{
					if (0 <= this.Angle3D && this.Angle3D < 40)
					{
						this.Angle3D += 5;
					}
					else if (this.Angle3D < 0)
					{
						this.OnRotate = true;
					}
				}
				else
				{
					this.Angle3D += 13;
				}
			}
			else if (this.Vx < -0.5f)
			{
				if (this.SlowMove)
				{
					this.OnRotate = false;
					if (-30 < this.Angle3D && this.Angle3D <= 0)
					{
						this.Angle3D -= 5;
					}
					else if (this.Angle3D > 0)
					{
						this.Angle3D = 0;
					}
				}
				else if (!this.OnRotate)
				{
					if (-40 < this.Angle3D && this.Angle3D <= 0)
					{
						this.Angle3D -= 5;
					}
					else if (this.Angle3D > 0)
					{
						this.OnRotate = true;
					}
				}
				else
				{
					this.Angle3D -= 13;
				}
			}
			else
			{
				this.Angle3D /= 2;
				if (-10 < this.Angle3D && this.Angle3D < 10)
				{
					this.Angle3D = 0;
					this.OnRotate = false;
				}
			}
			if (this.Angle3D > 180)
			{
				this.Angle3D -= 360;
			}
			else if (this.Angle3D < -180)
			{
				this.Angle3D += 360;
			}
			int num = this.Angle3D / 5;
			if (num < 0)
			{
				num += 72;
			}
			else if (num > 72)
			{
				num -= 72;
			}
			base.TxtureObject = this.TxtureObjects[num];
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00018A04 File Offset: 0x00016C04
		public override void Shoot()
		{
			if (base.Time % 3 == 0)
			{
				base.StageData.SoundPlay("se_plst00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
				float x = base.Position.X;
				float y = base.Position.Y;
				float num = 7f;
				num *= (float)Math.Cos((double)this.Angle3D * 3.141592653589793 / 180.0);
				new BaseMyBullet(base.StageData, "自机子弹", new PointF(x - num, y), 30f, -1.5707963267948966);
				new BaseMyBullet(base.StageData, "自机子弹", new PointF(x + num, y), 30f, -1.5707963267948966);
			}
			if (base.Time % 5 == 0)
			{
				for (int i = 0; i < this.PowerLevel; i++)
				{
					this.SubPlaneList[i].Shoot();
				}
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00018B37 File Offset: 0x00016D37
		public virtual void SpellShoot()
		{
			new Spell(base.StageData);
			base.StageData.SoundPlay("se_nep00.wav");
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00018B88 File Offset: 0x00016D88
		public virtual void PreMiss()
		{
			if (base.Time >= this.UnmatchedTime)
			{
				if (base.Time >= this.DeadTime)
				{
					if (base.SpellList.Count == 0)
					{
						if (this.EnchantmentState == EnchantmentType.Green)
						{
							BulletRemover_Small bulletRemover_Small = new BulletRemover_Small(base.StageData, base.OriginalPosition);
							bulletRemover_Small.Region = 500;
							this.ChangeEnchantment(EnchantmentType.None);
							base.EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
							{
								if (x is BaseSpellCard)
								{
									((BaseSpellCard)x).Missed = true;
								}
							});
						}
						else
						{
							new Emitter(base.StageData, base.MyPlane.Position);
							base.StageData.SoundPlay("se_pldead00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
							this.DeadTime = base.Time + 20;
						}
					}
				}
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00018CA0 File Offset: 0x00016EA0
		public virtual void Miss()
		{
			if (this.EnchantmentState == EnchantmentType.Red || this.EnchantmentState == EnchantmentType.Blue)
			{
				this.ChangeEnchantment(EnchantmentType.None);
			}
			new BulletRemover(base.StageData, base.OriginalPosition);
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height + 100));
			this.Vx = 0f;
			this.Vy = 0f;
			this.Life--;
			this.Power -= 16;
			base.Time = 0;
			this.DeadTime = -1;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00018DBC File Offset: 0x00016FBC
		public override void Ctrl()
		{
			if (base.Enabled)
			{
				base.Time++;
				if (base.Time == this.DeadTime)
				{
					this.Miss();
				}
				else if (base.Time > this.DeadTime)
				{
					if (base.Time == 1)
					{
						if (this.Spell < 2)
						{
							this.Spell = 2;
						}
					}
					if (base.Time > 70)
					{
						this.Move();
					}
					else
					{
						base.DestPoint = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height - 30));
						base.Velocity = 2f;
						base.Move();
						this.TextureCtrl();
					}
					this.TextureCtrl();
					base.Direction += 0.019999999552965164;
					this.SubPlaneCtrl();
					this.EnchantmentTime--;
					if (this.EnchantmentState == EnchantmentType.Blue)
					{
						base.ItemList.ForEach(delegate(BaseItem x)
						{
							if (!(x is ShootingStarShard))
							{
								x.Obtain = true;
								x.Doubled = true;
							}
						});
					}
				}
				if (base.Story == null && base.Time > 70)
				{
					if (base.KClass.Key_Z)
					{
						if (base.TimeMain > 15)
						{
							this.Shoot();
						}
					}
					if (base.KClass.Key_X && !this.lastX)
					{
						if (this.EnchantmentState == EnchantmentType.Green)
						{
							BulletRemover_Small bulletRemover_Small = new BulletRemover_Small(base.StageData, base.OriginalPosition);
							bulletRemover_Small.Region = 500;
							this.ChangeEnchantment(EnchantmentType.None);
							base.EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
							{
								if (x is BaseSpellCard)
								{
									((BaseSpellCard)x).Bombed = true;
								}
							});
						}
						else if (this.Spell > 0 && base.SpellList.Count == 0 && base.Time > 30 && this.SpellEnabled)
						{
							this.SpellShoot();
							this.Spell--;
						}
					}
					this.lastX = base.KClass.Key_X;
				}
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00019058 File Offset: 0x00017258
		public void RetryClear()
		{
			this.EnchantmentTime = 0;
			this.Life = 2;
			this.Spell = 2;
			this.Power = 400;
			this.Score = (long)base.StageData.ContinueTimes;
			this.Graze = 0;
			this.HiScore = base.StageData.S_History[0].Score;
			this.LifeChip = 0;
			this.SpellChip = 0;
			this.LifeUpCount = 0;
			this.StarPoint = 0;
			this.HighItemScore = 100000;
			this.Rate = 0f;
			this.LastColor = EnchantmentType.Green;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00019104 File Offset: 0x00017304
		public void Extend()
		{
			if (this.Life < 8)
			{
				this.Life++;
			}
			else
			{
				this.Spell++;
			}
			base.StageData.SoundPlay("se_extend.wav");
			BaseEffect baseEffect = new BaseEffect(base.StageData, "Extend", new PointF(0f, 0f), 0f, 1.5707963267948966)
			{
				Active = false,
				OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), 112f),
				LifeTime = 90,
				TransparentValueF = 0f,
				Layer = 1
			};
			baseEffect.TransparentVelocityDictionary.Add(1, 13f);
			baseEffect.TransparentVelocityDictionary.Add(70, -13f);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000191F4 File Offset: 0x000173F4
		public void SpellExtand()
		{
			base.StageData.SoundPlay("se_cardget.wav");
			this.Spell++;
			BaseEffect baseEffect = new BaseEffect(base.StageData, "SpellExtend", new PointF(0f, 0f), 0f, 1.5707963267948966)
			{
				Active = false,
				OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), 80f),
				LifeTime = 90,
				TransparentValueF = 0f,
				Layer = 1
			};
			baseEffect.TransparentVelocityDictionary.Add(1, 13f);
			baseEffect.TransparentVelocityDictionary.Add(70, -13f);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000192C0 File Offset: 0x000174C0
		public void ChangeEnchantment(EnchantmentType EnchantmentState)
		{
			switch (EnchantmentState)
			{
			case EnchantmentType.None:
				this.EnchantmentTime = 0;
				break;
			case EnchantmentType.Red:
				this.EnchantmentState = EnchantmentState;
				this.EnchantmentTime = 600;
				break;
			case EnchantmentType.Blue:
				this.EnchantmentState = EnchantmentState;
				this.EnchantmentTime = 600;
				break;
			case EnchantmentType.Green:
				this.EnchantmentState = EnchantmentState;
				this.EnchantmentTime = 420;
				break;
			default:
				this.EnchantmentTime = 0;
				break;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00019340 File Offset: 0x00017540
		public override void Show()
		{
			if (base.Time >= this.DeadTime)
			{
				if (base.Time != 0)
				{
					if (base.Time < this.UnmatchedTime && base.Time / 2 % 2 == 0)
					{
						base.SpriteMain.Draw2D(base.TxtureObject, 1f, 0f, base.Position, Color.DarkBlue);
					}
					else
					{
						base.SpriteMain.Draw2D(base.TxtureObject, 1f, 0f, base.Position, Color.White);
					}
					for (int i = 0; i < this.PowerLevel; i++)
					{
						this.SubPlaneList[i].Show();
					}
				}
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00019424 File Offset: 0x00017624
		public virtual void ShowCenter()
		{
			if (this.SlowMove)
			{
				base.SpriteMain.Draw2D(base.TextureObjectDictionary["MyPlaneCenter1"], 1f, (float)base.Direction * 4f, base.Position, Color.White);
			}
		}

		// Token: 0x04000100 RID: 256
		private const int unmatchedTime = 300;

		// Token: 0x04000101 RID: 257
		private const int deltaGreen = 120;

		// Token: 0x04000102 RID: 258
		private int life = 2;

		// Token: 0x04000103 RID: 259
		private int spell = 2;

		// Token: 0x04000104 RID: 260
		private int lifeChip = 0;

		// Token: 0x04000105 RID: 261
		private int spellChip = 0;

		// Token: 0x04000106 RID: 262
		public readonly int nextSpellChip = 4;

		// Token: 0x04000107 RID: 263
		public readonly int[] nextLifeChip = new int[] { 3, 5, 8, 10, 12, 15, 18, 20, 22 };

		// Token: 0x04000108 RID: 264
		private int lifeUpCount = 0;

		// Token: 0x04000109 RID: 265
		private int power = 100;

		// Token: 0x0400010A RID: 266
		private long score = 0L;

		// Token: 0x0400010B RID: 267
		private long hiScore = 0L;

		// Token: 0x0400010C RID: 268
		private bool lastX = false;

		// Token: 0x0400010D RID: 269
		private int starPoint = 0;

		// Token: 0x0400010E RID: 270
		public readonly int maxStarPoint = 3000;

		// Token: 0x0400010F RID: 271
		private int nextBluePoint = 300;

		// Token: 0x04000110 RID: 272
		private int nextGreenPoint = 3000;

		// Token: 0x04000111 RID: 273
		private int bluePoint = 0;

		// Token: 0x04000112 RID: 274
		private int greenPoint = 0;

		// Token: 0x04000113 RID: 275
		private int blueLevel = 0;

		// Token: 0x04000114 RID: 276
		private int greenLevel = 0;

		// Token: 0x04000115 RID: 277
		private EnchantmentType enchantmentState = EnchantmentType.None;

		// Token: 0x04000116 RID: 278
		private EnchantmentType lastColor = EnchantmentType.Green;

		// Token: 0x04000117 RID: 279
		private int enchantmentTime;

		// Token: 0x04000118 RID: 280
		private float rate = 1f;

		// Token: 0x04000119 RID: 281
		private int enchantmentCount = 0;

		// Token: 0x0400011A RID: 282
		private int enchantmentCountNeeded = 0;

		// Token: 0x0400011B RID: 283
		private bool OnRotate = false;

		// Token: 0x0400011C RID: 284
		private int Angle3D = 0;
	}
}
