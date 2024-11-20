using UnityEngine;

public class RaycastHandler
{
    private LayerMask _layerMask;
    private float _distance;
    private Transform _transform;

    public RaycastHandler(LayerMask layerMask, float distance, Transform transform)
    {
        _layerMask = layerMask;
        _distance = distance;
        _transform = transform;
    }

    public Transform GetTarget(Vector3 direction)
    {
        Ray ray = new Ray(_transform.position, direction); 

        if(Physics.Raycast(ray, out RaycastHit hit, _distance, _layerMask))
        {
            return hit.transform;
        }

        return null;
    }
}
