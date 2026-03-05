using BankOCR.Contracts.models;
using BankOCR.Implementations;

namespace BankOCRTests;

public class ChecksumNumberValidatorTests
{
    public static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            yield return new TestCaseData(new List<Digit>{Digit.Seven, Digit.One, Digit.One, Digit.One, Digit.One, Digit.One, Digit.One, Digit.One, Digit.One}).Returns(true);
            yield return new TestCaseData(new List<Digit>{Digit.One, Digit.Two, Digit.Three, Digit.Four, Digit.Five, Digit.Six, Digit.Seven, Digit.Eight, Digit.Nine}).Returns(true);
            yield return new TestCaseData(new List<Digit>{Digit.Four, Digit.Nine, Digit.Zero, Digit.Eight, Digit.Six, Digit.Seven, Digit.Seven, Digit.One, Digit.Five}).Returns(true);
            yield return new TestCaseData(new List<Digit>{Digit.Eight, Digit.Eight, Digit.Eight, Digit.Eight, Digit.Eight, Digit.Eight, Digit.Eight, Digit.Eight, Digit.Eight}).Returns(false);
            yield return new TestCaseData(new List<Digit>{Digit.Four, Digit.Nine, Digit.Zero, Digit.Zero, Digit.Six, Digit.Seven, Digit.Seven, Digit.One, Digit.Five}).Returns(false);
            yield return new TestCaseData(new List<Digit>{Digit.Zero, Digit.One, Digit.Two, Digit.Three, Digit.Four, Digit.Five, Digit.Six, Digit.Seven, Digit.Eight}).Returns(false);
            yield return new TestCaseData(new List<Digit>{Digit.One, Digit.One, Digit.One, Digit.One, Digit.One, Digit.One, Digit.One, Digit.One, Digit.One}).Returns(false);
        }
    }
    [TestCaseSource(nameof(TestCases))]
   
    public bool IsValid_ReturnsExpectedResult(List<Digit> number)
    {
        // Arrange
        var validator = new ChecksumNumberValidator();
        
        // Act
        return validator.IsValid(number);
    }
}