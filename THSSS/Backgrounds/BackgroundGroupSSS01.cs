using System;
using System.Drawing;

namespace Shooting
{
	internal class BackgroundGroupSSS01
	{
		public BackgroundGroupSSS01(StageDataPackage StageData)
		{
			BackgroundTouhouStage02_3D backgroundTouhouStage02_3D = new BackgroundTouhouStage02_3D(StageData);
			backgroundTouhouStage02_3D.TxtureObject = StageData.TextureObjectDictionary["BG01b"];
			for (int i = 0; i < 20; i++)
			{
				BackgroundTree backgroundTree = new BackgroundTree(StageData, "Tree", new PointF(290f, (float)(4200 - i * 200 + 100)), false);
				backgroundTree.Mirrored = true;
				new BackgroundTree(StageData, "Tree", new PointF(290f, (float)(4200 - i * 200)), false);
				BackgroundTree backgroundTree2 = new BackgroundTree(StageData, "Tree", new PointF(-400f, (float)(4200 - i * 200 + 162)), false);
				backgroundTree2.Mirrored = true;
				new BackgroundTree(StageData, "Tree", new PointF(-400f, (float)(4200 - i * 200 + 62)), false);
			}
			for (int i = 0; i < 10; i++)
			{
				Background3DObject background3DObject = new Background3DObject(StageData, "Cloud02", new PointF(0f, (float)(i * 500)), false);
				background3DObject.Scale = 2f;
				background3DObject.Mirrored = false;
				background3DObject.OriginalPositionZ = -10f;
				background3DObject.Angle3DX = -3.1415927f;
				background3DObject.TransparentValueF = 50f;
				background3DObject.Velocity = 0.5f;
				Background3DObject background3DObject2 = new Background3DObject(StageData, "Cloud02", new PointF(-1024f, (float)(i * 500)), false);
				background3DObject2.Scale = 2f;
				background3DObject2.Mirrored = false;
				background3DObject2.OriginalPositionZ = -10f;
				background3DObject2.Angle3DX = -3.1415927f;
				background3DObject2.TransparentValueF = 50f;
				background3DObject2.Velocity = 0.5f;
				Background3DObject background3DObject3 = new Background3DObject(StageData, "Cloud02", new PointF(-2048f, (float)(i * 500)), false);
				background3DObject3.Scale = 2f;
				background3DObject3.Mirrored = false;
				background3DObject3.OriginalPositionZ = -10f;
				background3DObject3.Angle3DX = -3.1415927f;
				background3DObject3.TransparentValueF = 50f;
				background3DObject3.Velocity = 0.5f;
				Background3DObject background3DObject4 = new Background3DObject(StageData, "Cloud01", new PointF(0f, (float)(i * 500 + 250)), false);
				background3DObject4.Scale = 2f;
				background3DObject4.Mirrored = true;
				background3DObject4.OriginalPositionZ = -20f;
				background3DObject4.Angle3DX = -3.1415927f;
				background3DObject4.TransparentValueF = 50f;
				background3DObject4.Velocity = -0.5f;
				Background3DObject background3DObject5 = new Background3DObject(StageData, "Cloud01", new PointF(1024f, (float)(i * 500 + 250)), false);
				background3DObject5.Scale = 2f;
				background3DObject5.Mirrored = true;
				background3DObject5.OriginalPositionZ = -20f;
				background3DObject5.Angle3DX = -3.1415927f;
				background3DObject5.TransparentValueF = 50f;
				background3DObject5.Velocity = -0.5f;
				Background3DObject background3DObject6 = new Background3DObject(StageData, "Cloud01", new PointF(2048f, (float)(i * 500 + 250)), false);
				background3DObject6.Scale = 2f;
				background3DObject6.Mirrored = true;
				background3DObject6.OriginalPositionZ = -20f;
				background3DObject6.Angle3DX = -3.1415927f;
				background3DObject6.TransparentValueF = 50f;
				background3DObject6.Velocity = -0.5f;
			}
		}
	}
}
