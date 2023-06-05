using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage structure
    public int damagePoint = 0;
    public float pushForce = 2.0f;

    //Upgrade section
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    //Swing weapon
    private float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start() {
        base.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update() {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space)) {
            if(Time.time - lastSwing > cooldown) {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D collider) {
        if(collider.tag == "Fighter") {
            if (collider.name == "Player")
                return;

            //create a new damage object, then we'll send that to the figher we've hit
            Damage dmg = new Damage {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce,
            };


            collider.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Swing() {
        Debug.Log("Swing weapon");
    }
}
