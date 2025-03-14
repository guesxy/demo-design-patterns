namespace MediatorDemo.Mediator;

public interface IColleague
{
    void SendMessage(string messageType, string message);
    void ReceiveMessage(string messageType, string message);
}