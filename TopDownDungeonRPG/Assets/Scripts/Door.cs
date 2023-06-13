using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Door : Collidable
{
    public Sprite TopLeft;
    public Sprite BottomLeft;
    public Sprite TopRight;
    public Sprite BottomRight;

    public string keyThatOpensTheDoor;

    protected override void OnCollide(Collider2D collider) {
        if (collider.name == "Player") {
            if (GameManager.instance.keyInInventory.Contains(keyThatOpensTheDoor)) {
                //replace closed door sprites to open door sprites
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = TopLeft;
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = BottomLeft;
                gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = TopRight;
                gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = BottomRight;

                //remove blocking layer so we can move forward
                gameObject.transform.GetChild(0).gameObject.layer = 0;
                gameObject.transform.GetChild(2).gameObject.layer = 0;

                //Debug.Log("Door collided with: " + GameManager.instance.keyInInventory.Contains(keyThatOpensTheDoor));
                GameManager.instance.ShowText(keyThatOpensTheDoor + " used!", 20, Color.white, transform.position, Vector3.up * 30, 1.5f);
                GameManager.instance.keyInInventory.Remove(keyThatOpensTheDoor);

            }
        }
    }
}
