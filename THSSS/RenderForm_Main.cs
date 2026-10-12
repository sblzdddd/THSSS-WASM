using System;
using System.Drawing;
using System.Windows.Forms;
using SlimDX.Windows;

namespace Shooting
{
	public partial class RenderForm_Main : RenderForm
	{
		public RenderForm_Main()
		{
			this.InitializeComponent();
			this.BackColor = Color.Black;
			if (Game_Main.useUnrealFullScreen)
			{
				this.panel1 = new Panel();
				base.Controls.Add(this.panel1);
				this.panel1.Size = new Size(base.ClientSize.Height * 4 / 3, base.ClientSize.Height);
				this.panel1.Location = new Point(base.ClientSize.Width / 2 - this.panel1.Width / 2, 0);
				this.panel1.Name = "panel1";
				this.panel1.TabIndex = 0;
			}
		}

		private void RenderForm_Main_SizeChanged(object sender, EventArgs e)
		{
			if (this.panel1 != null)
			{
				if (!Game_Main.stretchScreen)
				{
					this.panel1.Size = new Size(base.ClientSize.Height * 4 / 3, base.ClientSize.Height);
					this.panel1.Location = new Point(base.ClientSize.Width / 2 - this.panel1.Width / 2, 0);
					return;
				}
				this.panel1.Size = new Size(base.ClientSize.Width, base.ClientSize.Height);
				this.panel1.Location = new Point(0, 0);
			}
		}

		public Panel panel1;
	}
}
