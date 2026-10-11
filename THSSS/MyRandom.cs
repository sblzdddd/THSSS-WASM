using System;

namespace Shooting
{
	// Token: 0x020000E5 RID: 229
	public class MyRandom : Random
	{
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x000371F8 File Offset: 0x000353F8
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x0003720F File Offset: 0x0003540F
		public int ranValue { get; private set; }

		// Token: 0x06000631 RID: 1585 RVA: 0x00037218 File Offset: 0x00035418
		public MyRandom()
		{
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00037223 File Offset: 0x00035423
		public MyRandom(int Seed)
			: base(Seed)
		{
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00037230 File Offset: 0x00035430
		public override int Next()
		{
			this.ranValue = base.Next();
			return this.ranValue;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00037258 File Offset: 0x00035458
		public override int Next(int maxValue)
		{
			this.ranValue = base.Next(maxValue);
			return this.ranValue;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00037280 File Offset: 0x00035480
		public override int Next(int minValue, int maxValue)
		{
			this.ranValue = base.Next(minValue, maxValue);
			return this.ranValue;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000372A8 File Offset: 0x000354A8
		public override double NextDouble()
		{
			double num = base.NextDouble();
			this.ranValue = (int)(num * 100.0);
			return num;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x000372D5 File Offset: 0x000354D5
		public override void NextBytes(byte[] buffer)
		{
			this.ranValue = 0;
			base.NextBytes(buffer);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x000372E8 File Offset: 0x000354E8
		public double NextPMDouble()
		{
			return 2.0 * this.NextDouble() - 1.0;
		}
	}
}
