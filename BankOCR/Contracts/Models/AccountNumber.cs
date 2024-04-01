using System.Collections.Immutable;

namespace BankOCR.Contracts.models;

public class AccountNumber
{

    public AccountNumber(ImmutableArray<Digit> digits)
    {
        Digits = digits;
    }
    
    public ImmutableArray<Digit> Digits { get; }

    public bool IsIllegible => Digits.Any(d => d.IsIllegible());

    public override string ToString() => new string(Digits.Select(d => d.ToChar()).ToArray());
}