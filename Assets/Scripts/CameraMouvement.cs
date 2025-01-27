using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMouvement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    
    private Vector2 _moveInput;
    public void HandleCameraMovement(InputAction.CallbackContext context)
    {
        print("Handle Camera");
        _moveInput = Vector2.zero;
        if (context.phase == InputActionPhase.Performed)
        {
            _moveInput = context.ReadValue<Vector2>();
            print("Move Input");
            print(_moveInput);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print("START CAMERA MY BOYYY");
    }

    // Update is called once per frame
    void Update()
    {
        print("Update");
        var mouvement = moveSpeed * Time.deltaTime * (new Vector3(_moveInput.y, 0, -_moveInput.x));
        transform.Translate(mouvement, Space.World);
    }
}
