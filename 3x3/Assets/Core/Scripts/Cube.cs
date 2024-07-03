using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool _onMoveForMouse = false;
    private PlayerModel _model;

    private void Update()
    {
        if (_onMoveForMouse)
        {
            var ray = Camera.main.ScreenPointToRay(_model.positionMousePlayer);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 newPosition = new(hit.point.x, transform.position.y, hit.point.z);
                transform.position = newPosition;
            }
        }
    }

    public void FolowOn(PlayerModel model)
    {
        _onMoveForMouse = !_onMoveForMouse;
        _model = model;
    }
}
