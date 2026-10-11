using System;
using System.Drawing;

namespace System.Windows.Forms
{
	public class Form : Control
	{
		public FormBorderStyle FormBorderStyle { get; set; }

		public bool MaximizeBox { get; set; }

		public FormStartPosition StartPosition { get; set; }

		public Icon Icon { get; set; }

		public bool KeyPreview { get; set; }

		public SizeF AutoScaleDimensions { get; set; }

		public event EventHandler Load;

		public event EventHandler Activated;

		public event EventHandler Deactivate;

		public event FormClosingEventHandler FormClosing;

		public void Close()
		{
			FormClosingEventArgs formClosingEventArgs = new FormClosingEventArgs();
			FormClosingEventHandler formClosingEventHandler = this.FormClosing;
			if (formClosingEventHandler != null)
			{
				formClosingEventHandler(this, formClosingEventArgs);
			}
		}

		public void RaiseActivated()
		{
			EventHandler eventHandler = this.Activated;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		public void RaiseDeactivate()
		{
			EventHandler eventHandler = this.Deactivate;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}
	}

	public class UserControl : Control
	{
	}

	public class FormClosingEventArgs : EventArgs
	{
		public bool Cancel { get; set; }
	}

	public class KeyEventArgs : EventArgs
	{
		public Keys KeyCode { get; private set; }

		public KeyEventArgs(Keys keyCode)
		{
			this.KeyCode = keyCode;
		}
	}

	public delegate void KeyEventHandler(object sender, KeyEventArgs e);

	public delegate void FormClosingEventHandler(object sender, FormClosingEventArgs e);
}
