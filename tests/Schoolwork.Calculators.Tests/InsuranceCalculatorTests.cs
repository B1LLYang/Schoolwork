namespace Schoolwork.Calculators.Tests;

public class InsuranceCalculatorTests
{
    private const decimal Amount = 1_000_000m;

    [Test]
    public void CalculatePremium_ShouldUseHighRate_WhenPointsAtLeast10()
    {
        var result = InsuranceCalculator.CalculatePremium(Amount, 25, 'M', "已婚", 2);

        Assert.Multiple(() =>
        {
            Assert.That(result.Points, Is.EqualTo(13));
            Assert.That(result.Rate, Is.EqualTo(0.006m));
            Assert.That(result.Premium, Is.EqualTo(6000m));
        });
    }

    [Test]
    public void CalculatePremium_ShouldRoundPointsUsingHalfUp()
    {
        var result = InsuranceCalculator.CalculatePremium(Amount, 40, 'F', "已婚", 1);

        Assert.Multiple(() =>
        {
            Assert.That(result.Points, Is.EqualTo(10));
            Assert.That(result.Rate, Is.EqualTo(0.006m));
        });
    }

    [Test]
    public void CalculatePremium_ShouldUseLowRate_WhenPointsBelow10()
    {
        var result = InsuranceCalculator.CalculatePremium(Amount, 65, 'F', "已婚", 9);

        Assert.Multiple(() =>
        {
            Assert.That(result.Points, Is.EqualTo(5));
            Assert.That(result.Rate, Is.EqualTo(0.001m));
            Assert.That(result.Premium, Is.EqualTo(1000m));
        });
    }

    [Test]
    public void CalculatePremium_ShouldAllowBlankDependents()
    {
        var result = InsuranceCalculator.CalculatePremium(Amount, 45, 'F', "未婚", null);

        Assert.That(result.Points, Is.EqualTo(12));
    }

    [TestCase(0)]
    [TestCase(100)]
    public void CalculatePremium_ShouldThrow_WhenAgeInvalid(int age)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            InsuranceCalculator.CalculatePremium(Amount, age, 'M', "已婚", 1));
    }

    [TestCase('X')]
    [TestCase('1')]
    public void CalculatePremium_ShouldThrow_WhenGenderInvalid(char gender)
    {
        Assert.Throws<ArgumentException>(() =>
            InsuranceCalculator.CalculatePremium(Amount, 30, gender, "已婚", 1));
    }

    [TestCase("")]
    [TestCase("离异")]
    public void CalculatePremium_ShouldThrow_WhenMaritalStatusInvalid(string maritalStatus)
    {
        Assert.Throws<ArgumentException>(() =>
            InsuranceCalculator.CalculatePremium(Amount, 30, 'M', maritalStatus, 1));
    }

    [TestCase(0)]
    [TestCase(10)]
    public void CalculatePremium_ShouldThrow_WhenDependentsInvalid(int dependents)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            InsuranceCalculator.CalculatePremium(Amount, 30, 'M', "已婚", dependents));
    }
}
