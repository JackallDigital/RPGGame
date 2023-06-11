using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
public class Player : Mover {
    private SpriteRenderer spriteRenderer;

    protected override void Start() {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

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
}