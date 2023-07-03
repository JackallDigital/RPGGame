using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //public fields
    public int hitPoint = 10;
    public int maxHitPoint = 10;
    public float pushRecoverySpeed = 0.2f;

    //damage immunity time
    protected float damageImmunityTime = 1.0f;
    protected float lastDamageImmunityTime;

    //push
    protected Vector3 pushDirection;

    //All fighters can receive damage and die
    protected virtual void ReceiveDamage(Damage dmg) { 
        if(Time.time - lastDamageImmunityTime > damageImmunityTime) {
            lastDamageImmunityTime = Time.time;
            hitPoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, 0.5f);

            if(hitPoint <= 0) {
                hitPoint = 0;
                Death();
            }
        }
    }

    protected virtual void Death() {
    
    }
}
