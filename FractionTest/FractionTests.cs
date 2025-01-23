using Fraction;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;

namespace FractionTest
{
    public class FractionTests
    {
        public static IEnumerable<object[]> Fractions
        {
            get
            {
                string tests_filePath = "fraction_test.txt";
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
                            yield return new object[] { f, data[1] };
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

            var expected1 = "5 1/2";
            var actual1 = f1 ^ -1;
            Assert.Equal(expected1, actual1.ToString());

            var expected2 = "166 3/8";
            var actual2 = f1 ^ (-3);
            Assert.Equal(expected2, actual2.ToString());
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
        public void AddOperator_FractionClass_ReturnsExpected(FractionClass f1, FractionClass f2, string expected)
        {
            var actual = f1 + f2;
            Assert.Equal(expected, actual.ToString());
        }

        [Fact(DisplayName ="Addition operator on fractions throws exception unit tests.")]
        public void AddOperator_FractionClass_Throws()
        {
            Assert.Throws<DivideByZeroException>(() =>
            {
                var f1 = new FractionClass();
                var f2 = new FractionClass(3, 0);
                var actual = f1 + f2;
            });

            Assert.Throws<DivideByZeroException>(() =>
            {
                var f1 = new FractionClass(2, 0);
                var f2 = new FractionClass(1, 0);
                var actual = f1 + f2;
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                var f1 = new FractionClass();
                FractionClass? f2 = null;
                var actual = f1 + f2;
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                FractionClass? f1 = null;
                FractionClass? f2 = null;
                var actual = f1 + f2;
            });
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
        public void SubtractOperator_FractionClass_ReturnsExpected(FractionClass f1, FractionClass f2, string expected)
        {
            var actual = f1 - f2;
            Assert.Equal(expected, actual.ToString());
        }

        [Fact(DisplayName = "Subtraction operator on fractions throws exception unit tests.")]
        public void SubtractOperator_FractionClass_Throws()
        {
            Assert.Throws<DivideByZeroException>(() =>
            {
                var f1 = new FractionClass(3, 2);
                var f2 = new FractionClass(-1, 0);
                var actual = f1 - f2;
            });

            Assert.Throws<DivideByZeroException>(() =>
            {
                var f1 = new FractionClass(-1, 0);
                var f2 = new FractionClass(5, 0);
                var actual = f1 - f2;
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                var f1 = new FractionClass(4, 5);
                FractionClass? f2 = null;
                var actual = f1 - f2;
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                FractionClass? f1 = null ;
                FractionClass? f2 = new FractionClass(2, 3);
                var actual = f1 - f2;
            });
        }

        [Theory(DisplayName ="Multiplication operator on fractions unit tests.")]
        [InlineData(1, 5, 3, 7, "3/35")]
        [InlineData(4, 3, 2, 9, "8/27")]
        [InlineData(0, 2, -4, 9, "0")]
        [InlineData(-2, 5, 3, 7, "-6/35")]
        [InlineData(2, -5, 3, 7, "-6/35")]
        [InlineData(2, 5, -3, 7, "-6/35")]
        [InlineData(2, 5, 3, -7, "-6/35")]
        [InlineData(3, 4, 1, 2, "3/8")]
        [InlineData(-1, -2, -2, -3, "1/3")]
        public void MultiplyOperator_Fraction_ReturnsExpected(int n1, int d1, int n2, int d2, string expected)
        {
            var f1 = new FractionClass(n1, d1);
            var f2 = new FractionClass(n2, d2);

            var actual = f1 * f2;
            Assert.Equal(expected, actual.ToString());
        }

        [Fact(DisplayName ="Multiplication operator on fractions throws exception unit tests.")]
        public void MultiplyOperator_FractionClass_Throws()
        {
            Assert.Throws<DivideByZeroException>(() =>
            {
                var f1 = new FractionClass(-3, 5);
                var f2 = new FractionClass(5, 0);
                var expected = f1 * f2;
            });

            Assert.Throws<DivideByZeroException>(() =>
            {
                var f1 = new FractionClass(1, 0);
                var f2 = new FractionClass(-1, 0);
                var expected = f1 * f2;
            });
        }

        [Theory(DisplayName ="Division operator on fractions unit tests.")]
        [InlineData(1, 5, 3, 7, "7/15")]
        [InlineData(-2, 3, 1, 4, "-2 -2/3")]
        [InlineData(1, 5, -4, 9, "-9/20")]
        [InlineData(1, -5, 4, 9, "-9/20")]
        [InlineData(1, 5, 4, -9, "-9/20")]
        [InlineData(0, 5, 4, 9, "0")]
        [InlineData(-1, -5, -4, -9, "9/20")]
        [InlineData(1, 5, -4, -9, "9/20")]
        public void DivisionOperator_Fraction_ReturnsExpected(int n1, int d1, int n2, int d2, string expected)
        {
            var f1 = new FractionClass(n1, d1);
            var f2 = new FractionClass(n2, d2);

            var actual = f1 / f2;
            Assert.Equal(expected, actual.ToString());
        }

        [Fact(DisplayName ="Division operator on fractions and objects of different types throws exception unit tests.")]
        public void DivisionOperator_FractionClass_Throws()
        {
            Assert.Throws<DivideByZeroException>(() =>
            {
                var f1 = new FractionClass(2, 4);
                var f2 = new FractionClass(-3, 0);
                var expected = f1 / f2;
            });
        }

        [Theory(DisplayName ="Constructor with whole number unit tests.")]
        [InlineData(1, 1, 3, "1 1/3")]
        [InlineData(2, 3, 5, "2 3/5")]
        [InlineData(-1, -1, 3, "-1 -1/3")]
        [InlineData(3, 3, 4, "3 3/4")]
        public void FractionClass_Constructor_WithWholeNumber_ReturnsExpected(int wholeNum, int numerator, int denominator, string expected)
        {
            var f1 = new FractionClass(numerator, denominator, wholeNum);
            Assert.Equal(expected, f1.ToString());
        }

        [Fact(DisplayName ="FractionClass constructor with whole number throw exception unit tests.")]
        public void FractionClass_Constructor_WithWholeNumber_Throws()
        {
            Assert.Throws<ArgumentException>(() => new FractionClass(1, 3, -1));
            Assert.Throws<ArgumentException>(() => new FractionClass(3, 5, -5));
            Assert.Throws<ArgumentException>(() => new FractionClass(5, 3, -2));
            Assert.Throws<ArgumentException>(() => new FractionClass(-1, 3, 5));
            Assert.Throws<ArgumentException>(() => new FractionClass(-3, 5, 1));
        }

        [Fact(DisplayName ="FractionClass constructor with no parameters unit tests.")]
        public void FractionClass_Constructor_WithNoParams_ReturnsExpected()
        {
            var f1 = new FractionClass();
            var expected = "0";
            Assert.Equal(expected, f1.ToString());

            var f2 = new FractionClass();
            Assert.Equal(expected, f2.ToString());
        }

        [Fact(DisplayName ="GetHashCode method on fractions with same values unit tests.")]
        public void GetHashCode_FractionClass_SameValues_ReturnsExpected()
        {
            var f1 = new FractionClass(1, 2);
            var f2 = new FractionClass(2, 4);
            var f3 = new FractionClass(1, 2);

            var f1_HashCode = f1.GetHashCode();
            var f2_HashCode = f2.GetHashCode();
            var f3_HashCode = f3.GetHashCode();
            
            Assert.True(f1_HashCode == f2_HashCode);
            Assert.True(f1_HashCode == f3_HashCode);
            Assert.True(f2_HashCode == f3_HashCode);
        }   
        
        [Fact(DisplayName ="GetHashCode method on fractions with different values unit tests.")]
        public void GetHashCode_FractionClass_DiffValues_ReturnsExpected()
        {
            var f1 = new FractionClass(1, 3);
            var f2 = new FractionClass(2, 4);
            var f3 = new FractionClass(-1, 2);

            var f1_HashCode = f1.GetHashCode();
            var f2_HashCode = f2.GetHashCode();
            var f3_HashCode = f3.GetHashCode();
            
            Assert.False(f1_HashCode == f2_HashCode);
            Assert.False(f1_HashCode == f3_HashCode);
            Assert.False(f2_HashCode == f3_HashCode);
        }

        public static IEnumerable<object[]> EqualComparisonMethodTests
        {
            get
            {
                string tests_filename = "comparison_methods_tests.txt";
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
                            yield return new object[] { f1, f2 };
                        }
                        else yield break;

                    }
                }
            }
        }
        [Theory(DisplayName ="Equal method on fractions with same values unit tests.")]
        [MemberData(nameof(EqualComparisonMethodTests))]
        public void Equal_FractionClass_WithSameValues_ReturnsExpected(FractionClass f1, FractionClass f2)
        {
            Assert.True(f1.Equals(f2));
        }

