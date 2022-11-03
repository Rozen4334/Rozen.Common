using Disqord.Bot.Commands;
using Qmmands;
using System.Text.RegularExpressions;

namespace Common.Disqord
{
    public class CalculationParser : DiscordTypeParser<Calculation>
    {
        private readonly Regex _charEscape;

        public CalculationParser()
            => _charEscape = new(@"[a-z]", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public override ValueTask<ITypeParserResult<Calculation>> ParseAsync(IDiscordCommandContext context, IParameter parameter, ReadOnlyMemory<char> value)
        {
            string equation = value.Span.ToString();

            if (string.IsNullOrEmpty(equation))
                return Failure("The expression cannot be empty.");

            if (_charEscape.IsMatch(equation))
                return Failure("The expression can only contain numbers and operators.");

            try
            {
                var result = new System.Data.DataTable()
                    .Compute(equation, string.Empty);

                if (result == DBNull.Value)
                    return Failure("The result of this calculation is not a number.");

                return Success(new Calculation(equation, Convert.ToDouble(result)));
            }
            catch (Exception ex)
            {
                return Failure(ex.Message);
            }
        }
    }
}
