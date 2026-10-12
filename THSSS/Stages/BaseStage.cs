using System;
using System.Collections.Generic;
using System.Drawing;
using SlimDX.Direct3D9;

namespace Shooting
{
	internal partial class BaseStage : BaseGameState, IGameState
	{
		private byte Slowflag { get; set; }

		protected bool RoadFlag { get; set; }

		public BaseStage(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			InitializeComponent();
			var font = new System.Drawing.Font("Arial", 14f * GlobalDataPackage.FontScale, FontStyle.Bold);
			DXFont = new SlimDX.Direct3D9.Font(DeviceMain, font);
		}

		public override void Init()
		{
			BoundRect = new Rectangle(GameRegion_Main.Location, GameRegion_Main.Size);
			var point = new Point(BoundRect.Width / 2, BoundRect.Bottom - 50);
			if (MyPlane == null)
			{
				MyPlane = new MyPlane_Aya(StageData, point);
			}
			Boss = null;
			BulletList = new List<BaseBullet_Touhou>();
			EnemyPlaneList = new List<BaseEnemyPlane>();
			MyBulletList = new List<BaseObject>();
			SpellList = new List<BaseObject>();
			EffectList = new List<BaseEffect>();
			ItemList = new List<BaseItem>();
			InterfaceList = new List<BaseObject>
			{
				new StarPointLineG(StageData, new PointF(66f, 404f)),
				new StarPointLineB(StageData, new PointF(-6f, 404f)),
				new StarPointLineR(StageData, new PointF(-6f, 404f)),
				new BaseInterface(StageData, "TotalBackgroundHalf", new Point(0, 0))
				{
					Delay = 0
				},
				new BossLife_Line(StageData, "表示符卡数量的星星（单个）", new PointF((float)(BoundRect.Left + 65), (float)(BoundRect.Top + 22))),
				new HP_Line(StageData, "HP_Line1", new PointF((float)(BoundRect.Left + BoundRect.Width / 2 + 18), (float)(BoundRect.Top + 13))),
				new SpellClock(StageData),
				new MyLife_Line(StageData, MyPlane.TxtureObject, gameRegion_LifeSelf.Location)
				{
					Delay = 30
				},
				new Item_Line_LifeChip(StageData, new Point(gameRegion_LifeSelf.Location.X + 82, gameRegion_LifeSelf.Location.Y + 11))
				{
					Delay = 35
				},
				new MySpell_Line(StageData, "Bomb", gameRegion_SpellLine.Location)
				{
					Delay = 40
				},
				new Item_Line_SpellChip(StageData, new Point(gameRegion_SpellLine.Location.X + 82, gameRegion_SpellLine.Location.Y + 11))
				{
					Delay = 45
				},
				new BaseInterface(StageData, "最高得分", gameRegion_HiScore.Location)
				{
					Delay = 10
				},
				new BaseInterface(StageData, "InterfaceLine00", new Point(gameRegion_HiScore.Location.X + 60, gameRegion_HiScore.Location.Y + 16))
				{
					Delay = 15
				},
				new BaseInterface(StageData, "得分", gameRegion_Score.Location)
				{
					Delay = 20
				},
				new BaseInterface(StageData, "InterfaceLine01", new Point(gameRegion_Score.Location.X + 60, gameRegion_Score.Location.Y + 16))
				{
					Delay = 25
				},
				new BaseInterface(StageData, "剩余人数", gameRegion_Player.Location)
				{
					Delay = 30
				},
				new BaseInterface(StageData, "InterfaceLine02", new Point(gameRegion_Player.Location.X + 60, gameRegion_Player.Location.Y + 22))
				{
					Delay = 35
				},
				new BaseInterface(StageData, "SC", gameRegion_Spell.Location)
				{
					Delay = 40
				},
				new BaseInterface(StageData, "InterfaceLine03", new Point(gameRegion_Spell.Location.X + 60, gameRegion_Spell.Location.Y + 22))
				{
					Delay = 45
				},
				new BaseInterface(StageData, "灵力", gameRegion_Power.Location)
				{
					Delay = 70
				},
				new BaseInterface(StageData, "InterfaceLine04", new Point(gameRegion_Spell.Location.X + 60, gameRegion_Power.Location.Y + 16))
				{
					Delay = 75
				},
				new BaseInterface(StageData, "最大得点", gameRegion_BluePoint.Location)
				{
					Delay = 80
				},
				new BaseInterface(StageData, "InterfaceLine05", new Point(gameRegion_Spell.Location.X + 60, gameRegion_BluePoint.Location.Y + 16))
				{
					Delay = 85
				},
				new BaseInterface(StageData, "擦弹", new Point(gameRegion_Graze.Location.X, gameRegion_GreenPoint.Location.Y))
				{
					Delay = 90
				},
				new Item_Line_HiScore(StageData, new Point(gameRegion_HiScoreValue.Location.X - 20, gameRegion_HiScoreValue.Location.Y))
				{
					Delay = 10
				},
				new Item_Line_Score(StageData, new Point(gameRegion_ScoreValue.Location.X - 20, gameRegion_ScoreValue.Location.Y))
				{
					Delay = 20
				},
				new Item_Line_Red(StageData, new Point(gameRegion_Power.Location.X + 50, gameRegion_Power.Location.Y))
				{
					Delay = 70
				},
				new Item_Line_Blue(StageData, new Point(gameRegion_BluePoint.Location.X + 50, gameRegion_BluePoint.Location.Y))
				{
					Delay = 80
				},
				new Item_Line_Gray(StageData, new Point(gameRegion_Graze.Location.X + 50, gameRegion_GreenPoint.Location.Y))
				{
					Delay = 90
				},
				new BossMark(StageData),
				new InterfaceDifficulty(StageData, new PointF((float)((BoundRect.Right + 640) / 2), 24f))
				{
					Delay = 110
				}
			};
			if (StageData.Difficulty > DifficultLevel.Normal)
			{
				InterfaceList.Add(new BaseInterface(StageData, "BD", new Point(gameRegion_Spell.Location.X, gameRegion_Spell.Location.Y + 26))
				{
					Delay = 50
				});
				InterfaceList.Add(new MyBorder_Line(StageData, "Border", new Point(gameRegion_SpellLine.Location.X, gameRegion_SpellLine.Location.Y + 26))
				{
					Delay = 50
				});
			}
			SoundPlayList = new List<XAudio2_Player>();
			Background = new BackgroundManager();
			Background3D = new BackgroundManager3D(StageData);
			Particle3D = new ParticleManager3D(StageData);
			TimeMain = 0;
			StageData.Ran = new MyRandom(0);
			StageData.C_History = StageData.PData.C_History.Find(new Predicate<ClearHistory>(StageData.FindClear));
			StageData.S_History = StageData.PData.S_History.FindAll(new Predicate<ScoreHistory>(StageData.FindScore));
			if (!StageData.OnReplay)
			{
				StageData.C_History.StartTimes++;
			}
			MyPlane.HiScore = StageData.S_History[0].Score;
		}

