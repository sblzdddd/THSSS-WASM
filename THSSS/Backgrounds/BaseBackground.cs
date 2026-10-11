using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000074 RID: 116
	public class BaseBackground : BaseObject_CS
	{
		// Token: 0x06000402 RID: 1026 RVA: 0x0001B98C File Offset: 0x00019B8C
		public BaseBackground()
		{
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001B997 File Offset: 0x00019B97
		public BaseBackground(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.Background.BackgroundList.Add(this);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001B9BC File Offset: 0x00019BBC
		public BaseBackground(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
			base.Background.BackgroundList.Add(this);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001B9E0 File Offset: 0x00019BE0
		public override void Ctrl()
		{
			base.Ctrl();
			if (this.OutBoundary())
			{
				base.Background.BackgroundList.Remove(this);
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001BA18 File Offset: 0x00019C18
		public override bool HitCheck(BaseObject Target)
		{
			return false;
		}
	}
}
