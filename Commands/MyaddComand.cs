using System;
using Spectre.Console.Cli;
using Spectre.Console;
using System.ComponentModel;

namespace TaskManager.Commands
{
    public class MyAddCommand : Command<MyAddCommand.AddSettings>
    {
        public class AddSettings : CommandSettings
        {
            [CommandArgument(0, "<task-info>")]
            [Description("Add Task pls")]
            public string Name { get; set; }
        }
        public override int Execute(CommandContext context, AddSettings settings)
        {
            TaskRegistry taskRegistry = new();
            taskRegistry.Load(@"D:\Downloads\book1.json");
            try { taskRegistry.CreateTask(settings.Name); }
            catch (ArgumentException e) { AnsiConsole.WriteException(e); return -1; }
            taskRegistry.Save(@"D:\Downloads\book1.json");
            AnsiConsole.MarkupLine($"The [bold green]{settings.Name}[/] is added!");
            return 0;
        }
    }

}
