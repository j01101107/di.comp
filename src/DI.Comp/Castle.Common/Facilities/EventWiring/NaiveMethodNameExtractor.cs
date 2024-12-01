using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Castle.Common.Facilities.EventWiring
{
    internal class NaiveMethodNameExtractor
    {
        private readonly MethodBody body;
        private readonly MethodBase delegateMethod;
        private readonly Module module;
        private readonly MemoryStream stream;

        public NaiveMethodNameExtractor(Delegate @delegate)
        {
            delegateMethod = @delegate.Method;
            body = delegateMethod.GetMethodBody();
            Debug.Assert(body != null);
            module = delegateMethod.Module;
            stream = new MemoryStream(body.GetILAsByteArray());
            Read();
        }

        public MethodBase CalledMethod { get; private set; }

        private MethodBase GetCalledMethod(byte[] rawOperand)
        {
            Type[] genericTypeArguments = null;
            Type[] genericMethodArguments = null;
            if (delegateMethod.DeclaringType.IsGenericType)
            {
                genericTypeArguments = delegateMethod.DeclaringType.GetGenericArguments();
            }

            if (delegateMethod.IsGenericMethod)
            {
                genericMethodArguments = delegateMethod.GetGenericArguments();
            }

            var methodBase = module.ResolveMethod(OperandValueAsInt32(rawOperand), genericTypeArguments, genericMethodArguments);
            return methodBase;
        }

        private void Read()
        {
            OpCodeValues currentOpCode;
            while (ReadOpCode(out currentOpCode))
            {
                if (IsSupportedOpCode(currentOpCode) == false)
                {
                    return;
                }

                if (currentOpCode == OpCodeValues.Callvirt || currentOpCode == OpCodeValues.Call)
                {
                    CalledMethod = GetCalledMethod(ReadOperand(32));
                    return;
                }
            }
        }

        private bool ReadOpCode(out OpCodeValues opCodeValue)
        {
            var valueInt = stream.ReadByte();
            if (valueInt == -1)
            {
                opCodeValue = 0;
                return false;
            }

            var xByteValue = (byte) valueInt;
            if (xByteValue == 0xFE)
            {
                valueInt = stream.ReadByte();
                if (valueInt == -1)
                {
                    opCodeValue = 0;
                    return false;
                }

                opCodeValue = (OpCodeValues) ((xByteValue << 8) | valueInt);
            }
            else
            {
                opCodeValue = (OpCodeValues) xByteValue;
            }

            return true;
        }

        private byte[] ReadOperand(byte operandSize)
        {
            var bytes = new byte[operandSize / 8];
            var actualSize = stream.Read(bytes, 0, bytes.Length);
            if (actualSize < bytes.Length)
            {
                throw new NotSupportedException();
            }

            return bytes;
        }

        private static bool IsSupportedOpCode(OpCodeValues currentOpCode)
        {
            return Enum.IsDefined(typeof(OpCodeValues), currentOpCode);
        }

        private static int OperandValueAsInt32(byte[] rawOperand)
        {
            var value = new byte[4];
            Array.Copy(rawOperand, value, Math.Min(4, rawOperand.Length));
            return BitConverter.ToInt32(value, 0);
        }
    }
}