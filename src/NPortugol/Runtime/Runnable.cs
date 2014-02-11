using System;

namespace NPortugol.Runtime
{
    public class Runnable
    {
        private InstrucStream stream;
        
        private FunctionTable functionTable;

        private Function mainFunction;

        public Runnable(InstrucStream instStream, FunctionTable fTable)
        {
            Setup(instStream, fTable, null);
        }

        public Runnable(InstrucStream instStream, FunctionTable fTable, SymbolTable symbolTable)
        {
            Setup(instStream, fTable, symbolTable);
        }

        private void Setup(InstrucStream instStream, FunctionTable fTable, SymbolTable symbolTable)
        {
            stream = instStream;
            functionTable = fTable;
			
            if (functionTable.ContainsKey(Function.MainName))
            {
                mainFunction = functionTable[Function.MainName];
            }

            ScriptSymbolTable = SymbolTable.CreateFor(symbolTable);

            RuntimeStack = new RuntimeStack();

            ParamStack = new ParamStack();
        }

        public SymbolTable ScriptSymbolTable { get; set; }

        public RuntimeStack RuntimeStack { get; private set; }

        public ParamStack ParamStack { get; private set; }

        public FunctionTable FunctionTable
        {
            get { return functionTable; }
        }

        public InstrucStream InstrucStream
        {
            get { return stream; }
        }

        public int IP { get; set; }

        public object GetSymbolValue(string id)
        {
            return !ScriptSymbolTable.ContainsKey(id) ? null : ScriptSymbolTable[id].Value;
        }

        public object GetSymbolValue(string id, int index)
        {
            var symbol = ScriptSymbolTable[id];

            if (symbol == null) 
                throw new Exception(string.Format("Variable {0} is not an array", id));

            var array = symbol.Value as object[];
    
            return array[index];
        }

        public bool HasMainFunction()
        {
            return mainFunction != null;
        }

        public Function MainFunction
        {
            get { return HasMainFunction() ? mainFunction : null; }
        }

        public Instruction[] Instructions
        {
            get { return stream.Instructions; }
        }

        public bool HasFunction(string name)
        {
            return functionTable.ContainsKey(name);
        }

        public Function RetrieveFunction(string name)
        {
            return functionTable[name];
        }
    }
}