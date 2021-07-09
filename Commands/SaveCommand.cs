using System;
using Spectre.Console.Cli;
using Spectre.Console;
using System.ComponentModel;

namespace TaskManager.Commands
{
    public class SaveCommand : Command<SaveCommand.SaveSettings>
    {
        public class SaveSettings : CommandSettings
        {
            [CommandArgument(0, "<absolute path baby>")]
            [Description("where should I save this")]
            public string Path { get; set; }
        }
        public override int Execute(CommandContext context, SaveSettings settings)
        {
            TaskRegistry taskRegistry = new();
            taskRegistry.Load(@"D:\Downloads\book1.json");
            taskRegistry.Save(@$"{settings.Path}");
            AnsiConsole.MarkupLine($"The [bold green]File[/] is saved! Hurray!");
            return 0;
        }
    }
}