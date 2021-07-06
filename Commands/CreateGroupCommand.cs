using System;
using Spectre.Console.Cli;
using Spectre.Console;
using System.ComponentModel;

namespace TaskManager.Commands
{
    public class CreateGroupCommand : Command<CreateGroupCommand.CreateGroupSettings>
    {
        public class CreateGroupSettings : CommandSettings
        {
            [CommandArgument(0, "<NAME>")]
            [Description("Name of new group")]
            public string Name { get; set; }
        }
        public override int Execute(CommandContext context, CreateGroupSettings settings)
        {
            TaskAPI.Load(@"D:\Downloads\book1.json");
            try { TaskAPI.CreateGroup(settings.Name); }
            catch (ArgumentException e) { AnsiConsole.WriteException(e); return -1; }
            TaskAPI.Save(@"D:\Downloads\book1.json");
            AnsiConsole.MarkupLine($"The [bold green]{settings.Name}[/] is added!");
            return 0;
        }
    }

}
