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
            TaskRegistry taskRegistry = new();
            taskRegistry.Load(@"D:\Downloads\book1.json");
            if (taskRegistry.ListAllTasks().Count != 0)
            {
                var tree = new Tree($"[gold1]{settings.Name}[/]")
                 .Style(Style.Parse("aqua"))
                 .Guide(TreeGuide.BoldLine);

                foreach (var kvp in taskRegistry.ListAllGroups())
                {
                    if (kvp.Value.Name == settings.Name)
                    {
                        foreach (var child in taskRegistry.ListAllChildren(kvp.Value))
                        {
                            if (taskRegistry.IsTaskDone(taskRegistry.GetId(child.Name)))
                            {
                                tree.AddNode($"[green]id[/]: [underline yellow]{ taskRegistry.GetId(child.Name)}[/] [green]Task[/]: [gold1]{child.Name}[/]" +
                                    $" [green]Deadline:[/] [gold1]{ taskRegistry.GetTaskDeadline(taskRegistry.GetId(child.Name))}[/] " +
                                    $"[green]Completed[/] {(taskRegistry.IsTaskDone(taskRegistry.GetId(child.Name)) ? "[green]Yes![/]" : "[red]No[/]!") }");
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
