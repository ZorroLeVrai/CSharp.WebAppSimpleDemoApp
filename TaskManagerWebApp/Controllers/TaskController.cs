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
    private readonly ILogger<TaskController> _logger;

    public TaskController(ITaskService taskService, ILogger<TaskController> logger)
    {
        _taskService = taskService;
        _logger = logger;
    }

    // GET: api/<TaskController>
    [HttpGet]
    public ActionResult<IEnumerable<TaskItem>> Get()
    {
        _logger.LogInformation("Récupération de toutes les tâches");
        return Ok(_taskService.GetAllTasks());
    }

    // GET api/<TaskController>/5
    [HttpGet("{id}")]
    public ActionResult<TaskItem> Get(int id)
    {
        _logger.LogInformation("Récupération de la tâche {TaskId}", id);

        if (_taskService.TryGetTaskById(id, out var taskItem))
        {
            return Ok(taskItem);
        }

        _logger.LogWarning("Tâche avec l'ID {TaskId} introuvable", id);
        return NotFound($"Task with ID {id} not found.");
    }

    // POST api/<TaskController>
    [HttpPost]
    public ActionResult<TaskItem> Post([FromBody] TaskItem value)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Modèle de tâche invalide {Task}", value);
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Ajout d'une nouvelle tâche {Task}", value);
        var newTask = _taskService.AddTask(value.Title, value.Status, value.DueDate);
        return CreatedAtAction(nameof(Get), new { id = newTask.Id }, newTask);
    }

    // PUT api/<TaskController>/5
    [HttpPut("{id}")]
    public ActionResult<TaskItem> Put(int id, [FromBody] TaskItem dataToUpdate)
    {
        if (_taskService.TryUpdateTask(id, dataToUpdate, out var updatedTask))
        {
            _logger.LogInformation("Mise à jour de la tâche {TaskId} avec les données {DataToUpdate}", id, dataToUpdate);
            return Ok(updatedTask);
        }

        _logger.LogWarning("Tâche avec l'ID {TaskId} introuvable pour mise à jour", id);
        return NotFound($"Task with ID {id} not found.");
    }

    // DELETE api/<TaskController>/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Suppression de la tâche {TaskId}", id);
        if (_taskService.TryDeleteTask(id))
        {
            return Ok();
        }

        _logger.LogWarning("Tâche avec l'ID {TaskId} introuvable pour suppression", id);
        return NotFound($"Task with ID {id} not found.");
    }
}
