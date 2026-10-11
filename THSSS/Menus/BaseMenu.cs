using System;
using System.Windows.Forms;

namespace Shooting
{
	// Token: 0x0200005F RID: 95
	internal partial class BaseMenu : BaseGameState, IGameState
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00015FF0 File Offset: 0x000141F0
		// (set) Token: 0x0600032B RID: 811 RVA: 0x00016007 File Offset: 0x00014207
		public int BackgroundLight { get; set; }

		// Token: 0x0600032C RID: 812 RVA: 0x00016010 File Offset: 0x00014210
		public BaseMenu(GlobalDataPackage GlobalData)
		{
			base.Visible = false;
			base.Enabled = false;
			base.GlobalData = GlobalData;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00016033 File Offset: 0x00014233
		public BaseMenu()
		{
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0001603E File Offset: 0x0001423E
		public override void Init()
		{
			this.MenuInit();
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00016048 File Offset: 0x00014248
		public virtual void MenuInit()
		{
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00016060 File Offset: 0x00014260
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

		// Token: 0x06000331 RID: 817 RVA: 0x0001616A File Offset: 0x0001436A
		public virtual void BackgroundDrama()
		{
		}
	}
}
