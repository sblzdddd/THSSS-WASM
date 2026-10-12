using System;
using System.Drawing;

namespace Shooting
{
	public class BaseMyPlane_Touhou : BaseMyPlane
	{
		public float IndexX { get; set; }

		public int IndexY { get; set; }

		public BaseMyPlane_Touhou(StageDataPackage StageData, Point OriginalPosition, string Name)
		{
			this.StageData = StageData;
			this.OriginalPosition = OriginalPosition;
			this.Name = Name;
			Region = 1;
			Time = UnmatchedTime;
			DeadTime = -1;
			SubPlanePoint = new PointF[4];
			string text = Name + IndexY.ToString() + ((int)IndexX).ToString();
			TxtureObject = TextureObjectDictionary[text];
			SpellEnabled = true;
			HighItemScore = 10000;
		}

		public override void TextureCtrl()
		{
			int indexY = IndexY;
			if (Vx > 0.5f)
			{
				IndexY = 2;
				if (IndexX < 4f)
				{
					IndexX += 0.5f;
				}
				else
				{
					IndexX = (float)(4 + Time % 24 / 6);
				}
			}
			else if (Vx < -0.5f)
			{
				IndexY = 1;
				if (IndexX < 4f)
				{
					IndexX += 0.5f;
				}
				else
				{
					IndexX = (float)(4 + Time % 24 / 6);
				}
			}
			else
			{
				IndexY = 0;
				IndexX = (float)(Time % 48 / 6);
			}
			if (indexY != IndexY)
			{
				IndexX = 1f;
			}
			if (IndexX > 7f)
			{
				IndexX = 7f;
			}
			string text = Name + IndexY.ToString() + ((int)IndexX).ToString();
			TxtureObject = TextureObjectDictionary[text];
		}

		public override void ShowCenter()
		{
			if (SlowMove)
			{
				SpriteMain.Draw2D(TextureObjectDictionary["SlowEffect01"], 1f, -(float)Direction * 4f, new PointF(Position.X, Position.Y), 120);
				SpriteMain.Draw2D(TextureObjectDictionary["SlowEffect01"], 1f, (float)Direction * 4f, new PointF(Position.X, Position.Y), 120);
				SpriteMain.Draw2D(TextureObjectDictionary["Center"], 1f, (float)Direction * 4f, new PointF(Position.X, Position.Y), Color.White);
			}
		}

		public override void Miss()
		{
			double num = -2.2707963148739676;
			for (int i = 0; i < 8; i++)
			{
				PointF pointF = new PointF((float)((double)OriginalPosition.X + 40.0 * Math.Cos(num)), (float)((double)OriginalPosition.Y + 40.0 * Math.Sin(num)));
				PowerItem_Touhou powerItem_Touhou = new PowerItem_Touhou(StageData, pointF);
				powerItem_Touhou.Direction = num;
				powerItem_Touhou.Velocity = 5f;
				num += 0.1999999965940203;
			}
			base.Miss();
		}
	}
}
