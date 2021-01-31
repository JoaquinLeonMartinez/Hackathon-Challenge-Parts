using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    public UnityEvent OnPressStart;
    [SerializeField]
    public UnityEvent OnPressHold;
    [SerializeField]
    public UnityEvent OnPressEnd;

    SpriteRenderer spriteRenderer;

    public bool IsPressed = false;

    public Sprite ReleasedSprite;
    public Sprite PressedSprite;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ReleasedSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPressed)
            return;

        spriteRenderer.sprite = PressedSprite;

        FindObjectOfType<SoundController>().presionarBoton();

        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            IsPressed = true;
            if (OnPressStart != null)
                OnPressStart.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!IsPressed)
            return;

        spriteRenderer.sprite = ReleasedSprite;

        FindObjectOfType<SoundController>().presionarBoton();

        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            IsPressed = false;
            if (OnPressEnd != null)
                OnPressEnd.Invoke();
        }
    }

    void Update()
    {
        if (IsPressed && OnPressHold != null)
        {
            OnPressHold.Invoke();
        }
    }
}
