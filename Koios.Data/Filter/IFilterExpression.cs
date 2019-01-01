namespace Koios.Data.Filter
{
    public interface IFilterExpression<I1, I2>
    {
        void Compile(IFilterCompiler<I1, I2> compiler);
    }
}
