using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTN {
    Task AI;
    Stack<Task> tasks;
    public List<Task> decompose;

    // Use this for initialization
    public HTN() {
        tasks = new Stack<Task>();
        decompose = new List<Task>();

        AI = new Task("AI", Task.TaskType.COMPOUND);

        Task MoveToItem = new Task("MoveToItem", Task.TaskType.PRIMITIVE);
        AI.AddTask(MoveToItem);

        Task MoveAvoid = new Task("MoveAvoid", Task.TaskType.PRIMITIVE);
        Task IdleAvoid = new Task("IdleAvoid", Task.TaskType.PRIMITIVE);
        Task UseTrap = new Task("UseTrap", Task.TaskType.PRIMITIVE);
        Task Avoid = new Task("Avoid", Task.TaskType.COMPOUND);

        Avoid.AddTask(IdleAvoid);
        Avoid.AddTask(MoveAvoid);
        Avoid.AddTask(UseTrap);
        AI.AddTask(Avoid);
        
        Task MoveToNext = new Task("MoveToNext", Task.TaskType.PRIMITIVE);
        AI.AddTask(MoveToNext);

        Task MoveToClosest = new Task("MoveToClosest", Task.TaskType.PRIMITIVE);
        AI.AddTask(MoveToClosest);

    }
    

    public Task Traverse() {
        tasks.Push(AI);
        Task t;
        Task returnPlan = null;
        List<Task> visitedTasks = new List<Task>();

        while (tasks.Count > 0) {
            t = tasks.Pop();

            if (!visitedTasks.Contains(t) && !decompose.Contains(t)) {
                
                returnPlan = t;
                visitedTasks.Add(t);
                
                foreach (Task st in t.GetSubTasks()) {
                    tasks.Push(st);
                }
            }
        }
        return returnPlan;
    }

    public void ClearDecompsedList() {
        decompose.Clear();
    }
    
    public class Task {
        string taskName;
        List<Task> subtasks;
        TaskType taskType;

        public Task(string pTaskName, TaskType taskType) {
            taskName = pTaskName;
            subtasks = new List<Task>();
        }

        public Task(string pTaskName, List<Task> pSubtasks, TaskType taskType) {
            taskName = pTaskName;
            subtasks = pSubtasks;
        }

        public string GetTaskName() {
            return taskName;
        }

        public IList GetSubTasks() {
            return subtasks.AsReadOnly();
        }

        public TaskType GetTaskType() {
            return taskType;
        }

        public void AddTask(Task task) {
            subtasks.Add(task);
        }

        public enum TaskType {
            COMPOUND,
            PRIMITIVE
        }
    }
}
