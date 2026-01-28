using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] private Transform camTransform; 
    [SerializeField] private float velocity = 5f;
    public CharacterController controller;

    private Vector3 _inputVector;

    private void OnEnable() {
        _inputReader.MoveEvent += OnMove;
    }

    private void OnDisable() {
        _inputReader.MoveEvent -= OnMove;
    }

    private void OnMove(Vector3 movement) {
        _inputVector = movement; 
    }

    void Update(){
        MovePlayer();
    }

    void MovePlayer(){
        Vector3 forward = camTransform.forward;
        Vector3 right = camTransform.right;
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * _inputVector.y) + (right * _inputVector.x);

        controller.Move(moveDirection * velocity * Time.deltaTime);
        if (moveDirection != Vector3.zero){
            transform.rotation = Quaternion.Euler(0, camTransform.eulerAngles.y, 0);
        }
    }
}