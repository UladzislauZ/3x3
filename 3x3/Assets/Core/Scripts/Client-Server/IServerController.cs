public interface IServerController
{
    void InitServer();
    void CloseServer();
    void SendMessageToClient(string message);
}
