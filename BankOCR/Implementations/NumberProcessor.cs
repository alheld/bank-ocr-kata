using System.Collections.Immutable;
using BankOCR.Contracts;
using BankOCR.Contracts.models;

namespace BankOCR.Implementations;

public class NumberProcessor: INumberProcessor
{
    private readonly INumberValidator _numberValidator;

    public NumberProcessor(INumberValidator numberValidator)
    {
        _numberValidator = numberValidator;
    }

    public string Process(AccountNumber accountNumber)
    {
        if (!accountNumber.IsIllegible && _numberValidator.IsValid(accountNumber.Digits))
            return accountNumber.ToString();
        var accountDigits = accountNumber.Digits;
        var variations = GetAllValidVariations(accountDigits).ToList();

        return variations.Count switch
        {
            1 => variations.First().ToString(),
            0 when accountNumber.IsIllegible => accountNumber + " " + NumberState.Illegible,
            0 => accountNumber + " " + NumberState.Invalid,
            _ => accountNumber + " " + NumberState.Ambiguous + " [" + 
                 string.Join(", ", variations.Select(v => $"'{v}'")) + "]"
        };
    }
    
        
    private IEnumerable<AccountNumber> GetAllValidVariations(ImmutableArray<Digit> input)
    {
        var validVariations = new List<AccountNumber>();

        for (var i = 0; i < 9; i++)
        {
            validVariations
                .AddRange(input[i].GetVariations()
                    .Select(digitVariation => 
                        new AccountNumber(input.SetItem(i, digitVariation)))
                    .Where(accountNumber => !accountNumber.IsIllegible && _numberValidator.IsValid(accountNumber.Digits)));
        }

        return validVariations;
    }
}