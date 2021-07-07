using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager
{
    static class TaskAPI
    {
        private static TaskRegistry taskRegistry = new();
        public static void CreateTask(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException("Message cant be null or white space");

            Task task = new() { Name = message };

            if (taskRegistry.ContainsTask(task)) throw new ArgumentException("Task cant be added twice");

            taskRegistry.AddITask(task);
        }

        public static void CreateGroup(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cant be null or white space");

            Group group = new() { Name = name };

            taskRegistry.AddITask(group);
        }

        public static void DeleteGroup(string name)
        {
            if (taskRegistry.GetTask(name) is Group group) taskRegistry.DeleteTask(group);
        }

        public static void AddTaskToGroup(int Id, string groupName)
        {
            if (taskRegistry.GetTask(groupName) is Group) MarkParent(Id, groupName);

        }

        public static void DeleteFromGroup(int Id, string groupName)
        {
            if (taskRegistry.GetTask(groupName) is Group group) 
            {
                group.ChildrenId.Remove(Id);
            }
        }

        public static void CreateSubTask(string message, int ParentId) 
        {
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException("Empty subtask");

            SubTask subtask = new() { Name = message, ParentExists = true, ParentId = ParentId };
            
            taskRegistry.GetTask(ParentId).Child.Add(subtask);

            taskRegistry.AddITask(subtask);
        }

        public static void CompleteById(int Id)
        {
            if (taskRegistry.GetTask(Id) is Task t)
            {
                t.MarkedAsDone = true;
            }

            if (taskRegistry.GetTask(Id) is SubTask st)
            {
                st.MarkedAsDone = true;
            }            
        }

        public static void DeleteTask(int Id) 
        {
            if (taskRegistry.GetTask(Id) is SubTask st)
            {
                var match = taskRegistry.GetTask(st.ParentId).Child.FirstOrDefault(x => x.Name == taskRegistry.GetTask(Id).Name);
                taskRegistry.GetTask(st.ParentId).Child.Remove(match);
            }
            if (taskRegistry.ContainsTask(Id)) taskRegistry.DeleteTask(Id);
        }

        public static void TaskDeadline(int Id, DateTime deadline)
        {
            if (taskRegistry.GetTask(Id) is Task t) t.Deadline = deadline; 
        }

        public static DateTime GetTaskDeadline(int Id)
        {
            if (taskRegistry.GetTask(Id) is Task t) return t.Deadline;
            return DateTime.Now;
        }

        public static int GetId(string name) 
        {
            if (taskRegistry.GetTask(name) is Task or SubTask) return taskRegistry.GetId(name);
            return 0;
        }

        public static void MarkParent(int Id, string Name)
        {
            if (taskRegistry.GetTask(Id) is Task t)
            {
                t.ParentExists = true; t.ParentId = GetId(Name);
            }
            if (taskRegistry.GetTask(Name) is Group g)
            {
                g.ChildrenId.Add(Id);
            }
        }

        public static bool ParentExists(int Id)
        {
            if (taskRegistry.GetTask(Id) is Task t) return t.ParentExists;
            return false;
        }

        public static Dictionary<int, Task> ListAllTasks()
        {
            return taskRegistry.GetTasks().Where(x => (x.Value is Task)).OrderBy(x => (x.Value as Task).MarkedAsDone).ToDictionary(x => x.Key, x => x.Value as Task);
        }

        public static Dictionary<int, Group> ListAllGroups()
        {
            return taskRegistry.GetTasks().Where(x => (x.Value is Group)).ToDictionary(x => x.Key, x => x.Value as Group);
        }

        public static Dictionary<int, SubTask> ListAllSubTasks()
        {
            return taskRegistry.GetTasks().Where(x => (x.Value is SubTask)).OrderBy(x => (x.Value as SubTask).MarkedAsDone).ToDictionary(x => x.Key, x => x.Value as SubTask);
        }

        public static List<Task> ListAllChildren(Group group)
        {
            List<Task> list = new();
            foreach(int id in group.ChildrenId)
            {
                list.Add(taskRegistry.GetTask(id) as Task);
            }
            return list.Select(x => x).OrderBy(x => x.MarkedAsDone).ToList();
        }
        public static List<int> ListAllChildrenId(Group group)
        {
            return group.ChildrenId;
        }

        public static void SetParent(int Id, string groupname)
        {
            if (taskRegistry.GetTask(Id) is Task t) t.Parent = taskRegistry.GetTask(groupname);
        }

        public static void Save(string name) => taskRegistry.Save(name);
        public static void Load(string name) => taskRegistry.Load(name);

        public static bool CompletedTask(int Id)
        {
            if (taskRegistry.GetTask(Id) is Task t) return t.MarkedAsDone;
            if (taskRegistry.GetTask(Id) is SubTask st) return st.MarkedAsDone;
            return false;
        }

        public static bool TodayTasks(int Id) 
        {
            if (taskRegistry.GetTask(Id) is Task t) return t.Deadline == DateTime.Today;
            return false;
        }

     
    }
}
