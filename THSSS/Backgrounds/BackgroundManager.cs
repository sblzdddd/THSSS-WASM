using System.Collections.Generic;

namespace Shooting
{
	public class BackgroundManager
	{
		public int Light
		{
			get
			{
				return light;
			}
			set
			{
				if (value < 0)
				{
					light = 0;
				}
				else if (value > 255)
				{
					light = 255;
				}
				else
				{
					light = value;
				}
			}
		}

		public List<BaseObject> BackgroundList { get; set; }

		public BackgroundManager()
		{
			Light = 255;
			BackgroundList = new List<BaseObject>();
		}

		public void Ctrl()
		{
			BackgroundList.ForEach(delegate(BaseObject x)
			{
				if (x.OutBoundary())
				{
					BackgroundList.Remove(x);
				}
			});
			BackgroundList.ForEach(delegate(BaseObject x)
			{
				x.Ctrl();
			});
		}

		public void Show(bool Active)
		{
			if (Active)
			{
				BackgroundList.ForEach(delegate(BaseObject x)
				{
					if (x.Active)
					{
						x.Show();
					}
				});
			}
			else
			{
				BackgroundList.ForEach(delegate(BaseObject x)
				{
					if (!x.Active)
					{
						x.Show();
					}
				});
			}
		}

		public void Clear()
		{
			BackgroundList.Clear();
		}

		private int light = 0;
	}
}
