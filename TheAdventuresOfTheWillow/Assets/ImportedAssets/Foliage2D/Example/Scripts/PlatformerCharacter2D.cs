using UnityEngine;

namespace Foliage
{
    // This is a simplified version of the "PlatformerCharacter2D" class from the Sample Assets. 
    public class PlatformerCharacter2D : MonoBehaviour
    {
        bool facingRight = true;							// For determining which way the player is currently facing.

        [SerializeField]
        float maxSpeed = 10f;				// The fastest the player can travel in the x axis.
        [SerializeField]
        float jumpForce = 400f;			// Amount of force added when the player jumps.	

        [SerializeField]
        bool airControl = false;            // Whether or not a player can steer while jumping;
        [SerializeField]
        public LayerMask whatIsGround;			// A mask determining what is ground to the character

        Transform groundCheck;								// A position marking where to check if the player is grounded.
        float groundedRadius = .2f;							// Radius of the overlap circle to determine if grounded
        bool grounded = false;								// Whether or not the player is grounded.

        void Awake()
        {
            // Setting up references.
            groundCheck = transform.Find("GroundCheck");
        }


        void FixedUpdate()
        {
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
        }

        public void Move(float move, bool jump)
        {
            //only control the player if grounded or airControl is turned on
            if (grounded || airControl)
            {
                // Move the character
                GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !facingRight)
                    // ... flip the player.
                    Flip();
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && facingRight)
                    // ... flip the player.
                    Flip();
            }

            // If the player should jump...
            if (grounded && jump)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            }
        }

        void Flip()
        {
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
