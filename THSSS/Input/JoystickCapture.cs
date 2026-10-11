using System;
using SlimDX;
using SlimDX.DirectInput;

namespace Shooting
{
	public class JoystickCapture
	{
		public JoystickCapture(IntPtr handle)
		{
			DirectInput directInput = new DirectInput();
			foreach (DeviceInstance deviceInstance in directInput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
			{
				try
				{
					this.joystick = new Joystick(directInput, deviceInstance.InstanceGuid);
					this.joystick.SetCooperativeLevel(handle, CooperativeLevel.Foreground | CooperativeLevel.Exclusive);
					break;
				}
				catch (DirectInputException)
				{
				}
			}
			if (this.joystick != null)
			{
				foreach (DeviceObjectInstance deviceObjectInstance in this.joystick.GetObjects())
				{
					if ((deviceObjectInstance.ObjectType & ObjectDeviceType.Axis) != ObjectDeviceType.All)
					{
						this.joystick.GetObjectPropertiesById((int)deviceObjectInstance.ObjectType).SetRange(-1000, 1000);
					}
				}
				this.joystick.Acquire();
			}
		}

		public void UpdateInput(ref KeyClass KClass)
		{
			if (this.joystick != null)
			{
				if (!this.joystick.Acquire().IsFailure)
				{
					if (!this.joystick.Poll().IsFailure)
					{
						this.lastJoystickState = this.currentJoystickState;
						this.currentJoystickState = this.joystick.GetCurrentState();
						if (!Result.Last.IsFailure)
						{
							KClass.ArrowLeft = this.currentJoystickState.X < -600;
							KClass.ArrowRight = this.currentJoystickState.X > 600;
							KClass.ArrowUp = this.currentJoystickState.Y < -600;
							KClass.ArrowDown = this.currentJoystickState.Y > 600;
							KClass.Key_Shift = this.currentJoystickState.IsPressed(MapJoyStick.Slow);
							KClass.Key_Z = this.currentJoystickState.IsPressed(MapJoyStick.Shoot);
							KClass.Key_X = this.currentJoystickState.IsPressed(MapJoyStick.Bomb);
							KClass.Key_Ctrl = this.currentJoystickState.IsPressed(MapJoyStick.Skip);
							KClass.Key_ESC = this.currentJoystickState.IsPressed(MapJoyStick.Pause);
							KClass.Key_plus |= KClass.Key_Ctrl;
							KClass.Key_minus |= KClass.Key_Shift;
						}
					}
				}
			}
		}

		public void Dispose()
		{
			if (this.joystick != null)
			{
				this.joystick.Unacquire();
				this.joystick.Dispose();
			}
			this.joystick = null;
		}

		private Joystick joystick;

		public JoystickState currentJoystickState = new JoystickState();

		public JoystickState lastJoystickState = new JoystickState();
	}
}
