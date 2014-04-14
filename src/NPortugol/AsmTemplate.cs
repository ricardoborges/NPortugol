using System;

namespace NPortugol
{
    public class AsmTemplate
    {

        private static string _labelName = "label_{0}";
        private static string _label = ".{0}";
        private static string _tmpVar = "tmpvar_{0}";
        private static string _function = "{0}:";
        private static string _return = "RET";
        private static string _push = "PUSH {0}";
        private static string _pushIndex = "PUSH {0}:{1}";
        private static string _pop = "POP {0}";
        private static string _popIndex = "POP {0}:{1}";
        //private static string _popVarIndex = "POP {0}:{1}";
        private static string _var = "DCL {0}";
        private static string _inc = "INC {0}";
        private static string _dec = "DEC {0}";
        private static string _assign = "MOV {0}, {1}";
        private static string _assignIndex = "MOV {0}:{1}, {2}";

        private static string _stackAdd = "SADD";
        private static string _stackSub = "SSUB";
        private static string _stackPlus = "SMUL";
        private static string _stackDiv = "SDIV";
        private static string _stackMod = "SMOD";


        private static string _jumpNotEquals = "JNE {0}";
        private static string _jumpLessEquals = "JLE {0}";
        private static string _jumpGreater = "JG {0}";
        private static string _jumpGreaterEquals = "JGE {0}";
        private static string _jumpEquals = "JE {0}";
        private static string _jumpLess = "JL {0}";
        private static string _jump = "JMP {0}";

        private static string _call = "CALL {0}";
        private static string _phost = "HOST {0}, {1}";

        public static string Function(string name)
        {
            return string.Format(_function, name);
        }

        public static string Push(object value)
        {
            return string.Format(_push, value);
        }

        public static string Pop(object value)
        {
            return string.Format(_pop, value);
        }

        public static string Assign(object target, object value)
        {
            return string.Format(_assign, target, value);
        }

        public static string StackAdd()
        {
            return _stackAdd;
        }

        public static string StackSub()
        {
            return _stackSub;
        }

        public static string StackPlus()
        {
            return _stackPlus;
        }

        public static string StackDiv()
        {
            return _stackDiv;
        }

        public static string StackMod()
        {
            return _stackMod;
        }

        public static string Declare(object id)
        {
            return string.Format(_var, id);
        }

        public static string TempVar(object id)
        {
            return string.Format(_tmpVar, id);
        }

        public static string LabelName(object id)
        {
            return string.Format(_labelName, id);
        }

        public static string Label(string currentLabel)
        {
            return string.Format(_label, currentLabel);
        }

        public static string Jump(string slabel)
        {
            return string.Format(_jump, slabel);
        }

        public static string JumpNotEquals(string endLabel)
        {
            return string.Format(_jumpNotEquals, endLabel);
        }

        public static string JumpGreater(string label)
        {
            return string.Format(_jumpGreater, label);
        }

        public static string JumpLessEquals(string label)
        {
            return string.Format(_jumpLessEquals, label);
        }

        public static string JumpGreaterEquals(string label)
        {
            return string.Format(_jumpGreaterEquals, label);
        }

        public static string JumpEquals(string label)
        {
            return string.Format(_jumpEquals, label);
        }

        public static string JumpLess(string label)
        {
            return string.Format(_jumpLess, label);
        }

        public static string Inc(string id)
        {
            return string.Format(_inc, id);
        }

        public static string Dec(string id)
        {
            return string.Format(_dec, id);
        }

        public static string Pop(object value, int index)
        {
            return string.Format(_popIndex, value, index);
        }

        public static string Pop(object value, string id)
        {
            return string.Format(_popIndex, value, id);
        }

        public static string Push(object value, object index)
        {
            return string.Format(_pushIndex, value, index);
        }

        public static string AssignIndex(string id, int value, int index)
        {
            return string.Format(_assignIndex, id, index, value);
        }

        public static string Call(string id)
        {
            return string.Format(_call, id);
        }

        public static string PropertyCall(string id, string prop)
        {
            return string.Format(_phost, id, prop);
        }
    }
}