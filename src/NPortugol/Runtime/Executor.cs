using System;
using System.Collections.Generic;
using System.Linq;
using NPortugol.Runtime.Exceptions;
using NPortugol.Runtime.Interop;

namespace NPortugol.Runtime
{
    public class Executor
    {
        private readonly IRuntimeContext context;

        private readonly OperandResolver opResolver;

        public Executor(IRuntimeContext context)
        {
            this.context = context;

            opResolver = new OperandResolver(context);
        }

        public Instruction Instruction { get { return context.CurrentInst; } }

        public IHospedagem Container
        {
            get { return context.Hospedagem; }
        }

        public ParamStack ParamStack
        {
            get { return context.Runnable.ParamStack; }
        }

        public RuntimeStack RuntimeStack
        {
            get { return context.Runnable.RuntimeStack; }
        }

        public SymbolTable SymbolTable
        {
            get { return context.Runnable.ScriptSymbolTable; }
        }

        public void ExecuteInstruction()
        {
            var opcode = Instruction.OpCode;

            switch (opcode)
            {
                    // Memory
                case OpCode.MOV: ProcessMOV(); break;
                case OpCode.DCL: ProcessDCL(); break;

                    // Arithmetic
                case OpCode.SADD: ProcessArithmetic(true); break;
                case OpCode.SSUB: ProcessArithmetic(true); break;
                case OpCode.SMUL: ProcessArithmetic(true); break;
                case OpCode.SDIV: ProcessArithmetic(true); break;
                case OpCode.SMOD: ProcessArithmetic(true); break;
                case OpCode.ADD: ProcessArithmetic(false); break;
                case OpCode.SUB: ProcessArithmetic(false); break;
                case OpCode.MUL: ProcessArithmetic(false); break;
                case OpCode.MOD: ProcessArithmetic(false); break;
                case OpCode.DIV: ProcessArithmetic(false); break;
                case OpCode.POW: ProcessArithmetic(false); break;
                case OpCode.INC: ProcessArithmetic(false); break;
                case OpCode.DEC: ProcessArithmetic(false); break;
                case OpCode.NEG: ProcessArithmetic(false); break;	
			
                    // String
                case OpCode.CNT: ProcessCNT(); break;
                case OpCode.SCNT: ProcessSCNT(); break;

                    // Branching
                case OpCode.JMP: ProcessBranching(); break;
                case OpCode.JE: ProcessBranching(); break;
                case OpCode.JNE: ProcessBranching(); break;
                case OpCode.JG: ProcessBranching(); break;
                case OpCode.JL: ProcessBranching(); break;
                case OpCode.JGE: ProcessBranching(); break;
                case OpCode.JLE: ProcessBranching(); break; 
                
                    // Stack
                case OpCode.PUSH: ProcessPUSH(); break;
                case OpCode.POP: ProcessPOP(); break;

                    // Flow
                case OpCode.RET: ProcessRET(); break;
                case OpCode.EXIT: ProcessEXIT(); break;
                case OpCode.CALL: ProcessCALL(); break;

                    // Interop
                case OpCode.HOST: ProcessHOST(); break;
            }
        }

        private void ProcessCNT()
        {
            var first = opResolver.At(0).StringValue();
            var second = opResolver.At(1).StringValue();

            SetSymbolValue(Instruction.Operands[0], string.Concat(first, second));
        }

        private void ProcessSCNT()
        {
            var second = ParamStack.Pop() as Operand;
            var first = ParamStack.Pop() as Operand;

            var value = string.Concat(first.Value, second.Value);
            
            ParamStack.Push(new Operand(OperandType.Literal, value));
        }

