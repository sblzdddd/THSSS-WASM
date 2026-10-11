using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000B1 RID: 177
	public class Spell_PlaneB : BaseSpell
	{
		// Token: 0x06000518 RID: 1304 RVA: 0x000291B8 File Offset: 0x000273B8
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

		// Token: 0x06000519 RID: 1305 RVA: 0x00029281 File Offset: 0x00027481
		public override void Move()
		{
			base.OriginalPosition = base.MyPlane.OriginalPosition;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00029298 File Offset: 0x00027498
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

		// Token: 0x0600051B RID: 1307 RVA: 0x0002949C File Offset: 0x0002769C
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

		// Token: 0x0600051C RID: 1308 RVA: 0x000294E4 File Offset: 0x000276E4
		public override bool HitCheck(BaseObject Target)
		{
			return Target.OriginalPosition.Y < base.OriginalPosition.Y + 5f && base.OriginalPosition.X - 120f < Target.OriginalPosition.X && Target.OriginalPosition.X < base.OriginalPosition.X + 120f;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00029574 File Offset: 0x00027774
		public override void HitCheckAll()
		{
			if (base.Time >= 90)
			{
				base.HitCheckAll();
			}
		}
	}
}
