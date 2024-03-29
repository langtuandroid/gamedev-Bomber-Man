namespace ScreenFaderComponents.Actions
{
	public class ShowLogoAction : IAction
	{
		public bool Completed { get; set; }

		public bool IsLogoVisible { get; protected set; }

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
