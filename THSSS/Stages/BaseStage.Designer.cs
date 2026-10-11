namespace Shooting
{
	internal partial class BaseStage : global::Shooting.BaseGameState, global::Shooting.IGameState
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
			this.gameRegion_ScoreValue = new global::Shooting.GameRegion();
			this.gameRegion_SpellLine = new global::Shooting.GameRegion();
			this.gameRegion_Power = new global::Shooting.GameRegion();
			this.gameRegion_Spell = new global::Shooting.GameRegion();
			this.gameRegion_Player = new global::Shooting.GameRegion();
			this.gameRegion_Score = new global::Shooting.GameRegion();
			this.gameRegion_LifeSelf = new global::Shooting.GameRegion();
			this.GameRegion_Main = new global::Shooting.GameRegion();
			this.gameRegion_BluePoint = new global::Shooting.GameRegion();
			this.gameRegion_GreenPoint = new global::Shooting.GameRegion();
			this.gameRegion_Graze = new global::Shooting.GameRegion();
			this.gameRegion_HiScoreValue = new global::Shooting.GameRegion();
			this.gameRegion1 = new global::Shooting.GameRegion();
			this.gameRegion_HiScore = new global::Shooting.GameRegion();
			base.SuspendLayout();
			this.gameRegion_ScoreValue.BackColor = global::System.Drawing.Color.Transparent;
			this.gameRegion_ScoreValue.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Zoom;
			this.gameRegion_ScoreValue.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.gameRegion_ScoreValue.Location = new global::System.Drawing.Point(488, 70);
			this.gameRegion_ScoreValue.Margin = new global::System.Windows.Forms.Padding(10, 1, 3, 1);
			this.gameRegion_ScoreValue.Name = "gameRegion_ScoreValue";
			this.gameRegion_ScoreValue.Size = new global::System.Drawing.Size(110, 16);
			this.gameRegion_ScoreValue.TabIndex = 7;
			this.gameRegion_SpellLine.BackColor = global::System.Drawing.Color.Transparent;
			this.gameRegion_SpellLine.BackgroundImage = global::Shooting.Properties.Resources.FiveStar;
			this.gameRegion_SpellLine.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Zoom;
			this.gameRegion_SpellLine.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.gameRegion_SpellLine.Location = new global::System.Drawing.Point(504, 127);
			this.gameRegion_SpellLine.Margin = new global::System.Windows.Forms.Padding(10, 1, 3, 1);
			this.gameRegion_SpellLine.Name = "gameRegion_SpellLine";
			this.gameRegion_SpellLine.Size = new global::System.Drawing.Size(110, 16);
			this.gameRegion_SpellLine.TabIndex = 6;
			this.gameRegion_Power.BackColor = global::System.Drawing.Color.Transparent;
			this.gameRegion_Power.BackgroundImage = global::Shooting.Properties.Resources.FiveStar;
			this.gameRegion_Power.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.None;
			this.gameRegion_Power.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.gameRegion_Power.Location = new global::System.Drawing.Point(442, 188);
			this.gameRegion_Power.Name = "gameRegion_Power";
			this.gameRegion_Power.Size = new global::System.Drawing.Size(163, 25);
			this.gameRegion_Power.TabIndex = 5;
			this.gameRegion_Spell.BackColor = global::System.Drawing.Color.Transparent;
			this.gameRegion_Spell.BackgroundImage = global::Shooting.Properties.Resources.Spell;
			this.gameRegion_Spell.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.None;
			this.gameRegion_Spell.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.gameRegion_Spell.Location = new global::System.Drawing.Point(428, 125);
			this.gameRegion_Spell.Name = "gameRegion_Spell";
			this.gameRegion_Spell.Size = new global::System.Drawing.Size(70, 16);
			this.gameRegion_Spell.TabIndex = 4;
			this.gameRegion_Player.BackColor = global::System.Drawing.Color.Transparent;
			this.gameRegion_Player.BackgroundImage = global::Shooting.Properties.Resources.Player;
			this.gameRegion_Player.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.None;
			this.gameRegion_Player.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.gameRegion_Player.Location = new global::System.Drawing.Point(428, 99);
			this.gameRegion_Player.Name = "gameRegion_Player";
			this.gameRegion_Player.Size = new global::System.Drawing.Size(70, 16);
			this.gameRegion_Player.TabIndex = 3;
			this.gameRegion_Score.BackColor = global::System.Drawing.Color.Transparent;
			this.gameRegion_Score.BackgroundImage = global::Shooting.Properties.Resources.Score;
			this.gameRegion_Score.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.None;
			this.gameRegion_Score.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.gameRegion_Score.Location = new global::System.Drawing.Point(428, 67);
			this.gameRegion_Score.Name = "gameRegion_Score";
			this.gameRegion_Score.Size = new global::System.Drawing.Size(70, 16);
			this.gameRegion_Score.TabIndex = 2;
			this.gameRegion_LifeSelf.BackColor = global::System.Drawing.Color.Transparent;
			this.gameRegion_LifeSelf.BackgroundImage = global::Shooting.Properties.Resources.MyPlane;
			this.gameRegion_LifeSelf.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Zoom;
			this.gameRegion_LifeSelf.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.gameRegion_LifeSelf.Location = new global::System.Drawing.Point(504, 101);
			this.gameRegion_LifeSelf.Margin = new global::System.Windows.Forms.Padding(10, 1, 3, 1);
			this.gameRegion_LifeSelf.Name = "gameRegion_LifeSelf";
			this.gameRegion_LifeSelf.Size = new global::System.Drawing.Size(110, 16);
			this.gameRegion_LifeSelf.TabIndex = 0;
			this.GameRegion_Main.Location = new global::System.Drawing.Point(36, 16);
			this.GameRegion_Main.MaximumSize = new global::System.Drawing.Size(384, 448);
			this.GameRegion_Main.MinimumSize = new global::System.Drawing.Size(384, 448);
			this.GameRegion_Main.Name = "GameRegion_Main";
			this.GameRegion_Main.Size = new global::System.Drawing.Size(384, 448);
			this.GameRegion_Main.TabIndex = 1;
			this.gameRegion_BluePoint.BackColor = global::System.Drawing.Color.Transparent;
			this.gameRegion_BluePoint.BackgroundImage = global::Shooting.Properties.Resources.FiveStar;
			this.gameRegion_BluePoint.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.None;
			this.gameRegion_BluePoint.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.gameRegion_BluePoint.Location = new global::System.Drawing.Point(442, 214);
			this.gameRegion_BluePoint.Name = "gameRegion_BluePoint";
			this.gameRegion_BluePoint.Size = new global::System.Drawing.Size(163, 25);
			this.gameRegion_BluePoint.TabIndex = 9;
			this.gameRegion_GreenPoint.BackColor = global::System.Drawing.Color.Transparent;
			this.gameRegion_GreenPoint.BackgroundImage = global::Shooting.Properties.Resources.FiveStar;
			this.gameRegion_GreenPoint.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.None;
			this.gameRegion_GreenPoint.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.gameRegion_GreenPoint.Location = new global::System.Drawing.Point(442, 240);
			this.gameRegion_GreenPoint.Name = "gameRegion_GreenPoint";
			this.gameRegion_GreenPoint.Size = new global::System.Drawing.Size(163, 25);
			this.gameRegion_GreenPoint.TabIndex = 10;
			this.gameRegion_Graze.BackColor = global::System.Drawing.Color.Transparent;
			this.gameRegion_Graze.BackgroundImage = global::Shooting.Properties.Resources.FiveStar;
			this.gameRegion_Graze.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.None;
			this.gameRegion_Graze.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.gameRegion_Graze.Location = new global::System.Drawing.Point(442, 266);
			this.gameRegion_Graze.Name = "gameRegion_Graze";
			this.gameRegion_Graze.Size = new global::System.Drawing.Size(163, 25);
			this.gameRegion_Graze.TabIndex = 11;
			this.gameRegion_HiScoreValue.BackColor = global::System.Drawing.Color.Transparent;
			this.gameRegion_HiScoreValue.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Zoom;
			this.gameRegion_HiScoreValue.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.gameRegion_HiScoreValue.Location = new global::System.Drawing.Point(488, 47);
			this.gameRegion_HiScoreValue.Margin = new global::System.Windows.Forms.Padding(10, 1, 3, 1);
			this.gameRegion_HiScoreValue.Name = "gameRegion_HiScoreValue";
			this.gameRegion_HiScoreValue.Size = new global::System.Drawing.Size(110, 16);
			this.gameRegion_HiScoreValue.TabIndex = 12;
			this.gameRegion1.BackColor = global::System.Drawing.Color.Transparent;
			this.gameRegion1.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Zoom;
			this.gameRegion1.Location = new global::System.Drawing.Point(451, 48);
			this.gameRegion1.Margin = new global::System.Windows.Forms.Padding(10, 3, 3, 3);
			this.gameRegion1.Name = "gameRegion1";
			this.gameRegion1.Size = new global::System.Drawing.Size(165, 20);
			this.gameRegion1.TabIndex = 12;
			this.gameRegion_HiScore.BackColor = global::System.Drawing.Color.Transparent;
			this.gameRegion_HiScore.BackgroundImage = global::Shooting.Properties.Resources.Score;
			this.gameRegion_HiScore.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.None;
			this.gameRegion_HiScore.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.gameRegion_HiScore.Location = new global::System.Drawing.Point(428, 44);
			this.gameRegion_HiScore.Name = "gameRegion_HiScore";
			this.gameRegion_HiScore.Size = new global::System.Drawing.Size(70, 16);
			this.gameRegion_HiScore.TabIndex = 13;
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.BackgroundImage = global::Shooting.Properties.Resources.background;
			base.Controls.Add(this.gameRegion_Score);
			base.Controls.Add(this.gameRegion_HiScore);
			base.Controls.Add(this.gameRegion_HiScoreValue);
			base.Controls.Add(this.gameRegion_Graze);
			base.Controls.Add(this.gameRegion_GreenPoint);
			base.Controls.Add(this.gameRegion_BluePoint);
			base.Controls.Add(this.gameRegion_ScoreValue);
			base.Controls.Add(this.gameRegion_SpellLine);
			base.Controls.Add(this.gameRegion_Spell);
			base.Controls.Add(this.gameRegion_Player);
			base.Controls.Add(this.gameRegion_LifeSelf);
			base.Controls.Add(this.GameRegion_Main);
			base.Controls.Add(this.gameRegion_Power);
			base.Name = "BaseStage";
			base.Size = new global::System.Drawing.Size(640, 480);
			base.ResumeLayout(false);
		}

		private global::System.ComponentModel.IContainer components = null;

		private global::Shooting.GameRegion GameRegion_Main;

		private global::Shooting.GameRegion gameRegion_Score;

		private global::Shooting.GameRegion gameRegion_Player;

		private global::Shooting.GameRegion gameRegion_Spell;

		private global::Shooting.GameRegion gameRegion_Power;

		private global::Shooting.GameRegion gameRegion_LifeSelf;

		private global::Shooting.GameRegion gameRegion_SpellLine;

		private global::Shooting.GameRegion gameRegion_ScoreValue;

		private global::Shooting.GameRegion gameRegion_BluePoint;

		private global::Shooting.GameRegion gameRegion_GreenPoint;

		private global::Shooting.GameRegion gameRegion_Graze;

		private global::Shooting.GameRegion gameRegion_HiScoreValue;

		private global::Shooting.GameRegion gameRegion1;

		private global::Shooting.GameRegion gameRegion_HiScore;
	}
}
