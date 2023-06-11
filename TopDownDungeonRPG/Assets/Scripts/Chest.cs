using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int coinAmount = 5;

    protected override void OnCollect() {
        if(!collected) {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.coins += coinAmount;
            GameManager.instance.ShowText("+" + coinAmount + " coins!", 20, Color.yellow, transform.position, Vector3.up * 30, 1.2f);
            GameManager.instance.SaveState();
        }
    }
}
