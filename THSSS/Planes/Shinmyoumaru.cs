using System;
using System.Drawing;

namespace Shooting
{
	public class Shinmyoumaru : BaseSubPlane
	{
		public float IndexX { get; set; }

		public int IndexY { get; set; }

		public Shinmyoumaru(StageDataPackage StageData, PointF Position)
			: base(StageData, "ReimuSubPlane", Position)
		{
			base.AngularVelocityDegree = 5f;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			this.TextureCtrl();
		}

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
