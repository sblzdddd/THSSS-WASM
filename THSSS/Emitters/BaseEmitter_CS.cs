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
				if (EmitterMode == EmitterMode.Enemy)
				{
					baseObject_CS = CSEnemy;
				}
				else if (EmitterMode == EmitterMode.Effect)
				{
					baseObject_CS = CSEffect;
				}
				else if (EmitterMode == EmitterMode.StraightLaser)
				{
					baseObject_CS = CSLaserS;
				}
				else if (EmitterMode == EmitterMode.RadialLaser)
				{
					baseObject_CS = CSLaserR;
				}
				else if (EmitterMode == EmitterMode.BendLaser)
				{
					baseObject_CS = CSLaserB;
				}
				else
				{
					baseObject_CS = CSBullet;
				}
				return baseObject_CS;
			}
			set
			{
				if (EmitterMode == EmitterMode.Enemy)
				{
					CSEnemy = (BaseEnemyPlane_Touhou)value;
				}
				else if (EmitterMode == EmitterMode.Effect)
				{
					CSEffect = (BaseEffect_CS)value;
				}
				else if (EmitterMode == EmitterMode.StraightLaser)
				{
					CSLaserS = (BaseStraightLaser_CS)value;
				}
				else if (EmitterMode == EmitterMode.RadialLaser)
				{
					CSLaserR = (BaseRadialLaser_CS)value;
				}
				else if (EmitterMode == EmitterMode.BendLaser)
				{
					CSLaserB = (BaseBendLaser_CS)value;
				}
				else
				{
					CSBullet = (BaseBullet_CS)value;
				}
			}
		}

		public BaseEmitter_CS()
		{
		}

		public BaseEmitter_CS(StageDataPackage StageData, CS_Data CSData, EmitterMode EmitterMode)
		{
			this.StageData = StageData;
			this.CSData = CSData;
			EventGroupList = new List<EventGroup>();
			EventsExecutionList = new List<Execution>();
			HealthPoint = 1000000f;
			HitEnabled = false;
			Region = 0;
			Count = 1;
			this.EmitterMode = EmitterMode;
			if (EmitterMode == EmitterMode.Bullet)
			{
				CSBullet = new BaseBullet_CS(StageData);
			}
			else if (EmitterMode == EmitterMode.StraightLaser)
			{
				CSLaserS = new BaseStraightLaser_CS(StageData);
			}
			else if (EmitterMode == EmitterMode.RadialLaser)
			{
				CSLaserR = new BaseRadialLaser_CS(StageData);
			}
			else if (EmitterMode == EmitterMode.BendLaser)
			{
				CSLaserB = new BaseBendLaser_CS(StageData);
			}
			else if (EmitterMode == EmitterMode.Effect)
			{
				CSEffect = new BaseEffect_CS(StageData);
			}
		}

		public override void Ctrl()
		{
			if (Time == StartTime)
			{
				Velocity += (float)((double)RanVelocity * Ran.NextPMDouble());
				DirectionDegree += RanDirection * Ran.NextPMDouble();
				AccelerateCS += (float)((double)RanAccelerate * Ran.NextPMDouble());
				AccDirection += (double)((float)(RanAccDirection * Ran.NextPMDouble()));
			}
			if (LifeTime > StartTime + Duration)
			{
				LifeTime = StartTime + Duration;
			}
			if (Range != 360.0)
			{
				RangeShoot = true;
			}
			if (BindingObj != null)
			{
				if (BindingObj is BaseEnemyPlane_Touhou)
				{
					if (BindingObj.HealthPoint <= 0f)
					{
						EnemyPlaneList.Remove(this);
					}
				}
			}
			base.Ctrl();
		}

		public override void EventCtrl()
		{
			EventGroupList.ForEach(delegate(EventGroup a)
			{
				a.Update(this);
			});
			EventsExecutionList.ForEach(delegate(Execution a)
			{
				a.Update(this);
			});
		}

		public override void Shoot()
		{
			if (Time >= StartTime && Time <= StartTime + Duration)
			{
				int num = Circle + (int)((double)RanCircle * Ran.NextPMDouble());
				num = ((num < 1) ? 1 : num);
				if (!DeepBinding && (Time - StartTime + 1) % num == 0)
				{
					ShootBullet();
				}
				else if (DeepBinding && Time % num == 0)
				{
					ShootBullet();
				}
			}
		}

		public void ShootBullet()
		{
			float num = EmitPoint.X;
			float num2 = EmitPoint.Y;
			if (num == -99999f)
			{
				num = MyPlane.OriginalPosition.X;
			}
			else if (num == -99998f)
			{
				num = OriginalPosition.X;
			}
			else
			{
				num = num - 320f + 192f;
			}
			if (num2 == -99999f)
			{
				num2 = MyPlane.OriginalPosition.Y;
			}
			else if (num2 == -99998f)
			{
				num2 = OriginalPosition.Y;
			}
			else
			{
				num2 = num2 - 240f + 224f;
			}
			if (EmitterMode == EmitterMode.StraightLaser || EmitterMode == EmitterMode.RadialLaser || EmitterMode == EmitterMode.BendLaser)
			{
				num = OriginalPosition.X;
				num2 = OriginalPosition.Y;
			}
			PointF pointF = new PointF(num + (float)((double)RanX * Ran.NextPMDouble()), num2 + (float)((double)RanY * Ran.NextPMDouble()));
			double edi;
			if (EmitDirection == -99999.0)
			{
				edi = GetDirection(MyPlane) + (RanEmitDirection * Ran.NextPMDouble() + SubBullet.RanDirection * Ran.NextPMDouble()) * 3.141592653589793 / 180.0;
			}
			else
			{
				edi = (EmitDirection + RanEmitDirection * Ran.NextPMDouble() + SubBullet.RanDirection * Ran.NextPMDouble()) * 3.141592653589793 / 180.0;
			}
			double num3;
			if (RadiusDirection == -99999.0)
			{
				num3 = GetDirection(MyPlane) + RanRadiusDirection * Ran.NextPMDouble() * 3.141592653589793 / 180.0;
			}
			else
			{
				num3 = (RadiusDirection + RanRadiusDirection * Ran.NextPMDouble()) * 3.141592653589793 / 180.0;
			}
			float num4 = EmitRadius + (float)((double)RanRadius * Ran.NextPMDouble());
			int num5 = Way + (int)((double)RanWay * Ran.NextPMDouble());
			float num6 = SubBullet.Velocity + (float)((double)SubBullet.RanVelocity * Ran.NextPMDouble());
			float num7 = SubBullet.AccelerateCS + (float)((double)SubBullet.RanAccelerate * Ran.NextPMDouble());
			double num8 = SubBullet.AccDirection + SubBullet.RanAccDirection;
			double num9 = Range + (double)((float)(RanRange * Ran.NextPMDouble()));
			if (RDirectionWithDirection)
			{
				num3 += edi;
			}
			double num10 = num9 * 3.141592653589793 / 180.0 / (double)num5;
			edi -= (double)(num5 - 1) * num10 / 2.0;
			num3 -= (double)(num5 - 1) * num10 / 2.0;
			if (EffectType == 2)
			{
				new EmitterSaveEnegy3D(StageData, pointF, CSEffect.ColorValue);
				StageData.SoundPlay("se_ch02.wav");
			}
			else if (EffectType == 3)
			{
				new EmitterGiveOutEnegy3D(StageData, pointF, CSEffect.ColorValue);
				StageData.SoundPlay("se_cat00.wav");
				StageData.SoundPlay("se_enep02.wav");
			}
			else
			{
				for (int i = 0; i < num5; i++)
				{
					PointF p = new PointF((float)((double)pointF.X + (double)num4 * Math.Cos(num3)), (float)((double)pointF.Y + (double)num4 * Math.Sin(num3)));
					for (int j = 0; j < Count; j++)
					{
						if (EmitterMode == EmitterMode.Bullet || EmitterMode == EmitterMode.StraightLaser || EmitterMode == EmitterMode.RadialLaser || EmitterMode == EmitterMode.BendLaser)
						{
							BaseBullet_Touhou b = new BaseBullet_Touhou(StageData);
							if (CSBullet != null)
							{
								b = (BaseBullet_CS)CSBullet.Clone();
								b.OriginalPosition = p;
								b.GhostingCount = b.GhostingCount;
								b.AngleDegree += CSBullet.RanAngle * Ran.NextPMDouble();
							}
							else if (CSLaserS != null)
							{
								b = (BaseStraightLaser_CS)CSLaserS.Clone();
								b.OriginalPosition = p;
								b.Angle = -1.5707963267948966;
								b.Active = true;
							}
							else if (CSLaserR != null)
							{
								b = (BaseRadialLaser_CS)CSLaserR.Clone();
								b.OriginalPosition = p;
								b.Angle = 1.5707963267948966;
								b.UnRemoveable = true;
								b.Active = true;
							}
							else if (CSLaserB != null)
							{
								b = (BaseBendLaser_CS)CSLaserB.Clone();
								b.OriginalPosition = p;
								b.UnRemoveable = true;
								b.Active = true;
							}
							b.GhostingCount = b.GhostingCount;
							b.Velocity = num6 - (float)j * DeltaV;
							b.Direction = edi;
							b.AccelerateCS = num7;
							b.AccDirection = num8;
							b.ID = ID;
							b.LayerID = LayerID;
							if (MotionBinding)
							{
								b.SetBinding(this);
							}
							BulletList.Add(b);
							if (EmitterMode == EmitterMode.Bullet)
							{
								CSData.EmitterList.ForEach(delegate(BaseEmitter_CS em)
								{
									if (em.BindingID == ID)
									{
										b.UnRemoveable = true;
										BaseEmitter_CS baseEmitter_CS = (BaseEmitter_CS)em.Clone();
										StageData.EnemyPlaneList.Add(baseEmitter_CS);
										baseEmitter_CS.OriginalPosition = p;
										baseEmitter_CS.LifeTime = SubBullet.LifeTime;
										baseEmitter_CS.ColorValue = SubBullet.ColorValue;
										baseEmitter_CS.TransparentValueF = SubBullet.TransparentValueF;
										baseEmitter_CS.Direction = edi;
										baseEmitter_CS.DestPoint = SubBullet.DestPoint;
										baseEmitter_CS.Active = SubBullet.Active;
										baseEmitter_CS.OutBound = SubBullet.OutBound;
										if (baseEmitter_CS.BindWithDirection)
										{
											baseEmitter_CS.EmitDirection += edi * 180.0 / 3.141592653589793;
										}
										baseEmitter_CS.SetBinding(b);
										if (!baseEmitter_CS.DeepBinding)
										{
											baseEmitter_CS.Time = Time;
											baseEmitter_CS.LifeTime = Math.Min(SubBullet.LifeTime + Time, em.StartTime + em.Duration);
										}
									}
								});
							}
						}
						else if (EmitterMode == EmitterMode.Enemy)
						{
							BaseEnemyPlane_Touhou enemy = (BaseEnemyPlane_Touhou)SubBullet.Clone();
							enemy.LifeTime = 0;
							enemy.OriginalPosition = p;
							enemy.GhostingCount = enemy.GhostingCount;
							enemy.Velocity = num6 - (float)j * DeltaV;
							enemy.Direction = edi;
							enemy.AccelerateCS = num7;
							enemy.AccDirection = num8;
							enemy.ID = ID;
							enemy.LayerID = LayerID;
							if (MotionBinding)
							{
								enemy.SetBinding(this);
							}
							EnemyPlaneList.Add(enemy);
							CSData.EmitterList.ForEach(delegate(BaseEmitter_CS em)
							{
								if (em.BindingID == ID)
								{
									BaseEmitter_CS baseEmitter_CS2 = (BaseEmitter_CS)em.Clone();
									StageData.EnemyPlaneList.Add(baseEmitter_CS2);
									baseEmitter_CS2.OriginalPosition = p;
									baseEmitter_CS2.LifeTime = SubBullet.LifeTime;
									baseEmitter_CS2.ColorValue = SubBullet.ColorValue;
									baseEmitter_CS2.TransparentValueF = SubBullet.TransparentValueF;
									baseEmitter_CS2.Direction = edi;
									baseEmitter_CS2.DestPoint = SubBullet.DestPoint;
									baseEmitter_CS2.Active = SubBullet.Active;
									baseEmitter_CS2.OutBound = SubBullet.OutBound;
									baseEmitter_CS2.SetBinding(enemy);
									if (baseEmitter_CS2.BindWithDirection)
									{
										baseEmitter_CS2.EmitDirection += edi * 180.0 / 3.141592653589793;
									}
									if (!baseEmitter_CS2.DeepBinding)
									{
										baseEmitter_CS2.Time = Time;
										baseEmitter_CS2.LifeTime = Math.Min(SubBullet.LifeTime + Time, em.StartTime + em.Duration);
									}
								}
							});
						}
						else if (EmitterMode == EmitterMode.Effect)
						{
							BaseEffect_CS baseEffect_CS = (BaseEffect_CS)CSEffect.Clone();
							baseEffect_CS.OriginalPosition = p;
							baseEffect_CS.GhostingCount = baseEffect_CS.GhostingCount;
							baseEffect_CS.AngleDegree += CSEffect.RanAngle * Ran.NextPMDouble();
							baseEffect_CS.Velocity = num6 - (float)j * DeltaV;
							baseEffect_CS.Direction = edi;
							baseEffect_CS.AccelerateCS = num7;
							baseEffect_CS.AccDirection = num8;
							baseEffect_CS.ID = ID;
							baseEffect_CS.LayerID = LayerID;
							if (MotionBinding)
							{
								baseEffect_CS.SetBinding(this);
							}
							EffectList.Add(baseEffect_CS);
						}
					}
					num3 += num10;
					edi += num10;
					if (SpecifySE)
					{
						StageData.SoundPlay(SoundName);
					}
					else if (EmitterMode == EmitterMode.Bullet)
					{
						StageData.SoundPlay("se_tan00a.wav", pointF.X / (float)BoundRect.Width);
					}
					else if (EmitterMode == EmitterMode.StraightLaser || EmitterMode == EmitterMode.RadialLaser)
					{
						StageData.SoundPlay("se_lazer00.wav", pointF.X / (float)BoundRect.Width);
					}
				}
			}
		}

		public override object Clone()
		{
			BaseEmitter_CS baseEmitter_CS = (BaseEmitter_CS)base.Clone();
			if (CSBullet != null)
			{
				baseEmitter_CS.CSBullet = (BaseBullet_CS)CSBullet.Clone();
			}
			if (CSLaserS != null)
			{
				baseEmitter_CS.CSLaserS = (BaseStraightLaser_CS)CSLaserS.Clone();
			}
			if (CSLaserR != null)
			{
				baseEmitter_CS.CSLaserR = (BaseRadialLaser_CS)CSLaserR.Clone();
			}
			if (CSLaserB != null)
			{
				baseEmitter_CS.CSLaserB = (BaseBendLaser_CS)CSLaserB.Clone();
			}
			if (CSEnemy != null)
			{
				baseEmitter_CS.CSEnemy = (BaseEnemyPlane_Touhou)CSEnemy.Clone();
			}
			if (CSEffect != null)
			{
				baseEmitter_CS.CSEffect = (BaseEffect_CS)CSEffect.Clone();
			}
			return baseEmitter_CS;
		}

		public override bool HitCheck(BaseObject MyPlane)
		{
			return false;
		}
	}
}
