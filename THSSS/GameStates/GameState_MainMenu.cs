using System;
using System.Drawing;
using SlimDX.Direct3D9;

namespace Shooting
{
	internal partial class GameState_MainMenu : BaseMenu, IGameState
	{
		public GameState_MainMenu(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			InitializeComponent();
			System.Drawing.Font font = new System.Drawing.Font("Arial", 12f * GlobalDataPackage.FontScale, FontStyle.Bold | FontStyle.Italic);
			DXFont = new SlimDX.Direct3D9.Font(DeviceMain, font);
		}

		public override void Init()
		{
			StageData.PData.CheckEnable();
			StageData.PData.SaveData();
			BackgroundLight = 0;
			BGM_Player.Volume = StageData.GlobalData.BGMVolume;
		}

		public override void UpdateData()
		{
			if (TimeMain == 50)
			{
				BaseEffect baseEffect = new BaseEffect(StageData, "woshixingtu", new PointF(385f, 240f), 4f, 0.0);
				baseEffect.AngleDegree = 90.0;
				baseEffect.Active = false;
				baseEffect.DestPoint = new PointF(320f, 240f);
				baseEffect.AngleWithDirection = false;
				baseEffect.Scale = 1f;
				baseEffect.TransparentValueF = 0f;
				baseEffect.TransparentVelocity = 5f;
			}
			else if (TimeMain == 100)
			{
				BaseEffect baseEffect2 = new BaseEffect(StageData, "woshibiaotihebaisi", new PointF(385f, 240f), 4f, 0.0);
				baseEffect2.AngleDegree = 90.0;
				baseEffect2.Active = false;
				baseEffect2.DestPoint = new PointF(320f, 240f);
				baseEffect2.AngleWithDirection = false;
				baseEffect2.Scale = 1f;
				baseEffect2.TransparentValueF = 0f;
				baseEffect2.TransparentVelocity = 5f;
				new EmitterTitleStarFall(StageData);
				StageData.MenuGroupList.Add(new MenuGroup_Main(StageData));
			}
			else if (TimeMain == 130)
			{
				SelectFrame = new SelectBox(StageData)
				{
					Velocity = 100f,
					OriginalPosition = new PointF(0f, 500f)
				};
			}
			if (StageData.MenuGroupList.Count > 0)
			{
				BaseMenuItem baseMenuItem = StageData.MenuGroupList[StageData.MenuGroupList.Count - 1].MenuItemList[StageData.MenuGroupList[StageData.MenuGroupList.Count - 1].MenuSelectIndex];
				if (StageData.MenuGroupList.Count == 1 || StageData.MenuGroupList[StageData.MenuGroupList.Count - 1] is MenuOption)
				{
					Particle particle = new Particle(StageData, "Star", new PointF((float)Ran.Next((int)baseMenuItem.OriginalPosition.X - 10, (int)baseMenuItem.OriginalPosition.X + baseMenuItem.TxtureObject.PosRect.Width + 10), baseMenuItem.OriginalPosition.Y + 20f), (float)(StageData.Ran.Next(10, 20) / 10), -1.5707963267948966 - (double)((float)StageData.Ran.Next(1, 5) / 10f));
					particle.Active = true;
					particle.Scale = (float)StageData.Ran.Next(15, 40) / 100f;
					particle.LifeTime = 20;
				}
				if (SelectFrame != null)
				{
					baseMenuItem = StageData.MenuGroupList[0].MenuItemList[StageData.MenuGroupList[0].MenuSelectIndex];
					if (Math.Abs(SelectFrame.DestPoint.Y - baseMenuItem.OriginalPosition.Y - 6f) >= 1f)
					{
						SelectFrame.Velocity = Math.Abs(SelectFrame.DestPoint.Y - baseMenuItem.OriginalPosition.Y - 6f) / 5f;
						if (SelectFrame.Velocity > 16f)
						{
							SelectFrame.Velocity = 16f;
						}
						else if (SelectFrame.Velocity < 2f)
						{
							SelectFrame.Velocity = 2f;
						}
					}
					SelectFrame.DestPoint = new PointF(0f, baseMenuItem.OriginalPosition.Y + 6f);
				}
			}
			base.UpdateData();
			if (0 <= TimeMain && TimeMain < 130)
			{
				BackgroundLight += 4;
				if (BackgroundLight > 255)
				{
					BackgroundLight = 255;
				}
			}
			else
			{
				BackgroundLight = 255;
			}
		}

		public override void Render()
		{
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			SpriteMain.Draw2D(TextureObjectDictionary["MenuBackground"], 0.625f, 0f, new PointF(320f, 240f), 255);
			DXFont.DrawString(SpriteMain.sprite, "ver 1.01", 560, 460, Color.Black);
			SpriteMain.End();
			Background3D.Show();
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			Background2.Show(true);
			SpriteMain.End();
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			Background2.Show(false);
			EffectList.ForEach(delegate(BaseEffect x)
			{
				if (!x.Active)
				{
					x.Show();
				}
			});
			StageData.MenuGroupList.ForEach(delegate(BaseMenuGroup x)
			{
				x.Show();
			});
			SpriteMain.End();
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			EffectList.ForEach(delegate(BaseEffect x)
			{
				if (x.Active)
				{
					x.Show();
				}
			});
			SpriteMain.End();
			Particle3D.Show();
			if (TimeMain < 126)
			{
				GlobalData.ScreenTexMan.Begin();
				GlobalData.LastState.Render();
				GlobalData.ScreenTexMan.End();
				SpriteMain.Begin(SpriteFlags.AlphaBlend);
				GlobalData.SpriteMain.Draw2D(GlobalData.ScreenTexMan.RenderTexture, new Point(0, 0), 0f, new PointF(0f, 0f), Color.FromArgb(255 - TimeMain * 2, Color.White));
				SpriteMain.End();
			}
		}

		public override void BGM_ON()
		{
			StageData.ChangeBGM(".\\BGM\\OP.ogg", 0, 0, 255, 604611, 6047874);
		}

		public override void BGM_Resume()
		{
		}

		private BaseEffect SelectFrame;
	}
}
