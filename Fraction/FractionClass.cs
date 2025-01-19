using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Fraction
{
    public class FractionClass : IComparable<FractionClass>
    {
        private int _numerator;
        private int _denominator;
        public int Numerator
        {
            get => _numerator;
            private set => _numerator = value;
        }
        public int Denominator
        {
            get => _denominator;
            private set
            {
                if (value == 0)
                    throw new ArgumentException("Denominator cannot be zero.");
                _denominator = value;
            }
        }
        public double Value => (double)Numerator / Denominator;

        public FractionClass(int numerator, int denominator) 
        {
            Denominator = denominator;
            Numerator = numerator;
        }
        public FractionClass(int numerator, int denominator, int wholeNumber)
            : this(wholeNumber * denominator + numerator, denominator)
        {
            if (wholeNumber < 0 && numerator > 0)
                throw new ArgumentException($"In a mixed fraction, the {nameof(numerator)} must have the same sign as the whole number.");
        }
        public FractionClass() : this(0, 1) { }
        
        private int gcd(int a, int b)
        {
            if (a == 0) return b;
            return gcd(b % a, a);
        }
        private int lcm(int a, int b) => (a * b) / gcd(a, b);
       
        private FractionClass Add(FractionClass other)
        {
            int lcd = lcm(this.Denominator, other.Denominator);

            _numerator *= (lcd / this.Denominator);
            other._numerator *= (lcd / other.Denominator);

            int result_num = _numerator + other._numerator;
            return new FractionClass(result_num, lcd);
        }
        private FractionClass Subtract(FractionClass other)
        {
            int lcd = lcm(this.Denominator, other.Denominator);

            _numerator *= (lcd / this.Denominator);
            other._numerator *= (lcd / other.Denominator);

            int result_num = _numerator - other._numerator;
            return new FractionClass(result_num, lcd);
        }
        
        public override string ToString()
        {
            if (Numerator < 0 && Denominator < 0)
                return $"{Math.Abs(_numerator)}/{Math.Abs(_denominator)}";
            else if (Numerator > 0 && Denominator < 0 || Numerator < 0 && Denominator > 0)
                return $"-{Math.Abs(_numerator)}/{Math.Abs(_denominator)}";
            return $"{_numerator}/{_denominator}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is FractionClass other && Numerator == other.Numerator
                && Denominator == other.Denominator)
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return (Numerator,  Denominator).GetHashCode();
        }

        public int CompareTo(FractionClass? other)
        {
            if (other == null) return -1;
            return Numerator.CompareTo(other.Numerator) & Denominator.CompareTo(other.Denominator);
            //return Value.CompareTo(other.Value);
        }

        // operators
        public static FractionClass operator+(FractionClass _this, FractionClass other)
        {
            if (_this is null || other is null)
                throw new ArgumentNullException($"{(_this is null ? nameof(_this) : nameof(other))} is null.");
            return _this.Add(other);
        }
        public static FractionClass operator-(FractionClass _this, FractionClass other)
        {
            if (_this is null || other is null)
                throw new ArgumentNullException($"{(_this is null ? nameof(_this) : nameof(other))} is null.");
            return _this.Subtract(other);
        }
        public static FractionClass operator*(FractionClass _this, FractionClass other)
        {
            if (other.Denominator == 0 || _this.Denominator == 0)
                throw new DivideByZeroException("Division by zero not allowed.");
            return new FractionClass(
                _this.Numerator * other.Numerator,
                _this.Denominator* other.Denominator
                );
        }
        public static FractionClass operator /(FractionClass _this, FractionClass other)
        {
            if (other.Denominator == 0 || _this.Denominator == 0)
                throw new DivideByZeroException("Division by zero not allowed.");
            return new FractionClass(
                _this.Numerator * other.Denominator,
                _this.Denominator * other.Numerator
                );
        }
        public static FractionClass operator^(FractionClass _this, int exponent)
        {
            var absExponent = Math.Abs(exponent);
            var denominator = (int)Math.Pow(_this.Denominator, absExponent);
            var numerator = (int)Math.Pow(_this.Numerator, absExponent);

            if (exponent >= 0)
                return new FractionClass(numerator, denominator);
            return new FractionClass(denominator, numerator);
        }
    }
}
