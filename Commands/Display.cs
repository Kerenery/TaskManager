using System;
using Spectre.Console.Cli;
using Spectre.Console;

namespace TaskManager.Commands
{
    static class Display
    {
        static public void DisplayTree(TaskRegistry taskRegistry)
        {
            var tree = new Tree("[gold1]Groups[/]")
                .Style(Style.Parse("aqua"))
                .Guide(TreeGuide.BoldLine);

            foreach (var kvp in taskRegistry.ListAllGroups())
            {
                var groupnode = tree.AddNode($"[green]{kvp.Value.Name}[/]");
                foreach (var child in taskRegistry.ListAllChildren(kvp.Value))
                {
                    groupnode.AddNode($"[green]id[/]: [underline yellow]{taskRegistry.GetId(child.Name)}[/] [green]Task[/]: [gold1]{child.Name}[/]" +
                        $" [green]Deadline:[/] [gold1]{taskRegistry.GetTaskDeadline(taskRegistry.GetId(child.Name))}[/] " +
                        $"[green]Completed[/] {(taskRegistry.IsTaskDone(taskRegistry.GetId(child.Name)) ? "[green]Yes![/]" : "[red]No[/]!") }");
                }

            }
            AnsiConsole.Render(tree);
        }

        static public void DisplayTable(TaskRegistry taskRegistry)
        {
            var table = new Table().Centered();

            table.AddColumn("Id");
            table.AddColumn("Task");
            table.AddColumn("Deadline");
            table.AddColumn("Completed");
            table.AddColumn("SubTasks").Centered();
            table.Title("[[ [mediumpurple2]To do task list, hurry up![/] ]]");

            foreach (var kvp in taskRegistry.ListAllTasks())
            {
                if (!taskRegistry.ParentExists(kvp.Key))
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
                        subtable.AddRow(new Markup($"[underline yellow]{taskRegistry.GetId(sub.Name)}[/]"),
                            new Markup($"{(taskRegistry.IsTaskDone(taskRegistry.GetId(sub.Name)) ? $"[underline green]{sub.Name}[/]" : $"[gold1]{sub.Name}[/]")}"),
                            new Markup($"{(taskRegistry.IsTaskDone(taskRegistry.GetId(sub.Name)) ? "[green]Yes![/]" : "[red]No![/]") }"));
                    }

                    table.AddRow(new Markup($"[underline yellow]{kvp.Key}[/]"),
                         new Markup($"{(taskRegistry.IsTaskDone(kvp.Key) ? $"[underline green]{kvp.Value.Name}[/]" : $"[gold1]{kvp.Value.Name}[/]")}"),
                         new Markup($"[aqua]{taskRegistry.GetTaskDeadline(kvp.Key)}[/]"),
                         new Markup($"{(taskRegistry.IsTaskDone(kvp.Key) ? "[green]Yes![/]" : "[red]No![/]") }"),
                         subtable);
                }
            };

            AnsiConsole.Render(table);
        }
        
        static public void DisplayToday(TaskRegistry taskRegistry)
        {
            var table = new Table().Centered();


            table.BorderColor(Color.DarkOliveGreen3_2);
            table.SimpleHeavyBorder();
            table.AddColumn("Id");
            table.AddColumn("Task");
            table.AddColumn("Deadline");
            table.AddColumn("Completed");
            table.Title("[[ [mediumpurple2]Today tasks![/] ]]");

            foreach (var kvp in taskRegistry.ListAllTasks())
            {
                if (kvp.Value is Task)
                {
                    if (taskRegistry.TodayTasks(kvp.Key))
                    {
                        table.AddRow($"{kvp.Key}", $"{(taskRegistry.IsTaskDone(kvp.Key) ? $"[underline green]{kvp.Value.Name}[/]" : $"[gold1]{kvp.Value.Name}[/]")}",
                            $"{taskRegistry.GetTaskDeadline(kvp.Key)}", $"{(taskRegistry.IsTaskDone(kvp.Key) ? "[green]Yes![/]" : "[red]No[/]!") }");
                    }
                }
            };

            AnsiConsole.Render(table);
        }

        public static void DisplayComleted(TaskRegistry taskRegistry)
        {
            var table = new Table().Centered();

            table.BorderColor(Color.DarkOliveGreen3_2);
            table.SimpleHeavyBorder();
            table.AddColumn("Id");
            table.AddColumn("Task");
            table.AddColumn("Deadline");
            table.AddColumn("Completed");
            table.Title("[[ [mediumpurple2]Completed tasks btw![/] ]]");

            foreach (var kvp in taskRegistry.ListAllTasks())
            {
                if (kvp.Value is Task)
                {
                    if (taskRegistry.IsTaskDone(kvp.Key))
                    {
                        table.AddRow($"{kvp.Key}", $"{(taskRegistry.IsTaskDone(kvp.Key) ? $"[underline green]{kvp.Value.Name}[/]" : $"[gold1]{kvp.Value.Name}[/]")}",
                            $"{taskRegistry.GetTaskDeadline(kvp.Key)}", $"{(taskRegistry.IsTaskDone(kvp.Key) ? "[green]Yes![/]" : "[red]No[/]!") }");
                    }
                }
            };

            AnsiConsole.Render(table);
        }
    }
}
