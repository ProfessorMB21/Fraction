using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Fraction
{
    public class FractionClass
    {
        private int _numerator;
        private int _denominator;
        private int _wholeNum = 0;
        public int Numerator => _numerator;
        public int Denominator => _denominator;

        public FractionClass(int numerator, int denominator) 
        {
            if (denominator == 0) 
                throw new ArgumentException($"{nameof(denominator)} cannot be zero.");
            _numerator = numerator;
            _denominator = denominator;
        }
        public FractionClass(int numerator, int denominator, int wholeNum)
        {
            if (denominator == 0)
                throw new ArgumentException($"{nameof(denominator)} cannot be zero.");
            _numerator = numerator;
            _denominator = denominator;
            _wholeNum = wholeNum;
        }
        public FractionClass() { }
        private FractionClass AddWithSameDenominator(FractionClass other)
        {
            var numerator = _numerator + other._numerator;
            return new FractionClass(numerator, _denominator);
        }
        private int gcd(int a, int b)
        {
            if (a == 0) return b;
            return gcd(b % a, a);
        }
        private int lcm(int a, int b) => (a * b) / gcd(a, b);
        private FractionClass AddWithDiffDenominator(FractionClass other)
        {
            int lcd = lcm(this.Denominator, other.Denominator);

            _numerator *= (lcd / this.Denominator);
            other._numerator *= (lcd / other.Denominator);

            int result_num = _numerator + other._numerator;
            return new FractionClass(result_num, lcd);
        }
        public FractionClass Add(FractionClass other)
        {
            if (Denominator == other.Denominator)
                return AddWithSameDenominator(other);
            return AddWithDiffDenominator(other);
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

        // operators
        
    }
}
