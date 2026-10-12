using System.Drawing;

namespace Shooting
{
	internal class Boss_Ami03 : BaseBossTouhou
	{
		public Boss_Ami03(StageDataPackage StageData)
			: base(StageData)
		{
			var array = new TextureObject[3, 4];
			array[0, 0] = TextureObjectDictionary["BossAmi-00"];
			array[0, 1] = TextureObjectDictionary["BossAmi-01"];
			array[0, 2] = TextureObjectDictionary["BossAmi-02"];
			array[0, 3] = TextureObjectDictionary["BossAmi-03"];
			array[1, 0] = TextureObjectDictionary["BossAmi-10"];
			array[1, 1] = TextureObjectDictionary["BossAmi-11"];
			array[1, 2] = TextureObjectDictionary["BossAmi-12"];
			array[1, 3] = TextureObjectDictionary["BossAmi-13"];
			array[2, 0] = TextureObjectDictionary["BossAmi-20"];
			array[2, 1] = TextureObjectDictionary["BossAmi-21"];
			array[2, 2] = TextureObjectDictionary["BossAmi-22"];
			array[2, 3] = TextureObjectDictionary["BossAmi-23"];
			TxtureObjects = array;
			TxtureObject = TxtureObjects[0, 0];
			OriginalPosition = new PointF((float)(BoundRect.Width / 2), 150f);
			DestPoint = new Point(BoundRect.Width / 2, 150);
			MaxHP = 10000;
			HealthPoint = (float)MaxHP;
			Life = 1;
			Region = 20;
			Velocity = 4f;
			DirectionDegree = 90.0;
			SpellTime = 2400;
			LifeTime = 100000;
			OnSpell = false;
			SetBossLifeLineTex();
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (!OnSpell)
			{
				Armon = 0f;
			}
			switch (Life)
			{
			case 0:
				Velocity = 0f;
				DestPoint = new Point(Ran.Next(100, BoundRect.Width - 100), Ran.Next(70, 200));
				break;
			case 1:
				Ctrl1();
				break;
			}
			MoveToPoint(DestPoint);
			if (!OnSpell)
			{
				if (HealthPoint < (float)SpellcardHP || Time > SpellTime)
				{
					HealthPoint = (float)SpellcardHP;
					Time = 0;
					OnSpell = true;
					new ShootingStarShard(StageData, new PointF((float)(BoundRect.Width / 2), 0f));
				}
			}
			else if (HealthPoint <= 0f || Time > SpellTime)
			{
				Life--;
				OnAction = 0;
				if (HealthPoint <= 0f && Life >= -1)
				{
					GiveItems();
					new ShootingStarShard(StageData, new PointF((float)(BoundRect.Width / 2), 0f));
				}
				if (Life <= 0)
				{
					if (Life == 0)
					{
						DestPoint = new Point(Ran.Next((int)OriginalPosition.X - 30, (int)OriginalPosition.X + 30), Ran.Next((int)OriginalPosition.Y + 30, (int)OriginalPosition.Y + 50));
						Velocity = 0.5f;
						new BulletRemover2(StageData, OriginalPosition);
						HealthPoint = 0f;
						GiveEndEffect();
						Life = -1;
					}
				}
				else
				{
					new BulletRemover2(StageData, OriginalPosition);
					StageData.SoundPlay("se_tan00.wav");
					HealthPoint = (float)MaxHP;
					Time = 0;
					OnSpell = false;
					DestPoint = new Point(BoundRect.Width / 2, 120);
					Velocity = 4f;
				}
			}
		}

		public override void GiveEndEffect()
		{
			TransparentValueF = 255f;
			new Background2DRemover(StageData);
			Background3D.WarpEnabled = false;
			Region = 0;
			StageData.SoundPlay("se_tan01.wav");
			LifeTime = Time + 150;
			new EndBoss_Touhou01(StageData, OriginalPosition, Color.YellowGreen, Color.Green);
		}

		private void Ctrl1()
		{
			if (!OnSpell)
			{
				if (Time == 1)
				{
					OnSpell = true;
					SpellTime = 3000;
				}
			}
			else
			{
				Boss.Armon = 0.4f;
				if (Time == 2)
				{
					new SpellCard_SSS01_FSC(StageData);
				}
			}
		}

		public override void GiveItems()
		{
			base.GiveItems();
			new SpellItem_Touhou(StageData, OriginalPosition);
		}
	}
}
