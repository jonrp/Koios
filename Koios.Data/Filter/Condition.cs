namespace Koios.Data.Filter
{
    public class Condition<I1, I2> : IFilterExpression<I1, I2>
    {
        public readonly I1 Item1;
        public readonly FilterConditionOperator Comparer;
        public readonly I2 Item2;

        public Condition(I1 item1, FilterConditionOperator comparer, I2 item2)
        {
            Item1 = item1;
            Comparer = comparer;
            Item2 = item2;
        }

        public void Compile(IFilterCompiler<I1, I2> compiler)
        {
            compiler.Compile(this);
        }
    }

    public struct Condition<I1>
    {
        private readonly I1 item1;

        public Condition(I1 item1)
        {
            this.item1 = item1;
        }

        public IFilterExpression<I1, I2> Equals<I2>(I2 item2) => new Condition<I1, I2>(item1, FilterConditionOperator.EQ, item2);
        public IFilterExpression<I1, I2> NotEquals<I2>(I2 item2) => new Condition<I1, I2>(item1, FilterConditionOperator.NE, item2);
        public IFilterExpression<I1, I2> GreaterThan<I2>(I2 item2) => new Condition<I1, I2>(item1, FilterConditionOperator.GT, item2);
        public IFilterExpression<I1, I2> GreaterOrEquals<I2>(I2 item2) => new Condition<I1, I2>(item1, FilterConditionOperator.GE, item2);
        public IFilterExpression<I1, I2> LessThan<I2>(I2 item2) => new Condition<I1, I2>(item1, FilterConditionOperator.LT, item2);
        public IFilterExpression<I1, I2> LessOrEquals<I2>(I2 item2) => new Condition<I1, I2>(item1, FilterConditionOperator.LE, item2);
        public IFilterExpression<I1, I2> Like<I2>(I2 item2) => new Condition<I1, I2>(item1, FilterConditionOperator.LK, item2);
        public IFilterExpression<I1, I2> NotLike<I2>(I2 item2) => new Condition<I1, I2>(item1, FilterConditionOperator.NL, item2);
    }

    public static class Condition
    {
        public static Condition<T> That<T>(T item1) => new Condition<T>(item1);
    }
}
