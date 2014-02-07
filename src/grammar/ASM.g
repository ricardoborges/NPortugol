grammar ASM;

options{
    language=CSharp3;
    backtrack=true;
}

tokens {
    EMP='EMP' ;
    MOV='MOV' ;
    DCL='DCL' ;
    ADD='ADD' ;
    SADD='SADD' ;    
    SUB='SUB' ;
    SSUB='SSUB' ;    
    MUL='MUL' ;
    SMUL='SMUL' ;    
    DIV='DIV' ;
    SDIV='SDIV' ;    
    MOD='MOD' ;
    POW='POW' ;
    NEG='NEG' ;
    INC='INC' ;
    DEC='DEC' ;
    CNT='CNT';
    JMP='JMP' ;
    JE='JE' ;
    JNE='JNE' ;
    JG='JG' ;
    JL='JL' ;
    JGE='JGE' ;
    JLE='JLE' ;
    PUSH='PUSH' ;
    POP='POP' ;
    CALL='CALL' ;
    RET='RET' ;
    EXIT='EXIT' ;
    HOST='HOST';
    SCNT='SCNT';
}

@namespace{NPortugol.Runtime.Asm}

@members{
	public bool DebugInfo { get; set; }
        public Dictionary<string, int> _labels = new Dictionary<string, int>();
        public List<Instruction> _instructions = new List<Instruction>();
        public FunctionTable _functionTable = new FunctionTable();
        public int _index = 0;
}

public script returns[IList<Instruction> value] :	(func instructions+)*{

            foreach (Instruction inst in _instructions)
            {
                if (inst.OpCode.ToString().StartsWith("J"))
                {
                   inst.Operands[0].Value = _labels[inst.Operands[0].Value.ToString()];
                   inst.Operands[0].Type = OperandType.InstructionRef;
                }
            }
            
            $value = _instructions;
};


func	: ID ':' NEWLINE{
            var _function = new Function($ID.text, _index );
            _functionTable.Add(_function.Name, _function);
            
            if (DebugInfo)
            {
              var _instruction = new Instruction(OpCode.EMP, _index);
              _index++;
              _instructions.Add(_instruction);            
            }
};

instructions 
	:	func
	|       inst_no_op
	|       inst_one_op
	|       inst_two_op
	|       inst_n_op
	|       label;
	
label	: '.' ID NEWLINE { 
	_labels.Add($ID.text, _index);
	if (DebugInfo)
        {
            var _instruction = new Instruction(OpCode.EMP, _index);
            _index++;
            _instructions.Add(_instruction);	
        }
} ;

inst_no_op	:	e=opcode_no_op NEWLINE*{
            var _instruction = new Instruction($e.value, _index, new Operand[0]);
            _index++;
            _instructions.Add(_instruction);
};

inst_one_op	:	e=opcode_one_op o=operand NEWLINE{
            var _instruction = new Instruction($e.value, _index, new []{$o.value});
            _index++;
            _instructions.Add(_instruction);
};

inst_two_op	:	e=opcode_two_op o=operand ',' p=operand NEWLINE{
            var _instruction = new Instruction($e.value, _index, new []{$o.value, $p.value});
            _index++;
            _instructions.Add(_instruction);
};

inst_n_op	:	e=opcode_n_op o+=ID (',' o+=ID)* NEWLINE{

	    var plist = new List<Operand>();
				            
            foreach(var item in $o)
            {
            	plist.Add(new Operand(OperandType.HostObjectRef, item.Text));
            }
            
	    var _instruction = new Instruction(e, _index, plist.ToArray());
            _index++;
            _instructions.Add(_instruction);                          
};	

opcode_two_op returns [OpCode value]
	: MOV {$value = OpCode.MOV;}
	| ADD {$value = OpCode.ADD;}
	| SUB {$value = OpCode.SUB;}
	| MUL {$value = OpCode.MUL;}
	| DIV {$value = OpCode.DIV;}
	| MOD {$value = OpCode.MOD;}
	| POW {$value = OpCode.POW;}
	| CNT {$value = OpCode.CNT;}
	;			

