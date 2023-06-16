using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : Collidable
{
    public string message;
    public int fontSize;
    public bool interracted = false;

    private float cooldown = 5.0f;
    private float lastMessage;

    private SpawnOnInterract spawn;

    protected override void Start() {
        base.Start();
        lastMessage = -cooldown;
        spawn = GetComponent<SpawnOnInterract>();
    }

    protected override void OnCollide(Collider2D collider) {
        if (collider.name == "Player") {
            interracted = true;
            if (!spawn.spawned) {
                spawn.Spawn();
            }

            if (Time.time - lastMessage > cooldown) {
                lastMessage = Time.time;
                GameManager.instance.ShowText(message, fontSize, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, cooldown);
            }
        }
    }
}