using BankOCR.Contracts.models;

namespace BankOCR.Contracts;

public interface INumberProcessor
{
    string Process(AccountNumber accountNumbers);
}