        private void ProcessBranching()
        {
            Operand secondOp;
            Operand firstOp;

            switch (Instruction.OpCode)
            {
                case OpCode.JMP:

                    context.Runnable.IP = opResolver.At(0).IntValue();
                    return;
                case OpCode.JE:

                    secondOp = (Operand)ParamStack.Pop();
                    firstOp = (Operand)ParamStack.Pop();

                    if (firstOp.Value.ToString() == secondOp.Value.ToString())
                        context.Runnable.IP = opResolver.At(0).IntValue();
                    return;
                case OpCode.JNE:

                    secondOp = (Operand)ParamStack.Pop();
                    firstOp = (Operand)ParamStack.Pop();

                    if (firstOp.Value.ToString() != secondOp.Value.ToString())
                        context.Runnable.IP = opResolver.At(0).IntValue();
                    return;
            }

            secondOp = (Operand)ParamStack.Pop();
            firstOp = (Operand)ParamStack.Pop();

            var fp = float.Parse(firstOp.Value.ToString());
            var sp = float.Parse(secondOp.Value.ToString());

            var address = opResolver.At(0).IntValue();
            var jump = false;

            switch (Instruction.OpCode)
            {
                case OpCode.JG:

                    if (fp > sp)
                        jump = true;
                    break;
                case OpCode.JL:

                    if (fp < sp)
                        jump = true;
                    break;
                case OpCode.JGE:

                    if (fp >= sp)
                        jump = true;
                    break;
                case OpCode.JLE:

                    if (fp <= sp)
                        jump = true;
                    break;
            }

            if (jump) context.Runnable.IP = address;
        }

        private void ProcessArithmetic(bool stack)
        {
            float f1, f2;

            if (stack)
            {
                var op2 = ParamStack.Pop() as Operand;
                var op1 = ParamStack.Pop() as Operand;

                f1 = float.Parse(op1.Value.ToString());
                f2 = float.Parse(op2.Value.ToString());
            }
            else
            {
                Operand op = null;
                object opValue = null;

                var name = opResolver.At(0).SymbolId();

                var curValue = SymbolValue(name);

                if (Instruction.OperandCount > 1)
                    op = Instruction.Operands[1];

                if (op != null)
                    opValue = opResolver.At(1).Value();

                if (curValue == null)
                    throw new Exception("Variable was not initialized.");

                f1 = float.Parse(curValue.ToString());
                f2 = 0f;

                if (opValue != null)
                    f2 = float.Parse(opValue.ToString());
            }

            switch (context.CurrentInst.OpCode)
            {
                case OpCode.SADD:
                case OpCode.ADD: f1 += f2; break;
                case OpCode.SSUB:
                case OpCode.SUB: f1 -= f2; break;
                case OpCode.SMUL:
                case OpCode.MUL: f1 *= f2; break;
                case OpCode.SDIV:
                case OpCode.DIV: f1 /= f2; break;
                case OpCode.SMOD:
                case OpCode.MOD: f1 %= f2; break;
                case OpCode.POW: f1 = (float)Math.Pow(f1, f2); break;
                case OpCode.INC: f1++; break;
                case OpCode.DEC: f1--; break;
                case OpCode.NEG: f1 = -f1; break;
            }
            
            int r;
            
            if (stack)
            {
                var result = new Operand(OperandType.Literal, int.TryParse(f1.ToString(), out r) ? r : f1);

                ParamStack.Push(result);
            }
            else
            {
                object value = int.TryParse(f1.ToString(), out r) ? r : f1;

                SetSymbolValue(Instruction.Operands[0], value);
            }
        }

        private void ProcessHostedFunction(string name)
        {
            var handler = Container.ResolveHandler(name);
            var function = Container.Resolve(name);

            IList<object> parameters = new List<object>();

            foreach (Operand item in ParamStack)
            {
                if (item.Type == OperandType.Literal)
                {
                    if (item.Value != null)
                        parameters.Add(item.Value);
                }
                else
                {
                    parameters.Add(SymbolValue(item.Value.ToString()));
                }
            }

            var result = handler != null ? handler.Invoke(parameters.Reverse().ToArray()) : function.Executar(parameters.Reverse().ToArray());

            var operand = new Operand(OperandType.Literal, result);

            ParamStack.Clear();

            if (!Container.IsVoid(name))
                ParamStack.Push(operand);
        }

        private void ProcessHOST()
        {
            if (Instruction.OperandCount == 2)
            {
                ProcessPropertyRef();
            }
        }

        private void ProcessPropertyRef()
        {
            var @object = opResolver.At(0).Value();
            var propertyName = opResolver.At(1).Name();
            var propInfo = @object.GetType().GetProperty(propertyName);
            var value = propInfo.GetValue(@object, null);

            ParamStack.Push(new Operand(OperandType.Literal, value));
        }

