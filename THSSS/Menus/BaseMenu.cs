namespace Shooting
{
	internal partial class BaseMenu : BaseGameState, IGameState
	{
		public int BackgroundLight { get; set; }

		public BaseMenu(GlobalDataPackage GlobalData)
		{
			base.Visible = false;
			base.Enabled = false;
			base.GlobalData = GlobalData;
		}

		public BaseMenu()
		{
		}

		public override void Init()
		{
			this.MenuInit();
		}

		public virtual void MenuInit()
		{
		}

		public override void UpdateData()
		{
			base.UpdateData();
			int count = base.StageData.MenuGroupList.Count;
			if (count > 0)
			{
				base.StageData.MenuGroupList[count - 1].MenuSelect();
			}
			base.StageData.MenuGroupList.ForEach(delegate(BaseMenuGroup x)
			{
				x.Ctrl();
			});
			base.Background3D.Ctrl();
			base.Background.Ctrl();
			base.Background2.Ctrl();
			for (int i = base.EffectList.Count - 1; i >= 0; i--)
			{
				base.EffectList[i].Ctrl();
			}
			base.Particle3D.Ctrl();
			base.SoundPlayList.ForEach(delegate(XAudio2_Player x)
			{
				x.Play();
			});
		}

		public virtual void BackgroundDrama()
		{
		}
	}
}
