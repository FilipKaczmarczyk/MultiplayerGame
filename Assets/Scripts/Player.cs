using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsWalking { get; private set; }
    
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float playerSize = .7f;
    [SerializeField] private float playerHeight = 2f;
    
    private void Update()
    {
        Move(gameInput.GetMovementVectorNormalized());
    }

    private void Move(Vector2 inputVector)
    {
        var moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        var moveDistance = moveSpeed * Time.deltaTime;

        if (!CanMove(moveDir, moveDistance))
        {
            var moveDirX = new Vector3(inputVector.x, 0, 0).normalized;
            
            if (CanMove(moveDirX, moveDistance))
            {
                moveDir = moveDirX;
            }
            else
            {
                var moveDirZ = new Vector3(0, 0, inputVector.y).normalized;
                
                if (CanMove(moveDirZ, moveDistance))
                {
                    moveDir = moveDirZ;
                }
            }
        }
        else
        {
            transform.position += moveDir * moveDistance;
        }

        IsWalking = moveDir != Vector3.zero;
        
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
    
    private bool CanMove(Vector3 direction, float distance)
    {
        return !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, direction, distance);
    }
}
