using System;
using System.Collections.Generic;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using NPortugol;

namespace NPortugol
{
    public class Emissor
    {
        #region fields

        private int labelCount;
        
        private Stack<string> forIncId = new Stack<string>();

        public List<string> function = new List<string>();
        public List<object> @params = new List<object>();
        public List<string> script = new List<string>();
        public Stack<string> labels = new Stack<string>();

        public Dictionary<int, int> SourceMap = new Dictionary<int, int>();
        public Dictionary<int, int> SourceFunc = new Dictionary<int, int>();

        public bool DebugInfo { get; set; }

        public List<string> ScriptLines
        {
            get { return script.Count > 0 ? script : function; }
        }

        #endregion

        #region function

        public void EmitFunction(IToken token)
        {
            var first = script.Count == 0;

            string id = EnsureMainFunction(token.Text);

            EmitRet();

            EmitParam();

            function.Insert(0, AsmTemplate.Function(id));
            
            IndexRefactory(first);

            script.AddRange(function);

            SourceFunc.Clear();
            function.Clear();
        }

        private void IndexRefactory(bool first)
        {
            if (!DebugInfo) return;

            foreach (var item in SourceFunc)
            {
                if (!first)
                    SourceMap.Add(script.Count + item.Key, item.Value);
                else
                    SourceMap.Add(item.Key, item.Value);
            }
        }

        private void EmitParam()
        {
            function.Insert(0, "EMP");

            foreach (var param in @params)
            {
                function.Insert(0, AsmTemplate.Pop(param));
                function.Insert(0, AsmTemplate.Declare(param));
            }

            @params.Clear();
        }

        #endregion

        public void EmitVar(IToken id)
        {
            function.Add(AsmTemplate.Declare(id.Text));
            MapFunction(id);
        }

        private void MapFunction(IToken token)
        {
            if (!DebugInfo) return;

            SourceFunc.Add(function.Count, token.Line-1);
        }

        public void EmitAssign(IToken token, object value)
        {
            EmitPop(token);

            //function.Add(AsmTemplate.Assign(token.Text, value));
           // MapFunction(token);
        }

        public void EmitAssign(IToken token, int start, int end)
        {
            var index = 0;

            for (var i = start; i <= end; i++)
            {
                function.Add(AsmTemplate.AssignIndex(token.Text,i, index++));
                MapFunction(token);
            }
        }

        public void EmitAssignArray(IToken token, CommonTree tree)
        {
            var index = 0;
            var asgnNode = (CommonTree) tree.Parent;

            foreach (var item in asgnNode.Children)
            {
                function.Add(AsmTemplate.AssignIndex(token.Text, int.Parse(item.Text), index++));
                MapFunction(token);
            }
        }

        public void EmitPush(object value)
        {
            function.Add(AsmTemplate.Push(value));
        }

        public void EmitPush(object value, IToken token)
        {
            function.Add(AsmTemplate.Push(value));
            MapFunction(token);
        }

        public void EmitPush(object value, int index)
        {
            function.Add(AsmTemplate.Push(value, index));
        }


        public void EmitPush(object value, string id)
        {
            function.Add(AsmTemplate.Push(value, id));
        }

        public void EmitPop(IToken token)
        {
            function.Add(AsmTemplate.Pop(token.Text));
            MapFunction(token);
        }

        public void EmitPop(IToken token, int index)
        {
            function.Add(AsmTemplate.Pop(token.Text, index));
            MapFunction(token);
        }

        public void EmitPop(IToken token, string id)
        {
            function.Add(AsmTemplate.Pop(token.Text, id));
            MapFunction(token);
        }

        public void EmitStackAdd()
        {
            function.Add(AsmTemplate.StackAdd());
        }

        public void EmitStackSub()
        {
            function.Add(AsmTemplate.StackSub());
        }

        public void EmitStackPlus()
        {
            function.Add(AsmTemplate.StackPlus());
        }

        public void EmitStackDiv()
        {
            function.Add(AsmTemplate.StackDiv());
        }

        public void EmitStackMod()
        {
            function.Add(AsmTemplate.StackMod());
        }

        public void EmitCall(IToken token)
        {
            function.Add(AsmTemplate.Call(token.Text));
            MapFunction(token);
        }

        public void EmitRet()
        {
            if (function.Count > 0 && function[function.Count - 1].Equals("RET")) return;

            function.Add("RET"); 
        }

        public void EmitRet(IToken token)
        {
            if (function.Count > 0 && function[function.Count - 1].Equals("RET")) return;

            function.Add("RET");
            MapFunction(token);
        }

        private static string EnsureMainFunction(string id)
        {
            return id == "principal"? "main": id;
        }

        public void AddParam(object id)
        {
            @params.Add(id);
        }

        #region expression

