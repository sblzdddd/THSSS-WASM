using System.Drawing;

namespace Shooting
{
	public class Spell_AyaA : BaseSpell
	{
		public Spell_AyaA(StageDataPackage StageData)
		{
			this.StageData = StageData;
			OriginalPosition = MyPlane.OriginalPosition;
			Damage = 10;
			Region = 30;
			LifeTime = 300;
			Direction = 1.5707963267948966;
			SpellList.Add(this);
			new BackgroundBlack(StageData, Position);
			var backgroundMySC = new BackgroundMySC(StageData, "76-4");
			backgroundMySC.LifeTime = 350;
			backgroundMySC.MaxTransparent = 180;
			backgroundMySC.TransparentValueF = 0f;
			backgroundMySC.TransparentVelocity = 4f;
			backgroundMySC.ColorValue = Color.Green;
			var backgroundMySC2 = new BackgroundMySC(StageData, "76-4");
			backgroundMySC2.LifeTime = 350;
			backgroundMySC2.MaxTransparent = 150;
			backgroundMySC2.TransparentValueF = 0f;
			backgroundMySC2.TransparentVelocity = 4f;
			backgroundMySC2.ColorValue = Color.Green;
			backgroundMySC2.AngularVelocityDegree = -0.5f;
			backgroundMySC2.Active = true;
			StageData.VibrateStart(LifeTime);
			MyPlane.HighSpeed = 2f;
			MyPlane.LowSpeed = 1f;
			TransparentValueF = 0f;
			TransparentVelocity = 4f;
			ScaleLength = 0.2f;
			ScaleVelocity = 0.05f;
			MaxScale = 2f;
		}

		public override void Move()
		{
			OriginalPosition = MyPlane.OriginalPosition;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time < 16)
			{
				TxtureObject = TextureObjectDictionary["wind" + string.Format("{0:00}", Time % 16 / 2)];
			}
			else if (Time < 284)
			{
				TxtureObject = TextureObjectDictionary["wind" + string.Format("{0:00}", 7 + (Time - 16) % 36 / 2)];
			}
			else
			{
				TxtureObject = TextureObjectDictionary["wind" + string.Format("{0:00}", 25 + (Time - 300 + 16) % 16 / 2)];
			}
			TxtureObject.OffsetY = 100;
			if (Time == 240)
			{
				TransparentVelocity = -4f;
			}
		}

		public override bool OutBoundary()
		{
			var flag = base.OutBoundary();
			if (flag)
			{
				MyPlane.HighSpeed = 5f;
				MyPlane.LowSpeed = 2f;
			}
			return flag;
		}

		public override bool HitCheck(BaseObject Target)
		{
			return Target.OriginalPosition.Y - OriginalPosition.Y < 2.8f * (Target.OriginalPosition.X - OriginalPosition.X) + 136f && Target.OriginalPosition.Y - OriginalPosition.Y < -2.8f * (Target.OriginalPosition.X - OriginalPosition.X) + 136f;
		}

		public override void HitCheckAll()
		{
			for (var i = BulletList.Count - 1; i >= 0; i--)
			{
				if (HitCheck(BulletList[i]))
				{
					if (!BulletList[i].UnRemoveable)
					{
						BulletList[i].GiveEndEffect();
						BulletList[i].GiveItems();
						BulletList.RemoveAt(i);
					}
				}
			}
			for (var i = EnemyPlaneList.Count - 1; i >= 0; i--)
			{
				if (HitCheck(EnemyPlaneList[i]))
				{
					var num = Damage + (int)((400.0 - EnemyPlaneList[i].GetDistance(MyPlane)) / 50.0);
					if (num < Damage)
					{
						num = Damage;
					}
					else if (num > 19)
					{
						num = 19;
					}
					EnemyPlaneList[i].HealthPoint -= (float)num;
					if (EnemyPlaneList[i].HealthPoint <= 0f)
					{
						EnemyPlaneList[i].GiveEndEffect();
						EnemyPlaneList[i].GiveItems();
						EnemyPlaneList.RemoveAt(i);
						StageData.SoundPlay("se_enep00.wav");
						break;
					}
				}
			}
			if (Boss != null)
			{
				if (Boss.Time > Boss.UnmatchedTime && HitCheck(Boss))
				{
					var num = Damage + (int)((400.0 - Boss.GetDistance(MyPlane)) / 40.0);
					if (num < Damage)
					{
						num = Damage;
					}
					else if (num > 19)
					{
						num = 19;
					}
					Boss.HealthPoint -= (float)((int)((float)num * (1f - Boss.Armon)));
					if (Boss.HealthPoint <= 200f)
					{
						StageData.SoundPlay("se_damage01.wav");
					}
				}
			}
		}
	}
}