		public override void UpdateData()
		{
			if (StageData.OnReplay)
			{
				if (KClass.Key_minus)
				{
					Slowflag += 1;
					if (Slowflag < 4)
					{
						return;
					}
					Slowflag = 0;
				}
			}
			base.UpdateData();
			if (StageData.SlowMode > 0)
			{
				StageData.SlowMode--;
				if (TimeMain % 2 == 0)
				{
					return;
				}
			}
			if (KClass.Key_ESC)
			{
				StateSwitchData = new StateSwitchDataPackage
				{
					NextState = "PauseMenu",
					NeedInit = true,
					SDPswitch = new StageDataPackage(GlobalData)
				};
				GlobalData.LastState = this;
				StageData.SoundPlay("se_pause.wav");
			}
			if (StageData.Rep != null)
			{
				if (StageData.OnReplay)
				{
					var num = StageData.Rep.ReadKey();
					if (num == 57358)
					{
						num = StageData.Rep.ReadKey();
					}
					if (num == 4080)
					{
						StateSwitchData = new StateSwitchDataPackage
						{
							NextState = "GameOverMenu",
							NeedInit = true,
							SDPswitch = new StageDataPackage(GlobalData)
						};
						GlobalData.LastState = this;
						StageData.KClass.Hex2Key(0);
						StageData.SoundPlay("se_pause.wav");
					}
					else
					{
						StageData.KClass.Hex2Key(num);
					}
				}
				else
				{
					StageData.Rep.WriteKey(StageData.KClass.Key2Hex());
				}
			}
			Background3D.Ctrl();
			Background.Ctrl();
			Background2.Ctrl();
			Drama();
			if (Story != null)
			{
				Story.Ctrl();
			}
			for (var i = MyBulletList.Count - 1; i >= 0; i--)
			{
				MyBulletList[i].Ctrl();
			}
			MyPlane.Ctrl();
			for (var i = SpellList.Count - 1; i >= 0; i--)
			{
				SpellList[i].Ctrl();
			}
			for (var i = EnemyPlaneList.Count - 1; i >= 0; i--)
			{
				EnemyPlaneList[i].Ctrl();
			}
			for (var i = BulletList.Count - 1; i >= 0; i--)
			{
				BulletList[i].Ctrl();
			}
			if (Boss != null)
			{
				Boss.Ctrl();
			}
			for (var i = EffectList.Count - 1; i >= 0; i--)
			{
				EffectList[i].Ctrl();
			}
			Particle3D.Ctrl();
			for (var i = ItemList.Count - 1; i >= 0; i--)
			{
				ItemList[i].Ctrl();
			}
			StageData.Vibrate();
			HitCheckAllinOne();
			for (var i = InterfaceList.Count - 1; i >= 0; i--)
			{
				InterfaceList[i].Ctrl();
			}
			SoundPlayList.ForEach(delegate(XAudio2_Player x)
			{
				x.Play();
			});
			if (StageData.OnReplay)
			{
				if (TimeMain % 6 != 0 && KClass.Key_plus && MyPlane.Life >= 0 && StateSwitchData == null)
				{
					UpdateData();
				}
			}
			GlobalData.LastState = this;
		}

