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
                Display.DisplayToday(taskRegistry);
            }
            else
            {
                AnsiConsole.MarkupLine("Emty list of Tasks");
            }
            return 0;
        }
    }


}
