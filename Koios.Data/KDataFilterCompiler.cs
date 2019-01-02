using Koios.Data.Filter;
using System;
using System.Data.Common;

namespace Koios.Data
{
    public class KDataFilterCompiler : IFilterCompiler<string, object>
    {
        private readonly DbCommand cmd;

        public KDataFilterCompiler(DbCommand cmd)
        {
            this.cmd = cmd;
        }

        public void Compile(Condition<string, object> condition)
        {
            cmd.CommandText += " ";
            cmd.CommandText += condition.Item1;

            if (condition.Item2 == null)
            {
                switch (condition.Comparer)
                {
                    case FilterConditionOperator.EQ:
                        cmd.CommandText += " IS NULL";
                        return;
                    case FilterConditionOperator.NE:
                        cmd.CommandText += " IS NOT NULL";
                        break;
                    default:
                        throw new ArgumentNullException(condition.Comparer.ToString());
                }
                return;
            }

            switch (condition.Comparer)
            {
                case FilterConditionOperator.EQ:
                    cmd.CommandText += " = ";
                    break;
                case FilterConditionOperator.NE:
                    cmd.CommandText += " <> ";
                    break;
                case FilterConditionOperator.GT:
                    cmd.CommandText += " > ";
                    break;
                case FilterConditionOperator.GE:
                    cmd.CommandText += " >= ";
                    break;
                case FilterConditionOperator.LT:
                    cmd.CommandText += " < ";
                    break;
                case FilterConditionOperator.LE:
                    cmd.CommandText += " <= ";
                    break;
                case FilterConditionOperator.LK:
                    cmd.CommandText += " LIKE ";
                    break;
                case FilterConditionOperator.NL:
                    cmd.CommandText += " NOT LIKE ";
                    break;
                default:
                    throw new NotSupportedException(condition.Comparer.ToString());
            }
            cmd.CommandText += "?";
            cmd.Parameters.Add(condition.Item2 ?? DBNull.Value);
        }

        public void Compile(Composition<string, object> composition)
        {
            cmd.CommandText += "(";
            composition.Item1.Compile(this);
            switch (composition.Combiner)
            {
                case FilterCompositionOperator.AND:
                    cmd.CommandText += " AND ";
                    break;
                case FilterCompositionOperator.OR:
                    cmd.CommandText += " OR ";
                    break;
                default:
                    throw new NotSupportedException(composition.Combiner.ToString());
            }
            composition.Item1.Compile(this);
            cmd.CommandText += ")";
        }
    }
}