		public virtual void HitCheckAllinOne()
		{
			if (MyPlane.Life < 0)
			{
				StateSwitchData = new StateSwitchDataPackage
				{
					NextState = "GameOverMenu",
					NeedInit = true,
					SDPswitch = new StageDataPackage(GlobalData)
				};
			}
			ItemList.ForEach(delegate(BaseItem x)
			{
				x.HitCheckAll();
			});
			if (Story == null)
			{
				EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
				{
					x.HitCheckAll();
				});
				for (var i = 0; i < BulletList.Count; i++)
				{
					BulletList[i].HitCheckAll();
				}
				if (Boss != null)
				{
					Boss.HitCheckAll();
				}
				SpellList.ForEach(delegate(BaseObject x)
				{
					x.HitCheckAll();
				});
			}
		}

		public virtual void ShowInterface()
		{
			if (MyPlane != null)
			{
				MyPlane.ShowCenter();
			}
			SpriteMain.Draw2D(TextureObjectDictionary["TotalBackground"], 1f, 0f, new PointF(320f, 240f), 255);
			var fps = FPS.ToString("F1");
			var fpsWidth = 0;
			for (var i = 0; i < fps.Length; i++)
			{
				fpsWidth += fps[i] == '.' ? 10 : 16;
			}
			DrawText(fps, new Point(638 - fpsWidth, 460));
		}

		public virtual void Drama()
		{
			if (!StageData.OnReplay)
			{
				StageData.C_History.PlayingTime += 1L;
			}
			if (TimeMain == 1)
			{
				if (StageData.StateSwitchData == null)
				{
					StateSwitchData = new StateSwitchDataPackage
					{
						NextState = "MusicLoading",
						NeedInit = true,
						SDPswitch = new StageDataPackage(GlobalData)
					};
				}
				GlobalData.LastState = this;
				StageData.CurrentStageName = StageName;
				RoadFlag = false;
				EffectList.Clear();
				MyPlane.Refresh();
				Ran = new MyRandom(0);
			}
		}

