namespace Shooting
{
	internal class BaseFSC : BaseSpellCard
	{
		public BaseFSC(StageDataPackage StageData)
			: base(StageData)
		{
		}

		public override long GetSpellCardBonus()
		{
			long num;
			if (Bombed || Missed)
			{
				num = 0L;
			}
			else
			{
				long num2 = 99999990L;
				num = num2;
			}
			return num;
		}
	}
}
