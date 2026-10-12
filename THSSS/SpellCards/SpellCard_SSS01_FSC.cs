using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS01_FSC : BaseFSC
	{
		public SpellCard_SSS01_FSC(StageDataPackage StageData)
			: base(StageData)
		{
			if (Difficulty < DifficultLevel.Hard)
			{
				SC_Name = "夜风「轮回的幸运之星」";
			}
			else
			{
				SC_Name = "夜风「轮回的幸运之星」";
			}
			Boss.DestPoint = new Point(BoundRect.Width / 2, 120);
			Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			var backgroundMove = new BackgroundMove2(StageData, "Card01b");
			backgroundMove.DirectionDegree = 270.0;
			var backgroundMove2 = new BackgroundMove2(StageData, "Card01a");
			backgroundMove2.DirectionDegree = -80.0;
			backgroundMove2.Length = 1024;
			backgroundMove2.TransparentValueF = 80f;
			new BulletTitle(StageData, SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceAmi_ct");
		}

		public override void Shoot()
		{
			if (Time > 100)
			{
				Boss.MoveUpDown();
				Boss.Armon = 0f;
			}
			if (Time == 100)
			{
				Boss.OnAction = 10000;
				string text;
				switch (Difficulty)
				{
				case DifficultLevel.Hard:
					text = ".\\CS\\St01\\关底Boss\\FSC符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St01\\关底Boss\\FSC符L.mbg";
					break;
				default:
					text = ".\\CS\\St01\\关底Boss\\FSC符L.mbg";
					break;
				}
				var cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
		}
	}
}
