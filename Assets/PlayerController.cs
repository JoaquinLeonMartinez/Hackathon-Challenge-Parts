using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum RobotStatus { HeadOnly, WithLegs, WithArms, WithArmsAndLegs };

public class PlayerController : MonoBehaviour
{
    public RobotStatus CurrentRobotStatus = RobotStatus.HeadOnly;

    

    Rigidbody2D PlayerRB;
    public float PlayerBaseHorizontalVelocity = 10f;
    public float PlayerBaseJumpVelocity = 50f;


    float MaxHorizontalVelocity = 2f;


    bool Brake = false;
    public bool IsInGround = false;

    SpriteRenderer Sprite;
    SoundController soundController;

    TerrainCollisionChecker terrainCollisionChecker;

    ParticleLauncher[] particleLauncherArray;

    [Header("Final ability values")]
    public float PlayerFinalHorizontalVelocity = 0;
    public bool JumpAbility = false;
    public float PlayerFinalJumpVelocity = 0;
    public bool CanInteract = false;
    public bool CanFly = false;

    public UnityAction OnUse;

    PlayerComponentSystem PlayerCompSystem;
    Animator animator;

    CapsuleCollider2D capsuleCollider;

    public bool WantsToLeavePiece = false;

    public float GetPlayerHeight()
    {
        return capsuleCollider.size.y;
    }

    void Start()
    {
        PlayerRB = GetComponent<Rigidbody2D>();

        if(PlayerRB== null)
        {
            Debug.LogError("Rigidbody null!");
        }

        soundController = GetComponent<SoundController>();
        particleLauncherArray = GetComponentsInChildren<ParticleLauncher>();
        terrainCollisionChecker = GetComponentInChildren<TerrainCollisionChecker>();
        PlayerCompSystem = GetComponent<PlayerComponentSystem>();
        Sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    
    void Update()
    {
        PlayerCompSystem.UpdateStats();

        Brake = true;
        IsInGround = CheckIsInGround();

        //Horizontal movement
        float HorizontalInput = 0f;
        HorizontalInput += Input.GetKey(KeyCode.RightArrow) ? 1f : 0f;
        HorizontalInput -= Input.GetKey(KeyCode.LeftArrow) ? 1f : 0f;

        HorizontalInput += Input.GetKey(KeyCode.D) ? 1f : 0f;
        HorizontalInput -= Input.GetKey(KeyCode.A) ? 1f : 0f;
        HorizontalInput = Mathf.Clamp(HorizontalInput, -1f, 1f);

        if (!IsInGround)
            HorizontalInput *= 0.8f;

        if (HorizontalInput != 0)
        {
            Brake = false;
            PlayerRB.velocity = new Vector2(HorizontalInput * PlayerFinalHorizontalVelocity , PlayerRB.velocity.y);
            if (IsInGround)
            {
                if(PlayerCompSystem.Components.Count > 0)
                    soundController.footstep(true);
            }
            else
            {
                soundController.footstep(false);
            }   
        }
        else
        {
            //Frenar horitzontal si no cliquem

            PlayerRB.velocity = new Vector2(PlayerRB.velocity.x * 0.95f, PlayerRB.velocity.y);
            soundController.footstep(false);
        }
        
        //Jump control
        bool WantsToJump = false;
        WantsToJump |= Input.GetKeyDown(KeyCode.Space);
        WantsToJump |= Input.GetKeyDown(KeyCode.W);
        WantsToJump |= Input.GetKeyDown(KeyCode.UpArrow);

        if (CanFly)
        {
            //Jetpack
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                PlayerRB.velocity = Vector2.Lerp(PlayerRB.velocity, new Vector2(PlayerRB.velocity.x, 5f), 0.3f);
            }
        }
        else
        {
            if (WantsToJump && IsInGround)
            {
                soundController.playJump();
                PlayerRB.velocity = new Vector2(PlayerRB.velocity.x, PlayerFinalJumpVelocity);
                emitirParticulasJump();
            }
        }
        if (!IsInGround)
            Brake = false;

        if (Brake)
            PlayerRB.velocity = new Vector2(Mathf.Lerp(PlayerRB.velocity.x, 0, 0.05f), PlayerRB.velocity.y);

        //Interactions
        bool WantsToInteract = false;
        WantsToInteract |= Input.GetKeyDown(KeyCode.F);
        if(WantsToInteract && CanInteract)
        {
            if (OnUse != null)
                OnUse.Invoke();
        }

        WantsToLeavePiece = Input.GetKeyDown(KeyCode.E);

        VisualUpdate(HorizontalInput);
        AnimationUpdate(HorizontalInput != 0f, CurrentRobotStatus, IsInGround);
        CheckHeight();
    }

