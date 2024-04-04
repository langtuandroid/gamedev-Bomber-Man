namespace ScreenFaderComponents.Actions
{
    public class ShowLogoAction : IAction
    {
        public bool IsLogoVisible { get; protected set; }
        public bool Completed { get; set; }

        public void Execute()
        {
            if (!Completed)
            {
                IsLogoVisible = !IsLogoVisible;
                Completed = true;
            }
        }
    }
}