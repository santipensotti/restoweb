namespace BlazorAppWithServer.Server.Data;

public static class messagesRepository
{
    private static List<string> History = new List<string>();
    public static void addMessage(string m){
        History.Add(m);
    }
    public static List<string> GetHistory(){
        return History;
    }


}