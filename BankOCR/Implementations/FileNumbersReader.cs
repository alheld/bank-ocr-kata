
using BankOCR.Contracts;

namespace BankOCR.Implementations;

public class FileNumbersReader: INumbersReader
{
    private readonly INumberParser _numberParser;
    private readonly INumberProcessor _numberProcessor;

    public FileNumbersReader(INumberParser numberParser, INumberProcessor numberProcessor)
    {
        _numberParser = numberParser;
        _numberProcessor = numberProcessor;
    }

    public IEnumerable<string> Read(string filePath)
    {
        if(!File.Exists(filePath))
            throw new FileNotFoundException("File not found", filePath);
        
        var accountNumbers = new List<string>();
        var lines = File.ReadAllLines(filePath);

        for (var i = 1; i < lines.Length; i += 4)
        {
            var accountNumber = _numberParser.ParseNumber(lines, i);
            var processedNumber = _numberProcessor.Process(accountNumber);
            accountNumbers.Add(processedNumber);
        }

        return accountNumbers;
    }
   
}