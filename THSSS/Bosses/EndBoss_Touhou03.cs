using System.Drawing;
using Shooting.Planes.Story;

namespace Shooting
{
	internal class EndBoss_Touhou03 : EndBoss_Touhou
	{
		public EndBoss_Touhou03(StageDataPackage StageData, PointF OriginalPosition, Color Color1, Color Color2)
			: base(StageData, OriginalPosition, Color1, Color2)
		{
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == LifeTime)
			{
				string name = MyPlane.Name;
				if (name != null)
				{
					if (name == "Aya")
					{
						new Story_SSS03_02(StageData);
						goto IL_00CA;
					}
					if (name == "Reimu")
					{
						new Story_SSS03_02A(StageData);
						goto IL_00CA;
					}
					if (name == "Marisa")
					{
						new Story_SSS03_02B(StageData);
						goto IL_00CA;
					}
					if (name == "Sanae")
					{
						new Story_SSS03_02C(StageData);
						goto IL_00CA;
					}
					if (name == "Koishi")
					{
						new Story_SSS03_02D(StageData);
						goto IL_00CA;
					}
				}
				new Story_SSS03_02X(StageData);
				IL_00CA:;
			}
		}
	}
}
