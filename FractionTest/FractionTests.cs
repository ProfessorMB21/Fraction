using Fraction;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;

namespace FractionTest
{
    public class FractionTests
    {
        [Theory(DisplayName = "ToString method unit tests.")]
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
                string tests_filePath = "C:\\Users\\Lenovo\\source\\repos\\Fraction\\FractionTest\\fraction_test.txt";
                using (var sr = new StreamReader(tests_filePath, System.Text.Encoding.UTF8))
                {
                    while (true)
                    {
                        var data = sr.ReadLine()?.Split(',', ' ', 
                            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
                            );

                        if (data is not null)
                        {
                            FractionClass f;
                            var values = data[0].Split('/');
                            try
                            {
                                f = new FractionClass(int.Parse(values[0]), int.Parse(values[1]));
                            }
                            catch
                            {
                                f = new FractionClass();
                            }
                            yield return new object[] { f, data[1].Trim() };
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

        [Fact(DisplayName = "FractionClass constructor with two parameters only unit tests.")]
        public void FractionClass_Constructor_Throws()
        {
            //FractionClass fraction = new(2, 0);

            Assert.Throws<ArgumentException>(() => new FractionClass(2, 0));
            Assert.Throws<ArgumentException>(() => new FractionClass(-4, 0));
            Assert.Throws<ArgumentException>(() => new FractionClass(0, 0));
        }

        [Fact(DisplayName ="Raising a fraction to a positive nth power using Math.Pow method unit tests.")]
        public void RaiseToPowerOperator_PositivePowers_FractionClass_ReturnsExpected()
        {
            var f = new FractionClass(1, 5);

            var expected = "1/25";
            var actual = f ^ 2;
            Assert.Equal(expected, actual.ToString());

            var f2 = new FractionClass(3, -4);
            var expected1 = "9/16";
            var actual1 = f2 ^ 2;
            Assert.Equal(expected1, actual1.ToString());

            var expected2 = "1/1";
            var actual2 = f2 ^ 0;
            Assert.Equal(expected2, actual2.ToString());
        }

        [Fact(DisplayName ="Raising a fraction to a negative nth power unit tests.")]
        public void RaiseToPowerOperator_NegativePowers_FractionClass_ReturnsExpected()
        {
            var f1 = new FractionClass(2, 11);

            var expected1 = "11/2";
            var actual1 = f1 ^ -1;
            Assert.Equal(expected1, actual1.ToString());

            var expected2 = "1331/8";
            var actual2 = f1 ^ (-3);
            Assert.Equal(expected2, actual2.ToString());
        }

        [Fact(DisplayName ="Property Denominator unit tests.")]
        public void PropertyDenominator_Fraction_Throws()
        {
            Assert.Throws<ArgumentException>(()  => new FractionClass(1, 0));
            Assert.Throws<ArgumentException>(() => new FractionClass(-2, 0));
            Assert.Throws<ArgumentException>(() => new FractionClass(10000, 0));
        }

        public static IEnumerable<object[]> AddOperatorTests
        {
            get
            {
                string tests_filename = "operators_test.txt";
                using (var sr = new StreamReader(tests_filename, System.Text.Encoding.UTF8))
                {
                    while (true)
                    {
                        var data = sr.ReadLine()?.Split(',', ' ',
                            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
                            );

                        if (data is not null)
                        {
                            FractionClass f1, f2;
                            var val1 = data[0].Split('/');
                            var val2 = data[1].Split('/');
                            try
                            {
                                f1 = new FractionClass(int.Parse(val1[0]), int.Parse(val1[1]));
                                f2 = new FractionClass(int.Parse(val2[0]), int.Parse(val2[1]));
                            }
                            catch
                            {
                                f1 = new FractionClass();
                                f2 = new FractionClass();
                            }
                            yield return new object[] { f1, f2, data[2] };
                        }
                        else yield break;

                    }
                }
            }
        }
        [Theory(DisplayName = "Addition operator on fractions unit tests.")]
        [MemberData(nameof(AddOperatorTests))]
        public void AddOperator_Fraction_ReturnsExpected(FractionClass f1, FractionClass f2, string expected)
        {
            var actual = f1 + f2;
            Assert.Equal(expected, actual.ToString());
        }

        public static IEnumerable<object[]> SubtractOperatorTests
        {
            get
            {
                string tests_filename = "operators_test.txt";
                using (var sr = new StreamReader(tests_filename, System.Text.Encoding.UTF8))
                {
                    while (true)
                    {
                        var data = sr.ReadLine()?.Split(',', ' ',
                            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
                            );

                        if (data is not null)
                        {
                            FractionClass f1, f2;
                            var val1 = data[0].Split('/');
                            var val2 = data[1].Split('/');
                            try
                            {
                                f1 = new FractionClass(int.Parse(val1[0]), int.Parse(val1[1]));
                                f2 = new FractionClass(int.Parse(val2[0]), int.Parse(val2[1]));
                            }
                            catch
                            {
                                f1 = new FractionClass();
                                f2 = new FractionClass();
                            }
                            yield return new object[] { f1, f2, data[3] };
                        }
                        else yield break;

                    }
                }
            }
        }
        [Theory(DisplayName ="Subtraction operator on fractions unit tests.")]
        [MemberData(nameof(SubtractOperatorTests))]
        public void SubtractOperator_Fraction_ReturnsExpected(FractionClass f1, FractionClass f2, string expected)
        {
            var actual = f1 - f2;
            Assert.Equal(expected, actual.ToString());
        }

        [Theory(DisplayName ="Multiplication operator on fractions unit tests.")]
        [InlineData(1, 5, 3, 7, "3/35")]
        [InlineData(4, 3, 2, 9, "8/27")]
        [InlineData(0, 2, -4, 9, "0/18")]
        [InlineData(-2, 5, 3, 7, "-6/35")]
        [InlineData(2, -5, 3, 7, "-6/35")]
        [InlineData(2, 5, -3, 7, "-6/35")]
        [InlineData(2, 5, 3, -7, "-6/35")]
        [InlineData(3, 4, 1, 2, "3/8")]
        [InlineData(3, 0, -5, 8, "-15/0", Skip ="Division by zero.")]
        [InlineData(-1, -2, -2, -3, "2/6")]
        public void MultiplyOperator_Fraction_ReturnsExpected(int n1, int d1, int n2, int d2, string expected)
        {
            var f1 = new FractionClass(n1, d1);
            var f2 = new FractionClass(n2, d2);

            var actual = f1 * f2;
            Assert.Equal(expected, actual.ToString());
        }

        [Theory(DisplayName ="Division operator on fractions unit tests.")]
        [InlineData(1, 5, 3, 7, "7/15")]
        [InlineData(-2, 3, 1, 4, "-8/3")]
        [InlineData(1, 5, -4, 9, "-9/20")]
        [InlineData(1, -5, 4, 9, "-9/20")]
        [InlineData(1, 5, 4, -9, "-9/20")]
        [InlineData(0, 5, 4, 9, "0/20")]
        [InlineData(-1, -5, -4, -9, "9/20")]
        [InlineData(1, 5, -4, -9, "9/20")]
        public void DivisionOperator_Fraction_ReturnsExpected(int n1, int d1, int n2, int d2, string expected)
        {
            var f1 = new FractionClass(n1, d1);
            var f2 = new FractionClass(n2, d2);

            var actual = f1 / f2;
            Assert.Equal(expected, actual.ToString());
        }


    }
}
