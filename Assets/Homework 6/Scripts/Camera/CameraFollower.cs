using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _target;

    [Header("Offset Position")]
    [SerializeField] private float _offsetPositionY;
    [SerializeField] private float _offsetPositionZ;

    [Header("Offset Rotation")]
    [SerializeField] private float _offsetRotationX;

    public void Initialize(Transform target)
    {
        _target = target;
    }

    private void LateUpdate()
    {
        if(_target == null) return;

        Vector3 position = new Vector3(_target.position.x, _target.position.y + _offsetPositionY, _target.position.z + _offsetPositionZ);
        Vector3 rotation = Vector3.right * _offsetRotationX;
        _camera.transform.rotation = Quaternion.Euler(rotation);
        _camera.transform.position = position;
    }
}
