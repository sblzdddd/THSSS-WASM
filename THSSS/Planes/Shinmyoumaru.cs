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
			AngularVelocityDegree = 5f;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			TextureCtrl();
		}

		public void TextureCtrl()
		{
			if (!KClass.Key_Shift)
			{
				TxtureObject = TextureObjectDictionary["ReimuSubPlane"];
			}
			else
			{
				Angle = 1.5707963267948966;
				int indexY = IndexY;
				if (Vx > 0.5f)
				{
					IndexY = 2;
					if (IndexX < 4f)
					{
						IndexX += 0.5f;
					}
					else
					{
						IndexX = (float)(4 + Time % 24 / 6);
					}
				}
				else if (Vx < -0.5f)
				{
					IndexY = 1;
					if (IndexX < 4f)
					{
						IndexX += 0.5f;
					}
					else
					{
						IndexX = (float)(4 + Time % 24 / 6);
					}
				}
				else
				{
					IndexY = 0;
					IndexX = (float)(Time % 48 / 6);
				}
				if (indexY != IndexY)
				{
					IndexX = 1f;
				}
				if (IndexX > 7f)
				{
					IndexX = 7f;
				}
				string text = "Maru" + IndexY.ToString() + ((int)IndexX).ToString();
				TxtureObject = TextureObjectDictionary[text];
			}
		}

		public override void Shoot()
		{
			if (KClass.Key_Shift)
			{
				BaseMyBullet_Touhou baseMyBullet_Touhou = new BaseMyBullet_Touhou(StageData, "MaruBullet", OriginalPosition, 18f, ShootDirection + 0.05235987755982988);
				baseMyBullet_Touhou.Angle = 1.5707963705062866;
				baseMyBullet_Touhou.Damage = 5;
				BaseMyBullet_Touhou baseMyBullet_Touhou2 = new BaseMyBullet_Touhou(StageData, "MaruBullet", OriginalPosition, 18f, ShootDirection - 0.05235987755982988);
				baseMyBullet_Touhou2.Angle = 1.5707963705062866;
				baseMyBullet_Touhou2.Damage = 5;
			}
			else
			{
				BulletGuidance bulletGuidance = new BulletGuidance(StageData, "ReimuSubBullet", Position, 8f, ShootDirection);
				bulletGuidance.TransparentValueF = 180f;
				bulletGuidance.Angle = 1.5707963705062866;
				bulletGuidance.Damage = 9;
			}
		}

		public override void Show()
		{
			if (Enabled)
			{
				if (TxtureObject != null)
				{
					if (!KClass.Key_Shift)
					{
						double num = (double)(-(double)Time * 5 % 360) * 3.141592653589793 / 180.0;
						if (MyPlane.Power >= 400)
						{
							SpriteMain.Draw2D(TxtureObject, 1.3f, 1.3f, -(float)num, Position, Color.FromArgb(TransparentValue * 6 / 10, ColorValue), Mirrored);
						}
						SpriteMain.Draw2D(TxtureObject, ScaleWidth, ScaleLength, (float)num, Position, Color.FromArgb(TransparentValue, ColorValue), Mirrored);
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
