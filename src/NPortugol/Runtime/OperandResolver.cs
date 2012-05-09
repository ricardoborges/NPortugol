using NPortugol.Runtime.Exceptions;
using NPortugol.Runtime.Extensions;

namespace NPortugol.Runtime
{
    public class OperandResolver
    {
        private readonly IRuntimeContext context;
        
        private Operand target;

        private string targetName;

        public OperandResolver(IRuntimeContext context)
        {
            this.context = context;
        }

        public OperandResolver In(Operand operand)
        {
            target = operand;

            if (target.Value != null)
                targetName = target.Value.ToString();

            return this;
        }

        public OperandResolver At(int index)
        {
            target = context.CurrentInst.Operands[index];
            targetName = target.Value.ToString();
            return this;
        }

        public string Name()
        {
            return targetName;
        }

        public string SymbolId()
        {
            return Name().ToSymbolId(context);            
        }

        public object Value()
        {
            var value = target.Value;

            if (IsVariable())
            {
                context.Runnable.ScriptSymbolTable.EnsureExists(SymbolId(), Name());

                value = context.Runnable.ScriptSymbolTable[SymbolId()].Value;
            }

            if (target.IndexOffSet != null)
            {
                int index;
                
                if (!int.TryParse(target.IndexOffSet.ToString(), out index))
                {
                    index = ResolveIndex(target.IndexOffSet);
                }

                var array = (object[]) value;

                if (array == null)
                    new Ops().ThrowArrayNonInit(Name());

                value = array[index];
            }

            return value;
        }

        private int ResolveIndex(object value)
        {
            var name = value.ToString().ToSymbolId(context);

            var _value = context.Runnable.ScriptSymbolTable[name];

            return int.Parse(_value.ToString());
        }

        public string StringValue()
        {
            return Value().ToString();
        }

        public float FloatValue()
        {
            return float.Parse(StringValue());
        }

        public int IntValue()
        {
            return int.Parse(StringValue());
        }        

        public bool IsVariable()
        {
            return target.Type == OperandType.Variable ||
                   target.Type == OperandType.HostObjectRef;
        }

        public bool Indexed()
        {
            return target.IndexOffSet != null;
        }
    }
}