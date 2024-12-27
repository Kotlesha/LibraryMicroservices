namespace Auth.BLL.Constants.Passwords;

internal class PasswordRestrictions
{
    internal const string UppercaseLetter = "[A-Z]";
    internal const string LowercaseLetter = "[a-z]";
    internal const string Digit = "[0-9]";
    internal const string SpecialCharacter = "[^a-zA-Z0-9]";
    internal const int MinimumLength = 8;
    internal const int MaximumLength = 16;
}
