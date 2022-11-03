using System.Diagnostics.CodeAnalysis;

namespace Rozen.Common
{
    /// <summary>
    ///     A calculation along with its result.
    /// </summary>
    public readonly struct Calculation
    {
        /// <summary>
        ///     The base equation executed over the converter.
        /// </summary>
        public string Equation { get; }

        /// <summary>
        ///     The equation result. NaN or default(double) if unsuccesful.
        /// </summary>
        public double Result { get; } = double.NaN;

        public Calculation(string equation, double result)
        {
            Equation = equation;

            Result = Convert.ToDouble(result);
        }

        /// <summary>
        ///     Returns a string of the equation result.
        /// </summary>
        /// <returns>
        ///     The equation result.
        /// </returns>
        public override string ToString()
            => $"{Result}";

        public override int GetHashCode()
            => Result.GetHashCode();

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is Calculation calc && calc.Result == Result)
                return true;
            return false;
        }

        public static bool operator ==(Calculation left, Calculation right)
            => left.Equals(right);

        public static bool operator !=(Calculation left, Calculation right)
            => !(left == right);
    }
}
