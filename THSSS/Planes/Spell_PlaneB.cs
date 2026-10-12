using System.Drawing;

namespace Shooting
{
	public class Spell_PlaneB : BaseSpell
	{
		public Spell_PlaneB(StageDataPackage StageData)
		{
			this.StageData = StageData;
			OriginalPosition = MyPlane.OriginalPosition;
			Damage = 45;
			Region = 30;
			LifeTime = 390;
			Direction = 1.5707963267948966;
			SpellList.Add(this);
			var backgroundBlack = new BackgroundBlack(StageData, Position);
			backgroundBlack.LifeTime = 410;
			MyPlane.HighSpeed = 1.6f;
			MyPlane.LowSpeed = 0.8f;
			var emitterSaveEnegy = new EmitterSaveEnegy3(StageData, Position);
			emitterSaveEnegy.SetBinding(this);
			StageData.SoundPlay("boss_change.wav");
		}

		public override void Move()
		{
			OriginalPosition = MyPlane.OriginalPosition;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time >= 90)
			{
				if (Time == 90)
				{
					StageData.SoundPlay("se_nep00.wav");
					StageData.SoundPlay("se_spell.wav");
					StageData.VibrateStart(300);
					var pointF = new PointF(OriginalPosition.X, OriginalPosition.Y + 70f);
					var planeSpark = new PlaneSpark(StageData, "Master Spark", pointF, 0f, -1.5707963267948966)
					{
						MaxScaleW = 1f,
						MaxScaleL = 3f
					};
					planeSpark.SetBinding(this);
					planeSpark = new PlaneSpark(StageData, "Master Spark", pointF, 0f, -1.5707963267948966)
					{
						MaxScaleW = 1.6f,
						MaxScaleL = 3f
					};
					planeSpark.SetBinding(this);
					planeSpark = new PlaneSpark(StageData, "Master Spark", pointF, 0f, -1.5707963267948966)
					{
						MaxScaleW = 2.2f,
						MaxScaleL = 3f
					};
					planeSpark.SetBinding(this);
				}
				if (Time == LifeTime - 30)
				{
					Damage = 20;
				}
				if (MyPlane.Position.Y < (float)BoundRect.Bottom)
				{
					MyPlane.Position = new PointF(MyPlane.Position.X, MyPlane.Position.Y + 8f);
				}
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
			return Target.OriginalPosition.Y < OriginalPosition.Y + 5f && OriginalPosition.X - 120f < Target.OriginalPosition.X && Target.OriginalPosition.X < OriginalPosition.X + 120f;
		}

		public override void HitCheckAll()
		{
			if (Time >= 90)
			{
				base.HitCheckAll();
			}
		}
	}
}
