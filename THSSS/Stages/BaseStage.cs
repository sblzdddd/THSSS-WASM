using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Shooting.Properties;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x0200005D RID: 93
	internal partial class BaseStage : BaseGameState, IGameState
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00014154 File Offset: 0x00012354
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0001416B File Offset: 0x0001236B
		private byte Slowflag { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00014174 File Offset: 0x00012374
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0001418B File Offset: 0x0001238B
		protected bool RoadFlag { get; set; }

		// Token: 0x0600030D RID: 781 RVA: 0x00014194 File Offset: 0x00012394
		public BaseStage(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			this.InitializeComponent();
			global::System.Drawing.Font font = new global::System.Drawing.Font("Arial", 14f, FontStyle.Bold);
			base.DXFont = new SlimDX.Direct3D9.Font(base.DeviceMain, font);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000141E4 File Offset: 0x000123E4
		public override void Init()
		{
			base.BoundRect = new Rectangle(this.GameRegion_Main.Location, this.GameRegion_Main.Size);
			Point point = new Point(base.BoundRect.Width / 2, base.BoundRect.Bottom - 50);
			if (base.MyPlane == null)
			{
				base.MyPlane = new MyPlane_Aya(base.StageData, point);
			}
			base.Boss = null;
			base.BulletList = new List<BaseBullet_Touhou>();
			base.EnemyPlaneList = new List<BaseEnemyPlane>();
			base.MyBulletList = new List<BaseObject>();
			base.SpellList = new List<BaseObject>();
			base.EffectList = new List<BaseEffect>();
			base.ItemList = new List<BaseItem>();
			base.InterfaceList = new List<BaseObject>
			{
				new StarPointLineG(base.StageData, new PointF(66f, 404f)),
				new StarPointLineB(base.StageData, new PointF(-6f, 404f)),
				new StarPointLineR(base.StageData, new PointF(-6f, 404f)),
				new BaseInterface(base.StageData, "TotalBackgroundHalf", new Point(0, 0))
				{
					Delay = 0
				},
				new BossLife_Line(base.StageData, "表示符卡数量的星星（单个）", new PointF((float)(base.BoundRect.Left + 65), (float)(base.BoundRect.Top + 22))),
				new HP_Line(base.StageData, "HP_Line1", new PointF((float)(base.BoundRect.Left + base.BoundRect.Width / 2 + 18), (float)(base.BoundRect.Top + 13))),
				new SpellClock(base.StageData),
				new MyLife_Line(base.StageData, base.MyPlane.TxtureObject, this.gameRegion_LifeSelf.Location)
				{
					Delay = 30
				},
				new Item_Line_LifeChip(base.StageData, new Point(this.gameRegion_LifeSelf.Location.X + 82, this.gameRegion_LifeSelf.Location.Y + 11))
				{
					Delay = 35
				},
				new MySpell_Line(base.StageData, "Bomb", this.gameRegion_SpellLine.Location)
				{
					Delay = 40
				},
				new Item_Line_SpellChip(base.StageData, new Point(this.gameRegion_SpellLine.Location.X + 82, this.gameRegion_SpellLine.Location.Y + 11))
				{
					Delay = 45
				},
				new BaseInterface(base.StageData, "最高得分", this.gameRegion_HiScore.Location)
				{
					Delay = 10
				},
				new BaseInterface(base.StageData, "InterfaceLine00", new Point(this.gameRegion_HiScore.Location.X + 60, this.gameRegion_HiScore.Location.Y + 16))
				{
					Delay = 15
				},
				new BaseInterface(base.StageData, "得分", this.gameRegion_Score.Location)
				{
					Delay = 20
				},
				new BaseInterface(base.StageData, "InterfaceLine01", new Point(this.gameRegion_Score.Location.X + 60, this.gameRegion_Score.Location.Y + 16))
				{
					Delay = 25
				},
				new BaseInterface(base.StageData, "剩余人数", this.gameRegion_Player.Location)
				{
					Delay = 30
				},
				new BaseInterface(base.StageData, "InterfaceLine02", new Point(this.gameRegion_Player.Location.X + 60, this.gameRegion_Player.Location.Y + 22))
				{
					Delay = 35
				},
				new BaseInterface(base.StageData, "SC", this.gameRegion_Spell.Location)
				{
					Delay = 40
				},
				new BaseInterface(base.StageData, "InterfaceLine03", new Point(this.gameRegion_Spell.Location.X + 60, this.gameRegion_Spell.Location.Y + 22))
				{
					Delay = 45
				},
				new BaseInterface(base.StageData, "灵力", this.gameRegion_Power.Location)
				{
					Delay = 70
				},
				new BaseInterface(base.StageData, "InterfaceLine04", new Point(this.gameRegion_Spell.Location.X + 60, this.gameRegion_Power.Location.Y + 16))
				{
					Delay = 75
				},
				new BaseInterface(base.StageData, "最大得点", this.gameRegion_BluePoint.Location)
				{
					Delay = 80
				},
				new BaseInterface(base.StageData, "InterfaceLine05", new Point(this.gameRegion_Spell.Location.X + 60, this.gameRegion_BluePoint.Location.Y + 16))
				{
					Delay = 85
				},
				new BaseInterface(base.StageData, "擦弹", new Point(this.gameRegion_Graze.Location.X, this.gameRegion_GreenPoint.Location.Y))
				{
					Delay = 90
				},
				new Item_Line_HiScore(base.StageData, new Point(this.gameRegion_HiScoreValue.Location.X - 20, this.gameRegion_HiScoreValue.Location.Y))
				{
					Delay = 10
				},
				new Item_Line_Score(base.StageData, new Point(this.gameRegion_ScoreValue.Location.X - 20, this.gameRegion_ScoreValue.Location.Y))
				{
					Delay = 20
				},
				new Item_Line_Red(base.StageData, new Point(this.gameRegion_Power.Location.X + 50, this.gameRegion_Power.Location.Y))
				{
					Delay = 70
				},
				new Item_Line_Blue(base.StageData, new Point(this.gameRegion_BluePoint.Location.X + 50, this.gameRegion_BluePoint.Location.Y))
				{
					Delay = 80
				},
				new Item_Line_Gray(base.StageData, new Point(this.gameRegion_Graze.Location.X + 50, this.gameRegion_GreenPoint.Location.Y))
				{
					Delay = 90
				},
				new BossMark(base.StageData),
				new InterfaceDifficulty(base.StageData, new PointF((float)((base.BoundRect.Right + 640) / 2), 24f))
				{
					Delay = 110
				}
			};
			if (base.StageData.Difficulty > DifficultLevel.Normal)
			{
				base.InterfaceList.Add(new BaseInterface(base.StageData, "BD", new Point(this.gameRegion_Spell.Location.X, this.gameRegion_Spell.Location.Y + 26))
				{
					Delay = 50
				});
				base.InterfaceList.Add(new MyBorder_Line(base.StageData, "Border", new Point(this.gameRegion_SpellLine.Location.X, this.gameRegion_SpellLine.Location.Y + 26))
				{
					Delay = 50
				});
			}
			base.SoundPlayList = new List<XAudio2_Player>();
			base.Background = new BackgroundManager();
			base.Background3D = new BackgroundManager3D(base.StageData);
			base.Particle3D = new ParticleManager3D(base.StageData);
			base.TimeMain = 0;
			base.StageData.Ran = new MyRandom(0);
			base.StageData.C_History = base.StageData.PData.C_History.Find(new Predicate<ClearHistory>(base.StageData.FindClear));
			base.StageData.S_History = base.StageData.PData.S_History.FindAll(new Predicate<ScoreHistory>(base.StageData.FindScore));
			if (!base.StageData.OnReplay)
			{
				base.StageData.C_History.StartTimes++;
			}
			base.MyPlane.HiScore = base.StageData.S_History[0].Score;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00014CC4 File Offset: 0x00012EC4
		public override void UpdateData()
		{
			if (base.StageData.OnReplay)
			{
				if (base.KClass.Key_minus)
				{
					this.Slowflag += 1;
					if (this.Slowflag < 4)
					{
						return;
					}
					this.Slowflag = 0;
				}
			}
			base.UpdateData();
			if (base.StageData.SlowMode > 0)
			{
				base.StageData.SlowMode--;
				if (base.TimeMain % 2 == 0)
				{
					return;
				}
			}
			if (base.KClass.Key_ESC)
			{
				base.StateSwitchData = new StateSwitchDataPackage
				{
					NextState = "PauseMenu",
					NeedInit = true,
					SDPswitch = new StageDataPackage(base.GlobalData)
				};
				base.GlobalData.LastState = this;
				base.StageData.SoundPlay("se_pause.wav");
			}
			if (base.StageData.Rep != null)
			{
				if (base.StageData.OnReplay)
				{
					int num = base.StageData.Rep.ReadKey();
					if (num == 57358)
					{
						num = base.StageData.Rep.ReadKey();
					}
					if (num == 4080)
					{
						base.StateSwitchData = new StateSwitchDataPackage
						{
							NextState = "GameOverMenu",
							NeedInit = true,
							SDPswitch = new StageDataPackage(base.GlobalData)
						};
						base.GlobalData.LastState = this;
						base.StageData.KClass.Hex2Key(0);
						base.StageData.SoundPlay("se_pause.wav");
					}
					else
					{
						base.StageData.KClass.Hex2Key(num);
					}
				}
				else
				{
					base.StageData.Rep.WriteKey(base.StageData.KClass.Key2Hex());
				}
			}
			base.Background3D.Ctrl();
			base.Background.Ctrl();
			base.Background2.Ctrl();
			this.Drama();
			if (base.Story != null)
			{
				base.Story.Ctrl();
			}
			for (int i = base.MyBulletList.Count - 1; i >= 0; i--)
			{
				base.MyBulletList[i].Ctrl();
			}
			base.MyPlane.Ctrl();
			for (int i = base.SpellList.Count - 1; i >= 0; i--)
			{
				base.SpellList[i].Ctrl();
			}
			for (int i = base.EnemyPlaneList.Count - 1; i >= 0; i--)
			{
				base.EnemyPlaneList[i].Ctrl();
			}
			for (int i = base.BulletList.Count - 1; i >= 0; i--)
			{
				base.BulletList[i].Ctrl();
			}
			if (base.Boss != null)
			{
				base.Boss.Ctrl();
			}
			for (int i = base.EffectList.Count - 1; i >= 0; i--)
			{
				base.EffectList[i].Ctrl();
			}
			base.Particle3D.Ctrl();
			for (int i = base.ItemList.Count - 1; i >= 0; i--)
			{
				base.ItemList[i].Ctrl();
			}
			base.StageData.Vibrate();
			this.HitCheckAllinOne();
			for (int i = base.InterfaceList.Count - 1; i >= 0; i--)
			{
				base.InterfaceList[i].Ctrl();
			}
			base.SoundPlayList.ForEach(delegate(XAudio2_Player x)
			{
				x.Play();
			});
			if (base.StageData.OnReplay)
			{
				if (base.TimeMain % 6 != 0 && base.KClass.Key_plus && base.MyPlane.Life >= 0 && base.StateSwitchData == null)
				{
					this.UpdateData();
				}
			}
			base.GlobalData.LastState = this;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000151AC File Offset: 0x000133AC
		public virtual void HitCheckAllinOne()
		{
			if (base.MyPlane.Life < 0)
			{
				base.StateSwitchData = new StateSwitchDataPackage
				{
					NextState = "GameOverMenu",
					NeedInit = true,
					SDPswitch = new StageDataPackage(base.GlobalData)
				};
			}
			base.ItemList.ForEach(delegate(BaseItem x)
			{
				x.HitCheckAll();
			});
			if (base.Story == null)
			{
				base.EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
				{
					x.HitCheckAll();
				});
				base.BulletList.ForEach(delegate(BaseBullet_Touhou x)
				{
					x.HitCheckAll();
				});
				if (base.Boss != null)
				{
					base.Boss.HitCheckAll();
				}
				base.SpellList.ForEach(delegate(BaseObject x)
				{
					x.HitCheckAll();
				});
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000152DC File Offset: 0x000134DC
		public virtual void ShowInterface()
		{
			if (base.MyPlane != null)
			{
				base.MyPlane.ShowCenter();
			}
			base.SpriteMain.Draw2D(base.TextureObjectDictionary["TotalBackground"], 1f, 0f, new PointF(320f, 240f), 255);
			base.DrawText(base.FPS.ToString("F1"), new Point(580, 460));
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00015368 File Offset: 0x00013568
		public virtual void Drama()
		{
			if (!base.StageData.OnReplay)
			{
				base.StageData.C_History.PlayingTime += 1L;
			}
			if (base.TimeMain == 1)
			{
				if (base.StageData.StateSwitchData == null)
				{
					base.StateSwitchData = new StateSwitchDataPackage
					{
						NextState = "MusicLoading",
						NeedInit = true,
						SDPswitch = new StageDataPackage(base.GlobalData)
					};
				}
				base.GlobalData.LastState = this;
				base.StageData.CurrentStageName = base.StageName;
				this.RoadFlag = false;
				base.EffectList.Clear();
				base.MyPlane.Refresh();
				base.Ran = new MyRandom(0);
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00015638 File Offset: 0x00013838
		public override void Render()
		{
			base.GlobalData.ScreenTexMan.Begin();
			base.Background3D.Show();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			base.DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			base.Background2.Show(true);
			base.SpriteMain.End();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			TextureObject textureObject = base.TextureObjectDictionary["White"];
			switch (base.MyPlane.EnchantmentState)
			{
			case EnchantmentType.Red:
				base.SpriteMain.Draw2D(textureObject, 100f, 0f, new PointF((float)((base.BoundRect.Left + base.BoundRect.Right) / 2), (float)((base.BoundRect.Top + base.BoundRect.Height) / 2)), Color.FromArgb(180, 30, 0, 0));
				break;
			case EnchantmentType.Blue:
				base.SpriteMain.Draw2D(textureObject, 100f, 0f, new PointF((float)((base.BoundRect.Left + base.BoundRect.Right) / 2), (float)((base.BoundRect.Top + base.BoundRect.Height) / 2)), Color.FromArgb(180, 0, 0, 30));
				break;
			case EnchantmentType.Green:
				base.SpriteMain.Draw2D(textureObject, 100f, 0f, new PointF((float)((base.BoundRect.Left + base.BoundRect.Right) / 2), (float)((base.BoundRect.Top + base.BoundRect.Height) / 2)), Color.FromArgb(180, 0, 30, 0));
				break;
			}
			base.Background2.Show(false);
			if (base.Boss != null)
			{
				base.Boss.Show();
			}
			base.MyBulletList.ForEach(delegate(BaseObject x)
			{
				x.Show();
			});
			base.MyPlane.Show();
			base.EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
			{
				x.Show();
			});
			base.ItemList.ForEach(delegate(BaseItem x)
			{
				if (!x.Active)
				{
					x.Show();
				}
			});
			base.SpriteMain.End();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			base.DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			base.BulletList.ForEach(delegate(BaseBullet_Touhou x)
			{
				if (x.Active)
				{
					x.Show();
				}
			});
			base.EffectList.ForEach(delegate(BaseEffect x)
			{
				if (x.Active)
				{
					x.Show();
				}
			});
			base.SpellList.ForEach(delegate(BaseObject x)
			{
				x.Show();
			});
			base.ItemList.ForEach(delegate(BaseItem x)
			{
				if (x.Active)
				{
					x.Show();
				}
			});
			base.SpriteMain.End();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.BulletList.ForEach(delegate(BaseBullet_Touhou x)
			{
				if (!x.Active && x.Layer < 0)
				{
					x.Show();
				}
			});
			base.BulletList.ForEach(delegate(BaseBullet_Touhou x)
			{
				if (!x.Active && x.Layer == 0)
				{
					x.Show();
				}
			});
			base.BulletList.ForEach(delegate(BaseBullet_Touhou x)
			{
				if (!x.Active && x.Layer > 0)
				{
					x.Show();
				}
			});
			base.EffectList.ForEach(delegate(BaseEffect x)
			{
				if (!x.Active && x.Layer < 0)
				{
					x.Show();
				}
			});
			base.EffectList.ForEach(delegate(BaseEffect x)
			{
				if (!x.Active && x.Layer == 0)
				{
					x.Show();
				}
			});
			base.EffectList.ForEach(delegate(BaseEffect x)
			{
				if (!x.Active && x.Layer > 0)
				{
					x.Show();
				}
			});
			base.SpriteMain.End();
			base.Particle3D.Show();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			if (base.Story != null)
			{
				base.Story.Show();
			}
			this.ShowInterface();
			base.InterfaceList.ForEach(delegate(BaseObject x)
			{
				x.Show();
			});
			base.SpriteMain.End();
			base.GlobalData.ScreenTexMan.End();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.GlobalData.EffectMain = base.GlobalData.EffectDictionary["Filter"];
			base.GlobalData.EffectMain.Technique = "technique1";
			base.GlobalData.EffectMain.Begin();
			base.GlobalData.EffectMain.BeginPass(0);
			base.GlobalData.SpriteMain.Draw2D(base.GlobalData.ScreenTexMan.RenderTexture, new Point(0, 0), 0f, new PointF(0f, 0f), Color.White);
			base.SpriteMain.End();
			base.GlobalData.EffectMain.EndPass();
			base.GlobalData.EffectMain.End();
		}

		// Token: 0x040000DB RID: 219
		protected int testStartTime = 0;

		// Token: 0x040000DC RID: 220
		private string testString;
	}
}
