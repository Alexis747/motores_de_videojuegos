using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private PlayerControls _controls;
    private Vector2 _inputVector;

    [Range(0,1)]
    [SerializeField]
    private float speed;

    void FixedUpdate() {
        transform.position = new Vector3(
            transform.position.x + (_inputVector.y * speed),
            transform.position.y,
            transform.position.z - (_inputVector.x * speed)
        );
    }


    public void HandleInteract(InputAction.CallbackContext context){
        Debug.Log ("Player Interact");
    }

    public void HandleMove(InputAction.CallbackContext context) {
        _inputVector = context.ReadValue<Vector2>();
    }

}
