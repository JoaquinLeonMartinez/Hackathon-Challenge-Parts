using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponentSystem : MonoBehaviour
{

    PlayerController Player;

    public List<RobotComponent> Components;
    public List<GameObject> ComponentsGameObject;
    public float PieceImpulse = 6;

    private void Start()
    {
        Player = GetComponent<PlayerController>();

        if (Components == null)
            Components = new List<RobotComponent>();

        if (ComponentsGameObject == null)
            ComponentsGameObject = new List<GameObject>();
    }


    public void UpdateStats()
    {
        bool CalculateVelocity = true;
        Player.PlayerFinalHorizontalVelocity = Player.PlayerBaseHorizontalVelocity;

        bool CalculateJumpForce = true;
        Player.PlayerFinalJumpVelocity = Player.PlayerBaseJumpVelocity;
        Player.JumpAbility = false;

        for (int i = 0; i < Components.Count; i++)
        {
            //Horizontal V
            if (Components[i].ForceVelocity)
            {
                Player.PlayerFinalHorizontalVelocity = Components[i].VelocityBoost;
                CalculateVelocity = false;
            }

            if(CalculateVelocity)
                Player.PlayerFinalHorizontalVelocity += Components[i].VelocityBoost;

            //Jump Ability
            Player.JumpAbility |= Components[i].JumpAbility;

            //Jump Velocity
            if (Components[i].ForceJumpV)
            {
                Player.PlayerFinalJumpVelocity = Components[i].JumpVelocity;
                CalculateJumpForce = false;
            }

            if (CalculateJumpForce)
                Player.PlayerFinalJumpVelocity += Components[i].JumpVelocity;

            //Interaction
            Player.CanInteract |= Components[i].CanHandInteract;

            //Jetpack
            Player.CanFly |= Components[i].CanFly;

        }

        if (Player.WantsToLeavePiece)
        {
            soltarPieza();
            
        }
    }


    public bool checkTypePiece(RobotComponent pieza)
    {
        bool enc = false;
        for (int i = 0; i < Components.Count && !enc; i++)
        {
            if (Components[i].type == pieza.type)
            {
                enc = true;
            }
        }

        return enc;

    }

    void soltarPieza()
    {
        if(TryToThrow(PieceType.Arms))
        {
            return;
        }

        if (TryToThrow(PieceType.Legs))
        {
            return;
        }

        /*
        PieceType pieceToThrow = PieceType.Arms;

        for (int i = 0; i < Components.Count; i++)
        {
            //busca si hay pieza de brazos, si no la hay suelta la de piernas
            if (Components[i].type == pieceToThrow)//
            {

                ComponentsGameObject[i].SetActive(true);

                // seteamos la posicion al lado del player:
                Transform pieceTransform;
                pieceTransform = ComponentsGameObject[i].GetComponent<Transform>();
                pieceTransform.position = (transform.position + new Vector3(0, Player.GetPlayerHeight() + .25f));

                //Hacer que salga disparada al dejarla:
                Rigidbody2D PieceRigidBody2D;
                PieceRigidBody2D = ComponentsGameObject[i].GetComponent<Rigidbody2D>();
                PieceRigidBody2D.AddForce(Vector2.up * PieceImpulse, ForceMode2D.Impulse); //cambiar el vector por un que no sea solo hacia arriba

                Components.RemoveAt(i);
                ComponentsGameObject.RemoveAt(i);

                Player.emitirParticulasGetObject();

                Player.GetComponent<SoundController>().cogerPieza();
                return;
            }

            if (pieceToThrow == PieceType.Arms)
                pieceToThrow = PieceType.Legs;
        }*/
    }

    bool TryToThrow(PieceType type)
    {
        for (int i = 0; i < Components.Count; i++)
        {
            if (Components[i].type == type)//
            {

                ComponentsGameObject[i].SetActive(true);

                // seteamos la posicion al lado del player:
                Transform pieceTransform;
                pieceTransform = ComponentsGameObject[i].GetComponent<Transform>();
                pieceTransform.position = (transform.position + new Vector3(0, Player.GetPlayerHeight() + .25f));

                //Hacer que salga disparada al dejarla:
                Rigidbody2D PieceRigidBody2D;
                PieceRigidBody2D = ComponentsGameObject[i].GetComponent<Rigidbody2D>();
                PieceRigidBody2D.AddForce(Vector2.up * PieceImpulse, ForceMode2D.Impulse); //cambiar el vector por un que no sea solo hacia arriba

                Components.RemoveAt(i);
                ComponentsGameObject.RemoveAt(i);

                Player.emitirParticulasGetObject();

                Player.GetComponent<SoundController>().cogerPieza();
                return true;
            }
        }

        return false;
    }
}
