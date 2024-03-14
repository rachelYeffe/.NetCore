using Tasks.Models;
using Microsoft.AspNetCore.Mvc;
using Tasks.Interfaces;
namespace Tasks.services;

public class TaskService:ITaskService
{
    private  List<MyTask> list;
     public TaskService()
    {
        list = new List<MyTask>{
           new MyTask{Id=1,NameTasks="homeWork",IsDone=false},
           new MyTask{Id=2,NameTasks="wash the dishes",IsDone=true},
           new MyTask{Id=3,NameTasks="go to sleep",IsDone=false},

       };
    }
    public List<MyTask> GetAll()=>list;
 
    public MyTask Get(int id)
    {
        return list.FirstOrDefault(t=>t.Id==id); }
    public int Post(MyTask newTask)
    {
        int max = list.Max(p => p.Id);
        newTask.Id = max + 1;
        list.Add(newTask);
        return newTask.Id;
    }

    public void Put(int id, MyTask newTask)
    {
        if (id == newTask.Id)
        {
            var task = list.Find(p => p.Id == id);
            if (task != null)
            {
                int index = list.IndexOf(task);
                list[index] = newTask;
            }
        }
    }
    public void Delete(int id)
    {

        var task = list.Find(p => p.Id == id);
        if (task != null)
        {
            list.Remove(task);
        }

    }

}