namespace BankOCR.Contracts.models;

[Flags]
public enum Digit: byte
{       
    None = 0,
    TopBar = 1 << 0, // 00000001
    MiddleLeftPipe = 1 << 1, // 00000010
    MiddleBar = 1 << 2, // 100
    MiddleRightPipe = 1 << 3, // 00001000
    BottomLeftPipe = 1 << 4, // 00010000
    BottomBar = 1 << 5, // 00100000
    BottomRightPipe = 1 << 6, // 01000000

    Zero = TopBar | MiddleLeftPipe | MiddleRightPipe | BottomLeftPipe | BottomBar | BottomRightPipe,
    One = MiddleRightPipe | BottomRightPipe,
    Two = TopBar | MiddleRightPipe | MiddleBar | BottomLeftPipe |  BottomBar,
    Three = TopBar | MiddleRightPipe | MiddleBar | BottomRightPipe | BottomBar,
    Four = MiddleLeftPipe | MiddleRightPipe | MiddleBar | BottomRightPipe,
    Five = TopBar | MiddleLeftPipe | MiddleBar | BottomRightPipe | BottomBar,
    Six = TopBar | MiddleLeftPipe | MiddleBar | BottomLeftPipe | BottomRightPipe | BottomBar,
    Seven = TopBar | MiddleRightPipe | BottomRightPipe,
    Eight = TopBar | MiddleLeftPipe | MiddleBar | MiddleRightPipe | BottomLeftPipe | BottomBar | BottomRightPipe,
    Nine = TopBar | MiddleLeftPipe | MiddleBar | MiddleRightPipe | BottomRightPipe | BottomBar
}

public static class DigitExtensions
{
    public static int? ToNumber(this Digit digit)
    {
        return digit switch
        {
            Digit.Zero => 0,
            Digit.One => 1,
            Digit.Two => 2,
            Digit.Three => 3,
            Digit.Four => 4,
            Digit.Five => 5,
            Digit.Six => 6,
            Digit.Seven => 7,
            Digit.Eight => 8,
            Digit.Nine => 9,
            _ => null
        };
    }
    
    public static char ToChar(this Digit digit)
    {
        var number = digit.ToNumber();
        return number.HasValue ? number.Value.ToString()[0] : '?';
    }

    public static bool IsIllegible(this Digit digit) => !digit.ToNumber().HasValue;

    public static IEnumerable<Digit> GetVariations(this Digit input)
    {
        var variations = new List<Digit>();

        for (var bit = 0; bit < 7; bit++)
        {
            var variation = input ^ (Digit)(1 << bit);
            variations.Add(variation);
        }

        return variations;
    }

}