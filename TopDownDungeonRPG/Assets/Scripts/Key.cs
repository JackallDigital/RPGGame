using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Collectable
{
    //public List<string> Keys = new List<string>();
    protected override void OnCollect() {
        if (!collected) {
            collected = true;

            GameManager.instance.keyInInventory.Add(gameObject.name);
            GameManager.instance.ShowText(gameObject.name+" found!", 20, Color.yellow, transform.position, Vector3.up * 30, 1.5f);
            Destroy(gameObject);
        }
    }
}
