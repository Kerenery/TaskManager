using System.Collections.Generic;

namespace TaskManager
{
    class Group : ITask
    {
        public string Name { get; set; }
        public ITask Parent { get => null; set => throw new System.ArgumentException("Group cannot have parent"); }
        public List<ITask> Child { get; set; } = new();
        public List<int> ChildrenId { get; set; } = new();
    }
}
