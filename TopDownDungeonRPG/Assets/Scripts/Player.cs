using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
public class Player : Mover {
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;

    protected override void Start() {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ReceiveDamage(Damage dmg) {
        if (!isAlive)
            return;

        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }

    protected override void Death() {
        isAlive = false;
        GameManager.instance.deathMenuAnimator.SetTrigger("Show");
    }

    private void FixedUpdate() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if(isAlive)
            UpdateMotor(new Vector3 (x, y, 0));
    }

    public void SwapSprite(int skinID) {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinID];
    }

    public void OnLevelUp() {
        maxHitPoint++;
        hitPoint = maxHitPoint;
    }

    public void SetLevel(int level) {
        for(int i = 0; i < level; i++) {
            OnLevelUp();
        }
    }

    public void Heal(int healingAmount) {
        if(hitPoint == maxHitPoint) {
            return;
        }

        hitPoint += healingAmount;
        if(hitPoint > maxHitPoint) {
            hitPoint = maxHitPoint;
        }

        GameManager.instance.ShowText("+"+healingAmount.ToString()+" hp", 25, Color.green, transform.position, Vector3.up*30, 1f);
        GameManager.instance.OnHitpointChange();
    }

    public void Respawn() {
        isAlive = true;
        //reset hp values back to starting values
        hitPoint = 5;
        maxHitPoint = 5;

        Heal(maxHitPoint);

        //we get pushed back when respawning if this is not added
        lastDamageImmunityTime = Time.time; 
        pushDirection = Vector3.zero;
    }
}