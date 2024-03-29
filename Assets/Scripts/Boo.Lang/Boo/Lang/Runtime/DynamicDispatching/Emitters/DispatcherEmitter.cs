using System;
using System.Reflection.Emit;

namespace Boo.Lang.Runtime.DynamicDispatching.Emitters
{
	public abstract class DispatcherEmitter
	{
		private DynamicMethod _dynamicMethod;

		protected readonly ILGenerator _il;

		public DispatcherEmitter(Type owner, string dynamicMethodName)
		{
			_dynamicMethod = new DynamicMethod(owner.Name + "$" + dynamicMethodName, typeof(object), new Type[2]
			{
				typeof(object),
				typeof(object[])
			}, owner);
			_il = _dynamicMethod.GetILGenerator();
		}

		public Dispatcher Emit()
		{
			EmitMethodBody();
			return CreateMethodDispatcher();
		}

		protected abstract void EmitMethodBody();

		protected Dispatcher CreateMethodDispatcher()
		{
			return (Dispatcher)_dynamicMethod.CreateDelegate(typeof(Dispatcher));
		}

		protected void EmitCastOrUnbox(Type type)
		{
			if (type.IsValueType)
			{
				_il.Emit(OpCodes.Unbox, type);
				_il.Emit(OpCodes.Ldobj, type);
			}
			else
			{
				_il.Emit(OpCodes.Castclass, type);
			}
		}

		protected void BoxIfNeeded(Type returnType)
		{
			if (returnType.IsValueType)
			{
				_il.Emit(OpCodes.Box, returnType);
			}
		}

		protected void EmitReturn(Type typeOnStack)
		{
			if (typeOnStack == typeof(void))
			{
				_il.Emit(OpCodes.Ldnull);
			}
			else
			{
				BoxIfNeeded(typeOnStack);
			}
			_il.Emit(OpCodes.Ret);
		}
	}
}