		public override void Render()
		{
			GlobalData.ScreenTexMan.Begin();
			Background3D.Show();
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			Background2.Show(true);
			SpriteMain.End();
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			var textureObject = TextureObjectDictionary["White"];
			switch (MyPlane.EnchantmentState)
			{
			case EnchantmentType.Red:
				SpriteMain.Draw2D(textureObject, 100f, 0f, new PointF((float)((BoundRect.Left + BoundRect.Right) / 2), (float)((BoundRect.Top + BoundRect.Height) / 2)), Color.FromArgb(180, 30, 0, 0));
				break;
			case EnchantmentType.Blue:
				SpriteMain.Draw2D(textureObject, 100f, 0f, new PointF((float)((BoundRect.Left + BoundRect.Right) / 2), (float)((BoundRect.Top + BoundRect.Height) / 2)), Color.FromArgb(180, 0, 0, 30));
				break;
			case EnchantmentType.Green:
				SpriteMain.Draw2D(textureObject, 100f, 0f, new PointF((float)((BoundRect.Left + BoundRect.Right) / 2), (float)((BoundRect.Top + BoundRect.Height) / 2)), Color.FromArgb(180, 0, 30, 0));
				break;
			}
			Background2.Show(false);
			if (Boss != null)
			{
				Boss.Show();
			}
			MyBulletList.ForEach(delegate(BaseObject x)
			{
				x.Show();
			});
			MyPlane.Show();
			EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
			{
				x.Show();
			});
			ItemList.ForEach(delegate(BaseItem x)
			{
				if (!x.Active)
				{
					x.Show();
				}
			});
			SpriteMain.End();
			SpriteMain.Begin(SpriteFlags.AlphaBlend | SpriteFlags.SortTexture);
			DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			for (var i = 0; i < BulletList.Count; i++)
			{
				var bullet = BulletList[i];
				if (bullet.Active)
				{
					bullet.Show();
				}
			}
			for (var i = 0; i < EffectList.Count; i++)
			{
				var effect = EffectList[i];
				if (effect.Active)
				{
					effect.Show();
				}
			}
			SpellList.ForEach(delegate(BaseObject x)
			{
				x.Show();
			});
			ItemList.ForEach(delegate(BaseItem x)
			{
				if (x.Active)
				{
					x.Show();
				}
			});
			SpriteMain.End();
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			for (var i = 0; i < BulletList.Count; i++)
			{
				var bullet = BulletList[i];
				if (!bullet.Active && bullet.Layer < 0)
				{
					bullet.Show();
				}
			}
			for (var i = 0; i < BulletList.Count; i++)
			{
				var bullet = BulletList[i];
				if (!bullet.Active && bullet.Layer == 0)
				{
					bullet.Show();
				}
			}
			for (var i = 0; i < BulletList.Count; i++)
			{
				var bullet = BulletList[i];
				if (!bullet.Active && bullet.Layer > 0)
				{
					bullet.Show();
				}
			}
			for (var i = 0; i < EffectList.Count; i++)
			{
				var effect = EffectList[i];
				if (!effect.Active && effect.Layer < 0)
				{
					effect.Show();
				}
			}
			for (var i = 0; i < EffectList.Count; i++)
			{
				var effect = EffectList[i];
				if (!effect.Active && effect.Layer == 0)
				{
					effect.Show();
				}
			}
			for (var i = 0; i < EffectList.Count; i++)
			{
				var effect = EffectList[i];
				if (!effect.Active && effect.Layer > 0)
				{
					effect.Show();
				}
			}
			SpriteMain.End();
			Particle3D.Show();
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			if (Story != null)
			{
				Story.Show();
			}
			ShowInterface();
			InterfaceList.ForEach(delegate(BaseObject x)
			{
				x.Show();
			});
			for (var i = 0; i < Background.BackgroundList.Count; i++)
			{
				var cover = Background.BackgroundList[i] as BackgroundTransitionIn;
				if (cover != null)
				{
					cover.ShowLoading();
				}
			}
			SpriteMain.End();
			GlobalData.ScreenTexMan.End();
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			GlobalData.EffectMain = GlobalData.EffectDictionary["Filter"];
			GlobalData.EffectMain.Technique = "technique1";
			GlobalData.EffectMain.Begin();
			GlobalData.EffectMain.BeginPass(0);
			GlobalData.SpriteMain.Draw2D(GlobalData.ScreenTexMan.RenderTexture, new Point(0, 0), 0f, new PointF(0f, 0f), Color.White);
			SpriteMain.End();
			GlobalData.EffectMain.EndPass();
			GlobalData.EffectMain.End();
		}

		protected int testStartTime = 0;
	}
}
