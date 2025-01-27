using UnityEngine;

public class UnitPlacement : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Vector3 lastPosition;
    [SerializeField] private LayerMask placementLayer;

    public Vector3 MapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _camera.nearClipPlane;
        Ray ray = _camera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit, 100, placementLayer))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}
