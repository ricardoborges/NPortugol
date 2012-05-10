grammar NPortugol;

options {
output=AST;
language=CSharp3;
k=2; 
backtrack=true;
}

tokens{
	FUNC;
	PARAM;
	VAR;
	CALL;
	ASGN;
	ARG;
	EXP;
	LEXP;
	RET;
	SLIST;
	JMP;
	SJMP;
	LOOP;
	INIT;
	DEC;
	AR;
	INDEX;
	ILIST;
	MCALL;
	PCALL;
	ASM;
}

@namespace{NPortugol}

@header{using System.Collections;}

@members{

	Stack<string> paraphrases = new Stack<string>();
	
        public string GetErrorMessage(RecognitionException e)
        {
            string msg = "Erro na linha {0} posição {1}: '{2}'";
	    msg = string.Format(msg, e.Line, e.CharPositionInLine, e.Token.Text);
            if (paraphrases.Count > 0){
                string paraphrase = (string)paraphrases.Pop();
                msg = msg + " " + paraphrase;
            }
            return msg;
        }
	protected void mismatch(IIntStream input, int ttype, BitSet follow)	{
		throw new MismatchedTokenException(ttype, input);
	}
	public void recoverFromMismatchedSet(IIntStream input,RecognitionException e, BitSet follow) {
		throw e;
	}

	public List<string> Functions = new List<string>();
	public List<string> Symbols = new List<string>();	
	public bool IsDefinedID(string id){    return Symbols.Contains(id);}
	public bool IsDefined(string name) { return Functions.Contains(name); }
	public void DefineID(IList<IToken> ids){ foreach(var id in ids) { Symbols.Add(id.Text); }}
	public void DefineFunction(string name){ Functions.Add(name);}	
}

@rulecatch {
	catch (RecognitionException e) {
		throw e;
	}
}

public script	: declare_function* ;

declare_function
	:	'funcao' i=ID '(' function_param_list* ')' statement* 'fim'
		{DefineFunction($i.text);}
		-> ^(FUNC ID function_param_list* ^(SLIST statement*))
	;
	
statement	@init { paraphrases.Push("na sentença"); } @after { paraphrases.Pop(); }

	: declare_local 
	| if_stat 
	| for_stat
	| while_stat
	| repeat_stat	
	| function_call 
	| assign_var 
	| return_stat
	| asm_code
	;
	
function_param_list     @init { paraphrases.Push("na lista de parâmetros"); }   @after { paraphrases.Pop(); }
	:	ID (',' ID)* -> ^(PARAM ID*)
	;	
/*
declare_local            @init { paraphrases.Push("na definição de variável"); }   @after { paraphrases.Pop(); }
	:	'variavel' i+= ID (initialize_local)? (','i+= ID (initialize_local)?)* 
		{DefineID($i);}
		-> ^(VAR ID initialize_local?)+
	;
	*/

declare_local            @init { paraphrases.Push("na definição de variável"); }   @after { paraphrases.Pop(); }
	:	'variavel' i+=ID (',' i+=ID)* 
			{DefineID($i);}
	-> ^(VAR ID*)
	;

/*
initialize_local
	:	'=' plus_expression -> ^(INIT plus_expression)
	;	*/
	
if_stat @init { paraphrases.Push("se"); }   @after { paraphrases.Pop(); }

	: 'se' p=logic_expression 'entao' s1+=statement*
	( s2=senao_stat -> ^(SJMP ^(LEXP $p) ^(SLIST $s1 $s2))
	| 'fim' -> ^(JMP ^(LEXP $p) ^(SLIST $s1*))
	)
	;
	
senao_stat
	:	'senao' s2+=statement* 'fim' ->	^(SLIST statement*)
	;	
	
for_stat:	'para' assign_var 'ate' index 

		( 'dec' statement* 'fim' -> ^(LOOP DEC assign_var index ^(SLIST statement*))
		| statement* 'fim' -> ^(LOOP assign_var index ^(SLIST statement*))
		)
	;
	
while_stat
	:	'enquanto' logic_expression statement* 'fim'
		-> ^(LOOP ^(LEXP logic_expression) ^(SLIST statement*))
	;
	
repeat_stat	:	'repita' statement* 'ate' logic_expression
		-> ^(LOOP ^(SLIST statement*) ^(LEXP logic_expression))
	;

