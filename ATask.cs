using System.Collections.Generic;
using System.Linq;

namespace TaskManager
{
    abstract class ATask : ITask
    {
        public string Name { get; set; }
        public ITask Parent { get; set; }
        public List<ITask> Child { get; set; } = new();

        public bool MarkedAsDone = false;
        public bool IsDone() 
        {
            return MarkedAsDone && Child.All(x => x.IsDone());
        }
    }
}
