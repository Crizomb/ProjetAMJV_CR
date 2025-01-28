using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMouvement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    
    private Vector2 _moveInput;
    public void HandleCameraMovement(InputAction.CallbackContext context)
    {
        _moveInput = Vector2.zero;
        if (context.phase == InputActionPhase.Performed)
        {
            _moveInput = context.ReadValue<Vector2>();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var mouvement = (new Vector3(_moveInput.x, 0, _moveInput.y));
        var worldSpaceMouvement = transform.TransformVector(mouvement);
        var realMovement = Vector3.ProjectOnPlane(worldSpaceMouvement, Vector3.up);
        
        if (Input.GetAxis("Mouse ScrollWheel") != 0.0)
        {
            realMovement += Input.GetAxis("Mouse ScrollWheel") * 20.0f * transform.forward;
        }
        
        transform.Translate(moveSpeed*Time.deltaTime*realMovement, Space.World);
    }
}
