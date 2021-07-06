using System;
using Spectre.Console.Cli;
using Spectre.Console;
using System.ComponentModel;

namespace TaskManager.Commands
{
    public class DeleteTaskFromGroupCommand : Command<DeleteTaskFromGroupCommand.DeleteTaskFromGroupSettings>
    {
        public class DeleteTaskFromGroupSettings : CommandSettings
        {
            [CommandArgument(0, "<Id of Task>")]
            [Description("Id used to identify the task")]
            public int Id { get; set; }

            [CommandArgument(1, "<Group Name>")]
            [Description("Name of the group")]
            public string Name { get; set; }
        }
        public override int Execute(CommandContext context, DeleteTaskFromGroupSettings settings)
        {
            TaskAPI.Load(@"D:\Downloads\book1.json");
            try { TaskAPI.DeleteFromGroup(settings.Id, settings.Name); }
            catch (ArgumentException e) { AnsiConsole.WriteException(e); return -1; }
            try { TaskAPI.DeleteTask(settings.Id); }
            catch (ArgumentException e) { AnsiConsole.WriteException(e); return -1; }
            TaskAPI.Save(@"D:\Downloads\book1.json");
            AnsiConsole.MarkupLine($"The [bold green]{settings.Id}[/] is deleted from [bold aqua]{settings.Name}![/]");
            return 0;
        }
    }
}
