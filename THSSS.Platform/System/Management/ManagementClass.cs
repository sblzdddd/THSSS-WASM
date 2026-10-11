using System;
using System.Collections;

namespace System.Management
{
	public class ManagementBaseObject
	{
		public PropertyDataCollection Properties { get; protected set; }

		protected ManagementBaseObject()
		{
			this.Properties = new PropertyDataCollection();
		}
	}

	public class ManagementObject : ManagementBaseObject
	{
	}

	public class PropertyData
	{
		public object Value { get; set; }
	}

	public class PropertyDataCollection
	{
		public PropertyData this[string name]
		{
			get
			{
				return new PropertyData
				{
					Value = "96"
				};
			}
		}
	}

	public class ManagementObjectCollection : IEnumerable, IDisposable
	{
		public IEnumerator GetEnumerator()
		{
			return new object[0].GetEnumerator();
		}

		public void Dispose()
		{
		}
	}

	public class ManagementClass : ManagementObject, IDisposable
	{
		public ManagementClass(string path)
		{
		}

		public ManagementObjectCollection GetInstances()
		{
			return new ManagementObjectCollection();
		}

		public void Dispose()
		{
		}
	}
}
