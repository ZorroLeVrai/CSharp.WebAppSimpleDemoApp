namespace WebAppCours.Models;

public class Message
{
    public string Text { get; set; } = string.Empty;
    public int Id { get; set; } = 0;

    public Message(string text, int id)
    {
        Text = text;
        Id = id;
    }
}
