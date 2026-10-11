using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000065 RID: 101
	public class Shinmyoumaru : BaseSubPlane
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000353 RID: 851 RVA: 0x000166C0 File Offset: 0x000148C0
		// (set) Token: 0x06000354 RID: 852 RVA: 0x000166D7 File Offset: 0x000148D7
		public float IndexX { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000355 RID: 853 RVA: 0x000166E0 File Offset: 0x000148E0
		// (set) Token: 0x06000356 RID: 854 RVA: 0x000166F7 File Offset: 0x000148F7
		public int IndexY { get; set; }

		// Token: 0x06000357 RID: 855 RVA: 0x00016700 File Offset: 0x00014900
		public Shinmyoumaru(StageDataPackage StageData, PointF Position)
			: base(StageData, "ReimuSubPlane", Position)
		{
			base.AngularVelocityDegree = 5f;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0001671E File Offset: 0x0001491E
		public override void Ctrl()
		{
			base.Ctrl();
			this.TextureCtrl();
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00016730 File Offset: 0x00014930
		public void TextureCtrl()
		{
			if (!base.KClass.Key_Shift)
			{
				base.TxtureObject = base.TextureObjectDictionary["ReimuSubPlane"];
			}
			else
			{
				base.Angle = 1.5707963267948966;
				int indexY = this.IndexY;
				if (base.Vx > 0.5f)
				{
					this.IndexY = 2;
					if (this.IndexX < 4f)
					{
						this.IndexX += 0.5f;
					}
					else
					{
						this.IndexX = (float)(4 + base.Time % 24 / 6);
					}
				}
				else if (base.Vx < -0.5f)
				{
					this.IndexY = 1;
					if (this.IndexX < 4f)
					{
						this.IndexX += 0.5f;
					}
					else
					{
						this.IndexX = (float)(4 + base.Time % 24 / 6);
					}
				}
				else
				{
					this.IndexY = 0;
					this.IndexX = (float)(base.Time % 48 / 6);
				}
				if (indexY != this.IndexY)
				{
					this.IndexX = 1f;
				}
				if (this.IndexX > 7f)
				{
					this.IndexX = 7f;
				}
				string text = "Maru" + this.IndexY.ToString() + ((int)this.IndexX).ToString();
				base.TxtureObject = base.TextureObjectDictionary[text];
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000168DC File Offset: 0x00014ADC
		public override void Shoot()
		{
			if (base.KClass.Key_Shift)
			{
				BaseMyBullet_Touhou baseMyBullet_Touhou = new BaseMyBullet_Touhou(base.StageData, "MaruBullet", base.OriginalPosition, 18f, base.ShootDirection + 0.05235987755982988);
				baseMyBullet_Touhou.Angle = 1.5707963705062866;
				baseMyBullet_Touhou.Damage = 5;
				BaseMyBullet_Touhou baseMyBullet_Touhou2 = new BaseMyBullet_Touhou(base.StageData, "MaruBullet", base.OriginalPosition, 18f, base.ShootDirection - 0.05235987755982988);
				baseMyBullet_Touhou2.Angle = 1.5707963705062866;
				baseMyBullet_Touhou2.Damage = 5;
			}
			else
			{
				BulletGuidance bulletGuidance = new BulletGuidance(base.StageData, "ReimuSubBullet", base.Position, 8f, base.ShootDirection);
				bulletGuidance.TransparentValueF = 180f;
				bulletGuidance.Angle = 1.5707963705062866;
				bulletGuidance.Damage = 9;
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000169DC File Offset: 0x00014BDC
		public override void Show()
		{
			if (base.Enabled)
			{
				if (base.TxtureObject != null)
				{
					if (!base.KClass.Key_Shift)
					{
						double num = (double)(-(double)base.Time * 5 % 360) * 3.141592653589793 / 180.0;
						if (base.MyPlane.Power >= 400)
						{
							base.SpriteMain.Draw2D(base.TxtureObject, 1.3f, 1.3f, -(float)num, base.Position, Color.FromArgb(base.TransparentValue * 6 / 10, base.ColorValue), base.Mirrored);
						}
						base.SpriteMain.Draw2D(base.TxtureObject, base.ScaleWidth, base.ScaleLength, (float)num, base.Position, Color.FromArgb(base.TransparentValue, base.ColorValue), base.Mirrored);
					}
					else
					{
						base.Show();
					}
				}
			}
		}
	}
}
