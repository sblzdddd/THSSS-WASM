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
			base.StageData = StageData;
			base.OriginalPosition = OriginalPosition;
			base.Name = Name;
			base.Region = 1;
			base.Time = base.UnmatchedTime;
			base.DeadTime = -1;
			base.SubPlanePoint = new PointF[4];
			string text = Name + this.IndexY.ToString() + ((int)this.IndexX).ToString();
			base.TxtureObject = base.TextureObjectDictionary[text];
			base.SpellEnabled = true;
			base.HighItemScore = 10000;
		}

		public override void TextureCtrl()
		{
			int indexY = this.IndexY;
			if (base.Vx > 0.5f)
			{
				this.IndexY = 2;
				if (this.IndexX < 4f)
				{
					this.IndexX += 0.5f;
				}
				else
				{
					this.IndexX = (float)(4 + base.Time % 24 / 6);
				}
			}
			else if (base.Vx < -0.5f)
			{
				this.IndexY = 1;
				if (this.IndexX < 4f)
				{
					this.IndexX += 0.5f;
				}
				else
				{
					this.IndexX = (float)(4 + base.Time % 24 / 6);
				}
			}
			else
			{
				this.IndexY = 0;
				this.IndexX = (float)(base.Time % 48 / 6);
			}
			if (indexY != this.IndexY)
			{
				this.IndexX = 1f;
			}
			if (this.IndexX > 7f)
			{
				this.IndexX = 7f;
			}
			string text = base.Name + this.IndexY.ToString() + ((int)this.IndexX).ToString();
			base.TxtureObject = base.TextureObjectDictionary[text];
		}

		public override void ShowCenter()
		{
			if (base.SlowMove)
			{
				base.SpriteMain.Draw2D(base.TextureObjectDictionary["SlowEffect01"], 1f, -(float)base.Direction * 4f, new PointF(base.Position.X, base.Position.Y), 120);
				base.SpriteMain.Draw2D(base.TextureObjectDictionary["SlowEffect01"], 1f, (float)base.Direction * 4f, new PointF(base.Position.X, base.Position.Y), 120);
				base.SpriteMain.Draw2D(base.TextureObjectDictionary["Center"], 1f, (float)base.Direction * 4f, new PointF(base.Position.X, base.Position.Y), Color.White);
			}
		}

		public override void Miss()
		{
			double num = -2.2707963148739676;
			for (int i = 0; i < 8; i++)
			{
				PointF pointF = new PointF((float)((double)base.OriginalPosition.X + 40.0 * Math.Cos(num)), (float)((double)base.OriginalPosition.Y + 40.0 * Math.Sin(num)));
				PowerItem_Touhou powerItem_Touhou = new PowerItem_Touhou(base.StageData, pointF);
				powerItem_Touhou.Direction = num;
				powerItem_Touhou.Velocity = 5f;
				num += 0.1999999965940203;
			}
			base.Miss();
		}
	}
}
