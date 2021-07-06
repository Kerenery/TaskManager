using System;
using Spectre.Console.Cli;
using Spectre.Console;
using System.ComponentModel;

namespace TaskManager.Commands
{
    public class DeleteGroupCommand : Command<DeleteGroupCommand.DeleteGroupSettings>
    {
        public class DeleteGroupSettings : CommandSettings
        {
            [CommandArgument(0, "<NAME>")]
            [Description("name used to remove group.")]
            public string Name { get; set; }
        }

        public override int Execute(CommandContext context, DeleteGroupSettings settings)
        {
            TaskAPI.Load(@"D:\Downloads\book1.json");
            try { TaskAPI.DeleteGroup(settings.Name); }
            catch (ArgumentException e) { AnsiConsole.WriteException(e); return -1; }
            TaskAPI.Save(@"D:\Downloads\book1.json");
            AnsiConsole.MarkupLine($"[bold]Delete Group =>[/] name[[{settings.Name}]]");
            return 0;
        }
    }
}