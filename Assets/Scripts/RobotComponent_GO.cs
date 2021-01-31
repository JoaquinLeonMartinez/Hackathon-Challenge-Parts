
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotComponent_GO : MonoBehaviour
{
    public RobotComponent pieza;
    SpriteRenderer rend;
    PolygonCollider2D collider;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = pieza.getSprite();

        UpdateCollider();
    }

    private void UpdateCollider()
    {
        Destroy(GetComponent<PolygonCollider2D>());
        collider = gameObject.AddComponent<PolygonCollider2D>();
        collider.isTrigger = false; //true
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("trigger! " + collision);

        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            if (player.AddRobotComponent(pieza, gameObject))
            {
                //Debug.Log("Se recoje la pieza");
                gameObject.SetActive(false);
            }
            else
            {

            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("trigger! " + collision);

        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            if (player.AddRobotComponent(pieza, gameObject))
            {
                //Debug.Log("Se recoje la pieza");
                gameObject.SetActive(false);
            }
            else
            {

            }
        }
    }

}