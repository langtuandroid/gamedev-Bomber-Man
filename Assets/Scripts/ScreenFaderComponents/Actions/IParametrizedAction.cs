namespace ScreenFaderComponents.Actions
{
    public interface IParametrizedAction
    {
        bool Completed { get; set; }

        void Execute(params object[] args);
    }
}