using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000026 RID: 38
	public class BaseBullet_Touhou : BaseObject_CS
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000A21C File Offset: 0x0000841C
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x0000A233 File Offset: 0x00008433
		public int Layer { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000A23C File Offset: 0x0000843C
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x0000A253 File Offset: 0x00008453
		public byte ColorType { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000A25C File Offset: 0x0000845C
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x0000A273 File Offset: 0x00008473
		public int SizeValue { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000A27C File Offset: 0x0000847C
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x0000A293 File Offset: 0x00008493
		public byte Reflect { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000A29C File Offset: 0x0000849C
		// (set) Token: 0x060001BA RID: 442 RVA: 0x0000A2B3 File Offset: 0x000084B3
		public bool Grazed { get; set; }

		// Token: 0x060001BB RID: 443 RVA: 0x0000A2BC File Offset: 0x000084BC
		public BaseBullet_Touhou(StageDataPackage StageData)
			: base(StageData)
		{
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000A2C8 File Offset: 0x000084C8
		public BaseBullet_Touhou(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: this(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			this.ColorType = ColorType;
			base.BeginningEffect = true;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000A2EC File Offset: 0x000084EC
		public BaseBullet_Touhou(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			base.BulletList.Add(this);
			base.OriginalPosition = OriginalPosition;
			base.AngleDegree = 180.0;
			this.SizeValue = 32;
			this.SetRegion(textureName);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000A340 File Offset: 0x00008540
		public void SetRegion(string textureName)
		{
			if (textureName != null)
			{
				char[] array = new char[] { '_' };
				string[] array2 = textureName.Split(array);
				string text = array2[0];
				switch (text)
				{
				case "bullet0":
					base.Region = 1;
					break;
				case "bullet1":
					base.Region = 1;
					break;
				case "bullet2":
					base.Region = 2;
					break;
				case "bullet3":
					base.Region = 2;
					break;
				case "bullet4":
					base.Region = 1;
					break;
				case "bullet5":
					base.Region = 1;
					break;
				case "bullet6":
					base.Region = 1;
					break;
				case "bullet7":
					base.Region = 1;
					break;
				case "bullet8":
					base.Region = 1;
					break;
				case "bullet9":
					base.Region = 1;
					break;
				case "bullet10":
					base.Region = 2;
					break;
				case "bullet11":
					base.Region = 2;
					break;
				case "bullet12":
					base.Region = 1;
					break;
				case "bullet13":
					base.Region = 3;
					break;
				case "bullet15":
					base.Region = 1;
					break;
				case "bullet20":
					base.Region = 6;
					break;
				case "bullet21":
					base.Region = 6;
					break;
				case "bullet22":
					base.Region = 3;
					break;
				case "bullet23":
					base.Region = 3;
					break;
				case "bullet24":
					base.Region = 3;
					break;
				case "bullet26":
					base.Region = 16;
					base.AngularVelocityDegree = 10f;
					base.Active = true;
					this.SizeValue = 80;
					base.OutsideRegion = 32;
					break;
				case "bullet50":
					base.Region = 12;
					base.Active = true;
					this.SizeValue = 64;
					base.OutsideRegion = 64;
					break;
				case "bullet40":
					base.Region = 6;
					this.SizeValue = 32;
					base.OutsideRegion = 32;
					break;
				case "bullet46":
					base.Region = 1;
					break;
				case "bullet61":
					base.Region = 2;
					break;
				case "bullet62":
					base.Region = 3;
					break;
				case "bullet64":
					base.Region = 3;
					break;
				case "bullet65":
					base.Region = 3;
					break;
				case "bullet66":
					base.Region = 3;
					break;
				case "bullet67":
					base.Region = 3;
					break;
				}
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000A774 File Offset: 0x00008974
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 1 && base.BeginningEffect)
			{
				ParticleBeginBullet particleBeginBullet = new ParticleBeginBullet(base.StageData, base.Position, this.ColorType);
				particleBeginBullet.Active = base.Active;
			}
			if (base.Time == base.LifeTime && base.EndingEffect)
			{
				this.GiveEndEffect();
			}
			if (this.OutBoundary())
			{
				base.BulletList.Remove(this);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000A810 File Offset: 0x00008A10
		public override bool OutBoundary()
		{
			bool flag;
			if (base.LifeTime != 0 && base.Time > base.LifeTime)
			{
				flag = true;
			}
			else if (this.Reflect <= 0)
			{
				flag = base.OutBoundary();
			}
			else
			{
				if (base.OriginalPosition.X < 0f)
				{
					this.Reflect -= 1;
					base.Direction -= 2.0 * (base.Direction - 1.5707963267948966);
					base.OriginalPosition = new PointF(-base.OriginalPosition.X, base.OriginalPosition.Y);
				}
				else if (base.OriginalPosition.X >= (float)base.BoundRect.Width)
				{
					this.Reflect -= 1;
					base.Direction -= 2.0 * (base.Direction - 1.5707963267948966);
					base.OriginalPosition = new PointF((float)(2 * base.BoundRect.Width) - base.OriginalPosition.X, base.OriginalPosition.Y);
				}
				if (base.OriginalPosition.Y < 20f)
				{
					if (this.Reflect > 0)
					{
						this.Reflect -= 1;
						base.Direction = -base.Direction;
						base.OriginalPosition = new PointF(base.OriginalPosition.X, 20f - base.OriginalPosition.Y);
					}
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000AA00 File Offset: 0x00008C00
		public override void HitCheckAll()
		{
			if (base.MyPlane.HitEnabled)
			{
				if (this.HitCheck(base.MyPlane))
				{
					base.MyPlane.PreMiss();
					if (!base.UnRemoveable)
					{
						this.GiveEndEffect();
						base.BulletList.Remove(this);
					}
				}
				else if (!this.Grazed && this.HitCheck(base.MyPlane, 24f / base.ScaleLength + (float)base.Region))
				{
					this.Grazed = true;
					base.MyPlane.Graze++;
					if (base.MyPlane.Graze % 10 == 0)
					{
						base.MyPlane.HighItemScore += 10;
					}
					base.StageData.SoundPlay("se_graze.wav");
					BaseEffect baseEffect = new BaseEffect(base.StageData, "GrazeG", base.MyPlane.Position, 5f, base.Ran.NextDouble() * 3.141592653589793 * 2.0);
					baseEffect.Scale = 1f;
					baseEffect.TransparentVelocity = -20f;
					baseEffect.ScaleVelocity = -0.05f;
					baseEffect.AngleWithDirection = false;
				}
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000AB6A File Offset: 0x00008D6A
		public override void GiveEndEffect()
		{
			new ParticleEndBullet(base.StageData, base.OriginalPosition, this.ColorType, this.SizeValue);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000AB8B File Offset: 0x00008D8B
		public override void GiveItems()
		{
			new StarItem_Touhou(base.StageData, base.OriginalPosition);
		}
	}
}
