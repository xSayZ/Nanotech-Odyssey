using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Running,
        Dashing
    }

    public PlayerState playerState;

    [Header("References")]
    [SerializeField] private PlayerController controller;

    [Header("Variables")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float collisionOffset = 0.05f;
    private bool facingRight = true;

    [Header("Contact Filter")]
    [SerializeField] private ContactFilter2D movementFilter;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    public void HandleFixedUpdate(Vector2 movementInput, Vector2 aimInput)
    {
        HandleHorizontalMovement(movementInput);

        playerState = PlayerState.Idle;

        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
 
        // Check for potential collisions
        int count = controller.rb.Cast(
            direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
            movementFilter, // The setting that determine where a collision can occur on such as layers to collide with
            castCollisions, // List of collisions to store the found collisions into after the Cast is finished
            moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

        if (count == 0)
        {
            playerState = PlayerState.Running;
            controller.rb.MovePosition(controller.rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }

    void HandleHorizontalMovement(Vector2 direction)
    {
        if (direction.x > 0 && !facingRight)
        {
            Flip();
            facingRight = true;
        }
        else if (direction.x < 0 && facingRight)
        {
            Flip();
            facingRight = false;
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;


        transform.Rotate(0, 180f, 0);
    }
}
