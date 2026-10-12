namespace Shooting
{
	public class Spell_Koishi : BaseSpell
	{
		public Spell_Koishi(StageDataPackage StageData)
		{
			this.StageData = StageData;
			Position = MyPlane.Position;
			Damage = 0;
			Region = 1000;
			LifeTime = 60;
			SpellList.Add(this);
			BulletRemover_Small bulletRemover_Small = new BulletRemover_Small(StageData, MyPlane.OriginalPosition);
			bulletRemover_Small.Region = 150;
			StageData.SoundPlay("se_focusrot.wav");
		}
	}
}
