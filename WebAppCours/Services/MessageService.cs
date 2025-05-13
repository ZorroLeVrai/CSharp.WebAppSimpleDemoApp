using WebAppCours.Models;

namespace WebAppCours.Services;

public class MessageService : IMessageService
{
    private readonly Dictionary<int, Message> _messages = new Dictionary<int, Message>()
    {
        { 0, new Message("Hello World 1", 0) },
        { 1, new Message("Hello World 2", 1) }
    };

    private int currentMessageId = 1;

    private int GetNextMessageId()
    {
        return ++currentMessageId;
    }

    public void Add(Message msg)
    {
        var messageId = GetNextMessageId();
        msg.Id = messageId;
        _messages.Add(messageId, msg);
    }

    public IEnumerable<Message> GetAll()
    {
        return _messages.Values;
    }

    public Message? GetById(int id)
    {
        if (_messages.TryGetValue(id, out var msg))
        {
            return msg;
        }
        return null;
    }

    public void Remove(int id)
    {
        _messages.Remove(id);
    }

    public void Update(int id, Message msg)
    {
        if (_messages.ContainsKey(id))
        {
            _messages[id] = msg;
        }
    }
}
