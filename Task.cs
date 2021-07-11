using System;

namespace TaskManager
{
    class Task : ATask
    {
        public DateTime Deadline { get; set; } = DateTime.Today;
        public bool ParentExists { get; set; }
        public int ParentId{ get; set; }
    }
}
