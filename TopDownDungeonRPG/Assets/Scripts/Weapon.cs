using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage structure
    public int[] damagePoint = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
    public float[] pushForce = { 2f, 2.2f, 2.4f, 2.6f, 2.8f, 2f, 2.2f, 2.4f, 2.6f, 2f, 2.2f, 2.4f, 2.6f, 3f, 3f, 3f, 3f };

    //Upgrade section
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    //Swing weapon
    private Animator animatorController;
    private float cooldown = 0.5f;
    private float lastSwing;

    protected void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start() {
        base.Start();

        animatorController = GetComponent<Animator>();
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
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel],
            };


            collider.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Swing() {
        animatorController.SetTrigger("Swing");
    }

    public void UpgradeWeapon() {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    public void SetWeaponLevel(int level) {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
