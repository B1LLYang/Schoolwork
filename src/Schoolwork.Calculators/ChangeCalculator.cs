namespace Schoolwork.Calculators;

public static class ChangeCalculator
{
    public static ChangeResult CalculateBestChange(int price, int payment)
    {
        if (price is <= 0 or > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "货品价格必须是1到100的整数。");
        }

        if (payment < price || payment > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(payment), "顾客付款必须满足 R<=P<=100。");
        }

        var remain = payment - price;

        var n50 = remain / 50;
        remain %= 50;

        var n10 = remain / 10;
        remain %= 10;

        var n5 = remain / 5;
        remain %= 5;

        var n1 = remain;

        return new ChangeResult(n50, n10, n5, n1);
    }
}

public sealed record ChangeResult(int N50, int N10, int N5, int N1);
