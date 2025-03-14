namespace MediatorDemo.Mediator;

public interface IMediator
{
    void Register(string messageType, IColleague colleague);
    void Send(string messageType, string message);
}