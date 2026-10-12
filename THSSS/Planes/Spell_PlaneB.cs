using System.Drawing;

namespace Shooting
{
	public class Spell_PlaneB : BaseSpell
	{
		public Spell_PlaneB(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.OriginalPosition = base.MyPlane.OriginalPosition;
			base.Damage = 45;
			base.Region = 30;
			base.LifeTime = 390;
			base.Direction = 1.5707963267948966;
			base.SpellList.Add(this);
			BackgroundBlack backgroundBlack = new BackgroundBlack(StageData, base.Position);
			backgroundBlack.LifeTime = 410;
			base.MyPlane.HighSpeed = 1.6f;
			base.MyPlane.LowSpeed = 0.8f;
			EmitterSaveEnegy3 emitterSaveEnegy = new EmitterSaveEnegy3(StageData, base.Position);
			emitterSaveEnegy.SetBinding(this);
			StageData.SoundPlay("boss_change.wav");
		}

		public override void Move()
		{
			base.OriginalPosition = base.MyPlane.OriginalPosition;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time >= 90)
			{
				if (base.Time == 90)
				{
					base.StageData.SoundPlay("se_nep00.wav");
					base.StageData.SoundPlay("se_spell.wav");
					base.StageData.VibrateStart(300);
					PointF pointF = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y + 70f);
					PlaneSpark planeSpark = new PlaneSpark(base.StageData, "Master Spark", pointF, 0f, -1.5707963267948966)
					{
						MaxScaleW = 1f,
						MaxScaleL = 3f
					};
					planeSpark.SetBinding(this);
					planeSpark = new PlaneSpark(base.StageData, "Master Spark", pointF, 0f, -1.5707963267948966)
					{
						MaxScaleW = 1.6f,
						MaxScaleL = 3f
					};
					planeSpark.SetBinding(this);
					planeSpark = new PlaneSpark(base.StageData, "Master Spark", pointF, 0f, -1.5707963267948966)
					{
						MaxScaleW = 2.2f,
						MaxScaleL = 3f
					};
					planeSpark.SetBinding(this);
				}
				if (base.Time == base.LifeTime - 30)
				{
					base.Damage = 20;
				}
				if (base.MyPlane.Position.Y < (float)base.BoundRect.Bottom)
				{
					base.MyPlane.Position = new PointF(base.MyPlane.Position.X, base.MyPlane.Position.Y + 8f);
				}
			}
		}

		public override bool OutBoundary()
		{
			bool flag = base.OutBoundary();
			if (flag)
			{
				base.MyPlane.HighSpeed = 5f;
				base.MyPlane.LowSpeed = 2f;
			}
			return flag;
		}

		public override bool HitCheck(BaseObject Target)
		{
			return Target.OriginalPosition.Y < base.OriginalPosition.Y + 5f && base.OriginalPosition.X - 120f < Target.OriginalPosition.X && Target.OriginalPosition.X < base.OriginalPosition.X + 120f;
		}

		public override void HitCheckAll()
		{
			if (base.Time >= 90)
			{
				base.HitCheckAll();
			}
		}
	}
}
