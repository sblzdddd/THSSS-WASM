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
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		internal Resources()
		{
		}

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

		internal static Bitmap background
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("background", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap FiveStar
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("FiveStar", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap Menu_Start
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Menu_Start", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap MyPlane
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("MyPlane", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap Player
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Player", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap Power
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Power", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap Score
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Score", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap Spell
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Spell", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap 菜单背景
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("菜单背景", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;
	}
}
