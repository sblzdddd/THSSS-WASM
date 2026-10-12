namespace Shooting
{
	public class Spell_Koishi : BaseSpell
	{
		public Spell_Koishi(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.Position = base.MyPlane.Position;
			base.Damage = 0;
			base.Region = 1000;
			base.LifeTime = 60;
			base.SpellList.Add(this);
			BulletRemover_Small bulletRemover_Small = new BulletRemover_Small(StageData, base.MyPlane.OriginalPosition);
			bulletRemover_Small.Region = 150;
			StageData.SoundPlay("se_focusrot.wav");
		}
	}
}