        private void ProcessCALL()
        {
            var name = opResolver.At(0).Name();

            if (Container.IsRegistered(name))
            {
                ProcessHostedFunction(name);
                return;
            }

            var returnInst = context.Runnable.Instructions[Instruction.Index + 1];
            
            var returnAddress = new Operand(OperandType.InstructionRef, returnInst);

            RuntimeStack.Push(returnAddress);

            var function = context.Runnable.RetrieveFunction(name);

            context.Runnable.IP = function.EntryPoint;

            RuntimeStack.Push(function);

            context.CurrentFunction = null;
        }

        private void ProcessEXIT()
        {
            RuntimeStack.Clear();
            context.Completed = true;
        }

        private void ProcessPOP()
        {
            if (!opResolver.At(0).IsVariable())
                new Ops().ThrowNotVariable(opResolver.At(0).Name());
            
            SetSymbolValue(Instruction.Operands[0], (Operand)ParamStack.Pop());
        }

        private void ProcessPUSH()
        {
            var operand = Instruction.Operands[0];

            var @new = new Operand(OperandType.Literal, operand.Value);

            if (operand.Type == OperandType.Variable)
                @new.Value = opResolver.At(0).Value();

            if (@new.Value == null) 
                new Ops().ThrowVarNonInit(operand.Value.ToString());

            context.Runnable.ParamStack.Push(@new);
        }

        private void ProcessMOV()
        {
            if (Instruction.Operands[0].Type == OperandType.Literal)
                new Ops().ThrowNotVariable(opResolver.At(0).Name());

            var name = opResolver.At(0).SymbolId();

            if (!context.Runnable.ScriptSymbolTable.ContainsKey(name))
                new Ops().ThrowNonDeclared(opResolver.At(0).Name());

            SetSymbolValue(Instruction.Operands[0], Instruction.Operands[1]);
        }

        private void ProcessDCL()
        {
            SetSymbolValue(Instruction.Operands[0], new Operand(OperandType.Literal, null));
        }

        private void ProcessRET()
        {
            var lastFunction = RuntimeStack.Pop();

            if (context.EnableGC)
                GarbageCollection(lastFunction as FunctionCall);

            var address = RuntimeStack.Count > 0? (Operand) RuntimeStack.Pop()
                              : new Operand(OperandType.Null, null);

            context.CurrentFunction = null;
            
            if (address.Type == OperandType.InstructionRef)
            {
                context.Runnable.IP = ((Instruction) address.Value).Index;
            }

            if (RuntimeStack.Count == 0)
            {
                context.Completed = true;
                return;
            }
        }

        private void GarbageCollection(FunctionCall lastFunction)
        {
            var list = context.Runnable.ScriptSymbolTable
                .Where(x => x.Key.StartsWith(lastFunction.Function.Name.ToLower()))
                .Where(x => x.Key.EndsWith("_" + lastFunction.Index))
                .Select(x => x.Key);

            var removeList = new List<string>(list);

            foreach (var item in removeList)
            {
                context.Runnable.ScriptSymbolTable.Remove(item);
            }
        }

        public object SymbolValue(string id)
        {
            if (SymbolTable[id] == null)
                throw new Exception("Não inicializada");

            return SymbolTable[id].Value;
        }

        public object SetSymbolValue(Operand target, object value)
        {
            SetSymbolValue(target, new Operand(OperandType.Literal, value));

            return value;
        }

        public object SetSymbolValue(Operand target, Operand opvalue)
        {

            var id = opResolver.In(target).SymbolId();
            var name = opResolver.Name();
            var _value = opResolver.In(opvalue).Value();
			
			int? index = null;
			
			if (target.IndexOffSet != null) 
			    index = ResolveIndex(target.IndexOffSet);
					
            return SymbolTable.SetSymbolValue(name, id, context.CurrentFunction.Name, _value, index);
        }
		
        private int ResolveIndex(object value)
        {
            int r;

            if (int.TryParse(value.ToString(), out r)) return r;

            var name = opResolver.In(new Operand(OperandType.Variable, value)).SymbolId();

            var _value = SymbolTable[name];

            return int.Parse(_value.ToString());
        }		
    }
}