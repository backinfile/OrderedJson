using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedJson.Tokenize
{
    internal class Pattern
    {
        public static readonly List<(TokenType, string)> Patterns = new List<(TokenType, string)>() {
	        //("\n",        @"(\n)"),

	        (TokenType.Cmp,        @"(==)"),
            (TokenType.Cmp,        @"(!=)"),
            (TokenType.Cmp,        @"(>=)"),
            (TokenType.Cmp,        @"(<=)"),
            (TokenType.Cmp,        @"(>)"),
            (TokenType.Cmp,        @"(<)"),

            (TokenType.Int,        @"(\d+)"),
            (TokenType.Str,        @"'([^\n""]*?)'"),
            (TokenType.Str,        @"""([^\n']*?)"""),

            (TokenType.Or,      @"(\|\|)"),
            (TokenType.Or,      @"(\bor\b)"),
            (TokenType.And,     @"(&&)"),
            (TokenType.And,     @"(\band\b)"),

            //("inc",        @"(\+\+)"),
            //("dec",        @"(--)"),

            //("assign",    @"(\+=)"),
            //("assign",    @"(-=)"),
            //("assign",    @"(\/=)"),
            //("assign",    @"(\*=)"),
            //("assign",    @"(=)"),

            (TokenType.Add,       @"(\+)"),
            (TokenType.Sub,       @"(-)"),
            (TokenType.Mul,       @"(\*)"),
            (TokenType.Div,       @"(\/)"),
            (TokenType.Not,     @"(!)"),
            (TokenType.Not,     @"\b(not)\b"),
            //("print",   @"\b(print)\b"),

            (TokenType.Semicolon,        @"(;)"),
            (TokenType.Colon,        @"(:)"),
            (TokenType.Comma,        @"(,)"),
            (TokenType.Period,        @"(\.)"),
            (TokenType.LParenthes,        @"(\()"),
            (TokenType.RParenthes,        @"(\))"),
            (TokenType.LBracket,        @"(\[)"),
            (TokenType.RBracket,        @"(\])"),
            (TokenType.LBrace,        @"(\{)"),
            (TokenType.RBrace,        @"(\})"),

            //(TokenType.If,          @"(\bif\b)"),
            //(TokenType.Else,        @"(\belse\b)"),
            //(TokenType.For,         @"(\bfor\b)"),
            //(TokenType.While,       @"(\bwhile\b)"),
            //(TokenType.Break,       @"(\bbreak\b)"),
            //(TokenType.Continue,    @"(\bcontinue\b)"),
            //("return",      @"(\breturn\b)"),
            //("function",    @"(\bfunction\b)"),
            //(TokenType.True,        @"(\btrue\b)"),
            //(TokenType.False,       @"(\bfalse\b)"),

            (TokenType.Name,        @"([A-Za-z_$][\w_-]*)")

        };

        

    }
}
