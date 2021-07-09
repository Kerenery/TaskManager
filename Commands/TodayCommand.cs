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
            TaskRegistry taskRegistry = new(); 
            taskRegistry.Load(@"D:\Downloads\book1.json");
            if (taskRegistry.ListAllTasks().Count != 0)
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
            }
            else
            {
                AnsiConsole.MarkupLine("Emty list of Tasks");
            }
            return 0;
        }
    }


}
