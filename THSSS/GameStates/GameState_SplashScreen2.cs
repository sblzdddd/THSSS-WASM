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
			this.GlobalData = GlobalData;
			TxtureObject = GlobalData.TextureObjectDictionary["这只是测试版"];
			PreNextState = nextState;
		}

		public override void UpdateData()
		{
			TimeMain++;
			if (TimeMain < 70)
			{
				Background.Light += 4;
			}
			else if (TimeMain > 120)
			{
				Background.Light -= 4;
			}
			if (Background.Light == 0)
			{
				StateSwitchData = new StateSwitchDataPackage
				{
					NextState = PreNextState,
					NeedInit = true,
					SDPswitch = new StageDataPackage(GlobalData)
				};
				GlobalData.LastState = this;
			}
		}

		public override void Render()
		{
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			Background.Show(false);
			SpriteMain.Draw2D(TxtureObject, 1f, 0f, new Point(320, 240), Background.Light);
			StageData.EffectList.ForEach(delegate(BaseEffect x)
			{
				x.Show();
			});
			SpriteMain.End();
		}
	}
}
