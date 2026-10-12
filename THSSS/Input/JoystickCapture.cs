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
					joystick = new Joystick(directInput, deviceInstance.InstanceGuid);
					joystick.SetCooperativeLevel(handle, CooperativeLevel.Foreground | CooperativeLevel.Exclusive);
					break;
				}
				catch (DirectInputException)
				{
				}
			}
			if (joystick != null)
			{
				foreach (DeviceObjectInstance deviceObjectInstance in joystick.GetObjects())
				{
					if ((deviceObjectInstance.ObjectType & ObjectDeviceType.Axis) != ObjectDeviceType.All)
					{
						joystick.GetObjectPropertiesById((int)deviceObjectInstance.ObjectType).SetRange(-1000, 1000);
					}
				}
				joystick.Acquire();
			}
		}

		public void UpdateInput(ref KeyClass KClass)
		{
			if (joystick != null)
			{
				if (!joystick.Acquire().IsFailure)
				{
					if (!joystick.Poll().IsFailure)
					{
						lastJoystickState = currentJoystickState;
						currentJoystickState = joystick.GetCurrentState();
						if (!Result.Last.IsFailure)
						{
							KClass.ArrowLeft = currentJoystickState.X < -600;
							KClass.ArrowRight = currentJoystickState.X > 600;
							KClass.ArrowUp = currentJoystickState.Y < -600;
							KClass.ArrowDown = currentJoystickState.Y > 600;
							KClass.Key_Shift = currentJoystickState.IsPressed(MapJoyStick.Slow);
							KClass.Key_Z = currentJoystickState.IsPressed(MapJoyStick.Shoot);
							KClass.Key_X = currentJoystickState.IsPressed(MapJoyStick.Bomb);
							KClass.Key_Ctrl = currentJoystickState.IsPressed(MapJoyStick.Skip);
							KClass.Key_ESC = currentJoystickState.IsPressed(MapJoyStick.Pause);
							KClass.Key_plus |= KClass.Key_Ctrl;
							KClass.Key_minus |= KClass.Key_Shift;
						}
					}
				}
			}
		}

		public void Dispose()
		{
			if (joystick != null)
			{
				joystick.Unacquire();
				joystick.Dispose();
			}
			joystick = null;
		}

		private Joystick joystick;

		public JoystickState currentJoystickState = new JoystickState();

		public JoystickState lastJoystickState = new JoystickState();
	}
}
