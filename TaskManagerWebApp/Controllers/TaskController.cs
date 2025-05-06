using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApp.Models;
using TaskManagerWebApp.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagerWebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    // GET: api/<TaskController>
    [HttpGet]
    public ActionResult<IEnumerable<TaskItem>> Get()
    {
        return Ok(_taskService.GetAllTasks());
    }

    // GET api/<TaskController>/5
    [HttpGet("{id}")]
    public ActionResult<TaskItem> Get(int id)
    {
        if (_taskService.TryGetTaskById(id, out var taskItem))
        {
            return Ok(taskItem);
        }

        return NotFound($"Task with ID {id} not found.");
    }

    // POST api/<TaskController>
    [HttpPost]
    public ActionResult<TaskItem> Post([FromBody] TaskItem value)
    {
        var newTask = _taskService.AddTask(value.Title, value.Status, value.DueDate);
        return CreatedAtAction(nameof(Get), new { id = newTask.Id }, newTask);
    }

    // PUT api/<TaskController>/5
    [HttpPut("{id}")]
    public ActionResult<TaskItem> Put(int id, [FromBody] TaskItem dataToUpdate)
    {
        if (_taskService.TryUpdateTask(id, dataToUpdate, out var updatedTask))
        {
            return Ok(updatedTask);
        }
        
        return NotFound($"Task with ID {id} not found.");
    }

    // DELETE api/<TaskController>/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (_taskService.TryDeleteTask(id))
        {
            return Ok();
        }

        return NotFound($"Task with ID {id} not found.");
    }
}
