namespace Shooting
{
	internal partial class GameState_MainMenu : global::Shooting.BaseMenu, global::Shooting.IGameState
	{
		private void InitializeComponent()
		{
			this.GameRegion_Menu = new global::Shooting.GameRegion();
			base.SuspendLayout();
			this.GameRegion_Menu.AutoSize = true;
			this.GameRegion_Menu.BackColor = global::System.Drawing.Color.Transparent;
			this.GameRegion_Menu.BackgroundImage = global::Shooting.Properties.Resources.Menu_Start;
			this.GameRegion_Menu.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Center;
			this.GameRegion_Menu.Enabled = false;
			this.GameRegion_Menu.Location = new global::System.Drawing.Point(59, 138);
			this.GameRegion_Menu.Name = "GameRegion_Menu";
			this.GameRegion_Menu.Size = new global::System.Drawing.Size(103, 28);
			this.GameRegion_Menu.TabIndex = 0;
			this.GameRegion_Menu.Visible = false;
			this.BackgroundImage = global::Shooting.Properties.Resources.菜单背景;
			base.Controls.Add(this.GameRegion_Menu);
			base.Enabled = false;
			base.Name = "GameState_MainMenu";
			base.Size = new global::System.Drawing.Size(640, 480);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private global::Shooting.GameRegion GameRegion_Menu;
	}
}
