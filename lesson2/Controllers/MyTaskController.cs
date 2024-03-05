using Microsoft.AspNetCore.Mvc;
using Tasks.Models;
namespace Tasks.controllers;

[ApiController]
[Route("[controller]")]
public class MyTasksController : ControllerBase
{
    private static List<MyTask> list;
    static MyTasksController()
    {
        list = new List<MyTask>{
           new MyTask{Id=1,NameTasks="homeWork",IsDone=false},
           new MyTask{Id=2,NameTasks="wash the dishes",IsDone=true},
           new MyTask{Id=3,NameTasks="go to sleep",IsDone=false},

       };
    }
    [HttpGet]
    public IEnumerable<MyTask> Get()
    {
        return list;
    }
    [HttpGet("{id}")]
    public MyTask Get(int id)
    {
        return list.FirstOrDefault(t=>t.Id==id); }
    [HttpPost]
    public int Post(MyTask newTask)
    {
        int max = list.Max(p => p.Id);
        newTask.Id = max + 1;
        list.Add(newTask);
        return newTask.Id;
    }

    [HttpPut("{id}")]
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
    [HttpDelete("{id}")]
    public void Delete(int id)
    {

        var task = list.Find(p => p.Id == id);
        if (task != null)
        {
            list.Remove(task);
        }

    }

}