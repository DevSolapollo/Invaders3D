using UnityEngine;

public class TurretMovement : MonoBehaviour
{


    [SerializeField] private float moveSpeed;


    private float moveInput;


    private void Update() {
        moveInput = GameInput.Instance.GetMoveInput();

        Vector3 move = new Vector3(moveInput * moveSpeed * Time.deltaTime, 0f, 0f);
        
        transform.Translate(move);
    }

}
