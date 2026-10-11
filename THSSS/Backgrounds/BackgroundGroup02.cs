using System;
using System.Drawing;

namespace Shooting
{
	internal class BackgroundGroup02
	{
		public BackgroundGroup02(StageDataPackage StageData)
		{
			new BackgroundTouhouStage02_3D(StageData);
			for (int i = 0; i < 20; i++)
			{
				BackgroundTree backgroundTree = new BackgroundTree(StageData, "Background_Sunflowers", new PointF((float)StageData.Ran.Next(25, 300), (float)(5400 - i * 50 + 25)), false);
				backgroundTree.Mirrored = true;
				backgroundTree.Scale = 0.5f;
				BackgroundTree backgroundTree2 = new BackgroundTree(StageData, "Background_Sunflowers", new PointF((float)StageData.Ran.Next(-300, -25), (float)(5400 - i * 50 + 31)), false);
				backgroundTree2.Mirrored = true;
				backgroundTree2.Scale = 0.5f;
				BackgroundTree backgroundTree3 = new BackgroundTree(StageData, "Background_Sunflowers", new PointF((float)StageData.Ran.Next(-300, -25), (float)(5400 - i * 50 + 6)), false);
				backgroundTree3.Scale = 0.5f;
				BackgroundTree backgroundTree4 = new BackgroundTree(StageData, "Background_Sunflowers", new PointF((float)StageData.Ran.Next(25, 300), (float)(5400 - i * 50)), false);
				backgroundTree4.Scale = 0.5f;
			}
			for (int i = 0; i < 20; i++)
			{
				BackgroundTree backgroundTree5 = new BackgroundTree(StageData, "ea1655f0", new PointF(300f, (float)(4200 - i * 200 + 100)), false);
				backgroundTree5.Mirrored = true;
				new BackgroundTree(StageData, "ea1655f0", new PointF(290f, (float)(4200 - i * 200)), false);
				BackgroundTree backgroundTree6 = new BackgroundTree(StageData, "ea1655f0", new PointF(-400f, (float)(4200 - i * 200 + 162)), false);
				backgroundTree6.Mirrored = true;
				new BackgroundTree(StageData, "ea1655f0", new PointF(-410f, (float)(4200 - i * 200 + 62)), false);
			}
		}
	}
}
