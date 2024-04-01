using BankOCR.Contracts;
using BankOCR.Contracts.models;

namespace BankOCR.Implementations;

public class ChecksumNumberValidator: INumberValidator
{
    public bool IsValid(IEnumerable<Digit> number)
    {
        var checksum = number.Select((digit, i) => (9 - i) * digit.ToNumber()).Sum();
        return checksum % 11 == 0;
    }
}