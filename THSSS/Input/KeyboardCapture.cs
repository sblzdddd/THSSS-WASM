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
			DirectInput directInput = new DirectInput();
			CooperativeLevel cooperativeLevel = CooperativeLevel.NoWinKey | CooperativeLevel.Foreground | CooperativeLevel.Exclusive;
			try
			{
				this.keyboard = new Keyboard(directInput);
				this.keyboard.SetCooperativeLevel(handle, cooperativeLevel);
			}
			catch (DirectInputException ex)
			{
				MessageBox.Show("DirectInput Error " + ex.Message);
				return;
			}
			this.keyboard.Acquire();
		}

		public void UpdateInput(ref KeyClass KClass)
		{
			if (!this.keyboard.Acquire().IsFailure)
			{
				if (!this.keyboard.Poll().IsFailure)
				{
					this.lastKeyboardState = this.currentKeyboardState;
					this.currentKeyboardState = this.keyboard.GetCurrentState();
					if (!Result.Last.IsFailure)
					{
						KClass.ArrowLeft = this.currentKeyboardState.IsPressed(Key.LeftArrow);
						KClass.ArrowRight = this.currentKeyboardState.IsPressed(Key.RightArrow);
						KClass.ArrowUp = this.currentKeyboardState.IsPressed(Key.UpArrow);
						KClass.ArrowDown = this.currentKeyboardState.IsPressed(Key.DownArrow);
						KClass.Key_Shift = this.currentKeyboardState.IsPressed(Key.LeftShift);
						KClass.Key_Z = this.currentKeyboardState.IsPressed(Key.Z) | this.currentKeyboardState.IsPressed(Key.Return);
						KClass.Key_X = this.currentKeyboardState.IsPressed(Key.X);
						KClass.Key_C = this.currentKeyboardState.IsPressed(Key.R);
						KClass.Key_Ctrl = this.currentKeyboardState.IsPressed(Key.LeftControl);
						KClass.Key_ESC = this.currentKeyboardState.IsPressed(Key.Escape);
						KClass.Key_plus = this.currentKeyboardState.IsPressed(Key.Equals);
						KClass.Key_minus = this.currentKeyboardState.IsPressed(Key.Minus);
						KClass.Key_plus |= KClass.Key_Ctrl;
						KClass.Key_minus |= KClass.Key_Shift;
					}
				}
			}
		}

		public void Dispose()
		{
			if (this.keyboard != null)
			{
				this.keyboard.Unacquire();
				this.keyboard.Dispose();
			}
			this.keyboard = null;
		}

		private Keyboard keyboard;

		public KeyboardState currentKeyboardState;

		public KeyboardState lastKeyboardState;
	}
}