    public bool CanICatch(RobotComponent pieza)
    {
        if (pieza.type == PieceType.Arms)
        {
            for (int i = 0; i < PlayerCompSystem.Components.Count; i++)
            {
                if (PlayerCompSystem.Components[i].type == PieceType.Legs)
                    return true;
            }
        }else if (pieza.type == PieceType.Legs)
        {
            return true;
        }else if (pieza.type == PieceType.Back)
        {
            bool hasLegs = false;
            bool hasArms = false;
            for (int i = 0; i < PlayerCompSystem.Components.Count; i++)
            {
                if (PlayerCompSystem.Components[i].type == PieceType.Legs)
                    hasLegs = true;

                if (PlayerCompSystem.Components[i].type == PieceType.Arms)
                    hasArms = true;
            }
            if (hasLegs && hasArms)
                return true;
        }

        return false;
    }

    public bool AddRobotComponent(RobotComponent pieza, GameObject gameObjectComponent)
    {

        if (!CanICatch(pieza))
        {
            return false;
        }
        else 
        {
            //Permetre agafar una peça si ja està la anterior posada.

            PlayerCompSystem.Components.Add(pieza);
            PlayerCompSystem.ComponentsGameObject.Add(gameObjectComponent);

            emitirParticulasGetObject();

            soundController.cogerPieza();

            return true;
        }
    }
    

    void CheckHeight()
    {
        switch (PlayerCompSystem.Components.Count)
        {
            case 0:
                capsuleCollider.size = new Vector2(.7f, 0.7f);
                capsuleCollider.offset = new Vector2(0f, -0.6f);
                break;
            case 1:
            case 2:
                capsuleCollider.size = new Vector2(0.97f, 1.58f);
                capsuleCollider.offset = new Vector2(0f, -0.22f);
                break;
        }
    }

    void VisualUpdate(float HorizontalV)
    {
        if (HorizontalV < 0)
            Sprite.flipX = true;
        else if (HorizontalV > 0)
            Sprite.flipX = false;
    }

    void AnimationUpdate(bool isWalking, RobotStatus status_, bool Grounded)
    {
        animator.SetBool("IsWalking", isWalking);
        int bodyType = 0;
        for (int i = 0; i < PlayerCompSystem.Components.Count; i++)
        {
            if (PlayerCompSystem.Components[i].type == PieceType.Arms)
                bodyType++;

            if (PlayerCompSystem.Components[i].type == PieceType.Legs)
                bodyType++;
        }

        animator.SetInteger("BodyType", bodyType);
        animator.SetBool("Grounded",Grounded);
        animator.SetFloat("fallDirection", PlayerRB.velocity.y);

        
    }

    private bool CheckIsInGround()
    {
        return terrainCollisionChecker.IsGrounded;
    }



    //


    public void emitirParticulasGetObject()
    {
        //Paticulas
        for (int i = 0; i < particleLauncherArray.Length; i++)
        {
            particleLauncherArray[i].EmitParticles();
        }
    }

    public void emitirParticulasJump()
    {
        //Paticulas
        for (int i = 0; i < particleLauncherArray.Length; i++)
        {
            particleLauncherArray[i].ParticlesJump();
        }
    }
}
