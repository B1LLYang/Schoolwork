namespace Schoolwork.Calculators.Tests;

public class ChangeCalculatorTests
{
    [TestCase(100, 100, 0, 0, 0, 0)]
    [TestCase(50, 100, 1, 0, 0, 0)]
    [TestCase(90, 100, 0, 1, 0, 0)]
    [TestCase(80, 100, 0, 2, 0, 0)]
    [TestCase(70, 100, 0, 3, 0, 0)]
    [TestCase(60, 100, 0, 4, 0, 0)]
    [TestCase(95, 100, 0, 0, 1, 0)]
    [TestCase(99, 100, 0, 0, 0, 1)]
    [TestCase(96, 100, 0, 0, 0, 4)]
    public void CalculateBestChange_ShouldReturnExpectedCombination(
        int price,
        int payment,
        int n50,
        int n10,
        int n5,
        int n1)
    {
        var result = ChangeCalculator.CalculateBestChange(price, payment);

        Assert.Multiple(() =>
        {
            Assert.That(result.N50, Is.EqualTo(n50));
            Assert.That(result.N10, Is.EqualTo(n10));
            Assert.That(result.N5, Is.EqualTo(n5));
            Assert.That(result.N1, Is.EqualTo(n1));
        });
    }

    [TestCase(0, 50)]
    [TestCase(101, 101)]
    public void CalculateBestChange_ShouldThrow_WhenPriceOutOfRange(int price, int payment)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ChangeCalculator.CalculateBestChange(price, payment));
    }

    [TestCase(80, 70)]
    [TestCase(50, 101)]
    public void CalculateBestChange_ShouldThrow_WhenPaymentInvalid(int price, int payment)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ChangeCalculator.CalculateBestChange(price, payment));
    }
}
