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
    private bool unlocked = false;


    public string message;
    public int fontSize;

    private float cooldown = 4.0f;
    private float lastMessage;

    protected override void Start() {
        base.Start();
        lastMessage = -cooldown;
    }

    protected override void OnCollide(Collider2D collider) {
        if (collider.name == "Player") {
            if (GameManager.instance.keyInInventory.Contains(keyThatOpensTheDoor)) {

                if (gameObject.name != "Barricade") {
                    //replace closed door sprites to open door sprites
                    gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = TopLeft;
                    gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = BottomLeft;
                    gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = TopRight;
                    gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = BottomRight;

                    //remove blocking layer so we can move forward
                    gameObject.transform.GetChild(0).gameObject.layer = 0;
                    gameObject.transform.GetChild(2).gameObject.layer = 0;
                }
                else {
                    gameObject.SetActive(false);
                }

                GameManager.instance.ShowText(keyThatOpensTheDoor + " used!", 20, Color.white, transform.position, Vector3.up * 30, 1.2f);
                GameManager.instance.keyInInventory.Remove(keyThatOpensTheDoor);

                unlocked = true;
            }
            else if(!unlocked){
                if (Time.time - lastMessage > cooldown) {
                    lastMessage = Time.time;
                    GameManager.instance.ShowText(message, fontSize, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, cooldown);
                }
            }
        }
    }
}
