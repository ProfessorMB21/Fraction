using Fraction;

namespace FractionTest
{
    public class FractionTests
    {
        [Fact(DisplayName = "AddWithSameDenominator with positive fractions method unit test.")]
        public void PrivateAddWithSameDenominator_PositiveFractions_ReturnsExpectedResult()
        {
            var f1 = new FractionClass(1, 2);
            var f2 = new FractionClass(3, 2);

            FractionClass expectedResult = new(4, 2);
            FractionClass actualResult = f1.Add(f2);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact(DisplayName = "AddWithDiffDenominator with positive fractions method unit test.")]
        public void PrivateAddWithDiffDenominator_PositiveFractions_ReturnsExpected()
        {
            var f1 = new FractionClass(1, 2);
            var f2 = new FractionClass(3, 5);

            FractionClass expectedResult = new(11, 10);
            FractionClass actualResult = f1.Add(f2);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory(DisplayName = "ToString method unit test.")]
        [InlineData(1, 2, "1/2")]
        [InlineData(3, 5, "3/5")]
        [InlineData(0, 1, "0/1")]
        [InlineData(6, 7, "6/7")]
        [InlineData(-1, 9, "-1/9")]
        [InlineData(2, -11, "-2/11")]
        [InlineData(-1, -2, "1/2")]
        public void ToString_ReturnsString(int numerator, int denominator, string expected)
        {
            var f1 = new FractionClass(numerator, denominator);
            Assert.Equal(expected, f1.ToString());
        }

        public static IEnumerable<object[]> Fractions
        {
            get
            {
                using (var sr = new StreamReader("fraction_test.txt", System.Text.Encoding.UTF8))
                {
                    while (true)
                    {
                        var data = sr.ReadLine()?.Split(',', ' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                        if (data is not null)
                        {
                            FractionClass f;
                            try
                            {
                                f = new FractionClass(int.Parse(data[0]), int.Parse(data[1]));
                            }
                            catch
                            {
                                f = new FractionClass();
                            }
                            yield return new object[] { f, data[2] };
                        }
                        else yield break;
                    };
                }
            }

        }

        [Theory(DisplayName ="ToString unit test with external source test cases.")]
        [MemberData(nameof(Fractions))]
        public void ToString_Fractions_ReturnsString(FractionClass f, string expected)
        {
            Assert.Equal(expected, f.ToString());
        }
    }
}
