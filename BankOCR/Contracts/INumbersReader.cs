namespace BankOCR.Contracts;

public interface INumbersReader
{
    IEnumerable<string> Read(string filePath);
}