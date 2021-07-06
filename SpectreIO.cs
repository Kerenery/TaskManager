using Spectre.Console;
using Spectre.Console.Cli;
using TaskManager.Commands;

namespace TaskManager
{
    static class SpectreIO
    {
        private static CommandApp app = new();
        public static int Command(string[] args)
        {
            app.Configure(config =>
            {
                config.ValidateExamples();

                config.AddCommand<MyAddCommand>("/add")
                    .WithDescription("Add a todo task")
                    .WithExample(new[] { "/add", "SomeInfo" });

                config.AddCommand<AllCommand>("/all").WithDescription("See current tasks")
                .WithExample(new[] { "/all" });

                config.AddCommand<DeleteTaskCommand>("/delete-task").WithDescription("Delete task by Id")
                .WithExample(new[] { "/delete-task", "Id" });

                config.AddCommand<CompleteCommand>("/complete").WithDescription("Compete task by Id")
                .WithExample(new[] {"/complete", "Id" });

                config.AddCommand<SaveCommand>("/save").WithDescription("Save file lol")
                .WithExample(new[] { "/save", "path" });

                config.AddCommand<LoadCommand>("/load").WithDescription("Load file lol")
                .WithExample(new[] { "/load", "path"});

                config.AddCommand<CompletedCommand>("/completed").WithDescription("Mb completed")
                .WithExample(new[] { "/completed" });

                config.AddCommand<DeadlineCommand>("/deadline").WithDescription("Set deadline ex. 10/22/1987")
                .WithExample(new[] { "/deadline", "Id", "10/22/1987" });

                config.AddCommand<TodayCommand>("/today").WithDescription("U wanna see today tasks? Really?")
                .WithExample(new[] { "/today" });

                config.AddCommand<CreateGroupCommand>("/create-group").WithDescription("Create group <name>")
                .WithExample(new[] { "/create-group", "name" });

                config.AddCommand<DeleteGroupCommand>("/delete-group").WithDescription("Delete group <name>")
                .WithExample(new[] { "/delete-group", "name" });

                config.AddCommand<AddTaskToGroupCommand>("/add-to-group").WithDescription("Add Task to group")
                .WithExample(new[] { "/add-to-group", "id", "group-name" });

                config.AddCommand<DeleteTaskFromGroupCommand>("/delete-from-group").WithDescription("Delete task from group")
                .WithExample(new[] { "/delete-from-group", "id", "group-name" });

                config.AddCommand<CompletedInGroupCommand>("/completed-in-group").WithDescription("See completed tasks in group-name")
                .WithExample(new[] { "/completed-in-group", "group-name" });

                config.AddCommand<AddSubTaskCoomand>("/add-subtask").WithDescription("Add subtask to Task by id")
                .WithExample(new[] { "/add-subtask", "id"});
            });

            return app.Run(args);
        }

        

    }
}
