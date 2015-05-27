namespace ArgaamPlus.CSQuery
{
    using System;

    /// <summary>
    /// Represent a token and optionally any text associated with it.
    /// </summary>
    public struct Token : IEquatable<Token>
    {
         
        public TokenKind Kind { get; private set; }

         
        public string Text { get; private set; }

        private Token(TokenKind kind) : this(kind, null) { }

        private Token(TokenKind kind, string text)
            : this()
        {
            Kind = kind;
            Text = text;
        }

         
        public static Token Eoi()
        {
            return new Token(TokenKind.Eoi);
        }

        private static readonly Token _star = Char('*');
        private static readonly Token _dot = Char('.');
        private static readonly Token _colon = Char(':');
        private static readonly Token _comma = Char(',');
        private static readonly Token _semicolon = Char(';');
        private static readonly Token _rightParenthesis = Char(')');
        private static readonly Token _equals = Char('=');
        private static readonly Token _pipe = Char('|');
        private static readonly Token _leftBracket = Char('[');
        private static readonly Token _rightBracket = Char(']');

         
        public static Token Star()
        {
            return _star;
        }

         
        public static Token Dot()
        {
            return _dot;
        }

         
        public static Token Colon()
        {
            return _colon;
        }

         
        public static Token Comma()
        {
            return _comma;
        }

         
        public static Token Semicolon()
        {
            return _semicolon;
        }

         
        public static Token RightParenthesis()
        {
            return _rightParenthesis;
        }

         
        public static Token Equals()
        {
            return _equals;
        }

         
        public static Token NotEqual()
        {
            return new Token(TokenKind.NotEqual);
        }

         
        public static Token LeftBracket()
        {
            return _leftBracket;
        }

         
        public static Token RightBracket()
        {
            return _rightBracket;
        }

         
        public static Token Pipe()
        {
            return _pipe;
        }

         
        public static Token Plus()
        {
            return new Token(TokenKind.Plus);
        }

         
        public static Token Greater()
        {
            return new Token(TokenKind.Greater);
        }

         
        public static Token Includes()
        {
            return new Token(TokenKind.Includes);
        }

         
        public static Token RegexMatch()
        {
            return new Token(TokenKind.RegexMatch);
        }

         
        public static Token DashMatch()
        {
            return new Token(TokenKind.DashMatch);
        }

         
        public static Token PrefixMatch()
        {
            return new Token(TokenKind.PrefixMatch);
        }

         
        public static Token SuffixMatch()
        {
            return new Token(TokenKind.SuffixMatch);
        }

         
        public static Token SubstringMatch()
        {
            return new Token(TokenKind.SubstringMatch);
        }

         
        public static Token Tilde()
        {
            return new Token(TokenKind.Tilde);
        }

         
        public static Token Slash()
        {
            return new Token(TokenKind.Slash);
        }

         
        public static Token Ident(string text)
        {
            ValidateTextArgument(text);
            return new Token(TokenKind.Ident, text);
        }

         
        public static Token Integer(string text)
        {
            ValidateTextArgument(text);
            return new Token(TokenKind.Integer, text);
        }

         
        public static Token Hash(string text)
        {
            ValidateTextArgument(text);
            return new Token(TokenKind.Hash, text);
        }

         
        public static Token WhiteSpace(string space)
        {
            ValidateTextArgument(space);
            return new Token(TokenKind.WhiteSpace, space);
        }

         
        public static Token String(string text)
        {
            return new Token(TokenKind.String, text ?? string.Empty);
        }

         
        public static Token Function(string text)
        {
            ValidateTextArgument(text);
            return new Token(TokenKind.Function, text);
        }

         
        public static Token Char(char ch)
        {
            return new Token(TokenKind.Char, ch.ToString());
        }

         
        public override bool Equals(object obj)
        {
            return obj != null && obj is Token && Equals((Token)obj);
        }

         
        public override int GetHashCode()
        {
            return Text == null ? Kind.GetHashCode() : Kind.GetHashCode() ^ Text.GetHashCode();
        }

         
        public bool Equals(Token other)
        {
            return Kind == other.Kind && Text == other.Text;
        }

         
        public override string ToString()
        {
            return Text == null ? Kind.ToString() : Kind + ": " + Text;
        }
         
        public static bool operator ==(Token a, Token b)
        {
            return a.Equals(b);
        }

         
        public static bool operator !=(Token a, Token b)
        {
            return !a.Equals(b);
        }

        private static void ValidateTextArgument(string text)
        {
            if (text == null) throw new ArgumentNullException("text");
            if (text.Length == 0) throw new ArgumentException(null, "text");
        }
    }
}