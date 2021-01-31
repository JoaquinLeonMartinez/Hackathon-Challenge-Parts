using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEnabler : MonoBehaviour
{

    Collider2D collider;
    SpriteRenderer spriteRenderer;
    public Sprite ClosedSprite;
    public Sprite OpenedSprite;

    public bool IsOpen = false;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (IsOpen)
            DisableCollision(false);
        else
            EnableCollision(false);
    }

    public void DisableCollision(bool playSound = true)
    {
        if (playSound)
            FindObjectOfType<SoundController>().playSlidinDoorStone();

        IsOpen = true;
        collider.enabled = false;
        spriteRenderer.sprite = OpenedSprite;
    }

    public void EnableCollision(bool playSound = true)
    {
        if(playSound)
            FindObjectOfType<SoundController>().playSlidinDoorStone();

        IsOpen = false;
        collider.enabled = true;
        spriteRenderer.sprite = ClosedSprite;
    }

    public void Toggle()
    {
        FindObjectOfType<SoundController>().playSlidinDoorStone();
        IsOpen = !IsOpen;
        if (IsOpen)
        {
            DisableCollision();
        }
        else
        {
            EnableCollision();
        }
    }

}
