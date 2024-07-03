using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool _onMoveForMouse = false;

    private void Update()
    {
        if (_onMoveForMouse)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 newPosition = new(hit.point.x, transform.position.y, hit.point.z);
                transform.position = newPosition;
            }
        }
    }

    private void OnMouseDown()
    {
        _onMoveForMouse = !_onMoveForMouse;
    }
}
