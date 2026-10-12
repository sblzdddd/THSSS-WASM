using System.Drawing;

namespace Shooting
{
	public class SpellClock : BaseObject
	{
		public int TimeRemaining
		{
			get
			{
				return timeRemaining;
			}
			set
			{
				if (value < 0)
				{
					timeRemaining = 0;
				}
				else if (value > 5940)
				{
					timeRemaining = 5940;
				}
				else
				{
					timeRemaining = value;
				}
			}
		}

		public SpellClock(StageDataPackage StageData)
		{
			this.StageData = StageData;
			OriginalPosition = new PointF((float)(BoundRect.Left + 25), 40f);
		}

		public override void Ctrl()
		{
			if (Boss != null)
			{
				base.Ctrl();
				TimeRemaining = Boss.SpellTime - Boss.Time;
				if (Boss.Life > 0)
				{
					if (TimeRemaining <= 300)
					{
						ColorValue = Color.FromArgb(255, 100, 100);
						if (0 < TimeRemaining)
						{
							if (TimeRemaining % 60 == 0)
							{
								if (TimeRemaining <= 120)
								{
									StageData.SoundPlay("se_timeout2.wav");
								}
								else
								{
									StageData.SoundPlay("se_timeout.wav");
								}
							}
						}
					}
					else if (TimeRemaining <= 600)
					{
						ColorValue = Color.FromArgb(255, 200, 200);
					}
					else
					{
						ColorValue = Color.White;
					}
				}
				if (MyPlane.OriginalPosition.Y < 30f && MyPlane.OriginalPosition.X < OriginalPosition.X + 60f && MyPlane.OriginalPosition.X > OriginalPosition.X - 60f)
				{
					TransparentValueF -= 10f;
					if (TransparentValueF < 50f)
					{
						TransparentValueF = 50f;
					}
				}
				else
				{
					TransparentValueF += 10f;
				}
			}
		}

		public override void Show()
		{
			if (Boss != null)
			{
				if (Boss.Life > 0)
				{
					SpriteMain.Draw2D(TextureObjectDictionary["球球"], 1f, 0f, OriginalPosition, Color.FromArgb(TransparentValue, Color.White));
					string text = "tsnum";
					int num = 3;
					int num2 = 9;
					int num3 = 0;
					char[] array = ((float)TimeRemaining / 60f).ToString("F1").ToCharArray();
					for (int i = array.Length - 1; i >= 0; i--)
					{
						if (array[i] == '.')
						{
							num2 = 10;
							text = "tnum";
							num3 = 4;
						}
						PointF pointF = new PointF(OriginalPosition.X + 18f - (float)((22 - num2) * (array.Length - 1 - i)) + (float)num3, OriginalPosition.Y + (float)num);
						SpriteMain.Draw2D(TextureObjectDictionary[text + array[i].ToString()], Scale, 0f, pointF, Color.FromArgb(TransparentValue, ColorValue));
						if (array[i] == '.')
						{
							num = 0;
							num2 = 3;
							num3 = 20;
						}
					}
				}
			}
		}

		private int timeRemaining;
	}
}
