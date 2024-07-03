public interface IClientController
{
    void StartConnect();
    void SendMessageToServer(string message);
    void CloseConnect();
}
