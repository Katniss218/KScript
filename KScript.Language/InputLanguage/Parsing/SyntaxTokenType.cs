
namespace KScript.Language.Parsing
{
    public enum SyntaxTokenType
    {
        #region Primitive Tokens (single-iteration only)

        /// <summary>
        /// '['
        /// </summary>
        OpenSquareBracket,

        /// <summary>
        /// ']'
        /// </summary>
        CloseSquareBracket,

        /// <summary>
        /// '{'
        /// </summary>
        OpenCurlyBracket,

        /// <summary>
        ///  '}'
        /// </summary>
        CloseCurlyBracket,

        /// <summary>
        /// '('
        /// </summary>
        OpenParenthesis,

        /// <summary>
        /// ')'
        /// </summary>
        CloseParenthesis,

        /// <summary>
        /// ' ', '\t'
        /// </summary>
        WhiteSpace,

        /// <summary>
        /// '\n' or '\r\n'
        /// </summary>
        NewLine,

        /// <summary>
        /// '.'
        /// </summary>
        Dot,

        /// <summary>
        /// '+'
        /// </summary>
        Plus,

        /// <summary>
        /// '-'
        /// </summary>
        Minus,

        /// <summary>
        /// '*'
        /// </summary>
        Asterisk,

        /// <summary>
        /// '/'
        /// </summary>
        Slash,

        /// <summary>
        /// '%'
        /// </summary>
        Percent,

        /// <summary>
        /// ';'
        /// </summary>
        Semicolon,

        /// <summary>
        /// '//'
        /// </summary>
        DoubleSlash,

        /// <summary>
        /// '/*'
        /// </summary>
        SlashAsterisk,

        /// <summary>
        /// '*/'
        /// </summary>
        AsteriskSlash,

        /// <summary>
        /// [0-9]
        /// </summary>
        Digit,

        #endregion

        #region Hybrid Tokens (exist in single-iteration, can be combined with each other)

        /// <summary>
        /// Any token that is not defined otherwise.
        /// </summary>
        /// <remarks>
        /// Text
        ///     : Text*
        /// </remarks>
        Text,

        #endregion

        #region Compound Tokens (created by combining multiple primitive tokens)

        /// <summary>
        /// Integer
        ///     : Digit*
        /// </summary>
        Integer,

        #endregion

        /// <summary>
        /// technical use, invalid token
        /// </summary>
        NULL,
    }
}