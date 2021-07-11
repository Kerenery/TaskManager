using Spectre.Console.Cli;
using Spectre.Console;

namespace TaskManager.Commands
{
    public class CompletedCommand : Command
    {
        public override int Execute(CommandContext context)
        {
            TaskRegistry taskRegistry = new();
            taskRegistry.Load(@"D:\Downloads\book1.json");
            if (taskRegistry.ListAllTasks().Count != 0)
            {
                Display.DisplayComleted(taskRegistry);
            }
            else
            {
                AnsiConsole.MarkupLine("Emty list of Tasks");
            }
            return 0;
        }
    }


}

