using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _places;

    [SerializeField]
    private string _idZone;

    [SerializeField]
    private Transform _parentCubesTransform;

    public GameObject[] Places => _places;
    public string IdZone => _idZone;
    public Transform ParentCubesTransform => _parentCubesTransform;
}
