using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeaponToMouse : MonoBehaviour
{
    public Transform player; // Assign this to your player in the Inspector
    public float radius = 1.0f; // Distance from the player
    public float minMouseDistance = 0.5f; // Minimum distance mouse has to be from player
    private bool isFlipped = false; // track if the sprite is flipped

    void Update()
    {
        // Get direction from player to mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - player.position;

        if (direction.magnitude >= minMouseDistance)
        {
            // Set weapon position on circle around player
            float angle = Mathf.Atan2(direction.y, direction.x);
            transform.position = player.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

            // Rotate weapon to face mouse
            direction = mousePos - transform.position;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if(isFlipped)
            {
                // if the sprite is flipped, we need to invert the angle
                angle = -angle;
            }
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        }
    }

    // Call this function when you flip the sprite
    public void FlipSprite()
    {
        isFlipped = !isFlipped;
    }
}
