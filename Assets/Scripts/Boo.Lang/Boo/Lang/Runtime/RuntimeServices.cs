using System;
using System.Collections.Generic;
using System.Reflection;
using Boo.Lang.Runtime.DynamicDispatching;
using Boo.Lang.Runtime.DynamicDispatching.Emitters;

namespace Boo.Lang.Runtime
{
	public class RuntimeServices
	{
		internal const BindingFlags InstanceMemberFlags = default(BindingFlags);

		internal const BindingFlags DefaultBindingFlags = default(BindingFlags);

		private const BindingFlags InvokeBindingFlags = default(BindingFlags);

		private const BindingFlags SetPropertyBindingFlags = default(BindingFlags);

		private const BindingFlags GetPropertyBindingFlags = default(BindingFlags);

		private static readonly object[] NoArguments = new object[0];

		private static readonly Type RuntimeServicesType = typeof(RuntimeServices);

		private static readonly DispatcherCache _cache = new DispatcherCache();

		private static readonly ExtensionRegistry _extensions = new ExtensionRegistry();

		private static readonly object True = true;

		private static Dispatcher GetDispatcher(object target, string cacheKeyName, Type[] cacheKeyTypes, DispatcherCache.DispatcherFactory factory)
		{
			Type type = (target as Type) ?? target.GetType();
			DispatcherKey key = new DispatcherKey(type, cacheKeyName, cacheKeyTypes);
			return _cache.Get(key, factory);
		}

		public static object Coerce(object value, Type toType)
		{
			if (value == null)
			{
				return null;
			}
			object[] args = new object[1] { toType };
			Dispatcher dispatcher = GetDispatcher(value, "$Coerce$", new Type[1] { toType }, () => CreateCoerceDispatcher(value, toType));
			return dispatcher(value, args);
		}

		private static Dispatcher CreateCoerceDispatcher(object value, Type toType)
		{
			if (toType.IsInstanceOfType(value))
			{
				return IdentityDispatcher;
			}
			if (value is ICoercible)
			{
				return CoercibleDispatcher;
			}
			Type type = value.GetType();
			if (IsPromotableNumeric(type) && IsPromotableNumeric(toType))
			{
				return EmitPromotionDispatcher(type, toType);
			}
			MethodInfo methodInfo = FindImplicitConversionOperator(type, toType);
			if (methodInfo == null)
			{
				return IdentityDispatcher;
			}
			return EmitImplicitConversionDispatcher(methodInfo);
		}

		private static Dispatcher EmitPromotionDispatcher(Type fromType, Type toType)
		{
			return (Dispatcher)Delegate.CreateDelegate(typeof(Dispatcher), typeof(NumericPromotions).GetMethod(string.Concat("From", Type.GetTypeCode(fromType), "To", Type.GetTypeCode(toType))));
		}

		private static bool IsPromotableNumeric(Type fromType)
		{
			return IsPromotableNumeric(Type.GetTypeCode(fromType));
		}

		private static Dispatcher EmitImplicitConversionDispatcher(MethodInfo method)
		{
			return new ImplicitConversionEmitter(method).Emit();
		}

		private static object CoercibleDispatcher(object o, object[] args)
		{
			return ((ICoercible)o).Coerce((Type)args[0]);
		}

		private static object IdentityDispatcher(object o, object[] args)
		{
			return o;
		}

		private static bool IsNumeric(TypeCode code)
		{
			switch (code)
			{
			case TypeCode.Byte:
				return true;
			case TypeCode.SByte:
				return true;
			case TypeCode.Int16:
				return true;
			case TypeCode.Int32:
				return true;
			case TypeCode.Int64:
				return true;
			case TypeCode.UInt16:
				return true;
			case TypeCode.UInt32:
				return true;
			case TypeCode.UInt64:
				return true;
			case TypeCode.Single:
				return true;
			case TypeCode.Double:
				return true;
			case TypeCode.Decimal:
				return true;
			default:
				return false;
			}
		}

		public static string op_Addition(string lhs, string rhs)
		{
			return lhs + rhs;
		}

		public static bool EqualityOperator(object lhs, object rhs)
		{
			if (lhs == rhs)
			{
				return true;
			}
			if (lhs == null)
			{
				return rhs.Equals(lhs);
			}
			if (rhs == null)
			{
				return lhs.Equals(rhs);
			}
			TypeCode typeCode = Type.GetTypeCode(lhs.GetType());
			TypeCode typeCode2 = Type.GetTypeCode(rhs.GetType());
			if (IsNumeric(typeCode) && IsNumeric(typeCode2))
			{
				return EqualityOperator(lhs, typeCode, rhs, typeCode2);
			}
			Array array = lhs as Array;
			if (array != null)
			{
				Array array2 = rhs as Array;
				if (array2 != null)
				{
					return ArrayEqualityImpl(array, array2);
				}
			}
			return lhs.Equals(rhs) || rhs.Equals(lhs);
		}

		private static bool ArrayEqualityImpl(Array lhs, Array rhs)
		{
			if (lhs.Rank != 1 || rhs.Rank != 1)
			{
				throw new ArgumentException("array rank must be 1");
			}
			if (lhs.Length != rhs.Length)
			{
				return false;
			}
			for (int i = 0; i < lhs.Length; i++)
			{
				if (!EqualityOperator(lhs.GetValue(i), rhs.GetValue(i)))
				{
					return false;
				}
			}
			return true;
		}

