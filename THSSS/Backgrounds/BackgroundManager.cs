using System;
using System.Collections.Generic;

namespace Shooting
{
	// Token: 0x02000041 RID: 65
	public class BackgroundManager
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000E878 File Offset: 0x0000CA78
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0000E890 File Offset: 0x0000CA90
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

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000E8DC File Offset: 0x0000CADC
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000E8F3 File Offset: 0x0000CAF3
		public List<BaseObject> BackgroundList { get; set; }

		// Token: 0x0600024C RID: 588 RVA: 0x0000E8FC File Offset: 0x0000CAFC
		public BackgroundManager()
		{
			this.Light = 255;
			this.BackgroundList = new List<BaseObject>();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000E95C File Offset: 0x0000CB5C
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

		// Token: 0x0600024E RID: 590 RVA: 0x0000E9F8 File Offset: 0x0000CBF8
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

		// Token: 0x0600024F RID: 591 RVA: 0x0000EA6A File Offset: 0x0000CC6A
		public void Clear()
		{
			this.BackgroundList.Clear();
		}

		// Token: 0x040000B2 RID: 178
		private int light = 0;
	}
}
