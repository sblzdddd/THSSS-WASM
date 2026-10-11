using System;
using System.Drawing;
using System.Windows.Forms;
using Shooting.Properties;
using SlimDX.Direct3D9;

namespace Shooting
{
	internal partial class GameState_MainMenu : BaseMenu, IGameState
	{
		public GameState_MainMenu(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			this.InitializeComponent();
			global::System.Drawing.Font font = new global::System.Drawing.Font("Arial", 12f * GlobalDataPackage.FontScale, FontStyle.Bold | FontStyle.Italic);
			base.DXFont = new SlimDX.Direct3D9.Font(base.DeviceMain, font);
		}

		public override void Init()
		{
			base.StageData.PData.CheckEnable();
			base.StageData.PData.SaveData();
			base.BackgroundLight = 0;
			base.BGM_Player.Volume = base.StageData.GlobalData.BGMVolume;
		}

		public override void UpdateData()
		{
			if (base.TimeMain == 50)
			{
				BaseEffect baseEffect = new BaseEffect(base.StageData, "woshixingtu", new PointF(385f, 240f), 4f, 0.0);
				baseEffect.AngleDegree = 90.0;
				baseEffect.Active = false;
				baseEffect.DestPoint = new PointF(320f, 240f);
				baseEffect.AngleWithDirection = false;
				baseEffect.Scale = 1f;
				baseEffect.TransparentValueF = 0f;
				baseEffect.TransparentVelocity = 5f;
			}
			else if (base.TimeMain == 100)
			{
				BaseEffect baseEffect2 = new BaseEffect(base.StageData, "woshibiaotihebaisi", new PointF(385f, 240f), 4f, 0.0);
				baseEffect2.AngleDegree = 90.0;
				baseEffect2.Active = false;
				baseEffect2.DestPoint = new PointF(320f, 240f);
				baseEffect2.AngleWithDirection = false;
				baseEffect2.Scale = 1f;
				baseEffect2.TransparentValueF = 0f;
				baseEffect2.TransparentVelocity = 5f;
				new EmitterTitleStarFall(base.StageData);
				base.StageData.MenuGroupList.Add(new MenuGroup_Main(base.StageData));
			}
			else if (base.TimeMain == 130)
			{
				this.SelectFrame = new SelectBox(base.StageData)
				{
					Velocity = 100f,
					OriginalPosition = new PointF(0f, 500f)
				};
			}
			if (base.StageData.MenuGroupList.Count > 0)
			{
				BaseMenuItem baseMenuItem = base.StageData.MenuGroupList[base.StageData.MenuGroupList.Count - 1].MenuItemList[base.StageData.MenuGroupList[base.StageData.MenuGroupList.Count - 1].MenuSelectIndex];
				if (base.StageData.MenuGroupList.Count == 1 || base.StageData.MenuGroupList[base.StageData.MenuGroupList.Count - 1] is MenuOption)
				{
					Particle particle = new Particle(base.StageData, "Star", new PointF((float)base.Ran.Next((int)baseMenuItem.OriginalPosition.X - 10, (int)baseMenuItem.OriginalPosition.X + baseMenuItem.TxtureObject.PosRect.Width + 10), baseMenuItem.OriginalPosition.Y + 20f), (float)(base.StageData.Ran.Next(10, 20) / 10), -1.5707963267948966 - (double)((float)base.StageData.Ran.Next(1, 5) / 10f));
					particle.Active = true;
					particle.Scale = (float)base.StageData.Ran.Next(15, 40) / 100f;
					particle.LifeTime = 20;
				}
				if (this.SelectFrame != null)
				{
					baseMenuItem = base.StageData.MenuGroupList[0].MenuItemList[base.StageData.MenuGroupList[0].MenuSelectIndex];
					if (Math.Abs(this.SelectFrame.DestPoint.Y - baseMenuItem.OriginalPosition.Y - 6f) >= 1f)
					{
						this.SelectFrame.Velocity = Math.Abs(this.SelectFrame.DestPoint.Y - baseMenuItem.OriginalPosition.Y - 6f) / 5f;
						if (this.SelectFrame.Velocity > 16f)
						{
							this.SelectFrame.Velocity = 16f;
						}
						else if (this.SelectFrame.Velocity < 2f)
						{
							this.SelectFrame.Velocity = 2f;
						}
					}
					this.SelectFrame.DestPoint = new PointF(0f, baseMenuItem.OriginalPosition.Y + 6f);
				}
			}
			base.UpdateData();
			if (0 <= base.TimeMain && base.TimeMain < 130)
			{
				base.BackgroundLight += 4;
				if (base.BackgroundLight > 255)
				{
					base.BackgroundLight = 255;
				}
			}
			else
			{
				base.BackgroundLight = 255;
			}
		}

		public override void Render()
		{
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.SpriteMain.Draw2D(base.TextureObjectDictionary["MenuBackground"], 0.625f, 0f, new PointF(320f, 240f), 255);
			base.DXFont.DrawString(base.SpriteMain.sprite, "ver 1.01", 560, 460, Color.Black);
			base.SpriteMain.End();
			base.Background3D.Show();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			base.DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			base.Background2.Show(true);
			base.SpriteMain.End();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.Background2.Show(false);
			base.EffectList.ForEach(delegate(BaseEffect x)
			{
				if (!x.Active)
				{
					x.Show();
				}
			});
			base.StageData.MenuGroupList.ForEach(delegate(BaseMenuGroup x)
			{
				x.Show();
			});
			base.SpriteMain.End();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			base.DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			base.EffectList.ForEach(delegate(BaseEffect x)
			{
				if (x.Active)
				{
					x.Show();
				}
			});
			base.SpriteMain.End();
			base.Particle3D.Show();
			if (base.TimeMain < 126)
			{
				base.GlobalData.ScreenTexMan.Begin();
				base.GlobalData.LastState.Render();
				base.GlobalData.ScreenTexMan.End();
				base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
				base.GlobalData.SpriteMain.Draw2D(base.GlobalData.ScreenTexMan.RenderTexture, new Point(0, 0), 0f, new PointF(0f, 0f), Color.FromArgb(255 - base.TimeMain * 2, Color.White));
				base.SpriteMain.End();
			}
		}

		public override void BGM_ON()
		{
			base.StageData.ChangeBGM(".\\BGM\\OP.ogg", 0, 0, 255, 604611, 6047874);
		}

		public override void BGM_Resume()
		{
		}

		private BaseEffect SelectFrame;
	}
}