function_call     @init { paraphrases.Push("na chamada de função"); }   @after { paraphrases.Pop(); }
	:	ID '(' function_arg_list* ')' -> ^(CALL ID function_arg_list*)
	;
	
property_call     @init { paraphrases.Push("na chamada de propriedade"); }   @after { paraphrases.Pop(); }
	:	o=ID'.'p=ID  -> ^(PCALL $o $p)
	;	
	
method_call     @init { paraphrases.Push("na chamada de propriedade"); }   @after { paraphrases.Pop(); }
	:	o=ID'.'p=ID '(' function_arg_list* ')' -> ^(MCALL $o $p function_arg_list*)
	;		

function_arg_list    
	@init { paraphrases.Push("nos argumentos da função"); }   @after { paraphrases.Pop(); }
	:	plus_expression (',' plus_expression)* -> ^(ARG plus_expression*)
	;	
		
assign_var	@init { paraphrases.Push("na atribuição de variável"); }   @after { paraphrases.Pop(); }
	:       
	       ID '=' '[' INT '..' INT ']' -> ^(ASGN ID INT INT)
	|       ID '=' '{' INT (',' INT)* '}' -> ^(ASGN ID ^(ILIST INT*))
	|	ID '[' index ']' '=' assign_expression  -> ^(ASGN ^(AR index) ID assign_expression)
        |	ID '=' assign_expression  -> ^(ASGN ID assign_expression)
	;	

return_stat
	: 'retorne' assign_expression  -> ^(RET assign_expression)
	;


asm_code:	'#' STRING* '#' -> ^(ASM STRING*)
        ;

// ##########################################################################################################################
// Expressions


logic_expression
	:	(plus_expression) (binop^ plus_expression)*
	;	

binop	: '<' | '>' | '<=' | '>=' | '==' | '!=' | 'e' | 'ou' ;

//###########################################################################################################################
assign_expression
	options{k=3;}
	:	 plus_expression;

plus_expression
	: (mul_expression) ('+'^ mul_expression | '-'^ mul_expression)* 
	;

mul_expression
	: (primary_ar_expression) ( '/'^ primary_ar_expression | '%'^ primary_ar_expression | '*'^ primary_ar_expression)*
	;

primary_ar_expression 
	: { (!IsDefinedID(input.LT(1).Text)) && input.LT(2).Text =="(" }? => function_call
	| ID
	| ID '[' index ']' -> ^(INDEX index) ID
	| method_call	
	| property_call
	| constant
	| parenthesisExpression	
	;    
	
parenthesisExpression: '(' plus_expression ')' -> plus_expression
	;
	
constant: INT | FLOAT | STRING;	

atom	: constant | ID;

index	: INT | ID;

number	: INT | FLOAT;

               
// ##########################################################################################################################    
// Lexer    
    
ID : ('a'..'z'|'A'..'Z'|'_') ('a'..'z'|'A'..'Z'|'0'..'9'|'_')* ;

INT : ('0'..'9')+ ;

FLOAT :INT '.' INT ;

NEWLINE	: '\r'?'\n'  { $channel = Hidden; };

WS : ( ' ' | '\t' | '\r' | '\n' )+ { $channel = Hidden; } ; 

STRING
    : '"' ( ESC_SEQ | ~('\\'|'"') )* '"'
    ;

CHAR: '\'' ( ESC_SEQ | ~('\''|'\\') ) '\''
    ;

fragment
EXPONENT : ('e'|'E') ('+'|'-')? ('0'..'9')+ ;

fragment
HEX_DIGIT : ('0'..'9'|'a'..'f'|'A'..'F') ;

fragment
ESC_SEQ
    : '\\' ('b'|'t'|'n'|'f'|'r'|'\"'|'\''|'\\')
    | UNICODE_ESC
    | OCTAL_ESC
    ;

fragment
OCTAL_ESC
    : '\\' ('0'..'3') ('0'..'7') ('0'..'7')
    | '\\' ('0'..'7') ('0'..'7')
    | '\\' ('0'..'7')
    ;

fragment
UNICODE_ESC
    : '\\' 'u' HEX_DIGIT HEX_DIGIT HEX_DIGIT HEX_DIGIT
    ;
