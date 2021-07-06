using System;
using Spectre.Console.Cli;
using Spectre.Console;
using System.ComponentModel;

namespace TaskManager.Commands
{
    public class DeadlineCommand : Command<DeadlineCommand.DeadlineSettings>
    {
        public class DeadlineSettings : CommandSettings
        {
            [CommandArgument(0, "<Id of a task>")]
            [Description("id of a task to set deadline")]
            public int Id { get; set; }
            
            [CommandArgument(1, "<time limit получается>")]
            [Description("Add deadline pls")]
            public DateTime Deadline { get; set; }
        }
        public override int Execute(CommandContext context, DeadlineSettings settings)
        {
            TaskAPI.Load(@"D:\Downloads\book1.json");
            try { TaskAPI.TaskDeadline(settings.Id, settings.Deadline); }
            catch (ArgumentException e) { AnsiConsole.WriteException(e); return -1; }
            TaskAPI.Save(@"D:\Downloads\book1.json");
            AnsiConsole.MarkupLine($"The [bold red] deadline [/] is added!");
            return 0;
        }
    }

}
