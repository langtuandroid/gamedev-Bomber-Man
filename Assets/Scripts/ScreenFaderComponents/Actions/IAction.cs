namespace ScreenFaderComponents.Actions
{
	public interface IAction
	{
		bool Completed { get; set; }

		void Execute();
	}
}
