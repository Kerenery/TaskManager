using System;
using Spectre.Console.Cli;
using Spectre.Console;
using System.ComponentModel;

namespace TaskManager.Commands
{
    public class DeleteTaskCommand : Command<DeleteTaskCommand.Settings>
    {
        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<ID>")]
            [Description("Id used to remove task.")]
            public int Id { get; set; }
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            TaskRegistry taskRegistry = new();
            taskRegistry.Load(@"D:\Downloads\book1.json");
            try { taskRegistry.DeleteTask(settings.Id); }
            catch (ArgumentException e) { AnsiConsole.WriteException(e); return -1; }
            taskRegistry.Save(@"D:\Downloads\book1.json");
            AnsiConsole.MarkupLine($"[bold]Delete Task =>[/] id[[{settings.Id}]]");
            return 0;
        }
    }
}
