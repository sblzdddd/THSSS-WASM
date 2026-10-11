using System;
using SlimDX;
using SlimDX.DirectInput;

namespace Shooting
{
	// Token: 0x020001BD RID: 445
	public class JoystickCapture
	{
		// Token: 0x06000A5E RID: 2654 RVA: 0x000637BC File Offset: 0x000619BC
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

		// Token: 0x06000A5F RID: 2655 RVA: 0x00063918 File Offset: 0x00061B18
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

		// Token: 0x06000A60 RID: 2656 RVA: 0x00063AB0 File Offset: 0x00061CB0
		public void Dispose()
		{
			if (this.joystick != null)
			{
				this.joystick.Unacquire();
				this.joystick.Dispose();
			}
			this.joystick = null;
		}

		// Token: 0x040003DF RID: 991
		private Joystick joystick;

		// Token: 0x040003E0 RID: 992
		public JoystickState currentJoystickState = new JoystickState();

		// Token: 0x040003E1 RID: 993
		public JoystickState lastJoystickState = new JoystickState();
	}
}
