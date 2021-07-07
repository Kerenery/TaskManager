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
            TaskAPI.Load(@"D:\Downloads\book1.json");
            if (TaskAPI.ListAllTasks().Count != 0)
            {
                var table = new Table().Centered();
                //var grouptable = new Table().Centered();

                AnsiConsole.Live(table)
                    .AutoClear(false)
                    .Overflow(VerticalOverflow.Ellipsis)
                    .Cropping(VerticalOverflowCropping.Top)
                    .Start(ctx =>
                    {
                        void Update(int delay, Action action)
                        {
                            action();
                            ctx.Refresh();
                            Thread.Sleep(delay);
                        }
                        table.BorderColor(Color.DarkOliveGreen3_2);
                        //table.SimpleHeavyBorder();
                        Update(0, () => table.AddColumn("Id"));
                        Update(0, () => table.AddColumn("Task"));
                        Update(0, () => table.AddColumn("Deadline"));
                        Update(0, () => table.AddColumn("Completed"));
                        Update(0, () => table.AddColumn("SubTasks").Centered());
                        Update(0, () => table.Title("[[ [mediumpurple2]To do task list, hurry up![/] ]]"));

                        foreach (KeyValuePair<int, Task> kvp in TaskAPI.ListAllTasks())
                        {
                            if (!TaskAPI.ParentExists(kvp.Key))
                            {
                                var subtable = new Table()
                                    .Border(TableBorder.Rounded)
                                    .BorderColor(Color.Green)
                                    .AddColumn(new TableColumn("[u]Id[/]"))
                                    .AddColumn(new TableColumn("[u]Task[/]"))
                                    .AddColumn(new TableColumn("[u]Completed[/]"));
                                subtable.BorderColor(Color.DarkOliveGreen3_2);
                                //subtable.SimpleHeavyBorder();
                                foreach (var sub in kvp.Value.Child)
                                {
                                    subtable.AddRow(new Markup($"[underline yellow]{TaskAPI.GetId(sub.Name)}[/]"),
                                        new Markup($"{(TaskAPI.CompletedTask(TaskAPI.GetId(sub.Name)) ? $"[underline green]{sub.Name}[/]" : $"[gold1]{sub.Name}[/]")}"),
                                        new Markup($"{(TaskAPI.CompletedTask(TaskAPI.GetId(sub.Name)) ? "[green]Yes![/]" : "[red]No![/]") }"));
                                }
                                //Update(230, () => table.AddRow($"[underline yellow]{kvp.Key}[/]",
                                //    $"{(TaskAPI.CompletedTask(kvp.Key) ? $"[underline green]{kvp.Value.Name}[/]" : $"[gold1]{kvp.Value.Name}[/]")}",
                                //    $"{TaskAPI.GetTaskDeadline(kvp.Key)}",
                                //    $"{(TaskAPI.CompletedTask(kvp.Key) ? "[green]Yes![/]" : "[red]No[/]!") }",
                                //    $"{new Panel("Дождь в окно постучит, это я")}"));
                                Update(0, () => table.AddRow(new Markup($"[underline yellow]{kvp.Key}[/]"),
                                    new Markup($"{(TaskAPI.CompletedTask(kvp.Key) ? $"[underline green]{kvp.Value.Name}[/]" : $"[gold1]{kvp.Value.Name}[/]")}"),
                                    new Markup($"[aqua]{TaskAPI.GetTaskDeadline(kvp.Key)}[/]"),
                                    new Markup($"{(TaskAPI.CompletedTask(kvp.Key) ? "[green]Yes![/]" : "[red]No![/]") }"),
                                    subtable));
                            }
                        }
                    });

                var tree = new Tree("[gold1]Groups[/]")
                 .Style(Style.Parse("aqua"))
                 .Guide(TreeGuide.BoldLine);

                foreach (var kvp in TaskAPI.ListAllGroups())
                {
                    var groupnode = tree.AddNode($"[green]{kvp.Value.Name}[/]");
                    foreach (var child in TaskAPI.ListAllChildren(kvp.Value))
                    {
                        groupnode.AddNode($"[green]id[/]: [underline yellow]{TaskAPI.GetId(child.Name)}[/] [green]Task[/]: [gold1]{child.Name}[/] [green]Deadline:[/] [gold1]{TaskAPI.GetTaskDeadline(TaskAPI.GetId(child.Name))}[/] [green]Completed[/] {(TaskAPI.CompletedTask(TaskAPI.GetId(child.Name)) ? "[green]Yes![/]" : "[red]No[/]!") }");
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
