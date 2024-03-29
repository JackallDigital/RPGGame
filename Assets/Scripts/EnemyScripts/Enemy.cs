using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    //Experience
    public int xpValue = 1;

    //Logic
    public float triggerLength = 0.4f;
    public float chaseLength = 1.0f;
    public float enemySpeeed = 0.3f;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    // Hitbox
    public ContactFilter2D contactFilter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start() {
        base.Start();

        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate() {
        //is the player in range?
        if(Vector3.Distance(playerTransform.position, transform.position) < chaseLength) {

            if(Vector3.Distance(playerTransform.position, startingPosition) < triggerLength) {
                chasing = true;
            }

            if (chasing) {
                if(!collidingWithPlayer) {
                    UpdateMotor((playerTransform.position - transform.position).normalized * enemySpeeed);
                }
            }
            else {
                UpdateMotor((startingPosition - transform.position) * enemySpeeed);
            }
        }
        else {
            UpdateMotor((startingPosition - transform.position) * enemySpeeed);
            chasing = false;
        }

        //check for player collision
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(contactFilter, hits);
        for (int i = 0; i < hits.Length; i++) {
            if (hits[i] == null)
                continue;

            if(hits[i].tag == "Fighter" && hits[i].name == "Player") {
                collidingWithPlayer = true;
            }

            //we need to clear the array ourselves
            hits[i] = null;
        }
    }

    protected override void Death() {
        Destroy(gameObject);
        GameManager.instance.GrantExperience(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.2f);
    }
}
