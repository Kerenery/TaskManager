using System;
using Spectre.Console.Cli;
using Spectre.Console;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager.Commands
{
    public class AllCommand : Command
    {
        public override int Execute(CommandContext context)
        {
            TaskRegistry t = new();

            t.Load(@"D:\Downloads\book1.json");

            if (t.ListAllTasks().Count == 0)
            {
                AnsiConsole.MarkupLine("[red]List of all tasks is empty[/]");
            }

            else
            {
                AnsiConsole.WriteLine("1");
                var table = new Table().Centered();


                table.AddColumn("Id");
                table.AddColumn("Task");
                table.AddColumn("Deadline");
                table.AddColumn("Completed");
                table.AddColumn("SubTasks").Centered();
                table.Title("[[ [mediumpurple2]To do task list, hurry up![/] ]]");

                foreach (KeyValuePair<int, Task> kvp in t.ListAllTasks())
                {
                    if (!t.ParentExists(kvp.Key))
                    {

                        var subtable = new Table()
                            .Border(TableBorder.Rounded)
                            .BorderColor(Color.Green)
                            .AddColumn(new TableColumn("[u]Id[/]"))
                            .AddColumn(new TableColumn("[u]Task[/]"))
                            .AddColumn(new TableColumn("[u]Completed[/]"));
                        subtable.BorderColor(Color.DarkOliveGreen3_2);

                        foreach (var sub in kvp.Value.Child)
                        {
                            subtable.AddRow(new Markup($"[underline yellow]{t.GetId(sub.Name)}[/]"),
                                new Markup($"{(t.IsTaskDone(t.GetId(sub.Name)) ? $"[underline green]{sub.Name}[/]" : $"[gold1]{sub.Name}[/]")}"),
                                new Markup($"{(t.IsTaskDone(t.GetId(sub.Name)) ? "[green]Yes![/]" : "[red]No![/]") }"));
                        }

                        table.AddRow(new Markup($"[underline yellow]{kvp.Key}[/]"),
                             new Markup($"{(t.IsTaskDone(kvp.Key) ? $"[underline green]{kvp.Value.Name}[/]" : $"[gold1]{kvp.Value.Name}[/]")}"),
                             new Markup($"[aqua]{t.GetTaskDeadline(kvp.Key)}[/]"),
                             new Markup($"{(t.IsTaskDone(kvp.Key) ? "[green]Yes![/]" : "[red]No![/]") }"),
                             subtable);
                    }
                };

                AnsiConsole.Render(table);

                var tree = new Tree("[gold1]Groups[/]")
                 .Style(Style.Parse("aqua"))
                 .Guide(TreeGuide.BoldLine);

                foreach (var kvp in t.ListAllGroups())
                {
                    var groupnode = tree.AddNode($"[green]{kvp.Value.Name}[/]");
                    foreach (var child in t.ListAllChildren(kvp.Value))
                    {
                        groupnode.AddNode($"[green]id[/]: [underline yellow]{t.GetId(child.Name)}[/] [green]Task[/]: [gold1]{child.Name}[/]" +
                            $" [green]Deadline:[/] [gold1]{t.GetTaskDeadline(t.GetId(child.Name))}[/] " +
                            $"[green]Completed[/] {(t.IsTaskDone(t.GetId(child.Name)) ? "[green]Yes![/]" : "[red]No[/]!") }");
                    }

                }
                AnsiConsole.Render(tree);
            }

            return 0;

        }
    }
}
