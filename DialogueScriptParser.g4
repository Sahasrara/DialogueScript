parser grammar DialogueScriptParser
    ;

options {
    tokenVocab = DialogueScriptLexer;
}

// Entry Point
script: scheduled_block* EOF;

// Scheduled Blocks
scheduled_block
    : scheduled_block_open block scheduled_block_close
    ;
scheduled_block_open: LT flag_list? LT;
scheduled_block_close: GT flag_list? GT;
block: statement*;

// Statements 
statement
    : if_statement
    | switch_statement
    | compound_statement
    | expression_statement
    | declaration_statement
    | break_statement
    ;

// Compound Statement
compound_statement: LBRACE statement_list? RBRACE;
statement_list: statement+;

// Expression Statement
expression_statement: expression SEMI;

// If Statement
if_statement
    : IF LPAREN expression RPAREN statement (ELSE statement)?
    ;

// Switch Statement
switch_statement: SWITCH LPAREN expression RPAREN switch_block;
switch_block: LBRACE switch_label* RBRACE;
switch_label
    : CASE expression COLON statement*
    | DEFAULT COLON statement*
    ;

// Declaration Statement
declaration_statement: type declarator_init SEMI;
declarator_init: declarator (ASSIGN expression)?;
declarator: IDENTIFIER;

// Break Statement
break_statement: BREAK SEMI;

// Expression
expression_list: expression (COMMA expression)*;
// Primary
expression
    : name                                      # expression_primary_name
    | literal                                   # expression_primary_literal
    | LPAREN expression RPAREN                  # expression_primary_parenthetical
    | expression (INC | DEC)                    # expression_postfix_inc_dec
    | expression LBRACK expression RBRACK       # expression_postfix_array_access
    | expression LPAREN expression_list? RPAREN # expression_postfix_invoke
    | expression LBRACE expression_list? RBRACE # expression_postfix_invoke_async
    // Unary 
    | <assoc = right> (
        SUB
        | ADD
        | NOT
        | BIT_NOT
        | DEC
        | INC
        | LPAREN type RPAREN
    ) expression # expression_unary
    // Concat
    | expression CONCAT expression # expression_concat
    // Multiplicative
    | expression MUL expression # expression_multiplicative_mul
    | expression DIV expression # expression_multiplicative_div
    | expression MOD expression # expression_multiplicative_mod
    // Additive	
    | expression ADD expression     # expression_additive_add
    | expression SUB expression     # expression_additive_sub
    | expression '<' '<' expression # expression_shift_left
    | expression '>' '>' expression # expression_shift_right
    // Relational 
    | expression LT expression # expression_relational_lt
    | expression GT expression # expression_relational_gt
    | expression LE expression # expression_relational_le
    | expression GE expression # expression_relational_ge
    // Equality
    | expression EQUAL expression    # expression_equality_eq
    | expression NOTEQUAL expression # expression_equality_not_eq
    // Bitwise And
    | expression BIT_AND expression # expression_bitwise_and
    // Bitwise Or
    | expression BIT_OR expression # expression_bitwise_or
    // Bitwise Xor 
    | expression BIT_XOR expression # expression_bitwise_xor
    // Logical And
    | expression AND expression # expression_logical_and
    // Logical Or
    | expression OR expression # expression_logical_or
    // Ternary 
    | <assoc = right> expression TERNARY expression COLON expression # expression_ternary
    // Assignment
    | expression assignment_operator expression # expression_assignment
    ;

// Operators
assignment_operator
    : ASSIGN
    | ADD_ASSIGN
    | SUB_ASSIGN
    | MUL_ASSIGN
    | DIV_ASSIGN
    | AND_ASSIGN
    | OR_ASSIGN
    | XOR_ASSIGN
    | MOD_ASSIGN
    | LSHIFT_ASSIGN
    | RSHIFT_ASSIGN
    ;

// Types
type
    : primitive_type
    | type LBRACK RBRACK
    | name ('<' type (COMMA type)* '>')?
    ;

primitive_type
    : TYPE_BOOLEAN
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
    | TYPE_STRING
    ;

// Name
name: namespace? IDENTIFIER (DOT IDENTIFIER)*;

// Namespace
namespace: IDENTIFIER COLONCOLON (namespace)*;

// Flags 
flag_list: IDENTIFIER (COMMA IDENTIFIER)*;

// Literals
literal
    : INTEGER_LITERAL
    | FLOATING_POINT_LITERAL
    | BOOLEAN_LITERAL
    | CHARACTER_LITERAL
    | STRING_LITERAL
    | NULL_LITERAL
    ;

/* 
// This is a more structured version of expression parsing, and I'm not sure if 
// I'll need it yet, but I don't want to write it again if I do.

expression: expression_assignment;
expression_assignment
    : expression_ternary
    | expression_unary assignment_operator expression
    ;
expression_ternary
    : expression_or
    | <assoc = right> expression_or TERNARY expression COLON expression_ternary
    ;
expression_or: expression_and | expression_or OR expression_or;
expression_and
    : expression_bit_or
    | expression_and AND expression_and
    ;
expression_bit_or
    : expression_bit_xor
    | expression_bit_or BIT_OR expression_bit_or
    ;
expression_bit_xor
    : expression_bit_and
    | expression_bit_xor BIT_XOR expression_bit_xor
    ;
expression_bit_and
    : expression_equality
    | expression_bit_and BIT_AND expression_bit_and
    ;
expression_equality
    : expression_relational
    | expression_equality EQUAL expression_equality
    | expression_equality NOTEQUAL expression_equality
    ;
expression_relational
    : expression_shift
    | expression_relational LT expression_relational
    | expression_relational GT expression_relational
    | expression_relational LE expression_relational
    | expression_relational GE expression_relational
    ;
expression_shift
    : expression_string_concat
    | expression_shift '<' '<' expression_shift
    | expression_shift '>' '>' expression_shift
    ;
expression_string_concat
    : expression_additive
    | expression_string_concat CONCAT expression_string_concat
    ;
expression_additive
    : expression_multiplicative
    | expression_additive ADD expression_additive
    | expression_additive SUB expression_additive
    ;
expression_multiplicative
    : expression_unary
    | expression_multiplicative MUL expression_multiplicative
    | expression_multiplicative DIV expression_multiplicative
    | expression_multiplicative MOD expression_multiplicative
    ;
expression_unary
    : <assoc = right> (
        SUB
        | ADD
        | NOT
        | BIT_NOT
        | DEC
        | INC
        | LPAREN type RPAREN
    )* expression_postfix
    ;
expression_postfix
    : expression_primary (
        (INC | DEC)
        | LBRACK expression RBRACK
        | LPAREN expression_list? RPAREN
        | LBRACE expression_list? RBRACE
    )*
    ;
expression_primary: literal | name | LPAREN expression RPAREN;
*/