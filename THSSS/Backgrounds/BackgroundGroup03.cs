using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class BackgroundGroup03
	{
		public BackgroundGroup03(StageDataPackage StageData)
		{
			var backgroundTouhouWater = new BackgroundTouhouWater(StageData, "world02");
			backgroundTouhouWater.OriginalPosition = new PointF(0f, 0f);
			backgroundTouhouWater.Height = 20;
			backgroundTouhouWater.V1 = -0.2f;
			var backgroundTouhouWater2 = new BackgroundTouhouWater(StageData, "world02");
			backgroundTouhouWater2.OriginalPosition = new PointF(0f, 1000f);
			backgroundTouhouWater2.Height = 20;
			backgroundTouhouWater2.V1 = -0.2f;
			var backgroundTouhouWater3 = new BackgroundTouhouWater(StageData, "world02");
			backgroundTouhouWater3.OriginalPosition = new PointF(0f, 2000f);
			backgroundTouhouWater3.Height = 20;
			backgroundTouhouWater3.V1 = -0.2f;
			var backgroundTouhouWater4 = new BackgroundTouhouWater(StageData, "world02");
			backgroundTouhouWater4.OriginalPosition = new PointF(0f, 3000f);
			backgroundTouhouWater4.Height = 20;
			backgroundTouhouWater4.V1 = -0.2f;
			var backgroundTouhouWater5 = new BackgroundTouhouWater(StageData, "world02");
			backgroundTouhouWater5.OriginalPosition = new PointF(0f, 4000f);
			backgroundTouhouWater5.Height = 20;
			backgroundTouhouWater5.V1 = -0.2f;
			var backgroundTouhouWater6 = new BackgroundTouhouWater(StageData, "world02");
			backgroundTouhouWater6.ColorValue = Color.BlueViolet;
			backgroundTouhouWater6.OriginalPosition = new PointF(0f, -500f);
			backgroundTouhouWater6.Height = 25;
			backgroundTouhouWater6.TransparentValueF = 150f;
			backgroundTouhouWater6.V1 = 0.2f;
			backgroundTouhouWater6.Time = 100;
			var backgroundTouhouWater7 = new BackgroundTouhouWater(StageData, "world02");
			backgroundTouhouWater7.ColorValue = Color.BlueViolet;
			backgroundTouhouWater7.OriginalPosition = new PointF(0f, 500f);
			backgroundTouhouWater7.Height = 25;
			backgroundTouhouWater7.TransparentValueF = 150f;
			backgroundTouhouWater7.V1 = 0.2f;
			backgroundTouhouWater7.Time = 100;
			var backgroundTouhouWater8 = new BackgroundTouhouWater(StageData, "world02");
			backgroundTouhouWater8.ColorValue = Color.BlueViolet;
			backgroundTouhouWater8.OriginalPosition = new PointF(0f, 1500f);
			backgroundTouhouWater8.Height = 25;
			backgroundTouhouWater8.TransparentValueF = 150f;
			backgroundTouhouWater8.V1 = 0.2f;
			backgroundTouhouWater8.Time = 100;
			var backgroundTouhouWater9 = new BackgroundTouhouWater(StageData, "world02");
			backgroundTouhouWater9.ColorValue = Color.BlueViolet;
			backgroundTouhouWater9.OriginalPosition = new PointF(0f, 2500f);
			backgroundTouhouWater9.Height = 25;
			backgroundTouhouWater9.TransparentValueF = 150f;
			backgroundTouhouWater9.V1 = 0.2f;
			backgroundTouhouWater9.Time = 100;
			var backgroundTouhouWater10 = new BackgroundTouhouWater(StageData, "world02");
			backgroundTouhouWater10.ColorValue = Color.BlueViolet;
			backgroundTouhouWater10.OriginalPosition = new PointF(0f, 3500f);
			backgroundTouhouWater10.Height = 25;
			backgroundTouhouWater10.TransparentValueF = 150f;
			backgroundTouhouWater10.V1 = 0.2f;
			backgroundTouhouWater10.Time = 100;
			var list = new List<int>();
			var list2 = new List<int>();
			for (var i = 0; i < 20; i++)
			{
				var num = StageData.Ran.Next(100, 350);
				list.Add(num);
				num = StageData.Ran.Next(100, 350);
				list2.Add(num);
			}
			for (var j = 3; j >= 1; j--)
			{
				for (var i = 0; i < 20; i++)
				{
					new BackgroundTree(StageData, "EquinoxFlower00", new PointF((float)list[i], (float)(1000 * j - i * 50)), false);
					new BackgroundTree(StageData, "EquinoxFlower01", new PointF((float)list2[i], (float)(1000 * j - i * 50)), false);
				}
			}
		}
	}
}
