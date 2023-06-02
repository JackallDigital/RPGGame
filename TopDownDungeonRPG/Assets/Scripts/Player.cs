using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D raycastHit;

    private void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDelta = new Vector3(x, y, 0);

        //swap sprite direction - right and left
        if(moveDelta.x > 0) {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Make sure we can move in this direction by casting a box there first, if the box returns null we're free to move. This is for Y axis
        raycastHit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if(raycastHit.collider == null) {
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
