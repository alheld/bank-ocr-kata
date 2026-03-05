using BankOCR.Contracts;
using BankOCR.Contracts.models;
using BankOCR.Implementations;
using Moq;

namespace BankOCRTests;

public class NumberParserTests
{
    [TestCase("000000000.txt", "000000000")]
    [TestCase("111111111.txt", "111111111")]
    [TestCase("222222222.txt", "222222222")]
    [TestCase("333333333.txt", "333333333")]
    [TestCase("444444444.txt", "444444444")]
    [TestCase("555555555.txt", "555555555")]
    [TestCase("666666666.txt", "666666666")]
    [TestCase("777777777.txt", "777777777")]
    [TestCase("888888888.txt", "888888888")]
    [TestCase("999999999.txt", "999999999")]
    [TestCase("49006771#.txt", "49006771?")]
    [TestCase("1234#678#.txt", "1234?678?")]
    public void ParseNumber_WithValidInputs_Returns_Parsed_Numbers(string fileName, string expected)
    {
        // Arrange
        var lines = File.ReadAllLines($"./TestFiles/{fileName}");
        var validator = new Mock<INumberValidator>();
        validator.Setup(x => x.IsValid(It.IsAny<IReadOnlyList<Digit>>())).Returns(true);
        var numberParser = new NumberParser();
        
        // Act
        var actual = numberParser.ParseNumber(lines, 1);
        
        // Assert
        Assert.That(actual.ToString(), Is.EqualTo(expected));
    }
}