using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : Collidable
{
    public string message;
    public int fontSize;

    private float cooldown = 5.0f;
    private float lastMessage;

    protected override void Start() {
        base.Start();
        lastMessage = -cooldown;
    }

    protected override void OnCollide(Collider2D collider) {

        if(Time.time - lastMessage > cooldown) {
            lastMessage = Time.time;
            GameManager.instance.ShowText(message, fontSize, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, cooldown);
        }
    }
}