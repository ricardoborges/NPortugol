using System.Reflection.Emit;
using NPortugol.Runtime;
using NPortugol.Runtime.Asm;

namespace NPortugol
{
    public class ILTranslator
    {
        private readonly Runnable runnable;
        private readonly AntlrRunnableBuilder builder;
        private readonly string name;

        public ILTranslator(Bytecode bytecode)
        {
            builder = new AntlrRunnableBuilder();
            builder.Load(bytecode.Script);
            runnable = builder.Build();
            name = bytecode.FunctionNames[0];
        }

        public DynamicMethod Translate()
        {
            var dm = new DynamicMethod(name, typeof (int), null);
            var gen = dm.GetILGenerator();

            foreach (var inst in runnable.Instructions)
            {
                switch (inst.OpCode)
                {
                    case Runtime.OpCode.PUSH:
                        gen.Emit(OpCodes.Ldc_I4, (int) inst.Operands[0].Value);
                        break;
                    case Runtime.OpCode.SADD:
                        gen.Emit(OpCodes.Add);
                        break;
                    case Runtime.OpCode.RET:
                        gen.Emit(OpCodes.Ret);
                        break;
                }
            }

            return dm;
        }
    }
}