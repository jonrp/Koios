namespace Koios.Data.Filter
{
    public class Composition<I1, I2> : IFilterExpression<I1, I2>
    {
        public readonly IFilterExpression<I1, I2> Item1;
        public readonly FilterCompositionOperator Combiner;
        public readonly IFilterExpression<I1, I2> Item2;

        public Composition(IFilterExpression<I1, I2> item1, FilterCompositionOperator combiner, IFilterExpression<I1, I2> item2)
        {
            Item1 = item1;
            Combiner = combiner;
            Item2 = item2;
        }

        public void Compile(IFilterCompiler<I1, I2> compiler)
        {
            compiler.Compile(this);
        }
    }

    public static class Composition
    {
        public static IFilterExpression<I1, I2> And<I1, I2>(this IFilterExpression<I1, I2> e1, IFilterExpression<I1, I2> e2)
        {
            return new Composition<I1, I2>(e1, FilterCompositionOperator.AND, e2);
        }

        public static IFilterExpression<I1, I2> Or<I1, I2>(this IFilterExpression<I1, I2> e1, IFilterExpression<I1, I2> e2)
        {
            return new Composition<I1, I2>(e1, FilterCompositionOperator.OR, e2);
        }
    }
}
