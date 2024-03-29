using System;
using System.Collections;
using System.Text;

namespace Boo.Lang
{
	public class Builtins
	{
		public static string join(IEnumerable enumerable, string separator)
		{
			StringBuilder stringBuilder = new StringBuilder();
			IEnumerator enumerator = enumerable.GetEnumerator();
			using (enumerator as IDisposable)
			{
				if (enumerator.MoveNext())
				{
					stringBuilder.Append(enumerator.Current);
					while (enumerator.MoveNext())
					{
						stringBuilder.Append(separator);
						stringBuilder.Append(enumerator.Current);
					}
				}
			}
			return stringBuilder.ToString();
		}
	}
}
