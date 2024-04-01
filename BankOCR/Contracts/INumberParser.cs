using BankOCR.Contracts.models;

namespace BankOCR.Contracts;

public interface INumberParser
{
    AccountNumber ParseNumber(IReadOnlyList<string> lines, int startLine);
}