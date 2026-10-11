using System;

namespace Shooting
{
	public class MyRandom : Random
	{
		public int ranValue { get; private set; }

		public MyRandom()
		{
		}

		public MyRandom(int Seed)
			: base(Seed)
		{
		}

		public override int Next()
		{
			this.ranValue = base.Next();
			return this.ranValue;
		}

		public override int Next(int maxValue)
		{
			this.ranValue = base.Next(maxValue);
			return this.ranValue;
		}

		public override int Next(int minValue, int maxValue)
		{
			this.ranValue = base.Next(minValue, maxValue);
			return this.ranValue;
		}

		public override double NextDouble()
		{
			double num = base.NextDouble();
			this.ranValue = (int)(num * 100.0);
			return num;
		}

		public override void NextBytes(byte[] buffer)
		{
			this.ranValue = 0;
			base.NextBytes(buffer);
		}

		public double NextPMDouble()
		{
			return 2.0 * this.NextDouble() - 1.0;
		}
	}
}
