namespace Shooting
{
	// Token: 0x02000011 RID: 17
	public partial class RenderForm_Main : global::SlimDX.Windows.RenderForm
	{
		// Token: 0x06000111 RID: 273 RVA: 0x0000630C File Offset: 0x0000450C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006344 File Offset: 0x00004544
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Shooting.RenderForm_Main));
			base.SuspendLayout();
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.ClientSize = new global::System.Drawing.Size(640, 480);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "RenderForm_Main";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "东方夏夜祭 ～ Shining Shooting Star. ver 1.00";
			base.ResumeLayout(false);
		}

		// Token: 0x04000050 RID: 80
		private global::System.ComponentModel.IContainer components = null;
	}
}
