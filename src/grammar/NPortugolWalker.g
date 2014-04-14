tree grammar NPortugolWalker;

options{
tokenVocab=NPortugol;
language=CSharp3;
ASTLabelType=CommonTree;
backtrack=true;
k=3;
}

@namespace{NPortugol}

@header{using System.Collections;}

@members{
	Emissor emitter = new Emissor();
	
	bool inExpression;
	
	bool invertExp = true;
	
	public Dictionary<int, int> SourceMap { get { return emitter.SourceMap; } }
	
	public bool DebugInfo {get {return emitter.DebugInfo;} set{emitter.DebugInfo = value;} }
}

public script returns[IList<string> scriptLines] : declare_function*
	{ return emitter.ScriptLines;}
;

declare_function : ^(FUNC ID function_param_list* ^(SLIST statement*))
	{emitter.EmitFunction($ID.Token);}
;
			
statement: declare_local
	| if_stat 
	| for_stat
	| while_stat
	| repeat_stat
	| function_call 
	| assign_var
	| return_stat
	| asm_code
	;
	
	
function_param_list    
	:	 ^(PARAM p+=ID*) { foreach(var item in $p) emitter.AddParam(item.Text); }
	;	

declare_local 
	:  ^(VAR i+=ID*)  { foreach(var item in $i) emitter.EmitVar(item.Token); }
	;

if_stat
	:  ^(SJMP ^(LEXP logic_expression) ^(SLIST statement* {emitter.EmitIf(true);} senao_stat))
	|  ^(JMP ^(LEXP logic_expression) ^(SLIST statement*))
	{emitter.EmitIf(false);}
	;
	
senao_stat
	: ^(SLIST statement*)
	   {emitter.EmitElse();}
	;		
	
	
for_stat
: ^(LOOP a=assign_var { emitter.SetForInc(a); } i=INT ^(SLIST  {emitter.EmitInitFor($i.Token, true);} statement* ))	 {emitter.EmitEndFor($i.Token, true);}
	| ^(LOOP DEC a=assign_var { emitter.SetForInc(a); } i=INT ^(SLIST {emitter.EmitInitFor($i.Token, false);} statement*))  {emitter.EmitEndFor($i.Token, false);}
	| ^(LOOP a=assign_var { emitter.SetForInc(a); } i=ID ^(SLIST  {emitter.EmitInitFor($i.Token, true);} statement* ))	 {emitter.EmitEndFor($i.Token, true);}
	| ^(LOOP DEC a=assign_var { emitter.SetForInc(a); } i=ID ^(SLIST {emitter.EmitInitFor($i.Token, false);} statement*))  {emitter.EmitEndFor($i.Token, false);}	
	;
	

while_stat
	:	 ^(LOOP ^(LEXP {emitter.EmitInitWhile();} logic_expression) ^(SLIST  statement*)) {emitter.EmitEndWhile();} 
			
	;
	
repeat_stat	
@init { invertExp = false; }
@after { invertExp = true; }
	: ^(LOOP ^(SLIST {emitter.EmitInitRepeat();} statement*) ^(LEXP logic_expression)) {emitter.EmitEndRepeat();}
	;


function_call   
	:	 ^(CALL ID function_arg_list*)
		{emitter.EmitCall($ID.Token);}
	;
	
property_call 
	:	 ^(PCALL o=ID p=ID)
		{emitter.EmitPropCall($o.Token, $p.Token);}
	;	
	
method_call     
	:	 ^(MCALL o=ID p=ID function_arg_list*)
		{emitter.EmitMethodCall($o.Token, $p.Token);}	
	;		

function_arg_list    
	
	:	^(ARG plus_expression*)
	;	
	
	
asm_code:	^(ASM s+=STRING*)
		{emitter.EmitAsmCode($s);}
        ;
	

assign_var returns[string id]
    :  
      ^(ASGN ID a=atom) {$id = $ID.text; emitter.EmitAssign($ID.Token, $a.value);}
    | ^(ASGN ^(AR INT) ID plus_expression) {emitter.EmitPop($ID.Token, int.Parse($INT.text));}      
    | ^(ASGN ^(AR i=ID) p=ID plus_expression) {emitter.EmitPop($p.Token, $i.text);}          
    | ^(ASGN ID plus_expression) {emitter.EmitPop($ID.Token);} 
    | ^(ASGN ID 'nulo') {emitter.EmitAssign($ID.Token, null);} 
    | ^(ASGN ID 'falso') {emitter.EmitAssign($ID.Token, false);} 
    | ^(ASGN ID 'verdadeiro') {emitter.EmitAssign($ID.Token, true);}         
    | ^(ASGN ID l=INT r=INT) {emitter.EmitAssign($ID.Token, int.Parse($l.text), int.Parse($r.text));}
    | ^(ASGN ID ^(ILIST i=INT*)) {emitter.EmitAssignArray($ID.Token, $i);}
    ;		

return_stat
	:  ^(RET plus_expression){emitter.EmitRet($RET.Token);}
	;

// ##########################################################################################################################
// Expressions
plus_expression
@init { inExpression = true; }
@after { inExpression = false; }
: ^('+' plus_expression plus_expression) {emitter.EmitStackAdd();}
| ^('-' plus_expression plus_expression) {emitter.EmitStackSub();}
| ^('*' plus_expression plus_expression) {emitter.EmitStackPlus();}
| ^('/' plus_expression plus_expression) {emitter.EmitStackDiv();}
| ^('%' plus_expression plus_expression) {emitter.EmitStackMod();}
| ^(INDEX INT) ID {emitter.EmitPush($ID.text, int.Parse($INT.text));}
| ^(INDEX i2=ID) i1=ID {emitter.EmitPush($i1.text, $i2.text);}
| function_call
| method_call
| property_call
| atom
;

logic_expression
	:	  ^('<' plus_expression plus_expression) {emitter.EmitLessExp(invertExp);}
	|	  ^('>' plus_expression plus_expression) {emitter.EmitGreaterExp(invertExp);}
	|	  ^('<=' plus_expression plus_expression) {emitter.EmitLessEqExp(invertExp);}
	|	  ^('>=' plus_expression plus_expression) {emitter.EmitGreaterEqExp(invertExp);}
	|	  ^('==' plus_expression plus_expression) {emitter.EmitEqualsExp(invertExp);}
	|	  ^('!=' plus_expression plus_expression) {emitter.EmitNotEqExp(invertExp);}
	|	  ^('e' plus_expression plus_expression)
	|	  ^('ou' plus_expression plus_expression)						
	| plus_expression
	;	

//###########################################################################################################################
    
atom returns[object value]: 
      a=ID {$value = $a.text; emitter.EmitPush($value, $a.Token);}
    | a=INT{$value = int.Parse($a.text);  emitter.EmitPush($value, $a.Token);}
    | a=FLOAT{$value = float.Parse($a.text);  emitter.EmitPush($value, $a.Token);}
    | a=STRING{$value = $a.text;  emitter.EmitPush($value, $a.Token);}
    | a=T{$value = $a.text;  emitter.EmitPush(true, $a.Token);}    
    | a=F{$value = $a.text;  emitter.EmitPush(false, $a.Token);}    
    ;  