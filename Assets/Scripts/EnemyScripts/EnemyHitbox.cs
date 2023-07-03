using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    //Damage section
    public int damage = 1;
    public float pushForce = 0.5f;

    protected override void OnCollide(Collider2D collider) {
        if (collider.tag == "Fighter" && collider.name == "Player") {
            //create a new damage object, before sending it to the fighter we've hit
            Damage dmg = new Damage {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce,
            };

            collider.SendMessage("ReceiveDamage", dmg);
        }
    }
}
