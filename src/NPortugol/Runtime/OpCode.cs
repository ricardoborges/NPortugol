namespace NPortugol.Runtime
{
    /// <summary>
    /// Instructions set of virtual machine
    /// </summary>
    public enum OpCode
    {
        EMP,

        #region Memory

        MOV,
        DCL,

        #endregion

        #region Arithmetic

        ADD,
        SUB,
        MUL,
        DIV,
        MOD,
        POW,
        NEG,
        INC,
        DEC,

        SADD,
        SSUB,
        SMUL,
        SDIV,
        SMOD,
        #endregion

        //#region Bitwise

        //And,
        //Or,
        //XOr,
        //Not,
        //ShL,
        //ShR,

        //#endregion

        //#region String Processing

        CNT,
        SCNT,
        //GCh,
        //SCh,

        //#endregion

        //#region Conditional Branching

        JMP,
        JE,                  // left = right
        JNE,                 // left ! right
        JG,                  // left > right
        JL,                  // left < right
        JGE,                 
        JLE,

        //#endregion

        #region Stack

        PUSH,
        POP,

        #endregion

        #region Flow
        CALL,
        RET,
        EXIT,
        #endregion

        #region Interop

        HOST

        #endregion
    }
}