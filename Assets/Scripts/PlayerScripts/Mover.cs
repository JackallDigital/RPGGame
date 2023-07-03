using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    private Vector3 originalSize;

    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D raycastHit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;


    protected virtual void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
        originalSize = transform.localScale;
    }

    protected virtual void UpdateMotor(Vector3 input) {

        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        //swap sprite direction - right and left
        if (moveDelta.x > 0) {
            transform.localScale = originalSize;
            
        }
        else if (moveDelta.x < 0) {
            transform.localScale = Vector3.Scale(new Vector3(-1, 1, 1) , originalSize);
        }

        //Add push vector if there is any
        moveDelta += pushDirection;

        //Reduce the push force every frame using the push recovry speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        //Make sure we can move in this direction by casting a box there first, if the box returns null we're free to move. This is for Y axis
        raycastHit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (raycastHit.collider == null) {
            //make the movement
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        raycastHit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (raycastHit.collider == null) {
            //make the movement
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
