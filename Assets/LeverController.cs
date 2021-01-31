using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverController : MonoBehaviour
{
    PlayerController Player;
    public float InteractionDistance = 1f;
    SpriteRenderer SpriteRenderer;

    public UnityEvent OnTunOn;
    public UnityEvent OnTunOff;

    public Sprite ActivatedSprite;
    public Sprite DeactivatedSprite;

    public bool Activated = false;

    void Start()
    {
        Player = FindObjectOfType<PlayerController>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Player.OnUse += Trigger;
    }

    public void Trigger()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) <= InteractionDistance)
        {
            FindObjectOfType<SoundController>().activarPalanca();

            Activated = !Activated;
            if (Activated)
            {
                SpriteRenderer.sprite = ActivatedSprite;

                if (OnTunOn != null)
                    OnTunOn.Invoke();
            }
            else
            {
                SpriteRenderer.sprite = DeactivatedSprite;

                if (OnTunOff != null)
                    OnTunOff.Invoke();
            }
        }
    }

}
