using System.Collections.Generic;
using System.Reflection;

namespace Boo.Lang.Runtime
{
	public class ExtensionRegistry
	{
		private List<MemberInfo> _extensions = new List<MemberInfo>();

		private object _classLock = new object();

		public IEnumerable<MemberInfo> Extensions
		{
			get
			{
				return _extensions;
			}
		}
	}
}
