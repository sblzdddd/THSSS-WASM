using System;
using System.Collections.Generic;

namespace Shooting
{
	public class BackgroundManager
	{
		public int Light
		{
			get
			{
				return this.light;
			}
			set
			{
				if (value < 0)
				{
					this.light = 0;
				}
				else if (value > 255)
				{
					this.light = 255;
				}
				else
				{
					this.light = value;
				}
			}
		}

		public List<BaseObject> BackgroundList { get; set; }

		public BackgroundManager()
		{
			this.Light = 255;
			this.BackgroundList = new List<BaseObject>();
		}

		public void Ctrl()
		{
			this.BackgroundList.ForEach(delegate(BaseObject x)
			{
				if (x.OutBoundary())
				{
					this.BackgroundList.Remove(x);
				}
			});
			this.BackgroundList.ForEach(delegate(BaseObject x)
			{
				x.Ctrl();
			});
		}

		public void Show(bool Active)
		{
			if (Active)
			{
				this.BackgroundList.ForEach(delegate(BaseObject x)
				{
					if (x.Active)
					{
						x.Show();
					}
				});
			}
			else
			{
				this.BackgroundList.ForEach(delegate(BaseObject x)
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
			this.BackgroundList.Clear();
		}

		private int light = 0;
	}
}
