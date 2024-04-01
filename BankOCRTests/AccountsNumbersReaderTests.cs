using BankOCR.Contracts;
using BankOCR.Contracts.models;
using BankOCR.Implementations;
using Moq;

namespace BankOCRTests;

public class AccountsNumbersReaderTests
{
    [Test]
    public void ReadFromFile_WithValidFilePath_HandlesMultipleNumbers()
    {
        // Arrange
        var validator = new Mock<INumberValidator>();
        validator.Setup(x => x.IsValid(It.IsAny<IEnumerable<Digit>>())).Returns(true);
        var numberParser = new NumberParser();
        var numberProcessor = new NumberProcessor(validator.Object);
        var sut = new FileNumbersReader(numberParser, numberProcessor);
        const string filePath = "./TestFiles/multiple_numbers.txt";
        
        // Act
        var actual = sut.Read(filePath);
        
        // Assert
        Assert.That(actual, Is.EquivalentTo(new[] { "123456789", "777777777", "888888888" }));
    }
    
    [Test]
    public void ReadFromFile_WithInvalidFilePath_Throws()
    {
        // Arrange
        var validator = new Mock<INumberValidator>();
        validator.Setup(x => x.IsValid(It.IsAny<IEnumerable<Digit>>())).Returns(true);
        var numberParser = new NumberParser();
        var numberProcessor = new NumberProcessor(validator.Object);
        var sut = new FileNumbersReader(numberParser, numberProcessor);
        const string filePath = "./TestFiles/unknown.txt";
        
        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => sut.Read(filePath), "File not found");
    }
}