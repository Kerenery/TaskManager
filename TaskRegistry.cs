using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System;

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

        public void CreateTask(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException("Message cant be null or white space");

            Task task = new() { Name = message };

            if (ContainsTask(task)) throw new ArgumentException("Task cant be added twice");

            AddITask(task);
        }

        public void CreateGroup(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cant be null or white space");

            Group group = new() { Name = name };

            AddITask(group);
        }

        public void DeleteGroup(string name)
        {
            if (GetTask(name) is Group group) DeleteTask(group);
        }

        public void AddTaskToGroup(int Id, string groupName)
        {
            if (GetTask(groupName) is Group) MarkParent(Id, groupName);
        }

        public void DeleteFromGroup(int Id, string groupName)
        {
            if (GetTask(groupName) is Group group)
            {
                group.ChildrenId.Remove(Id);
            }
        }

        public void CreateSubTask(string message, int ParentId)
        {
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException("Empty subtask");

            SubTask subtask = new() { Name = message, ParentExists = true, ParentId = ParentId };

            GetTask(ParentId).Child.Add(subtask);

            AddITask(subtask);
        }

        public void CompleteById(int Id)
        {
            if (GetTask(Id) is Task t)
            {
                t.MarkedAsDone = true;
            }

            if (GetTask(Id) is SubTask st)
            {
                st.MarkedAsDone = true;
            }
        }

        public void TaskDeadline(int Id, DateTime deadline)
        {
            if (GetTask(Id) is Task t) t.Deadline = deadline;
        }

        public DateTime GetTaskDeadline(int Id)
        {
            if (GetTask(Id) is Task t) return t.Deadline;
            return DateTime.Now;
        }

        public void MarkParent(int Id, string Name)
        {
            if (GetTask(Id) is Task t)
            {
                t.ParentExists = true; t.ParentId = GetId(Name);
            }
            if (GetTask(Name) is Group g)
            {
                g.ChildrenId.Add(Id);
            }
        }

        public bool ParentExists(int Id)
        {
            if (GetTask(Id) is Task t) return t.ParentExists;
            return false;
        }

        public Dictionary<int, Task> ListAllTasks()
        {
            return GetTasks().Where(x => (x.Value is Task)).OrderBy(x => (x.Value as Task).MarkedAsDone).ToDictionary(x => x.Key, x => x.Value as Task);
        }

        public Dictionary<int, Group> ListAllGroups()
        {
            return GetTasks().Where(x => (x.Value is Group)).ToDictionary(x => x.Key, x => x.Value as Group);
        }

        public Dictionary<int, SubTask> ListAllSubTasks()
        {
            return GetTasks().Where(x => (x.Value is SubTask)).OrderBy(x => (x.Value as SubTask).MarkedAsDone).ToDictionary(x => x.Key, x => x.Value as SubTask);
        }

        public List<Task> ListAllChildren(Group group)
        {
            List<Task> list = new();
            foreach (int id in group.ChildrenId)
            {
                list.Add(GetTask(id) as Task);
            }
            return list.Select(x => x).OrderBy(x => x.MarkedAsDone).ToList();
        }
        public static List<int> ListAllChildrenId(Group group)
        {
            return group.ChildrenId;
        }

        public void SetParent(int Id, string groupname)
        {
            if (GetTask(Id) is Task t) t.Parent = GetTask(groupname);
        }

        public bool IsTaskDone(int Id)
        {
            if (GetTask(Id) is Task t) return t.MarkedAsDone;
            if (GetTask(Id) is SubTask st) return st.MarkedAsDone;
            else
            {
                throw new ArgumentException("Group cant be done");
            }
        }

        public bool TodayTasks(int Id)
        {
            if (GetTask(Id) is Task t) return t.Deadline == DateTime.Today;
            return false;
        }
    }
}

