using System;
using Spectre.Console.Cli;
using Spectre.Console;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace TaskManager.Commands
{
    public class CompletedInGroupCommand : Command<CompletedInGroupCommand.CompletedInGroupSettings>
    {
        public class CompletedInGroupSettings : CommandSettings
        {
            [CommandArgument(0, "<Group name>")]
            public string Name { get; set; }
        }
        public override int Execute(CommandContext context, CompletedInGroupSettings settings)
        {
            TaskAPI.Load(@"D:\Downloads\book1.json");
            if (TaskAPI.ListAllTasks().Count != 0)
            {
                var tree = new Tree($"[gold1]{settings.Name}[/]")
                 .Style(Style.Parse("aqua"))
                 .Guide(TreeGuide.BoldLine);

                foreach (var kvp in TaskAPI.ListAllTasks())
                {
                    if (kvp.Value is Group && kvp.Value.Name == settings.Name)
                    {
                        foreach (var child in kvp.Value.Child)
                        {
                            if (TaskAPI.CompletedTask(TaskAPI.GetId(child.Name)))
                            {
                                tree.AddNode($"[green]id[/]: [underline yellow]{TaskAPI.GetId(child.Name)}[/] [green]Task[/]: [gold1]{child.Name}[/] [green]Deadline:[/] [gold1]{TaskAPI.GetTaskDeadline(TaskAPI.GetId(child.Name))}[/] [green]Completed[/] {(TaskAPI.CompletedTask(TaskAPI.GetId(child.Name)) ? "[green]Yes![/]" : "[red]No[/]!") }");
                            }
                        }
                    }
                }
                AnsiConsole.Render(tree);
            }
            else
            {
                AnsiConsole.MarkupLine("Emty list of Tasks");
            }
            return 0;
        }
    }
}
