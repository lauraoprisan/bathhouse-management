using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private float horizontalInput;
    private float verticalInput;
    public float playerSpeed;
    private float xRange = 8.3f;
    private float yRange = 4.5f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float prevHorizontalInput;
    public Vector2 movementDir;

    void Update() {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput==0 && prevHorizontalInput < 0 || horizontalInput < 0) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }

        if (horizontalInput != 0) { 
            prevHorizontalInput = horizontalInput;
        }

         movementDir = new Vector2(horizontalInput, verticalInput);

        //normalize the movement vector to ensure consistent speed in all directions
        if (movementDir.magnitude > 1) {
            movementDir.Normalize();
        }

        transform.Translate(movementDir * playerSpeed * Time.deltaTime);

        float clampedX = Mathf.Clamp(transform.position.x, -xRange, xRange);
        float clampedY = Mathf.Clamp(transform.position.y, -yRange, yRange);
        transform.position = new Vector2(clampedX, clampedY);
    }
}
