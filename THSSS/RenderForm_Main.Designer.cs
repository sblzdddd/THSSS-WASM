namespace Shooting
{
	public partial class RenderForm_Main : global::SlimDX.Windows.RenderForm
	{
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Shooting.RenderForm_Main));
			base.SuspendLayout();
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = global::System.Drawing.Color.Black;
			base.ClientSize = new global::System.Drawing.Size(640, 480);
			this.DoubleBuffered = true;
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.ImeMode = global::System.Windows.Forms.ImeMode.Off;
			base.Name = "RenderForm_Main";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "东方夏夜祭 ～ Shining Shooting Star. ver 1.01";
			base.SizeChanged += new global::System.EventHandler(this.RenderForm_Main_SizeChanged);
			base.ResumeLayout(false);
		}

		private global::System.ComponentModel.IContainer components = null;
	}
}
