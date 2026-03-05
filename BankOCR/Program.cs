using BankOCR.Implementations;

const string inputPath = "your path to the input file here";

var validator = new ChecksumNumberValidator();
var parser = new NumberParser();
var processor = new NumberProcessor(validator);
var reader = new FileNumbersReader(parser, processor);
var accountNumbers = reader.Read(inputPath);

const string outputPath = "D:\\projects\\BankOCR\\BankOCR\\output.txt";
File.WriteAllLines(outputPath, accountNumbers);