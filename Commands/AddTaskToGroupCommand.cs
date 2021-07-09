using System;
using Spectre.Console.Cli;
using Spectre.Console;
using System.ComponentModel;

namespace TaskManager.Commands
{
    public class AddTaskToGroupCommand : Command<AddTaskToGroupCommand.AddTaskToGroupSettings>
    {
        public class AddTaskToGroupSettings : CommandSettings
        {
            [CommandArgument(0, "<Id of Task>")]
            [Description("Id used to identify the task")]
            public int Id { get; set; }

            [CommandArgument(1, "<Group Name>")]
            [Description("Name of the group")]
            public string Name { get; set; }
        }
        public override int Execute(CommandContext context, AddTaskToGroupSettings settings)
        {
            TaskRegistry taskRegistry = new();
            taskRegistry.Load(@"D:\Downloads\book1.json");
            try { taskRegistry.AddTaskToGroup(settings.Id, settings.Name); }
            catch (ArgumentException e) { AnsiConsole.WriteException(e); return -1; }
            taskRegistry.Save(@"D:\Downloads\book1.json");
            AnsiConsole.MarkupLine($"The [bold green]{settings.Id}[/] is added to [bold aqua]{settings.Name}![/]");
            return 0;
        }
    }

}