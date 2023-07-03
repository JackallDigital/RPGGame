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

            if(GameManager.instance.coins >= GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel]) {
                GameManager.instance.ShowText("<color=#00D4CE>UPGRADE</color> your weapon<br>click on the hammer!", 20, Color.white, GameObject.Find("Player").transform.position, Vector3.up * 30, 1.8f);
            }
        }
    }
}
