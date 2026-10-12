namespace Shooting
{
	public class KeyClass
	{
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

		public bool ArrowUp = false;

		public bool ArrowDown = false;

		public bool ArrowLeft = false;

		public bool ArrowRight = false;

		public bool Key_Shift = false;

		public bool Key_Z = false;

		public bool Key_X = false;

		public bool Key_C = false;

		public bool Key_Ctrl = false;

		public bool Key_ESC = false;

		public bool Key_plus = false;

		public bool Key_minus = false;
	}
}
