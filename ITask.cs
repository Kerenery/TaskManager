using System.Collections.Generic;
using System.Linq;

namespace TaskManager
{ 
    interface ITask
    {
        string Name { get; set; }
        ITask Parent { get; set; }
        List<ITask> Child { get; set; } 
        bool IsDone() 
        {
            return Child.All(x => x.IsDone());
        }
    }
}
