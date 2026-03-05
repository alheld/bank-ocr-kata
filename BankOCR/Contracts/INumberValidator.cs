using BankOCR.Contracts.models;

namespace BankOCR.Contracts;

public interface INumberValidator
{
    bool IsValid(IEnumerable<Digit> number);
}