        public void EmitEqualsExp(bool invertExp)
        {
            var inst = invertExp
                           ? AsmTemplate.JumpNotEquals(CreateAndPushLabel())
                           : AsmTemplate.JumpEquals(CreateAndPushLabel());

            function.Add(inst);
        }

        public void EmitNotEqExp(bool invertExp)
        {
            var inst = invertExp
                          ? AsmTemplate.JumpEquals(CreateAndPushLabel())
                          : AsmTemplate.JumpNotEquals(CreateAndPushLabel());

            function.Add(inst);
        }

        public void EmitGreaterExp(bool invertExp)
        {
            var inst = invertExp
                           ? AsmTemplate.JumpLessEquals(CreateAndPushLabel())
                           : AsmTemplate.JumpGreater(CreateAndPushLabel());

            function.Add(inst);
        }

        public void EmitGreaterEqExp(bool invertExp)
        {
            var inst = invertExp
                        ? AsmTemplate.JumpLess(CreateAndPushLabel())
                        : AsmTemplate.JumpGreaterEquals(CreateAndPushLabel());

            function.Add(inst);
        }

        public void EmitLessExp(bool invertExp)
        {
            var inst = invertExp
                          ? AsmTemplate.JumpGreaterEquals(CreateAndPushLabel())
                          : AsmTemplate.JumpLess(CreateAndPushLabel());

            function.Add(inst);
        }

        public void EmitLessEqExp(bool invertExp)
        {
            var inst = invertExp
                        ? AsmTemplate.JumpGreater(CreateAndPushLabel())
                        : AsmTemplate.JumpLessEquals(CreateAndPushLabel());

            function.Add(inst);
        }

        private void EmitJMP(string slabel)
        {
            function.Add(AsmTemplate.Jump(slabel));
        }
        
        #endregion

        private string CreateAndPushLabel()
        {
            var label = string.Format(AsmTemplate.LabelName(labelCount++));
            
            labels.Push(label);

            return label;
        }

        public void EmitIf(bool withElse)
        {
            var label = labels.Pop();

            if (withElse)
                EmitJMP(CreateAndPushLabel());

            function.Add(AsmTemplate.Label(label));
        }

        public void EmitElse()
        {
            var slabel = labels.Pop();

            function.Add(AsmTemplate.Label(slabel));
        }

        public void EmitInitFor(IToken token, bool inc)
        {
            var id = forIncId.Pop();

            EmitLabel();

            EmitPush(id);
            EmitPush(token.Text, token);

            forIncId.Push(id);

            function.Add(inc ? AsmTemplate.JumpGreaterEquals(CreateAndPushLabel())
                             : AsmTemplate.JumpLessEquals(CreateAndPushLabel()));
        }

        public void EmitEndFor(IToken token, bool inc)
        {
            var endLoop = labels.Pop();
            var initLoop = labels.Pop();

            if (inc)
                EmitIncId();
            else
                EmitDecId();

            EmitJMP(initLoop);
            
            EmitLabel(endLoop);
        }


        public void EmitInitWhile()
        {
            EmitLabel();
        }

        public void EmitEndWhile()
        {
            var endLoop = labels.Pop();
            var initLoop = labels.Pop();

            EmitJMP(initLoop);

            EmitLabel(endLoop);
        }

        public void EmitInitRepeat()
        {
            EmitLabel();
        }

        public void EmitEndRepeat()
        {
            var endLoop = labels.Pop();
            var initLoop = labels.Pop();

            EmitJMP(initLoop);

            EmitLabel(endLoop);
        }

        private void EmitLabel(string endLoop)
        {
            function.Add(AsmTemplate.Label(endLoop));
        }

        public void EmitLabel()
        {
            function.Add(AsmTemplate.Label(CreateAndPushLabel()));
        }

        public void EmitIncId()
        {
            function.Add(AsmTemplate.Inc(forIncId.Pop()));
        }

        public void SetForInc(string id)
        {
            forIncId.Push(id);
        }

        public void EmitDecId()
        {
            function.Add(AsmTemplate.Dec(forIncId.Pop()));
        }

        public void EmitDebug(string temp)
        {
            function.Add(temp);
        }

        private void EmitHost(IToken objectToken, IToken propToken)
        {
            function.Add(AsmTemplate.PropertyCall(objectToken.Text, propToken.Text));
            MapFunction(propToken);
        }

        public void EmitPropCall(IToken objectToken, IToken propToken)
        {
            EmitHost(objectToken, propToken);
        }

        public void EmitMethodCall(IToken objectToken, IToken propToken)
        {

        }

        public void EmitAsmCode(List<CommonTree> tree)
        {
            foreach (var item in tree)
            {
                function.Add(item.Text.Substring(1, item.Text.Length - 2));
            }
        }
    }
}