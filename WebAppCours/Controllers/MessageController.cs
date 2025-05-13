using Microsoft.AspNetCore.Mvc;
using WebAppCours.Models;
using WebAppCours.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppCours.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    // GET: api/<MessageController>
    [HttpGet]
    public ActionResult<IEnumerable<Message>> Get()
    {
        return Ok(_messageService.GetAll());
    }

    // GET api/<MessageController>/5
    [HttpGet("{id}")]
    async public Task<ActionResult<Message>> Get(int id)
    {
        var message = _messageService.GetById(id);

        await Task.Delay(1000);
        if (message == null)
        {
            return NotFound();
        }

        return Ok(message);
    }

    // POST api/<MessageController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
        var msg = new Message(value, 1);
        _messageService.Add(msg);
    }

    // PUT api/<MessageController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
        var msg = new Message(value, id);
        _messageService.Update(id, msg);
    }

    // DELETE api/<MessageController>/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _messageService.Remove(id);

        return Ok();
    }
}
