using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCollisionChecker : MonoBehaviour
{
    Collider2D SelfCollider;
    public bool IsGrounded = false;
    void Start()
    {
        SelfCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
            IsGrounded = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
            IsGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
            IsGrounded = false;
    }
}
