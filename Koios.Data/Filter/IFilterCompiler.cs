namespace Koios.Data.Filter
{
    public interface IFilterCompiler<I1, I2>
    {
        void Compile(Condition<I1, I2> condition);

        void Compile(Composition<I1, I2> composition);
    }
}
