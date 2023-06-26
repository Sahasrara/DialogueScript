//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from ./grammar/DialogueScriptParser.g4 by ANTLR 4.13.0

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="DialogueScriptParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.0")]
[System.CLSCompliant(false)]
public interface IDialogueScriptParserListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.script"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterScript([NotNull] DialogueScriptParser.ScriptContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.script"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitScript([NotNull] DialogueScriptParser.ScriptContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.scheduled_block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterScheduled_block([NotNull] DialogueScriptParser.Scheduled_blockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.scheduled_block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitScheduled_block([NotNull] DialogueScriptParser.Scheduled_blockContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.scheduled_block_open"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterScheduled_block_open([NotNull] DialogueScriptParser.Scheduled_block_openContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.scheduled_block_open"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitScheduled_block_open([NotNull] DialogueScriptParser.Scheduled_block_openContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.scheduled_block_close"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterScheduled_block_close([NotNull] DialogueScriptParser.Scheduled_block_closeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.scheduled_block_close"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitScheduled_block_close([NotNull] DialogueScriptParser.Scheduled_block_closeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlock([NotNull] DialogueScriptParser.BlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlock([NotNull] DialogueScriptParser.BlockContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement([NotNull] DialogueScriptParser.StatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement([NotNull] DialogueScriptParser.StatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.compound_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCompound_statement([NotNull] DialogueScriptParser.Compound_statementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.compound_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCompound_statement([NotNull] DialogueScriptParser.Compound_statementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.statement_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement_list([NotNull] DialogueScriptParser.Statement_listContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.statement_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement_list([NotNull] DialogueScriptParser.Statement_listContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.expression_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_statement([NotNull] DialogueScriptParser.Expression_statementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.expression_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_statement([NotNull] DialogueScriptParser.Expression_statementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.if_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIf_statement([NotNull] DialogueScriptParser.If_statementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.if_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIf_statement([NotNull] DialogueScriptParser.If_statementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.switch_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSwitch_statement([NotNull] DialogueScriptParser.Switch_statementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.switch_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSwitch_statement([NotNull] DialogueScriptParser.Switch_statementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.switch_block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSwitch_block([NotNull] DialogueScriptParser.Switch_blockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.switch_block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSwitch_block([NotNull] DialogueScriptParser.Switch_blockContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.switch_label"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSwitch_label([NotNull] DialogueScriptParser.Switch_labelContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.switch_label"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSwitch_label([NotNull] DialogueScriptParser.Switch_labelContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.declaration_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDeclaration_statement([NotNull] DialogueScriptParser.Declaration_statementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.declaration_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDeclaration_statement([NotNull] DialogueScriptParser.Declaration_statementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.declarator_init"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDeclarator_init([NotNull] DialogueScriptParser.Declarator_initContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.declarator_init"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDeclarator_init([NotNull] DialogueScriptParser.Declarator_initContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.declarator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDeclarator([NotNull] DialogueScriptParser.DeclaratorContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.declarator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDeclarator([NotNull] DialogueScriptParser.DeclaratorContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.break_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBreak_statement([NotNull] DialogueScriptParser.Break_statementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.break_statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBreak_statement([NotNull] DialogueScriptParser.Break_statementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.expression_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_list([NotNull] DialogueScriptParser.Expression_listContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.expression_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_list([NotNull] DialogueScriptParser.Expression_listContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_bitwise_or</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_bitwise_or([NotNull] DialogueScriptParser.Expression_bitwise_orContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_bitwise_or</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_bitwise_or([NotNull] DialogueScriptParser.Expression_bitwise_orContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_concat</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_concat([NotNull] DialogueScriptParser.Expression_concatContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_concat</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_concat([NotNull] DialogueScriptParser.Expression_concatContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_bitwise_and</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_bitwise_and([NotNull] DialogueScriptParser.Expression_bitwise_andContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_bitwise_and</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_bitwise_and([NotNull] DialogueScriptParser.Expression_bitwise_andContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_relational_gt</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_relational_gt([NotNull] DialogueScriptParser.Expression_relational_gtContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_relational_gt</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_relational_gt([NotNull] DialogueScriptParser.Expression_relational_gtContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_postfix_invoke</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_postfix_invoke([NotNull] DialogueScriptParser.Expression_postfix_invokeContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_postfix_invoke</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_postfix_invoke([NotNull] DialogueScriptParser.Expression_postfix_invokeContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_bitwise_xor</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_bitwise_xor([NotNull] DialogueScriptParser.Expression_bitwise_xorContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_bitwise_xor</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_bitwise_xor([NotNull] DialogueScriptParser.Expression_bitwise_xorContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_postfix_inc_dec</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_postfix_inc_dec([NotNull] DialogueScriptParser.Expression_postfix_inc_decContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_postfix_inc_dec</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_postfix_inc_dec([NotNull] DialogueScriptParser.Expression_postfix_inc_decContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_primary_parenthetical</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_primary_parenthetical([NotNull] DialogueScriptParser.Expression_primary_parentheticalContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_primary_parenthetical</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_primary_parenthetical([NotNull] DialogueScriptParser.Expression_primary_parentheticalContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_additive_add</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_additive_add([NotNull] DialogueScriptParser.Expression_additive_addContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_additive_add</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_additive_add([NotNull] DialogueScriptParser.Expression_additive_addContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_shift_right</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_shift_right([NotNull] DialogueScriptParser.Expression_shift_rightContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_shift_right</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_shift_right([NotNull] DialogueScriptParser.Expression_shift_rightContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_ternary</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_ternary([NotNull] DialogueScriptParser.Expression_ternaryContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_ternary</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_ternary([NotNull] DialogueScriptParser.Expression_ternaryContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_equality_not_eq</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_equality_not_eq([NotNull] DialogueScriptParser.Expression_equality_not_eqContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_equality_not_eq</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_equality_not_eq([NotNull] DialogueScriptParser.Expression_equality_not_eqContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_logical_or</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_logical_or([NotNull] DialogueScriptParser.Expression_logical_orContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_logical_or</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_logical_or([NotNull] DialogueScriptParser.Expression_logical_orContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_primary_literal</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_primary_literal([NotNull] DialogueScriptParser.Expression_primary_literalContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_primary_literal</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_primary_literal([NotNull] DialogueScriptParser.Expression_primary_literalContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_relational_ge</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_relational_ge([NotNull] DialogueScriptParser.Expression_relational_geContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_relational_ge</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_relational_ge([NotNull] DialogueScriptParser.Expression_relational_geContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_postfix_invoke_async</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_postfix_invoke_async([NotNull] DialogueScriptParser.Expression_postfix_invoke_asyncContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_postfix_invoke_async</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_postfix_invoke_async([NotNull] DialogueScriptParser.Expression_postfix_invoke_asyncContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_shift_left</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_shift_left([NotNull] DialogueScriptParser.Expression_shift_leftContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_shift_left</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_shift_left([NotNull] DialogueScriptParser.Expression_shift_leftContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_equality_eq</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_equality_eq([NotNull] DialogueScriptParser.Expression_equality_eqContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_equality_eq</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_equality_eq([NotNull] DialogueScriptParser.Expression_equality_eqContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_primary_name</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_primary_name([NotNull] DialogueScriptParser.Expression_primary_nameContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_primary_name</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_primary_name([NotNull] DialogueScriptParser.Expression_primary_nameContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_assignment</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_assignment([NotNull] DialogueScriptParser.Expression_assignmentContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_assignment</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_assignment([NotNull] DialogueScriptParser.Expression_assignmentContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_relational_lt</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_relational_lt([NotNull] DialogueScriptParser.Expression_relational_ltContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_relational_lt</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_relational_lt([NotNull] DialogueScriptParser.Expression_relational_ltContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_unary</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_unary([NotNull] DialogueScriptParser.Expression_unaryContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_unary</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_unary([NotNull] DialogueScriptParser.Expression_unaryContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_additive_sub</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_additive_sub([NotNull] DialogueScriptParser.Expression_additive_subContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_additive_sub</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_additive_sub([NotNull] DialogueScriptParser.Expression_additive_subContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_multiplicative_div</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_multiplicative_div([NotNull] DialogueScriptParser.Expression_multiplicative_divContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_multiplicative_div</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_multiplicative_div([NotNull] DialogueScriptParser.Expression_multiplicative_divContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_postfix_array_access</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_postfix_array_access([NotNull] DialogueScriptParser.Expression_postfix_array_accessContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_postfix_array_access</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_postfix_array_access([NotNull] DialogueScriptParser.Expression_postfix_array_accessContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_logical_and</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_logical_and([NotNull] DialogueScriptParser.Expression_logical_andContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_logical_and</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_logical_and([NotNull] DialogueScriptParser.Expression_logical_andContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_multiplicative_mul</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_multiplicative_mul([NotNull] DialogueScriptParser.Expression_multiplicative_mulContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_multiplicative_mul</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_multiplicative_mul([NotNull] DialogueScriptParser.Expression_multiplicative_mulContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_multiplicative_mod</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_multiplicative_mod([NotNull] DialogueScriptParser.Expression_multiplicative_modContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_multiplicative_mod</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_multiplicative_mod([NotNull] DialogueScriptParser.Expression_multiplicative_modContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expression_relational_le</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression_relational_le([NotNull] DialogueScriptParser.Expression_relational_leContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expression_relational_le</c>
	/// labeled alternative in <see cref="DialogueScriptParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression_relational_le([NotNull] DialogueScriptParser.Expression_relational_leContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.assignment_operator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssignment_operator([NotNull] DialogueScriptParser.Assignment_operatorContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.assignment_operator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssignment_operator([NotNull] DialogueScriptParser.Assignment_operatorContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterType([NotNull] DialogueScriptParser.TypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitType([NotNull] DialogueScriptParser.TypeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.primitive_type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrimitive_type([NotNull] DialogueScriptParser.Primitive_typeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.primitive_type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrimitive_type([NotNull] DialogueScriptParser.Primitive_typeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterName([NotNull] DialogueScriptParser.NameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitName([NotNull] DialogueScriptParser.NameContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.namespace"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNamespace([NotNull] DialogueScriptParser.NamespaceContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.namespace"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNamespace([NotNull] DialogueScriptParser.NamespaceContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.flag_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFlag_list([NotNull] DialogueScriptParser.Flag_listContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.flag_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFlag_list([NotNull] DialogueScriptParser.Flag_listContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="DialogueScriptParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteral([NotNull] DialogueScriptParser.LiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DialogueScriptParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteral([NotNull] DialogueScriptParser.LiteralContext context);
}
