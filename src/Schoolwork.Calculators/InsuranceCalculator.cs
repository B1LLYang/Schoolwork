namespace Schoolwork.Calculators;

public static class InsuranceCalculator
{
    public static InsuranceCalculationResult CalculatePremium(
        decimal insuredAmount,
        int age,
        char gender,
        string maritalStatus,
        int? dependents)
    {
        if (insuredAmount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(insuredAmount), "投保额必须大于0。");
        }

        ValidateInputs(age, gender, maritalStatus, dependents);

        var totalPoints = CalculatePoints(age, gender, maritalStatus, dependents);
        var rate = totalPoints >= 10 ? 0.006m : 0.001m;
        var premium = insuredAmount * rate;

        return new InsuranceCalculationResult(totalPoints, rate, premium);
    }

    public static int CalculatePoints(int age, char gender, string maritalStatus, int? dependents)
    {
        ValidateInputs(age, gender, maritalStatus, dependents);

        var agePoints = age switch
        {
            >= 20 and <= 39 => 6m,
            >= 40 and <= 59 => 4m,
            _ => 2m
        };

        var normalizedGender = char.ToUpperInvariant(gender);
        var genderPoints = normalizedGender switch
        {
            'M' => 5m,
            'F' => 3m,
            _ => throw new ArgumentException("性别只能是 M 或 F。", nameof(gender))
        };

        var maritalPoints = maritalStatus switch
        {
            "已婚" => 3m,
            "未婚" => 5m,
            _ => throw new ArgumentException("婚姻状态只能是“已婚”或“未婚”。", nameof(maritalStatus))
        };

        var deduction = dependents is null
            ? 0m
            : Math.Min(3m, dependents.Value * 0.5m);

        return (int)Math.Round(agePoints + genderPoints + maritalPoints - deduction, MidpointRounding.AwayFromZero);
    }

    private static void ValidateInputs(int age, char gender, string maritalStatus, int? dependents)
    {
        if (age is < 1 or > 99)
        {
            throw new ArgumentOutOfRangeException(nameof(age), "年龄必须在1到99之间。");
        }

        var normalizedGender = char.ToUpperInvariant(gender);
        if (normalizedGender is not ('M' or 'F'))
        {
            throw new ArgumentException("性别只能是 M 或 F。", nameof(gender));
        }

        if (maritalStatus is not ("已婚" or "未婚"))
        {
            throw new ArgumentException("婚姻状态只能是“已婚”或“未婚”。", nameof(maritalStatus));
        }

        if (dependents is not null && (dependents.Value < 1 || dependents.Value > 9))
        {
            throw new ArgumentOutOfRangeException(nameof(dependents), "抚养人数只能为空或1到9之间。");
        }
    }
}

public sealed record InsuranceCalculationResult(int Points, decimal Rate, decimal Premium);
