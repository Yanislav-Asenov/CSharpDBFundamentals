namespace TeamBuilder.Client.Console
{
    using TeamBuilder.Application.Core;

    public class Application
    {
        public static void Main()
        {
            Engine engine = new Engine(new CommandDispatcher());
            engine.Run();
        }
    }
}
