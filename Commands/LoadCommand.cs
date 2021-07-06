using System;
using Spectre.Console.Cli;
using Spectre.Console;
using System.ComponentModel;

namespace TaskManager.Commands
{
    public class LoadCommand : Command<LoadCommand.LoadSettings>
    {
        public class LoadSettings : CommandSettings
        {
            [CommandArgument(0, "<absolute path baby>")]
            [Description("where should I read from")]
            public string Path { get; set; }
        }
        public override int Execute(CommandContext context, LoadSettings settings)
        {
            TaskAPI.Load((@$"{settings.Path}"));
            TaskAPI.Save(@"D:\Downloads\book1.json");
            AnsiConsole.MarkupLine($"The [bold green]File[/] is saved! Hurray!");
            return 0;
        }
    }
}