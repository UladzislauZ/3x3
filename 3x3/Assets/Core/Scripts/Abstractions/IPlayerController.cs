using UnityEngine;

public interface IPlayerController
{
    bool Check();
    void ImplementConnectedClientData(Vector3 positionMouseClient, bool onClickMouseClient);
}
