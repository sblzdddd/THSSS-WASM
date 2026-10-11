using System;
using System.Drawing;
using SlimDX.Direct3D9;

namespace Shooting
{
	internal class GameState_SplashScreen2 : GameState_SplashScreen, IGameState
	{
		public string PreNextState { get; set; }

		public GameState_SplashScreen2(GlobalDataPackage GlobalData)
			: this(GlobalData, "MainMenu")
		{
		}

		public GameState_SplashScreen2(GlobalDataPackage GlobalData, string nextState)
		{
			base.GlobalData = GlobalData;
			base.TxtureObject = GlobalData.TextureObjectDictionary["这只是测试版"];
			this.PreNextState = nextState;
		}

		public override void UpdateData()
		{
			base.TimeMain++;
			if (base.TimeMain < 70)
			{
				base.Background.Light += 4;
			}
			else if (base.TimeMain > 120)
			{
				base.Background.Light -= 4;
			}
			if (base.Background.Light == 0)
			{
				base.StateSwitchData = new StateSwitchDataPackage
				{
					NextState = this.PreNextState,
					NeedInit = true,
					SDPswitch = new StageDataPackage(base.GlobalData)
				};
				base.GlobalData.LastState = this;
			}
		}

		public override void Render()
		{
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.Background.Show(false);
			base.SpriteMain.Draw2D(base.TxtureObject, 1f, 0f, new Point(320, 240), base.Background.Light);
			base.StageData.EffectList.ForEach(delegate(BaseEffect x)
			{
				x.Show();
			});
			base.SpriteMain.End();
		}
	}
}
