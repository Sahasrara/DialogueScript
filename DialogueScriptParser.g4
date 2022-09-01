parser grammar DialogueScriptParser;

options {
	tokenVocab = DialogueScriptLexer;
}

// Entry Point
script: scheduled_block* EOF;

// Scheduled Blocks
scheduled_block:
	scheduled_block_open block scheduled_block_close;
scheduled_block_open: LT flag_list? LT;
scheduled_block_close: GT flag_list? GT;
block: statement*;

// Statements 
statement:
	if_statement
	| switch_statement
	| compound_statement
	| expression_statement
	| declaration_statement;

// Compound Statement
compound_statement: LBRACE statement_list? RBRACE;
statement_list: statement+;

// Expression Statement
expression_statement: expression SEMI;

// If Statement
if_statement:
	IF LPAREN expression RPAREN statement (ELSE statement)?;

// Switch Statement
switch_statement: SWITCH LPAREN expression LPAREN switch_block;
switch_block: LBRACE switch_label* RBRACE;
switch_label: CASE expression COLON | DEFAULT COLON;

// Declaration Statement
declaration_statement: type declarator_init SEMI;
declarator_init: declarator (ASSIGN expression)?;
declarator: IDENTIFIER;

// Expression
expression_list: expression (COMMA expression);
expression:
	name
	| literal
	| LPAREN expression RPAREN
	| expression (INC | DEC)
	| expression LBRACK expression RBRACK
	| expression LPAREN expression_list? RPAREN
	| expression LBRACE expression_list? RBRACE
	| (SUB | ADD | INC | DEC | NOT | BIT_NOT) expression
	| expression TURNARY expression COLON expression
	| expression mul_div_mod_operator expression
	| expression add_sub_operator expression
	| <assoc = right> expression concat_operator expression
	| expression relational_operator expression
	| expression and_operator expression
	| expression or_operator expression
	| expression bitwise_operator expression;

// Operators
concat_operator: CONCAT;
and_operator: AND;
or_operator: OR;
add_sub_operator: ADD | SUB;
mul_div_mod_operator: MUL | DIV | MOD;
relational_operator: GT | LT | LE | GE;
equality_operator: EQUAL | NOTEQUAL;
bitwise_operator:
	| '<' '<'
	| '>' '>'
	| BIT_AND
	| BIT_OR
	| BIT_XOR;
assignment_operator:
	ASSIGN
	| ADD_ASSIGN
	| SUB_ASSIGN
	| MUL_ASSIGN
	| DIV_ASSIGN
	| AND_ASSIGN
	| OR_ASSIGN
	| XOR_ASSIGN
	| MOD_ASSIGN
	| LSHIFT_ASSIGN
	| RSHIFT_ASSIGN;

// Types
type:
	primitive_type
	| type LBRACK RBRACK
	| name ('<' type (COMMA type)* '>')?;

primitive_type:
	TYPE_BOOLEAN
	| TYPE_CHAR
	| TYPE_FLOAT_DEFAULT
	| TYPE_FLOAT32
	| TYPE_FLOAT64
	| TYPE_INT_DEFAULT
	| TYPE_INT8
	| TYPE_INT16
	| TYPE_INT32
	| TYPE_INT64
	| TYPE_UINT_DEFAULT
	| TYPE_UINT8
	| TYPE_UINT16
	| TYPE_UINT32
	| TYPE_UINT64
	| TYPE_STRING;

// Name
name: namespace? IDENTIFIER (DOT IDENTIFIER)*;

// Namespace
namespace: IDENTIFIER COLONCOLON (namespace)*;

// Flags 
flag_list: IDENTIFIER (COMMA IDENTIFIER)*;

// Literals
literal:
	INTEGER_LITERAL
	| FLOATING_POINT_LITERAL
	| BOOLEAN_LITERAL
	| CHARACTER_LITERAL
	| STRING_LITERAL
	| NULL_LITERAL;