		private static TypeCode GetConvertTypeCode(TypeCode lhsTypeCode, TypeCode rhsTypeCode)
		{
			if (lhsTypeCode == TypeCode.Decimal || rhsTypeCode == TypeCode.Decimal)
			{
				return TypeCode.Decimal;
			}
			if (lhsTypeCode == TypeCode.Double || rhsTypeCode == TypeCode.Double)
			{
				return TypeCode.Double;
			}
			if (lhsTypeCode == TypeCode.Single || rhsTypeCode == TypeCode.Single)
			{
				return TypeCode.Single;
			}
			if (lhsTypeCode == TypeCode.UInt64)
			{
				if (rhsTypeCode == TypeCode.SByte || rhsTypeCode == TypeCode.Int16 || rhsTypeCode == TypeCode.Int32 || rhsTypeCode == TypeCode.Int64)
				{
					return TypeCode.Int64;
				}
				return TypeCode.UInt64;
			}
			if (rhsTypeCode == TypeCode.UInt64)
			{
				if (lhsTypeCode == TypeCode.SByte || lhsTypeCode == TypeCode.Int16 || lhsTypeCode == TypeCode.Int32 || lhsTypeCode == TypeCode.Int64)
				{
					return TypeCode.Int64;
				}
				return TypeCode.UInt64;
			}
			if (lhsTypeCode == TypeCode.Int64 || rhsTypeCode == TypeCode.Int64)
			{
				return TypeCode.Int64;
			}
			if (lhsTypeCode == TypeCode.UInt32)
			{
				if (rhsTypeCode == TypeCode.SByte || rhsTypeCode == TypeCode.Int16 || rhsTypeCode == TypeCode.Int32)
				{
					return TypeCode.Int64;
				}
				return TypeCode.UInt32;
			}
			if (rhsTypeCode == TypeCode.UInt32)
			{
				if (lhsTypeCode == TypeCode.SByte || lhsTypeCode == TypeCode.Int16 || lhsTypeCode == TypeCode.Int32)
				{
					return TypeCode.Int64;
				}
				return TypeCode.UInt32;
			}
			return TypeCode.Int32;
		}

		private static bool EqualityOperator(object lhs, TypeCode lhsTypeCode, object rhs, TypeCode rhsTypeCode)
		{
			IConvertible convertible = (IConvertible)lhs;
			IConvertible convertible2 = (IConvertible)rhs;
			switch (GetConvertTypeCode(lhsTypeCode, rhsTypeCode))
			{
			case TypeCode.Decimal:
				return convertible.ToDecimal(null) == convertible2.ToDecimal(null);
			case TypeCode.Double:
				return convertible.ToDouble(null) == convertible2.ToDouble(null);
			case TypeCode.Single:
				return convertible.ToSingle(null) == convertible2.ToSingle(null);
			case TypeCode.UInt64:
				return convertible.ToUInt64(null) == convertible2.ToUInt64(null);
			case TypeCode.Int64:
				return convertible.ToInt64(null) == convertible2.ToInt64(null);
			case TypeCode.UInt32:
				return convertible.ToUInt32(null) == convertible2.ToUInt32(null);
			default:
				return convertible.ToInt32(null) == convertible2.ToInt32(null);
			}
		}

		internal static bool IsPromotableNumeric(TypeCode code)
		{
			switch (code)
			{
			case TypeCode.Byte:
				return true;
			case TypeCode.SByte:
				return true;
			case TypeCode.Int16:
				return true;
			case TypeCode.Int32:
				return true;
			case TypeCode.Int64:
				return true;
			case TypeCode.UInt16:
				return true;
			case TypeCode.UInt32:
				return true;
			case TypeCode.UInt64:
				return true;
			case TypeCode.Single:
				return true;
			case TypeCode.Double:
				return true;
			case TypeCode.Boolean:
				return true;
			case TypeCode.Decimal:
				return true;
			case TypeCode.Char:
				return true;
			default:
				return false;
			}
		}

		internal static MethodInfo FindImplicitConversionOperator(Type from, Type to)
		{
			return FindImplicitConversionMethod(from.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy), from, to) ?? FindImplicitConversionMethod(to.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy), from, to) ?? FindImplicitConversionMethod(GetExtensionMethods(), from, to);
		}

		private static IEnumerable<MethodInfo> GetExtensionMethods()
		{
			foreach (MemberInfo member in _extensions.Extensions)
			{
				if (member.MemberType == MemberTypes.Method)
				{
					yield return (MethodInfo)member;
				}
			}
		}

		private static MethodInfo FindImplicitConversionMethod(IEnumerable<MethodInfo> candidates, Type from, Type to)
		{
			foreach (MethodInfo candidate in candidates)
			{
				if (!(candidate.Name != "op_Implicit") && candidate.ReturnType == to)
				{
					ParameterInfo[] parameters = candidate.GetParameters();
					if (parameters.Length == 1 && parameters[0].ParameterType.IsAssignableFrom(from))
					{
						return candidate;
					}
				}
			}
			return null;
		}
	}
}
