namespace BlazorAppWithServer.Server.Data;

public static class MessagesRepository
{
    private static List<string> _messages = new List<string>();

    public static void AddMessage(string message)
    {
        _messages.Add(message);
    }

    public static List<string> GetMessages()
    {
        return _messages;
    }
} 