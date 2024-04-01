using System.Collections.Immutable;
using BankOCR.Contracts;
using BankOCR.Contracts.models;

namespace BankOCR.Implementations;

public class NumberParser: INumberParser
{
    public AccountNumber ParseNumber(IReadOnlyList<string> lines, int startLine)
    {
        var digits = new List<Digit>();
        for (var col = 0; col < 27; col += 3)
        {
            var digit = GetDigit(lines[startLine].AsSpan(col, 3), 
                lines[startLine + 1].AsSpan(col, 3), 
                lines[startLine + 2].AsSpan(col, 3));
            digits.Add(digit);
        }

        var accountDigits = digits.ToImmutableArray();
        return new AccountNumber(accountDigits);
    }
    
    private Digit GetDigit(ReadOnlySpan<char> topChars, ReadOnlySpan<char> middleChars, ReadOnlySpan<char> bottomChars)
    {
        Digit GetDigitLine(ReadOnlySpan<char> line, int index, Digit flag)
        {
            return line[index] == ' ' ? Digit.None : flag;
        }
        return GetDigitLine(topChars, 1, Digit.TopBar) |
               GetDigitLine(middleChars, 0, Digit.MiddleLeftPipe) |
               GetDigitLine(middleChars, 1, Digit.MiddleBar) |
               GetDigitLine(middleChars, 2, Digit.MiddleRightPipe) |
               GetDigitLine(bottomChars, 0, Digit.BottomLeftPipe) |
               GetDigitLine(bottomChars, 1, Digit.BottomBar) |
               GetDigitLine(bottomChars, 2, Digit.BottomRightPipe);
    }
}