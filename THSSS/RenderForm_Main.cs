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
			InitializeComponent();
			BackColor = Color.Black;
			if (Game_Main.useUnrealFullScreen)
			{
				panel1 = new Panel();
				Controls.Add(panel1);
				panel1.Size = new Size(ClientSize.Height * 4 / 3, ClientSize.Height);
				panel1.Location = new Point(ClientSize.Width / 2 - panel1.Width / 2, 0);
				panel1.Name = "panel1";
				panel1.TabIndex = 0;
			}
		}

		private void RenderForm_Main_SizeChanged(object sender, EventArgs e)
		{
			if (panel1 != null)
			{
				if (!Game_Main.stretchScreen)
				{
					panel1.Size = new Size(ClientSize.Height * 4 / 3, ClientSize.Height);
					panel1.Location = new Point(ClientSize.Width / 2 - panel1.Width / 2, 0);
					return;
				}
				panel1.Size = new Size(ClientSize.Width, ClientSize.Height);
				panel1.Location = new Point(0, 0);
			}
		}

		public Panel panel1;
	}
}
