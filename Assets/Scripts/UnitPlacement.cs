using UnityEngine;
using UnityEngine.AI;

public class UnitPlacement : MonoBehaviour
{
    private Camera _camera;
    public Vector3 lastPosition;
    [SerializeField] private LayerMask placementLayer;

    void Start()
    {
        _camera = Camera.main;
    }

    public Vector3 MapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _camera.nearClipPlane;
        Ray ray = _camera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, placementLayer))
        {
            // Get the hit point from the raycast
            Vector3 hitPoint = hit.point;

            // Sample the closest valid position on the NavMesh
            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(hitPoint, out navMeshHit, 1.0f, NavMesh.AllAreas))
            {
                lastPosition = navMeshHit.position; // Update lastPosition to the valid NavMesh position
            }
            else
            {
                Debug.LogWarning("No valid NavMesh position near the hit point.");
            }
        }

        return lastPosition;
    }
}