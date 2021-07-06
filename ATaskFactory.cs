using System;
using System.Collections.Generic;

namespace TaskManager
{
    abstract class ATaskFactory
    {
        public abstract ITask CreateTask(string message, DateTime deadline, ITask parent = null, IEnumerable<ITask> child = null);
        public abstract ITask CreateSubTask(string message, ITask parent = null, IEnumerable<ITask> child = null);
        public abstract ITask CreateGroup(string name, IEnumerable<ITask> child = null);

    }

}
