using System;
using System.Windows.Forms;
using SlimDX;
using SlimDX.DirectInput;

namespace Shooting
{
	public class KeyboardCapture
	{
		public KeyboardCapture(IntPtr handle)
		{
			var directInput = new DirectInput();
			var cooperativeLevel = CooperativeLevel.NoWinKey | CooperativeLevel.Foreground | CooperativeLevel.Exclusive;
			try
			{
				keyboard = new Keyboard(directInput);
				keyboard.SetCooperativeLevel(handle, cooperativeLevel);
			}
			catch (DirectInputException ex)
			{
				MessageBox.Show("DirectInput Error " + ex.Message);
				return;
			}
			keyboard.Acquire();
		}

		public void UpdateInput(ref KeyClass KClass)
		{
			if (!keyboard.Acquire().IsFailure)
			{
				if (!keyboard.Poll().IsFailure)
				{
					lastKeyboardState = currentKeyboardState;
					currentKeyboardState = keyboard.GetCurrentState();
					if (!Result.Last.IsFailure)
					{
						KClass.ArrowLeft = currentKeyboardState.IsPressed(Key.LeftArrow);
						KClass.ArrowRight = currentKeyboardState.IsPressed(Key.RightArrow);
						KClass.ArrowUp = currentKeyboardState.IsPressed(Key.UpArrow);
						KClass.ArrowDown = currentKeyboardState.IsPressed(Key.DownArrow);
						KClass.Key_Shift = currentKeyboardState.IsPressed(Key.LeftShift);
						KClass.Key_Z = currentKeyboardState.IsPressed(Key.Z) | currentKeyboardState.IsPressed(Key.Return);
						KClass.Key_X = currentKeyboardState.IsPressed(Key.X);
						KClass.Key_C = currentKeyboardState.IsPressed(Key.R);
						KClass.Key_Ctrl = currentKeyboardState.IsPressed(Key.LeftControl);
						KClass.Key_ESC = currentKeyboardState.IsPressed(Key.Escape);
						KClass.Key_plus = currentKeyboardState.IsPressed(Key.Equals);
						KClass.Key_minus = currentKeyboardState.IsPressed(Key.Minus);
						KClass.Key_plus |= KClass.Key_Ctrl;
						KClass.Key_minus |= KClass.Key_Shift;
					}
				}
			}
		}

		public void Dispose()
		{
			if (keyboard != null)
			{
				keyboard.Unacquire();
				keyboard.Dispose();
			}
			keyboard = null;
		}

		private Keyboard keyboard;

		public KeyboardState currentKeyboardState;

		public KeyboardState lastKeyboardState;
	}
}
