using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Shooting.Properties
{
	// Token: 0x020000EB RID: 235
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x0600065C RID: 1628 RVA: 0x000382D3 File Offset: 0x000364D3
		internal Resources()
		{
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x000382E0 File Offset: 0x000364E0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("Shooting.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0003832C File Offset: 0x0003652C
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x00038343 File Offset: 0x00036543
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0003834C File Offset: 0x0003654C
		internal static Bitmap background
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("background", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0003837C File Offset: 0x0003657C
		internal static Bitmap FiveStar
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("FiveStar", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x000383AC File Offset: 0x000365AC
		internal static Bitmap Menu_Start
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Menu_Start", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x000383DC File Offset: 0x000365DC
		internal static Bitmap MyPlane
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("MyPlane", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0003840C File Offset: 0x0003660C
		internal static Bitmap Player
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Player", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0003843C File Offset: 0x0003663C
		internal static Bitmap Power
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Power", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0003846C File Offset: 0x0003666C
		internal static Bitmap Score
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Score", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0003849C File Offset: 0x0003669C
		internal static Bitmap Spell
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Spell", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x000384CC File Offset: 0x000366CC
		internal static Bitmap 菜单背景
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("菜单背景", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x0400022C RID: 556
		private static ResourceManager resourceMan;

		// Token: 0x0400022D RID: 557
		private static CultureInfo resourceCulture;
	}
}
