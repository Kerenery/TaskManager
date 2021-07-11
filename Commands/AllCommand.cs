
using Spectre.Console.Cli;
using Spectre.Console;


namespace TaskManager.Commands
{
    public class AllCommand : Command
    {
        
        public override int Execute(CommandContext context)
        {
            TaskRegistry t = new();

            t.Load(@"D:\Downloads\book1.json");

            if (t.ListAllTasks().Count == 0)
            {
                AnsiConsole.MarkupLine("[red]List of all tasks is empty[/]");
            }

            else
            {
                Display.DisplayTable(t);
                // groups
                Display.DisplayTree(t);
            }

            return 0;
        }
    }
}
