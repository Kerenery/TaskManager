using System;
using Spectre.Console.Cli;
using Spectre.Console;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace TaskManager.Commands
{
    public class TodayCommand : Command
    {
        public override int Execute(CommandContext context)
        {
            TaskAPI.Load(@"D:\Downloads\book1.json");
            if (TaskAPI.ListAllTasks().Count != 0)
            {
                var table = new Table().Centered();
                
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
                        table.SimpleHeavyBorder();
                        Update(230, () => table.AddColumn("Id"));
                        Update(230, () => table.AddColumn("Task"));
                        Update(230, () => table.AddColumn("Deadline"));
                        Update(230, () => table.AddColumn("Completed"));
                        Update(230, () => table.AddColumn("SubTask completed"));
                        Update(400, () => table.Title("[[ [mediumpurple2]Today tasks![/] ]]"));
                        //IOrderedEnumerable<KeyValuePair<int, Task>> result = from task in AllTasks orderby task.Value.IsDone select task;
                        //IOrderedEnumerable<KeyValuePair<int, Task>> result = from task in TaskAPI.ListAllTasks() where task.Value is Task orderby task;
                        foreach (var kvp in TaskAPI.ListAllTasks())
                        {
                            if (kvp.Value is Task)
                            {
                                if (TaskAPI.TodayTasks(kvp.Key))
                                {
                                    Update(70, () => table.AddRow($"{kvp.Key}", $"{(TaskAPI.CompletedTask(kvp.Key) ? $"[underline green]{kvp.Value.Name}[/]" : $"[gold1]{kvp.Value.Name}[/]")}", $"{TaskAPI.GetTaskDeadline(kvp.Key)}", $"{(TaskAPI.CompletedTask(kvp.Key) ? "[green]Yes![/]" : "[red]No[/]!") }", "шо"));
                                }
                            }
                        }
                    });
            }
            else
            {
                AnsiConsole.MarkupLine("Emty list of Tasks");
            }
            return 0;
        }
    }


}
