namespace TaskManager
{
    class SubTask : ATask 
    {
        public bool ParentExists { get; set; }
        public int ParentId { get; set; }
    }
}
