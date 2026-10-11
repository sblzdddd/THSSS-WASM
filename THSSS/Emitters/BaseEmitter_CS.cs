using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000F9 RID: 249
	[Serializable]
	public class BaseEmitter_CS : BaseEnemyPlane
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0003D1A4 File Offset: 0x0003B3A4
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x0003D1BB File Offset: 0x0003B3BB
		public bool BindingState { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0003D1C4 File Offset: 0x0003B3C4
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x0003D1DB File Offset: 0x0003B3DB
		public int BindingID { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0003D1E4 File Offset: 0x0003B3E4
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x0003D1FB File Offset: 0x0003B3FB
		public bool BindWithDirection { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0003D204 File Offset: 0x0003B404
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x0003D21B File Offset: 0x0003B41B
		public int StartTime { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0003D224 File Offset: 0x0003B424
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x0003D23B File Offset: 0x0003B43B
		public int Duration { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0003D244 File Offset: 0x0003B444
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0003D25B File Offset: 0x0003B45B
		public PointF EmitPoint { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0003D264 File Offset: 0x0003B464
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x0003D27B File Offset: 0x0003B47B
		public float EmitRadius { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0003D284 File Offset: 0x0003B484
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x0003D29B File Offset: 0x0003B49B
		public double RadiusDirection { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0003D2A4 File Offset: 0x0003B4A4
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x0003D2BB File Offset: 0x0003B4BB
		public PointF RadiusDirectionPoint { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0003D2C4 File Offset: 0x0003B4C4
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x0003D2DB File Offset: 0x0003B4DB
		public int Way { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0003D2E4 File Offset: 0x0003B4E4
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x0003D2FB File Offset: 0x0003B4FB
		public int Circle { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0003D304 File Offset: 0x0003B504
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x0003D31B File Offset: 0x0003B51B
		public double EmitDirection { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0003D324 File Offset: 0x0003B524
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0003D33B File Offset: 0x0003B53B
		public PointF EmitDirectionPoint { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0003D344 File Offset: 0x0003B544
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x0003D35B File Offset: 0x0003B55B
		public double Range { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0003D364 File Offset: 0x0003B564
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x0003D37B File Offset: 0x0003B57B
		public bool RDirectionWithDirection { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0003D384 File Offset: 0x0003B584
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x0003D39B File Offset: 0x0003B59B
		public float RanX { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0003D3A4 File Offset: 0x0003B5A4
		// (set) Token: 0x060006CA RID: 1738 RVA: 0x0003D3BB File Offset: 0x0003B5BB
		public float RanY { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0003D3C4 File Offset: 0x0003B5C4
		// (set) Token: 0x060006CC RID: 1740 RVA: 0x0003D3DB File Offset: 0x0003B5DB
		public float RanRadius { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0003D3E4 File Offset: 0x0003B5E4
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x0003D3FB File Offset: 0x0003B5FB
		public double RanRadiusDirection { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0003D404 File Offset: 0x0003B604
		// (set) Token: 0x060006D0 RID: 1744 RVA: 0x0003D41B File Offset: 0x0003B61B
		public int RanWay { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0003D424 File Offset: 0x0003B624
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x0003D43B File Offset: 0x0003B63B
		public int RanCircle { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0003D444 File Offset: 0x0003B644
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x0003D45B File Offset: 0x0003B65B
		public double RanEmitDirection { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0003D464 File Offset: 0x0003B664
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x0003D47B File Offset: 0x0003B67B
		public double RanRange { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0003D484 File Offset: 0x0003B684
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x0003D49B File Offset: 0x0003B69B
		public bool DeepBinding { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0003D4A4 File Offset: 0x0003B6A4
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x0003D4BB File Offset: 0x0003B6BB
		public bool RangeShoot { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0003D4C4 File Offset: 0x0003B6C4
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x0003D4DB File Offset: 0x0003B6DB
		public bool MotionBinding { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0003D4E4 File Offset: 0x0003B6E4
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x0003D4FB File Offset: 0x0003B6FB
		public string SoundName { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0003D504 File Offset: 0x0003B704
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x0003D51B File Offset: 0x0003B71B
		public bool SpecifySE { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0003D524 File Offset: 0x0003B724
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x0003D53B File Offset: 0x0003B73B
		public int EffectType { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0003D544 File Offset: 0x0003B744
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x0003D55B File Offset: 0x0003B75B
		public int Count { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0003D564 File Offset: 0x0003B764
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x0003D57B File Offset: 0x0003B77B
		public float DeltaV { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0003D584 File Offset: 0x0003B784
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x0003D59B File Offset: 0x0003B79B
		public CS_Data CSData { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x0003D5A4 File Offset: 0x0003B7A4
		// (set) Token: 0x060006EA RID: 1770 RVA: 0x0003D5BB File Offset: 0x0003B7BB
		private BaseBullet_CS CSBullet { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0003D5C4 File Offset: 0x0003B7C4
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x0003D5DB File Offset: 0x0003B7DB
		private BaseStraightLaser_CS CSLaserS { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0003D5E4 File Offset: 0x0003B7E4
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x0003D5FB File Offset: 0x0003B7FB
		private BaseRadialLaser_CS CSLaserR { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0003D604 File Offset: 0x0003B804
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x0003D61B File Offset: 0x0003B81B
		private BaseBendLaser_CS CSLaserB { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0003D624 File Offset: 0x0003B824
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x0003D63B File Offset: 0x0003B83B
		private BaseEnemyPlane_Touhou CSEnemy { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0003D644 File Offset: 0x0003B844
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x0003D65B File Offset: 0x0003B85B
		private BaseEffect_CS CSEffect { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0003D664 File Offset: 0x0003B864
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x0003D67B File Offset: 0x0003B87B
		public EmitterMode EmitterMode { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0003D684 File Offset: 0x0003B884
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x0003D720 File Offset: 0x0003B920
		public BaseObject_CS SubBullet
		{
			get
			{
				BaseObject_CS baseObject_CS;
				if (this.EmitterMode == EmitterMode.Enemy)
				{
					baseObject_CS = this.CSEnemy;
				}
				else if (this.EmitterMode == EmitterMode.Effect)
				{
					baseObject_CS = this.CSEffect;
				}
				else if (this.EmitterMode == EmitterMode.StraightLaser)
				{
					baseObject_CS = this.CSLaserS;
				}
				else if (this.EmitterMode == EmitterMode.RadialLaser)
				{
					baseObject_CS = this.CSLaserR;
				}
				else if (this.EmitterMode == EmitterMode.BendLaser)
				{
					baseObject_CS = this.CSLaserB;
				}
				else
				{
					baseObject_CS = this.CSBullet;
				}
				return baseObject_CS;
			}
			set
			{
				if (this.EmitterMode == EmitterMode.Enemy)
				{
					this.CSEnemy = (BaseEnemyPlane_Touhou)value;
				}
				else if (this.EmitterMode == EmitterMode.Effect)
				{
					this.CSEffect = (BaseEffect_CS)value;
				}
				else if (this.EmitterMode == EmitterMode.StraightLaser)
				{
					this.CSLaserS = (BaseStraightLaser_CS)value;
				}
				else if (this.EmitterMode == EmitterMode.RadialLaser)
				{
					this.CSLaserR = (BaseRadialLaser_CS)value;
				}
				else if (this.EmitterMode == EmitterMode.BendLaser)
				{
					this.CSLaserB = (BaseBendLaser_CS)value;
				}
				else
				{
					this.CSBullet = (BaseBullet_CS)value;
				}
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0003D7E5 File Offset: 0x0003B9E5
		public BaseEmitter_CS()
		{
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0003D7F0 File Offset: 0x0003B9F0
		public BaseEmitter_CS(StageDataPackage StageData, CS_Data CSData, EmitterMode EmitterMode)
		{
			base.StageData = StageData;
			this.CSData = CSData;
			base.EventGroupList = new List<EventGroup>();
			base.EventsExecutionList = new List<Execution>();
			base.HealthPoint = 1000000f;
			base.HitEnabled = false;
			base.Region = 0;
			this.Count = 1;
			this.EmitterMode = EmitterMode;
			if (EmitterMode == EmitterMode.Bullet)
			{
				this.CSBullet = new BaseBullet_CS(StageData);
			}
			else if (EmitterMode == EmitterMode.StraightLaser)
			{
				this.CSLaserS = new BaseStraightLaser_CS(StageData);
			}
			else if (EmitterMode == EmitterMode.RadialLaser)
			{
				this.CSLaserR = new BaseRadialLaser_CS(StageData);
			}
			else if (EmitterMode == EmitterMode.BendLaser)
			{
				this.CSLaserB = new BaseBendLaser_CS(StageData);
			}
			else if (EmitterMode == EmitterMode.Effect)
			{
				this.CSEffect = new BaseEffect_CS(StageData);
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0003D8E4 File Offset: 0x0003BAE4
		public override void Ctrl()
		{
			if (base.Time == this.StartTime)
			{
				base.Velocity += (float)((double)base.RanVelocity * base.Ran.NextPMDouble());
				base.DirectionDegree += base.RanDirection * base.Ran.NextPMDouble();
				base.AccelerateCS += (float)((double)base.RanAccelerate * base.Ran.NextPMDouble());
				base.AccDirection += (double)((float)(base.RanAccDirection * base.Ran.NextPMDouble()));
			}
			if (base.LifeTime > this.StartTime + this.Duration)
			{
				base.LifeTime = this.StartTime + this.Duration;
			}
			if (this.Range != 360.0)
			{
				this.RangeShoot = true;
			}
			if (this.BindingObj != null)
			{
				if (this.BindingObj is BaseEnemyPlane_Touhou)
				{
					if (this.BindingObj.HealthPoint <= 0f)
					{
						base.EnemyPlaneList.Remove(this);
					}
				}
			}
			base.Ctrl();
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0003DA49 File Offset: 0x0003BC49
		public override void EventCtrl()
		{
			base.EventGroupList.ForEach(delegate(EventGroup a)
			{
				a.Update(this);
			});
			base.EventsExecutionList.ForEach(delegate(Execution a)
			{
				a.Update(this);
			});
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0003DA7C File Offset: 0x0003BC7C
		public override void Shoot()
		{
			if (base.Time >= this.StartTime && base.Time <= this.StartTime + this.Duration)
			{
				int num = this.Circle + (int)((double)this.RanCircle * base.Ran.NextPMDouble());
				num = ((num < 1) ? 1 : num);
				if (!this.DeepBinding && (base.Time - this.StartTime + 1) % num == 0)
				{
					this.ShootBullet();
				}
				else if (this.DeepBinding && base.Time % num == 0)
				{
					this.ShootBullet();
				}
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0003DF0C File Offset: 0x0003C10C
		public void ShootBullet()
		{
			float num = this.EmitPoint.X;
			float num2 = this.EmitPoint.Y;
			if (num == -99999f)
			{
				num = base.MyPlane.OriginalPosition.X;
			}
			else if (num == -99998f)
			{
				num = base.OriginalPosition.X;
			}
			else
			{
				num = num - 320f + 192f;
			}
			if (num2 == -99999f)
			{
				num2 = base.MyPlane.OriginalPosition.Y;
			}
			else if (num2 == -99998f)
			{
				num2 = base.OriginalPosition.Y;
			}
			else
			{
				num2 = num2 - 240f + 224f;
			}
			if (this.EmitterMode == EmitterMode.StraightLaser || this.EmitterMode == EmitterMode.RadialLaser || this.EmitterMode == EmitterMode.BendLaser)
			{
				num = base.OriginalPosition.X;
				num2 = base.OriginalPosition.Y;
			}
			PointF pointF = new PointF(num + (float)((double)this.RanX * base.Ran.NextPMDouble()), num2 + (float)((double)this.RanY * base.Ran.NextPMDouble()));
			double edi;
			if (this.EmitDirection == -99999.0)
			{
				edi = base.GetDirection(base.MyPlane) + (this.RanEmitDirection * base.Ran.NextPMDouble() + this.SubBullet.RanDirection * base.Ran.NextPMDouble()) * 3.141592653589793 / 180.0;
			}
			else
			{
				edi = (this.EmitDirection + this.RanEmitDirection * base.Ran.NextPMDouble() + this.SubBullet.RanDirection * base.Ran.NextPMDouble()) * 3.141592653589793 / 180.0;
			}
			double num3;
			if (this.RadiusDirection == -99999.0)
			{
				num3 = base.GetDirection(base.MyPlane) + this.RanRadiusDirection * base.Ran.NextPMDouble() * 3.141592653589793 / 180.0;
			}
			else
			{
				num3 = (this.RadiusDirection + this.RanRadiusDirection * base.Ran.NextPMDouble()) * 3.141592653589793 / 180.0;
			}
			float num4 = this.EmitRadius + (float)((double)this.RanRadius * base.Ran.NextPMDouble());
			int num5 = this.Way + (int)((double)this.RanWay * base.Ran.NextPMDouble());
			float num6 = this.SubBullet.Velocity + (float)((double)this.SubBullet.RanVelocity * base.Ran.NextPMDouble());
			float num7 = this.SubBullet.AccelerateCS + (float)((double)this.SubBullet.RanAccelerate * base.Ran.NextPMDouble());
			double num8 = this.SubBullet.AccDirection + this.SubBullet.RanAccDirection;
			double num9 = this.Range + (double)((float)(this.RanRange * base.Ran.NextPMDouble()));
			if (this.RDirectionWithDirection)
			{
				num3 += edi;
			}
			double num10 = num9 * 3.141592653589793 / 180.0 / (double)num5;
			edi -= (double)(num5 - 1) * num10 / 2.0;
			num3 -= (double)(num5 - 1) * num10 / 2.0;
			if (this.EffectType == 2)
			{
				new EmitterSaveEnegy3D(base.StageData, pointF, this.CSEffect.ColorValue);
				base.StageData.SoundPlay("se_ch02.wav");
			}
			else if (this.EffectType == 3)
			{
				new EmitterGiveOutEnegy3D(base.StageData, pointF, this.CSEffect.ColorValue);
				base.StageData.SoundPlay("se_cat00.wav");
				base.StageData.SoundPlay("se_enep02.wav");
			}
			else
			{
				for (int i = 0; i < num5; i++)
				{
					PointF p = new PointF((float)((double)pointF.X + (double)num4 * Math.Cos(num3)), (float)((double)pointF.Y + (double)num4 * Math.Sin(num3)));
					for (int j = 0; j < this.Count; j++)
					{
						if (this.EmitterMode == EmitterMode.Bullet || this.EmitterMode == EmitterMode.StraightLaser || this.EmitterMode == EmitterMode.RadialLaser || this.EmitterMode == EmitterMode.BendLaser)
						{
							BaseBullet_Touhou b = new BaseBullet_Touhou(base.StageData);
							if (this.CSBullet != null)
							{
								b = (BaseBullet_CS)this.CSBullet.Clone();
								b.OriginalPosition = p;
								b.GhostingCount = b.GhostingCount;
								b.AngleDegree += this.CSBullet.RanAngle * base.Ran.NextPMDouble();
							}
							else if (this.CSLaserS != null)
							{
								b = (BaseStraightLaser_CS)this.CSLaserS.Clone();
								b.OriginalPosition = p;
								b.Angle = -1.5707963267948966;
								b.Active = true;
							}
							else if (this.CSLaserR != null)
							{
								b = (BaseRadialLaser_CS)this.CSLaserR.Clone();
								b.OriginalPosition = p;
								b.Angle = 1.5707963267948966;
								b.UnRemoveable = true;
								b.Active = true;
							}
							else if (this.CSLaserB != null)
							{
								b = (BaseBendLaser_CS)this.CSLaserB.Clone();
								b.OriginalPosition = p;
								b.UnRemoveable = true;
								b.Active = true;
							}
							b.GhostingCount = b.GhostingCount;
							b.Velocity = num6 - (float)j * this.DeltaV;
							b.Direction = edi;
							b.AccelerateCS = num7;
							b.AccDirection = num8;
							b.ID = base.ID;
							b.LayerID = base.LayerID;
							if (this.MotionBinding)
							{
								b.SetBinding(this);
							}
							base.BulletList.Add(b);
							if (this.EmitterMode == EmitterMode.Bullet)
							{
								this.CSData.EmitterList.ForEach(delegate(BaseEmitter_CS em)
								{
									if (em.BindingID == this.ID)
									{
										b.UnRemoveable = true;
										BaseEmitter_CS baseEmitter_CS = (BaseEmitter_CS)em.Clone();
										this.StageData.EnemyPlaneList.Add(baseEmitter_CS);
										baseEmitter_CS.OriginalPosition = p;
										baseEmitter_CS.LifeTime = this.SubBullet.LifeTime;
										baseEmitter_CS.ColorValue = this.SubBullet.ColorValue;
										baseEmitter_CS.TransparentValueF = this.SubBullet.TransparentValueF;
										baseEmitter_CS.Direction = edi;
										baseEmitter_CS.DestPoint = this.SubBullet.DestPoint;
										baseEmitter_CS.Active = this.SubBullet.Active;
										baseEmitter_CS.OutBound = this.SubBullet.OutBound;
										if (baseEmitter_CS.BindWithDirection)
										{
											baseEmitter_CS.EmitDirection += edi * 180.0 / 3.141592653589793;
										}
										baseEmitter_CS.SetBinding(b);
										if (!baseEmitter_CS.DeepBinding)
										{
											baseEmitter_CS.Time = this.Time;
											baseEmitter_CS.LifeTime = Math.Min(this.SubBullet.LifeTime + this.Time, em.StartTime + em.Duration);
										}
									}
								});
							}
						}
						else if (this.EmitterMode == EmitterMode.Enemy)
						{
							BaseEnemyPlane_Touhou enemy = (BaseEnemyPlane_Touhou)this.SubBullet.Clone();
							enemy.LifeTime = 0;
							enemy.OriginalPosition = p;
							enemy.GhostingCount = enemy.GhostingCount;
							enemy.Velocity = num6 - (float)j * this.DeltaV;
							enemy.Direction = edi;
							enemy.AccelerateCS = num7;
							enemy.AccDirection = num8;
							enemy.ID = base.ID;
							enemy.LayerID = base.LayerID;
							if (this.MotionBinding)
							{
								enemy.SetBinding(this);
							}
							base.EnemyPlaneList.Add(enemy);
							this.CSData.EmitterList.ForEach(delegate(BaseEmitter_CS em)
							{
								if (em.BindingID == this.ID)
								{
									BaseEmitter_CS baseEmitter_CS2 = (BaseEmitter_CS)em.Clone();
									this.StageData.EnemyPlaneList.Add(baseEmitter_CS2);
									baseEmitter_CS2.OriginalPosition = p;
									baseEmitter_CS2.LifeTime = this.SubBullet.LifeTime;
									baseEmitter_CS2.ColorValue = this.SubBullet.ColorValue;
									baseEmitter_CS2.TransparentValueF = this.SubBullet.TransparentValueF;
									baseEmitter_CS2.Direction = edi;
									baseEmitter_CS2.DestPoint = this.SubBullet.DestPoint;
									baseEmitter_CS2.Active = this.SubBullet.Active;
									baseEmitter_CS2.OutBound = this.SubBullet.OutBound;
									baseEmitter_CS2.SetBinding(enemy);
									if (baseEmitter_CS2.BindWithDirection)
									{
										baseEmitter_CS2.EmitDirection += edi * 180.0 / 3.141592653589793;
									}
									if (!baseEmitter_CS2.DeepBinding)
									{
										baseEmitter_CS2.Time = this.Time;
										baseEmitter_CS2.LifeTime = Math.Min(this.SubBullet.LifeTime + this.Time, em.StartTime + em.Duration);
									}
								}
							});
						}
						else if (this.EmitterMode == EmitterMode.Effect)
						{
							BaseEffect_CS baseEffect_CS = (BaseEffect_CS)this.CSEffect.Clone();
							baseEffect_CS.OriginalPosition = p;
							baseEffect_CS.GhostingCount = baseEffect_CS.GhostingCount;
							baseEffect_CS.AngleDegree += this.CSEffect.RanAngle * base.Ran.NextPMDouble();
							baseEffect_CS.Velocity = num6 - (float)j * this.DeltaV;
							baseEffect_CS.Direction = edi;
							baseEffect_CS.AccelerateCS = num7;
							baseEffect_CS.AccDirection = num8;
							baseEffect_CS.ID = base.ID;
							baseEffect_CS.LayerID = base.LayerID;
							if (this.MotionBinding)
							{
								baseEffect_CS.SetBinding(this);
							}
							base.EffectList.Add(baseEffect_CS);
						}
					}
					num3 += num10;
					edi += num10;
					if (this.SpecifySE)
					{
						base.StageData.SoundPlay(this.SoundName);
					}
					else if (this.EmitterMode == EmitterMode.Bullet)
					{
						base.StageData.SoundPlay("se_tan00a.wav", pointF.X / (float)base.BoundRect.Width);
					}
					else if (this.EmitterMode == EmitterMode.StraightLaser || this.EmitterMode == EmitterMode.RadialLaser)
					{
						base.StageData.SoundPlay("se_lazer00.wav", pointF.X / (float)base.BoundRect.Width);
					}
				}
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0003EA10 File Offset: 0x0003CC10
		public override object Clone()
		{
			BaseEmitter_CS baseEmitter_CS = (BaseEmitter_CS)base.Clone();
			if (this.CSBullet != null)
			{
				baseEmitter_CS.CSBullet = (BaseBullet_CS)this.CSBullet.Clone();
			}
			if (this.CSLaserS != null)
			{
				baseEmitter_CS.CSLaserS = (BaseStraightLaser_CS)this.CSLaserS.Clone();
			}
			if (this.CSLaserR != null)
			{
				baseEmitter_CS.CSLaserR = (BaseRadialLaser_CS)this.CSLaserR.Clone();
			}
			if (this.CSLaserB != null)
			{
				baseEmitter_CS.CSLaserB = (BaseBendLaser_CS)this.CSLaserB.Clone();
			}
			if (this.CSEnemy != null)
			{
				baseEmitter_CS.CSEnemy = (BaseEnemyPlane_Touhou)this.CSEnemy.Clone();
			}
			if (this.CSEffect != null)
			{
				baseEmitter_CS.CSEffect = (BaseEffect_CS)this.CSEffect.Clone();
			}
			return baseEmitter_CS;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0003EB14 File Offset: 0x0003CD14
		public override bool HitCheck(BaseObject MyPlane)
		{
			return false;
		}
	}
}
