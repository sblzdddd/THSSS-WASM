namespace Shooting
{
	internal partial class BaseMenu : BaseGameState, IGameState
	{
		public int BackgroundLight { get; set; }

		public BaseMenu(GlobalDataPackage GlobalData)
		{
			Visible = false;
			Enabled = false;
			this.GlobalData = GlobalData;
		}

		public BaseMenu()
		{
		}

		public override void Init()
		{
			MenuInit();
		}

		public virtual void MenuInit()
		{
		}

		public override void UpdateData()
		{
			base.UpdateData();
			int count = StageData.MenuGroupList.Count;
			if (count > 0)
			{
				StageData.MenuGroupList[count - 1].MenuSelect();
			}
			StageData.MenuGroupList.ForEach(delegate(BaseMenuGroup x)
			{
				x.Ctrl();
			});
			Background3D.Ctrl();
			Background.Ctrl();
			Background2.Ctrl();
			for (int i = EffectList.Count - 1; i >= 0; i--)
			{
				EffectList[i].Ctrl();
			}
			Particle3D.Ctrl();
			SoundPlayList.ForEach(delegate(XAudio2_Player x)
			{
				x.Play();
			});
		}

		public virtual void BackgroundDrama()
		{
		}
	}
}
