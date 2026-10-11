using System;

namespace Shooting
{
	// Token: 0x020000A0 RID: 160
	public class KeyClass
	{
		// Token: 0x060004C4 RID: 1220 RVA: 0x00025014 File Offset: 0x00023214
		public int Key2Hex()
		{
			int num = 0;
			if (this.ArrowUp)
			{
				num++;
			}
			num <<= 1;
			if (this.ArrowDown)
			{
				num++;
			}
			num <<= 1;
			if (this.ArrowLeft)
			{
				num++;
			}
			num <<= 1;
			if (this.ArrowRight)
			{
				num++;
			}
			num <<= 1;
			if (this.Key_Shift)
			{
				num++;
			}
			num <<= 1;
			if (this.Key_Z)
			{
				num++;
			}
			num <<= 1;
			if (this.Key_X)
			{
				num++;
			}
			num <<= 1;
			if (this.Key_C)
			{
				num++;
			}
			num <<= 1;
			if (this.Key_Ctrl)
			{
				num++;
			}
			num <<= 1;
			return num << 6;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000250EC File Offset: 0x000232EC
		public void Hex2Key(int keyValue)
		{
			keyValue >>= 7;
			this.Key_Ctrl = keyValue % 2 == 1;
			keyValue >>= 1;
			this.Key_C = keyValue % 2 == 1;
			keyValue >>= 1;
			this.Key_X = keyValue % 2 == 1;
			keyValue >>= 1;
			this.Key_Z = keyValue % 2 == 1;
			keyValue >>= 1;
			this.Key_Shift = keyValue % 2 == 1;
			keyValue >>= 1;
			this.ArrowRight = keyValue % 2 == 1;
			keyValue >>= 1;
			this.ArrowLeft = keyValue % 2 == 1;
			keyValue >>= 1;
			this.ArrowDown = keyValue % 2 == 1;
			keyValue >>= 1;
			this.ArrowUp = keyValue % 2 == 1;
			keyValue >>= 1;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000251BC File Offset: 0x000233BC
		public static void KeyMix(ref KeyClass KClass, KeyClass KClass1, KeyClass KClass2)
		{
			KClass.Key_Z = KClass1.Key_Z | KClass2.Key_Z;
			KClass.Key_X = KClass1.Key_X | KClass2.Key_X;
			KClass.Key_C = KClass1.Key_C | KClass2.Key_C;
			KClass.Key_Shift = KClass1.Key_Shift | KClass2.Key_Shift;
			KClass.Key_Ctrl = KClass1.Key_Ctrl | KClass2.Key_Ctrl;
			KClass.ArrowLeft = KClass1.ArrowLeft | KClass2.ArrowLeft;
			KClass.ArrowRight = KClass1.ArrowRight | KClass2.ArrowRight;
			KClass.ArrowDown = KClass1.ArrowDown | KClass2.ArrowDown;
			KClass.ArrowUp = KClass1.ArrowUp | KClass2.ArrowUp;
			KClass.Key_ESC = KClass1.Key_ESC | KClass2.Key_ESC;
			KClass.Key_plus = KClass1.Key_plus | KClass2.Key_plus;
			KClass.Key_minus = KClass1.Key_minus | KClass2.Key_minus;
		}

		// Token: 0x0400017E RID: 382
		public bool ArrowUp = false;

		// Token: 0x0400017F RID: 383
		public bool ArrowDown = false;

		// Token: 0x04000180 RID: 384
		public bool ArrowLeft = false;

		// Token: 0x04000181 RID: 385
		public bool ArrowRight = false;

		// Token: 0x04000182 RID: 386
		public bool Key_Shift = false;

		// Token: 0x04000183 RID: 387
		public bool Key_Z = false;

		// Token: 0x04000184 RID: 388
		public bool Key_X = false;

		// Token: 0x04000185 RID: 389
		public bool Key_C = false;

		// Token: 0x04000186 RID: 390
		public bool Key_Ctrl = false;

		// Token: 0x04000187 RID: 391
		public bool Key_ESC = false;

		// Token: 0x04000188 RID: 392
		public bool Key_plus = false;

		// Token: 0x04000189 RID: 393
		public bool Key_minus = false;
	}
}
