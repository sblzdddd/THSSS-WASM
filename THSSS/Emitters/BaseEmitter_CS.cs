using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	[Serializable]
	public class BaseEmitter_CS : BaseEnemyPlane
	{
		public bool BindingState { get; set; }

		public int BindingID { get; set; }

		public bool BindWithDirection { get; set; }

		public int StartTime { get; set; }

		public int Duration { get; set; }

		public PointF EmitPoint { get; set; }

		public float EmitRadius { get; set; }

		public double RadiusDirection { get; set; }

		public PointF RadiusDirectionPoint { get; set; }

		public int Way { get; set; }

		public int Circle { get; set; }

		public double EmitDirection { get; set; }

		public PointF EmitDirectionPoint { get; set; }

		public double Range { get; set; }

		public bool RDirectionWithDirection { get; set; }

		public float RanX { get; set; }

		public float RanY { get; set; }

		public float RanRadius { get; set; }

		public double RanRadiusDirection { get; set; }

		public int RanWay { get; set; }

		public int RanCircle { get; set; }

		public double RanEmitDirection { get; set; }

		public double RanRange { get; set; }

		public bool DeepBinding { get; set; }

		public bool RangeShoot { get; set; }

		public bool MotionBinding { get; set; }

		public string SoundName { get; set; }

		public bool SpecifySE { get; set; }

		public int EffectType { get; set; }

		public int Count { get; set; }

		public float DeltaV { get; set; }

		public CS_Data CSData { get; set; }

		private BaseBullet_CS CSBullet { get; set; }

		private BaseStraightLaser_CS CSLaserS { get; set; }

		private BaseRadialLaser_CS CSLaserR { get; set; }

		private BaseBendLaser_CS CSLaserB { get; set; }

		private BaseEnemyPlane_Touhou CSEnemy { get; set; }

		private BaseEffect_CS CSEffect { get; set; }

		public EmitterMode EmitterMode { get; set; }

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

		public BaseEmitter_CS()
		{
		}

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

		public override bool HitCheck(BaseObject MyPlane)
		{
			return false;
		}
	}
}
