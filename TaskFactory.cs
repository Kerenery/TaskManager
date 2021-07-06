using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager
{
    class TaskFactory : ATaskFactory
    {

        private TaskRegistry taskRegistry;
        public TaskFactory(TaskRegistry aTaskRegistry)
        {
            taskRegistry = aTaskRegistry;
        }

        public override ITask CreateTask(string message, DateTime deadline, ITask parent = null, IEnumerable<ITask> child = null)
        {
            Task task = new() { Name = message, Deadline = deadline, Parent = parent, Child = child.ToList() };
            taskRegistry.AddITask(task);
            return task;
        }

        public override ITask CreateSubTask(string message, ITask parent = null, IEnumerable<ITask> child = null)
        {
            SubTask subtask = new() { Name = message, Parent = parent, Child = child.ToList() };
            return subtask; 
        }

        public override ITask CreateGroup(string name, IEnumerable<ITask> child = null)
        {
            Group group = new() { Name = name, Child = child.ToList() };
            return group;
        }
         
    }
}
