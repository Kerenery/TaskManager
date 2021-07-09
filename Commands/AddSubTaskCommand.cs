using System;
using Spectre.Console.Cli;
using Spectre.Console;
using System.ComponentModel;

namespace TaskManager.Commands
{
    public class AddSubTaskCoomand : Command<AddSubTaskCoomand.AddSubTaskSettings>
    {
        public class AddSubTaskSettings : CommandSettings
        {
            [CommandArgument(0, "<Id>")]
            [Description("no description lol")]
            public int Id { get; set; }

            [CommandArgument(1, "<Task-Info>")]
            [Description("Subtask itself")]
            public string Name { get; set; }
        }
        public override int Execute(CommandContext context, AddSubTaskSettings settings)
        {
            TaskRegistry taskRegistry = new();
            taskRegistry.Load(@"D:\Downloads\book1.json");
            try { taskRegistry.CreateSubTask(settings.Name, settings.Id); }
            catch(ArgumentException e) { AnsiConsole.WriteException(e); return -1; }
            taskRegistry.Save(@"D:\Downloads\book1.json");
            AnsiConsole.MarkupLine($"The [bold green]{settings.Name}[/] is added!");
            return 0;
        }
    }

}