opcode_one_op returns [OpCode value]
	: DCL {$value = OpCode.DCL;}
	| NEG {$value = OpCode.NEG;}
	| INC {$value = OpCode.INC;}
	| DEC {$value = OpCode.DEC;}
	| JMP {$value = OpCode.JMP;}
	| PUSH {$value = OpCode.PUSH;}
	| POP {$value = OpCode.POP;}
	| JE {$value = OpCode.JE;}
	| JNE {$value = OpCode.JNE;}
	| JG {$value = OpCode.JG;}
	| JL {$value = OpCode.JL;}
	| JGE {$value = OpCode.JGE;}
	| JLE {$value = OpCode.JLE;}	
	| CALL {$value = OpCode.CALL;}	
	;
	
opcode_n_op returns [OpCode value]
	: HOST {$value = OpCode.HOST;}	
	;
	
opcode_no_op returns [OpCode value]
	: EXIT {$value = OpCode.EXIT;}
	| RET {$value = OpCode.RET;}
	| SADD {$value = OpCode.SADD;}
	| SSUB {$value = OpCode.SSUB;}
	| SMUL {$value = OpCode.SMUL;}
	| SDIV {$value = OpCode.SDIV;}			
	| SCNT {$value = OpCode.SCNT;}	
	| EMP {$value = OpCode.EMP;}			
	;
	
operand	returns [Operand value]
        : ID (':' i=index)? {
        $value = $i.value==null? new Operand(OperandType.Variable, $ID.text): new Operand(OperandType.Variable, $ID.text, $i.value);
        }
	| INT {$value = new Operand(OperandType.Literal, int.Parse($INT.text));}
	| FLOAT {$value = new Operand(OperandType.Literal, float.Parse($FLOAT.text));}
	| STRING {$value = new Operand(OperandType.Literal, $STRING.text.Substring(1, $STRING.Text.Length-2));}
	;	

index returns[object value]
	: INT {$value = int.Parse($INT.text);}
	| ID  {$value = $ID.text;}
	;
	
ID  :	('a'..'z'|'A'..'Z'|'_') ('a'..'z'|'A'..'Z'|'0'..'9'|'_')*
    ;

INT :	'0'..'9'+
    ;

FLOAT
    :   ('0'..'9')+ '.' ('0'..'9')* EXPONENT?
    |   '.' ('0'..'9')+ EXPONENT?
    |   ('0'..'9')+ EXPONENT
    ;

 NEWLINE:'\r'? '\n' ;    

COMMENT
    :   '//' ~('\n'|'\r')* '\r'? '\n' {$channel=Hidden;}
    |   '/*' ( options {greedy=false;} : . )* '*/' {$channel=Hidden;}
    ;

WS  :   ( ' '
        | '\t'
        | '\r'
        | '\n'
        ) {$channel=Hidden;}
    ;
    
    
STRING
    :  '"' ( ESC_SEQ | ~('\\'|'"') )* '"'
    ;
    
fragment
EXPONENT : ('e'|'E') ('+'|'-')? ('0'..'9')+ ;

fragment
HEX_DIGIT : ('0'..'9'|'a'..'f'|'A'..'F') ;

fragment
ESC_SEQ
    :   '\\' ('b'|'t'|'n'|'f'|'r'|'\"'|'\''|'\\')
    |   UNICODE_ESC
    |   OCTAL_ESC
    ;

fragment
OCTAL_ESC
    :   '\\' ('0'..'3') ('0'..'7') ('0'..'7')
    |   '\\' ('0'..'7') ('0'..'7')
    |   '\\' ('0'..'7')
    ;

fragment
UNICODE_ESC
    :   '\\' 'u' HEX_DIGIT HEX_DIGIT HEX_DIGIT HEX_DIGIT
    ;
