namespace MediatorDemo.Mediator;

public class ConcreteMediator : IMediator
{
    private readonly Dictionary<string, List<IColleague>> _colleagues = new();

    public void Register(string messageType, IColleague colleague)
    {
        if (!_colleagues.ContainsKey(messageType))
        {
            _colleagues[messageType] = new List<IColleague>();
        }
        
        _colleagues[messageType].Add(colleague);
    }

    public void Send(string messageType, string message)
    {
        if (!_colleagues.ContainsKey(messageType))
        {
            return;
        }

        foreach (var colleague in _colleagues[messageType])
        {
            colleague.ReceiveMessage(messageType, message);
        }
    }
}