using WebAppCours.Models;

namespace WebAppCours.Services;

public interface IMessageService
{
    void Add(Message msg);
    IEnumerable<Message> GetAll();
    Message? GetById(int id);
    void Remove(int id);
    void Update(int id, Message msg);
}