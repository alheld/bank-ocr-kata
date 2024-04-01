using System.Collections.Immutable;
using BankOCR.Contracts.models;
using BankOCR.Implementations;

namespace BankOCRTests;

public class NumberProcessorTests
{
    public static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            yield return new TestCaseData(
                new AccountNumber(new 
                    List<Digit>{Digit.Seven, Digit.One, Digit.One, Digit.One, Digit.One, Digit.One, Digit.One, Digit.One, Digit.One}.ToImmutableArray()))
                .Returns("711111111");
            yield return new TestCaseData(
                    new AccountNumber(new 
                        List<Digit>{Digit.One, Digit.Two, Digit.Three, Digit.Four, Digit.Five, Digit.Six, Digit.Seven, Digit.Eight, Digit.Nine}.ToImmutableArray()))
                .Returns("123456789");
            yield return new TestCaseData(
                    new AccountNumber(new 
                        List<Digit>{Digit.Four, Digit.Nine, Digit.Zero, Digit.Eight, Digit.Six, Digit.Seven, Digit.Seven, Digit.One, Digit.Five}.ToImmutableArray()))
                .Returns("490867715");
            yield return new TestCaseData(
                    new AccountNumber(new 
                        List<Digit>{Digit.Eight, Digit.Eight, Digit.Eight, Digit.Eight, Digit.Eight, Digit.Eight, Digit.Eight, Digit.Eight, Digit.Eight}.ToImmutableArray()))
                .Returns("888888888 AMB ['888886888', '888888988', '888888880']");
            yield return new TestCaseData(
                    new AccountNumber(new 
                        List<Digit>{Digit.One, Digit.One, Digit.One, Digit.One, Digit.One, Digit.One, Digit.One, Digit.One, Digit.One}.ToImmutableArray()))
                .Returns("711111111");
            yield return new TestCaseData(
                    new AccountNumber(new 
                        List<Digit>{Digit.Seven, Digit.Seven, Digit.Seven, Digit.Seven, Digit.Seven, Digit.Seven, Digit.Seven, Digit.Seven, Digit.Seven}.ToImmutableArray()))
                .Returns("777777177");
            yield return new TestCaseData(
                    new AccountNumber(new 
                        List<Digit>{Digit.Two, Digit.Zero, Digit.Zero, Digit.Zero, Digit.Zero, Digit.Zero, Digit.Zero, Digit.Zero, Digit.Zero}.ToImmutableArray()))
                .Returns("200800000");
            yield return new TestCaseData(
                    new AccountNumber(new 
                        List<Digit>{Digit.Three, Digit.Three, Digit.Three, Digit.Three, Digit.Three, Digit.Three, Digit.Three, Digit.Three, Digit.Three}.ToImmutableArray()))
                .Returns("333393333");
            yield return new TestCaseData(
                    new AccountNumber(new 
                        List<Digit>{Digit.Five, Digit.Five, Digit.Five, Digit.Five, Digit.Five, Digit.Five, Digit.Five, Digit.Five, Digit.Five}.ToImmutableArray()))
                .Returns("555555555 AMB ['559555555', '555655555']");
            yield return new TestCaseData(
                    new AccountNumber(new 
                        List<Digit>{Digit.Six, Digit.Six, Digit.Six, Digit.Six, Digit.Six, Digit.Six, Digit.Six, Digit.Six, Digit.Six}.ToImmutableArray()))
                .Returns("666666666 AMB ['686666666', '666566666']");
            yield return new TestCaseData(
                    new AccountNumber(new 
                        List<Digit>{Digit.Nine, Digit.Nine, Digit.Nine, Digit.Nine, Digit.Nine, Digit.Nine, Digit.Nine, Digit.Nine, Digit.Nine}.ToImmutableArray()))
                .Returns("999999999 AMB ['899999999', '993999999', '999959999']");
        }
    }
    
    [TestCaseSource(nameof(TestCases))]
   
    public string Process_ReturnsExpectedResult(AccountNumber number)
    {
        // Arrange
        var validator = new ChecksumNumberValidator();
        var processor = new NumberProcessor(validator);
        
        // Act
        return processor.Process(number);
    }
}