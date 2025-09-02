using UnityEngine;
using UnityEngine.Events;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private float speed = 5f;
    
    private Vector2 facingDirection;
    public LayerMask InteractableLayerMask;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool hasInterected;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TryInteractTile();
            hasInterected = false;
        }

    }

    void FixedUpdate()
    {
        playerRigidbody.linearVelocity = Vector2.zero;
        if (Input.GetKey(KeyCode.D)) {
            Move(Vector2.right);
            spriteRenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Move(Vector2.left);
            spriteRenderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.W))
        {
            Move(Vector2.up);

        }
        if (Input.GetKey(KeyCode.S))
        {
            Move(Vector2.down);
        }

    }

    private void Move(Vector2 direction) {
        facingDirection = direction;
        direction.Normalize();
        Vector2 targetVelocity = direction * speed;
        playerRigidbody.linearVelocity = Vector2.Lerp(playerRigidbody.linearVelocity, targetVelocity, 10*Time.deltaTime);
    }

    private void TryInteractTile()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + facingDirection, Vector2.up, 1f, InteractableLayerMask);

        if (hit.transform.TryGetComponent(out FieldTile fieldTile) && !hasInterected) {
            
            fieldTile.Interact();
            hasInterected = true;
        }
    }
}
