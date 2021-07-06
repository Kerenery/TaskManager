using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace TaskManager
{
    class TaskRegistry
    {
        private Dictionary<int, ITask> AllTasks = new();
        public void Save(string @absolutePath)
        {
            using StreamWriter file = File.CreateText(absolutePath); 
            JsonSerializer serializer = new();
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            serializer.Formatting = Formatting.Indented;
            serializer.DateParseHandling = DateParseHandling.DateTime;
            serializer.Serialize(file, AllTasks);
        }

        public void Load(string @absolutePath)
        {
            if (!File.Exists(absolutePath)) return;

            using StreamReader reader = new(absolutePath);
            string json = reader.ReadToEnd();
            AllTasks = JsonConvert.DeserializeObject<Dictionary<int, ITask>>(json, new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateParseHandling = DateParseHandling.DateTime
            });
            counter = AllTasks.Keys.Max();
        }

        int counter = 0;
        public void AddITask(ITask task) => AllTasks.Add(++counter, task);

        // aboba oop zero impact 
        public bool ContainsTask(ITask task) => AllTasks.Any(x => x.Value.Name == task.Name);
        public bool ContainsTask(int Id) => AllTasks.ContainsKey(Id);
        public void DeleteTask(int Id) => AllTasks.Remove(Id);
        public void DeleteTask(ITask task) => AllTasks.Remove(AllTasks.First(kvp => kvp.Value == task).Key);

        public ITask GetTask(int Id)
        {
            if (AllTasks.ContainsKey(Id)) return AllTasks[Id];
            throw new System.ArgumentException("There is no such task");
        }

        public ITask GetTask(string name) => AllTasks.Values.FirstOrDefault(x => x.Name == name);

        public int GetId(string name) => AllTasks.FirstOrDefault(x => x.Value.Name == name).Key;

        public Dictionary<int, ITask> GetTasks() => AllTasks;
    }
}
