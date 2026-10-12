using System.Collections.Generic;

namespace Shooting
{
	public class BaseStory : BaseObject
	{
		public BaseStory()
		{
		}

		public BaseStory(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.Story = this;
			if (base.Boss != null)
			{
				base.Boss.Enabled = false;
			}
		}

		public override void Ctrl()
		{
			if (base.KClass.Key_Z || base.KClass.Key_Ctrl)
			{
				if (!this.Z)
				{
					base.Time++;
					base.StageData.SoundPlay("se_plst00.wav");
				}
				if (!base.KClass.Key_Ctrl)
				{
					this.Z = true;
				}
			}
			else
			{
				this.Z = false;
			}
		}

		public List<BaseObject> StoryEffectList = new List<BaseObject>();

		private bool Z = true;
	}
}