        [Fact(DisplayName ="Equal method on fractions and objects of different types unit tests.")]
        public void Equal_FractionClass_ReturnsExpected()
        {
            var f1 = new FractionClass(2, 3);
            object? f2 = 1;
            Assert.False(f1.Equals(f2));

            var f3 = new FractionClass(-1, 4);
            object? f4 = "1/2";
            Assert.False(f2.Equals(f3));
        }

        public static IEnumerable<object[]> CompareToComparisonMethodTests
        {
            get
            {
                string tests_filename = "comparison_methods_tests.txt";
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
        [Theory(DisplayName ="CompareTo method on fractions with same values unit tests.")]
        [MemberData(nameof(CompareToComparisonMethodTests))]
        public void CompareTo_FractionClass_WithSameValues_ReturnsExpected(FractionClass f1, FractionClass f2, int expected)
        {
            Assert.Equal(expected, f1.CompareTo(f2));
        }

        [Fact(DisplayName ="CompareTo method on fractions and objects of different types throws exception unit tests.")]
        public void CompareTo_FractionClass_ReturnsExpected()
        {
            var f1 = new FractionClass(3, 4);
            FractionClass? f2 = null;
            Assert.Equal(1, f1.CompareTo(f2));
        }

        [Theory]
        [InlineData(1, 2, 0.5)]
        [InlineData(3, 2, 1.5)]
        [InlineData(0, 2, 0.0)]
        [InlineData(1, 3, 0.33333333333333333333333333)]
        [InlineData(-2, 4, -0.5)]
        [InlineData(1, -3, -0.333333333333333333333333)]
        public void ValueProperty_FractionClass_ReturnsExpected(int num, int denom, double expected)
        {
            var f = new FractionClass(num, denom);
            Assert.Equal(expected, f.Value);
        }

        [Theory(DisplayName ="Equal operator on fractions unit tests.")]
        [InlineData(1, 2, 3, 4, false)]
        [InlineData(-1, 2, 1, -2, true)]
        [InlineData(2, 4, 1, 2, true)]
        [InlineData(3, 4, 8, 12, false)]
        [InlineData(4, 5,  6, 7, false)]
        [InlineData(-1, -2, -4, -8, true)]
        public void EqualOperator_FractionClass_ReturnsExpected(int n1, int d1, int n2, int d2, bool expected)
        {
            var f1 = new FractionClass(n1, d1);
            var f2 = new FractionClass(n2, d2);

            Assert.Equal(expected, f1 == f2);
        }
        
        [Theory(DisplayName ="NotEqual operator on fractions unit tests.")]
        [InlineData(1, 2, 3, 4, true)]
        [InlineData(-1, 2, 1, -2, false)]
        [InlineData(2, 4, 1, 2, false)]
        [InlineData(3, 4, 8, 12, true)]
        [InlineData(0, 1, 1, 3, true)]
        [InlineData(-1, -2, -2, -4, false)]
        public void NotEqualOperator_FractionClass_ReturnsExpected(int n1, int d1, int n2, int d2, bool expected)
        {
            var f1 = new FractionClass(n1, d1);
            var f2 = new FractionClass(n2, d2);

            Assert.Equal(expected, f1 != f2);
        }
        
        [Theory(DisplayName ="GreaterThan operator on fractions unit tests.")]
        [InlineData(1, 2, 3, 4, false)]
        [InlineData(-1, 2, 1, -2, false)]
        [InlineData(2, 4, 1, 2, false)]
        [InlineData(3, 4, 8, 12, true)]
        [InlineData(0, 1, 1, 3, false)]
        [InlineData(-1, 2, 2, -3, true)]
        public void GreaterThanOperator_FractionClass_ReturnsExpected(int n1, int d1, int n2, int d2, bool expected)
        {
            var f1 = new FractionClass(n1, d1);
            var f2 = new FractionClass(n2, d2);

            Assert.Equal(expected, f1 > f2);
        }
        
        [Theory(DisplayName ="LessThan operator on fractions unit tests.")]
        [InlineData(1, 2, 3, 4, true)]
        [InlineData(-1, 2, 1, -2, false)]
        [InlineData(2, 4, 1, 2, false)]
        [InlineData(3, 4, 8, 12, false)]
        [InlineData(0, 1, 1, 3, true)]
        [InlineData(-1, 2, 2, -3, false)]
        public void LessThanOperator_FractionClass_ReturnsExpected(int n1, int d1, int n2, int d2, bool expected)
        {
            var f1 = new FractionClass(n1, d1);
            var f2 = new FractionClass(n2, d2);

            Assert.Equal(expected, f1 < f2);
        }

        [Theory(DisplayName = "GreaterThanOrEqual operator on fractions unit tests.")]
        [InlineData(1, 2, 3, 4, false)]
        [InlineData(-1, 2, 1, -2, true)]
        [InlineData(2, 4, 1, 2, true)]
        [InlineData(3, 4, 8, 12, true)]
        [InlineData(0, 1, 1, 3, false)]
        [InlineData(-1, 2, 2, -3, true)]
        public void GreaterThanOrEqualOperator_FractionClass_ReturnsExpected(int n1, int d1, int n2, int d2, bool expected)
        {
            var f1 = new FractionClass(n1, d1);
            var f2 = new FractionClass(n2, d2);

            Assert.Equal(expected, f1 >= f2);
        }

        [Theory(DisplayName = "LessThanOrEqual operator on fractions unit tests.")]
        [InlineData(1, 2, 3, 4, true)]
        [InlineData(-1, 2, 1, -2, true)]
        [InlineData(2, 4, 1, 2, true)]
        [InlineData(3, 4, 8, 12, false)]
        [InlineData(0, 1, 1, 3, true)]
        [InlineData(-1, 2, 2, -3, false)]
        public void LessThanOrEqualOperator_FractionClass_ReturnsExpected(int n1, int d1, int n2, int d2, bool expected)
        {
            var f1 = new FractionClass(n1, d1);
            var f2 = new FractionClass(n2, d2);

            Assert.Equal(expected, f1 <= f2);
        }
    }
}
