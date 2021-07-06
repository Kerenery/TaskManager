using System;
using Spectre.Console.Cli;
using Spectre.Console;
using System.ComponentModel;

namespace TaskManager.Commands
{
    public class CompleteCommand : Command<CompleteCommand.CompleteSettings>
    {
        public class CompleteSettings : CommandSettings
        { 
            [CommandArgument(0, "<Id>")]
            [Description("Id pls shshshs")]
            public int Id { get; set; }
        }
        public override int Execute(CommandContext context, CompleteSettings settings)
        {
            TaskAPI.Load(@"D:\Downloads\book1.json");
            try { TaskAPI.CompleteById(settings.Id); }
            catch (ArgumentException e) { AnsiConsole.WriteException(e); return -1; }
            TaskAPI.Save(@"D:\Downloads\book1.json");
            AnsiConsole.MarkupLine($"The [bold green]Task =>[/] id[[{settings.Id}]] is completed! Hurray!");
            return 0;
        }
    }

}
