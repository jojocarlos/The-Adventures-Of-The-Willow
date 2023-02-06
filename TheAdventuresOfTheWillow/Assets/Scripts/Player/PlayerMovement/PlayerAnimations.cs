using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animationPlayer;

    private void Start()
    {
        animationPlayer = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //walk anim
        if (PlayerMovement2D.PlayerMovement2Dinstance.isGrounded && !PlayerMovement2D.PlayerMovement2Dinstance.IsWallJumping)
        {

            animationPlayer.SetBool("JumpingV", false);
            animationPlayer.SetBool("FallingV", false);
            animationPlayer.SetBool("JumpingH", false);
            animationPlayer.SetBool("FallingH", false);
            animationPlayer.SetBool("Clibing", false);
            animationPlayer.SetBool("SwimmingMoving", false);

            if (PlayerMovement2D.PlayerMovement2Dinstance.RB.velocity.x != 0 && PlayerMovement2D.PlayerMovement2Dinstance._moveInput.x != 0)
            {
                animationPlayer.SetBool("IsWalking", true);
            }
            else
            {
                animationPlayer.SetBool("IsWalking", false);
            }

        }

        else
        {
            //Jump anim

            if (PlayerMovement2D.PlayerMovement2Dinstance.RB.velocity.x == 0)
            {
                animationPlayer.SetBool("IsWalking", false);

                if (PlayerMovement2D.PlayerMovement2Dinstance.RB.velocity.y > 0 && !PlayerMovement2D.PlayerMovement2Dinstance.isLadder && !PlayerMovement2D.PlayerMovement2Dinstance.IsWallJumping)
                {
                    animationPlayer.SetBool("JumpingV", true);
                    animationPlayer.SetBool("FallingV", false);
                    animationPlayer.SetBool("JumpingH", false);
                    animationPlayer.SetBool("FallingH", false);
                }
                if (PlayerMovement2D.PlayerMovement2Dinstance.RB.velocity.y < 0 && !PlayerMovement2D.PlayerMovement2Dinstance.isLadder && !PlayerMovement2D.PlayerMovement2Dinstance.IsWallJumping)
                {
                    animationPlayer.SetBool("JumpingV", false);
                    animationPlayer.SetBool("FallingV", true);
                    animationPlayer.SetBool("JumpingH", false);
                    animationPlayer.SetBool("FallingH", false);
                }
            }
            else
            {
                if (PlayerMovement2D.PlayerMovement2Dinstance.RB.velocity.y > 0 && !PlayerMovement2D.PlayerMovement2Dinstance.isLadder && !PlayerMovement2D.PlayerMovement2Dinstance.IsWallJumping)
                {
                    animationPlayer.SetBool("JumpingV", false);
                    animationPlayer.SetBool("FallingV", false);
                    animationPlayer.SetBool("JumpingH", true);
                    animationPlayer.SetBool("FallingH", false);
                }
                if (PlayerMovement2D.PlayerMovement2Dinstance.RB.velocity.y < 0 && !PlayerMovement2D.PlayerMovement2Dinstance.isLadder && !PlayerMovement2D.PlayerMovement2Dinstance.IsWallJumping)
                {
                    animationPlayer.SetBool("JumpingV", false);
                    animationPlayer.SetBool("FallingV", false);
                    animationPlayer.SetBool("JumpingH", false);
                    animationPlayer.SetBool("FallingH", true);
                }

            }

            //Wall Jump
            if(PlayerMovement2D.PlayerMovement2Dinstance.IsWallJumping && !PlayerMovement2D.PlayerMovement2Dinstance.isGrounded && !PlayerMovement2D.PlayerMovement2Dinstance.isLadder)
            {
                animationPlayer.SetBool("WallJumping", true);
            }
            else if(PlayerMovement2D.PlayerMovement2Dinstance.IsWallJumping && !PlayerMovement2D.PlayerMovement2Dinstance.isGrounded && !PlayerMovement2D.PlayerMovement2Dinstance.isLadder && PlayerMovement2D.PlayerMovement2Dinstance.RB.velocity.y != 0)
            {
                animationPlayer.SetBool("WallJumping", true);
            }
            else
            {
                animationPlayer.SetBool("WallJumping", false);
            }


            //Clibing anim
            if (PlayerMovement2D.PlayerMovement2Dinstance.RB.velocity.x == 0 && !PlayerMovement2D.PlayerMovement2Dinstance.IsWallJumping)
            {
                animationPlayer.SetBool("IsWalking", false);
                if (PlayerMovement2D.PlayerMovement2Dinstance.RB.velocity.y > 0 && PlayerMovement2D.PlayerMovement2Dinstance.isClimbing)
                {
                    animationPlayer.SetBool("JumpingV", false);
                    animationPlayer.SetBool("FallingV", false);
                    animationPlayer.SetBool("JumpingH", false);
                    animationPlayer.SetBool("FallingH", false);
                    animationPlayer.SetBool("Clibing", true);
                }
                if (PlayerMovement2D.PlayerMovement2Dinstance.RB.velocity.y < 0 && PlayerMovement2D.PlayerMovement2Dinstance.isClimbing)
                {
                    animationPlayer.SetBool("JumpingV", false);
                    animationPlayer.SetBool("FallingV", false);
                    animationPlayer.SetBool("JumpingH", false);
                    animationPlayer.SetBool("FallingH", false);
                    animationPlayer.SetBool("Clibing", true);
                }
                if(!PlayerMovement2D.PlayerMovement2Dinstance.isClimbing)
                {
                    animationPlayer.SetBool("Clibing", false);
                }
            }

            //swim
            if (PlayerMovement2D.PlayerMovement2Dinstance.isOnWater)
            {
                animationPlayer.SetBool("JumpingV", false);
                animationPlayer.SetBool("FallingV", false);
                animationPlayer.SetBool("JumpingH", false);
                animationPlayer.SetBool("FallingH", false);

                animationPlayer.SetBool("Swimming", true);

                if (PlayerMovement2D.PlayerMovement2Dinstance.RB.velocity.x != 0 && PlayerMovement2D.PlayerMovement2Dinstance._moveInput.x != 0)
                {
                    animationPlayer.SetBool("SwimmingMoving", true);
                }
                else if (PlayerMovement2D.PlayerMovement2Dinstance.RB.velocity.y != 0 && PlayerMovement2D.PlayerMovement2Dinstance._moveInput.y != 0)
                {
                    animationPlayer.SetBool("SwimmingMoving", true);
                }
                else
                {
                    animationPlayer.SetBool("SwimmingMoving", false);
                }
            }
            else
            {
                animationPlayer.SetBool("Swimming", false);
                animationPlayer.SetBool("SwimmingMoving", false);
            }
        }

    }

    public void PlayerStart()
    {
        animationPlayer.SetBool("Dead", false);
    }

    public void PlayerDie()
    {
        animationPlayer.SetBool("Dead", true);
        animationPlayer.SetBool("JumpingV", false);
        animationPlayer.SetBool("FallingV", false);
        animationPlayer.SetBool("JumpingH", false);
        animationPlayer.SetBool("FallingH", false);
        animationPlayer.SetBool("IsWalking", false);
        animationPlayer.SetBool("Swimming", false);
    }
}
