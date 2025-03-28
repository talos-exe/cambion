using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float PlayerSpeed = 2f;
    [SerializeField] private int TimeDivider = 3;
    [SerializeField] private Animator animator;
    Rigidbody2D rb;
    public PlayerData playerData;
    private bool playerIsPaused = false;
    private bool isMovingHorizontal = false;
    private bool isMovingVertical = false;
    [Header("Interactible NPCs")]
    [SerializeField] private NPCWandering oldManNPC;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector2(3,3);
        
    }

    // Update is called once per frame
    public void PlayerUpdate()
    {
        PlayerMovement();
        UpdateAnimation();

        if (Input.GetKeyUp(KeyCode.E) && oldManNPC.GetPlayerInRange() && oldManNPC.IfNPCVelocityZero())
        {
            oldManNPC.TalkToNPC();
            //oldManNPC.CreateQuest("Bleak Beginnings", "Look for the cave entrance Southwest of the island.");
        }
    }

    public bool PlayerIsMoving()
    {
        if (isMovingHorizontal || isMovingVertical) { return true; } else { return false; }
    }

    //Character Movement
    public void PlayerMovement()
    {

        // checks to make sure we are not pressing more than one input at once.
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");


        if (isMovingHorizontal)
        {
            if (Mathf.Abs(horizontalInput) > 0.1f)
            {
                rb.velocity = new Vector2(horizontalInput * PlayerSpeed, 0);
            }
            else
            {
                rb.velocity = Vector2.zero;
                isMovingHorizontal = false;
            }
        }

        else if (isMovingVertical)
        {
            if (Mathf.Abs(verticalInput) > 0.1f)
            {
                rb.velocity = new Vector2(0, verticalInput * PlayerSpeed);
            }
            else
            {
                rb.velocity = Vector2.zero;
                isMovingVertical = false;
            }
        }

        else
        {
            if (Mathf.Abs(horizontalInput) > 0.1f && Mathf.Abs(verticalInput) <= 0.1f)
            {

                rb.velocity = new Vector2(horizontalInput * PlayerSpeed, 0);
                isMovingHorizontal = true;
            }
            else if (Mathf.Abs(verticalInput) > 0.1f && Mathf.Abs(horizontalInput) <= 0.1f)
            {

                rb.velocity = new Vector2(0, verticalInput * PlayerSpeed);
                isMovingVertical = true;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    public void UpdateAnimation() {
        if (!playerIsPaused)
        {
            if (rb.velocity != Vector2.zero)
            {
                animator.SetBool("Walking", true);
                animator.SetFloat("Horizontal", rb.velocity.x);
                animator.SetFloat("Vertical", rb.velocity.y);

                GetComponent<SpriteRenderer>().flipX = rb.velocity.x > 0;
            }
            else
            {
                animator.SetBool("Walking", false);
            }
        }
        
    }


    public void PausePlayerMovement()
    {
        this.playerIsPaused = !playerIsPaused;
    }

}
