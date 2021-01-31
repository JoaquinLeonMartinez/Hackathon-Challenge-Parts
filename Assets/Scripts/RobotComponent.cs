using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceType { Arms, Legs, Back };

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ComponenteRobot", order = 1)]
public class RobotComponent : ScriptableObject
{

    public string componentName;
    //HorizontalV
    public bool ForceVelocity = false;
    public float VelocityBoost = 0;
    //Jump
    public bool JumpAbility = false;
    public bool ForceJumpV = false;
    public float JumpVelocity = 0;
    public bool CanFly = false;
    //Hand interaction
    public bool CanHandInteract = false;

    public PieceType type;

    public Sprite sprite;

    public Sprite getSprite()
    {
        return this.sprite;
    }

}
