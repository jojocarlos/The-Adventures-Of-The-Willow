/*
	Created by @DawnosaurDev at youtube.com/c/DawnosaurStudios
	Thanks so much for checking this out and I hope you find it helpful! 
	If you have any further queries, questions or feedback feel free to reach out on my twitter or leave a comment on youtube :D

	Feel free to use this in your own games, and I'd love to see anything you make!
 */

using FMOD.Studio;
using System.Collections;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2D : MonoBehaviour, IDataPersistence
{
	public static PlayerMovement2D PlayerMovement2Dinstance;
	//Scriptable object which holds all the player's movement parameters. If you don't want to use it
	//just paste in all the parameters, though you will need to manuly change all references in this script
	public PlayerData Data;

	#region COMPONENTS
    public Rigidbody2D RB { get; private set; }
	//Script to handle all player animations, all references can be safely removed if you're importing into your own project.
	public PlayerAnimator AnimHandler { get; private set; }
	#endregion

	#region STATE PARAMETERS
	//Variables control the various actions the player can perform at any time.
	//These are fields which can are public allowing for other sctipts to read them
	//but can only be privately written to.
	public bool IsFacingRight { get; private set; }
	public bool facingRight;

    public bool IsJumping { get; private set; }
	public bool IsWallJumping { get; private set; }
	public bool IsDashing { get; private set; }
	public bool IsSliding { get; private set; }

	//Timers (also all fields, could be private and a method returning a bool could be used)
	public float LastOnGroundTime { get; private set; }
	public float LastOnWallTime { get; private set; }
	public float LastOnWallRightTime { get; private set; }
	public float LastOnWallLeftTime { get; private set; }

	public bool isGrounded;

	//Jump
	private bool _isJumpCut;
	private bool _isJumpFalling;

	//Wall Jump
	private float _wallJumpStartTime;
	private int _lastWallJumpDir;

	//Dash
	private int _dashesLeft;
	private bool _dashRefilling;
	private Vector2 _lastDashDir;
	private bool _isDashAttacking;

	#endregion

	#region INPUT PARAMETERS
	public Vector2 _moveInput;

	public float LastPressedJumpTime { get; private set; }
	public float LastPressedDashTime { get; private set; }
	#endregion

	#region CHECK PARAMETERS
	//Set all of these up in the inspector
	[Header("Checks")] 
	[SerializeField] private Transform _groundCheckPoint;
	//Size of groundCheck depends on the size of your character generally you want them slightly small than width (for ground) and height (for the wall check)
	[SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
	[Space(5)]
	[SerializeField] private Transform _frontWallCheckPoint;
	[SerializeField] private Transform _backWallCheckPoint;
	[SerializeField] private Vector2 _wallCheckSize = new Vector2(0.5f, 1f);
    #endregion

    #region LAYERS & TAGS
    [Header("Layers & Tags")]
	[SerializeField] private LayerMask _groundLayer;
    #endregion


    [Header("ladder")]
    public float vertical;
    public bool isLadder;
    public bool isClimbing;


	//Swimming
	[Header("Swiming System")]
	public bool isOnWater;
    public bool waterTop;
    public bool Swimming;
	private Vector2 swimMoveDirection;

    //New input
    private float horizontal;

    //slopes
    [SerializeField] public PhysicsMaterial2D withFriction;
    [SerializeField] public PhysicsMaterial2D noFrictionNormal0;
	private BoxCollider2D BC2D;

    //Particles
    public ParticleSystem Bubble;


    [SerializeField] private bool getposition;
    //Pipe
    public bool isOnPipe = false;

    //Platforms
    private Transform _originalParent;
    public Transform PlayerTrans;
    public float zRotation = 0f;

    //Sounds
    private EventInstance playerFootsteps;

    //Sounds Condition
    private bool isForest;

    //Death
    public bool isDead = false;


    //KnockBack
    public float KnockBack;
    public float KnockBackCount;
    public float KnockBackLength;

    public bool KnockFromRight;


    private void Awake()
	{
		RB = GetComponent<Rigidbody2D>();
		AnimHandler = GetComponent<PlayerAnimator>();
		if(PlayerMovement2Dinstance == null)
		{
            PlayerMovement2Dinstance = this;

        }
	}

	private void Start()
	{
		SetGravityScale(Data.gravityScale);
		IsFacingRight = true;
		BC2D = GetComponent<BoxCollider2D>();
        _originalParent = transform.parent;
        isOnPipe = false;
        playerFootsteps = AudioManager.instance.CreateInstance(FMODEvents.instance.playerFootsteps);
    }

    public void enemyStompJump()
    {
        RB.velocity = new Vector2(RB.velocity.x, Data.jumpStompEnemy);
		IsJumping = true;
    }

    //New input system Jump
    public void Jump(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
            OnJumpInput();
        }
		if (context.canceled)
		{
            OnJumpUpInput();
        }

    }
    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnDashInput();
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    private void Update()
	{

		//fix player rotation on platform rotation
		if (!Swimming)
		{
			Vector3 eulers = PlayerTrans.eulerAngles;
			PlayerTrans.eulerAngles = new Vector3(eulers.x, eulers.y, zRotation);
		}

        #region TIMERS
        LastOnGroundTime -= Time.deltaTime;
		LastOnWallTime -= Time.deltaTime;
		LastOnWallRightTime -= Time.deltaTime;
		LastOnWallLeftTime -= Time.deltaTime;

		LastPressedJumpTime -= Time.deltaTime;
		LastPressedDashTime -= Time.deltaTime;
		#endregion

		#region INPUT HANDLER
		_moveInput.x = horizontal;
		_moveInput.y = vertical;

		if (_moveInput.x != 0)
			CheckDirectionToFace(_moveInput.x > 0);

		//if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.J))
		//{
		//	OnJumpInput();
		//}

		//if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.J))
		//{
		//	OnJumpUpInput();
		//}

		//if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.K))
		//{
		//	OnDashInput();
		//}
		#endregion

		#region COLLISION CHECKS
		if (!IsDashing && !IsJumping)
		{
			//Ground Check
			if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer) && !IsJumping) //checks if set box overlaps with ground
			{
				if(LastOnGroundTime < -0.1f)
                {
					AnimHandler.justLanded = true;
                }
				isGrounded = true;

				LastOnGroundTime = Data.coyoteTime; //if so sets the lastGrounded to coyoteTime
            }	
			else
			{
				isGrounded = false;
			}

			//Right Wall Check
			if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && IsFacingRight)
					|| (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !IsFacingRight)) && !IsWallJumping)
				LastOnWallRightTime = Data.coyoteTime;

			//Right Wall Check
			if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !IsFacingRight)
				|| (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && IsFacingRight)) && !IsWallJumping)
				LastOnWallLeftTime = Data.coyoteTime;

			//Two checks needed for both left and right walls since whenever the play turns the wall checkPoints swap sides
			LastOnWallTime = Mathf.Max(LastOnWallLeftTime, LastOnWallRightTime);
		}
		#endregion

		#region JUMP CHECKS
		if (IsJumping && RB.velocity.y < 0)
		{
			IsJumping = false;

			if(!IsWallJumping)
				_isJumpFalling = true;
		}

		if (IsWallJumping && Time.time - _wallJumpStartTime > Data.wallJumpTime)
		{
			IsWallJumping = false;
		}

		if (LastOnGroundTime > 0 && !IsJumping && !IsWallJumping)
        {
			_isJumpCut = false;

			if(!IsJumping)
				_isJumpFalling = false;
		}

		if (!IsDashing)
		{
			//Jump
			if (CanJump() && LastPressedJumpTime > 0)
			{
				IsJumping = true;
				IsWallJumping = false;
				_isJumpCut = false;
				_isJumpFalling = false;
				Jump();

				AnimHandler.startedJumping = true;
			}
			//WALL JUMP
			else if (CanWallJump() && LastPressedJumpTime > 0)
			{
				IsWallJumping = true;
				IsJumping = false;
				_isJumpCut = false;
				_isJumpFalling = false;

				_wallJumpStartTime = Time.time;
				_lastWallJumpDir = (LastOnWallRightTime > 0) ? -1 : 1;

				WallJump(_lastWallJumpDir);
			}
		}
		#endregion

		#region DASH CHECKS
		if (CanDash() && LastPressedDashTime > 0)
		{
			//Freeze game for split second. Adds juiciness and a bit of forgiveness over directional input
			Sleep(Data.dashSleepTime); 

			//If not direction pressed, dash forward
			if (_moveInput != Vector2.zero)
				_lastDashDir = _moveInput;
			else
				_lastDashDir = IsFacingRight ? Vector2.right : Vector2.left;



			IsDashing = true;
			IsJumping = false;
			IsWallJumping = false;
			_isJumpCut = false;

			StartCoroutine(nameof(StartDash), _lastDashDir);
		}
		#endregion

		#region SLIDE CHECKS
		if (CanSlide() && ((LastOnWallLeftTime > 0 && _moveInput.x < 0) || (LastOnWallRightTime > 0 && _moveInput.x > 0)))
			IsSliding = true;
		else
			IsSliding = false;
		#endregion

		#region GRAVITY
		if (!_isDashAttacking && !isOnWater)
		{
			//Higher gravity if we've released the jump input or are falling
			if (IsSliding)
			{
				SetGravityScale(0);
			}
			else if (RB.velocity.y < 0 && _moveInput.y < 0)
			{
				//Much higher gravity if holding down
				SetGravityScale(Data.gravityScale * Data.fastFallGravityMult);
				//Caps maximum fall speed, so when falling over large distances we don't accelerate to insanely high speeds
				RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFastFallSpeed));
			}
			else if (_isJumpCut)
			{
				//Higher gravity if jump button released
				SetGravityScale(Data.gravityScale * Data.jumpCutGravityMult);
				RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFallSpeed));
			}
			else if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
			{
				SetGravityScale(Data.gravityScale * Data.jumpHangGravityMult);
			}
			else if (RB.velocity.y < 0)
			{
				//Higher gravity if falling
				SetGravityScale(Data.gravityScale * Data.fallGravityMult);
				//Caps maximum fall speed, so when falling over large distances we don't accelerate to insanely high speeds
				RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFallSpeed));
			}
			else
			{
				//Default gravity if standing on a platform or moving upwards
				SetGravityScale(Data.gravityScale);
			}
		}
		else
		{
			if (!isOnWater)
			{
				//No gravity when dashing (returns to normal once initial dashAttack phase over)
				SetGravityScale(0);
			}
		}

		if(waterTop)
		{
            if (RB.velocity.y < 0)
            {
                //Higher gravity if falling
                SetGravityScale(Data.gravityScale * Data.fallGravityMult);
                //Caps maximum fall speed, so when falling over large distances we don't accelerate to insanely high speeds
                RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFallSpeed));
            }
            else
            {
                //Default gravity if standing on a platform or moving upwards
                SetGravityScale(Data.gravityScale);
            }
        }
		#endregion


		if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }

		if (Swimming && !isDead && !waterTop)
		{
            swimMoveDirection = new Vector2(_moveInput.x, _moveInput.y);
			float inputMagnitude = Mathf.Clamp01(swimMoveDirection.magnitude);
			swimMoveDirection.Normalize();
			transform.Translate(swimMoveDirection * Data.SwimSpeed * inputMagnitude * Time.deltaTime, Space.World);

			if (swimMoveDirection != Vector2.zero)
			{
				Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, swimMoveDirection);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Data.RotationSpeed * Time.deltaTime);
			}
		}
		else if(waterTop)
		{
            swimMoveDirection = new Vector2(_moveInput.x, _moveInput.y);
            float inputMagnitude = Mathf.Clamp01(swimMoveDirection.magnitude);
            swimMoveDirection.Normalize();
            transform.Translate(swimMoveDirection * Data.SwimSpeed * inputMagnitude * Time.deltaTime, Space.World);
        }

		if(_moveInput.x == 0 && isGrounded)
		{
            BC2D.sharedMaterial = withFriction;
            RB.sharedMaterial = withFriction;
        }
		else 
		{
            BC2D.sharedMaterial = noFrictionNormal0;
            RB.sharedMaterial = noFrictionNormal0;
        }


        UpdateSound();
    }
    private void FixedUpdate()
	{
        //Climb
        if (isClimbing && !isDead)
        {
            RB.velocity = new Vector2(RB.velocity.x, _moveInput.y * Data.speedladder);
        }
		if(_moveInput.x != 0)
		{
            isClimbing = false;
        }

        //Handle Run
        if (!IsDashing)
		{
			if (IsWallJumping)
				Run(Data.wallJumpRunLerp);
			else
				Run(1);
		}
		else if (_isDashAttacking)
		{
			Run(Data.dashEndRunLerp);
		}

		//Handle Slide
		if (IsSliding)
			Slide();
    }

    #region INPUT CALLBACKS
	//Methods which whandle input detected in Update()
    public void OnJumpInput()
	{
		if (!isDead)
		{
			LastPressedJumpTime = Data.jumpInputBufferTime;
			isClimbing = false;
			if (Swimming)
			{
				Data.SwimSpeed = 20f;
				RB.AddForce(transform.rotation * Vector2.up * Data.SwimForce * Data.SwimSpeed, ForceMode2D.Impulse);
				//RB.AddForce(new Vector2(RB.velocity.x, Data.SwimForce), ForceMode2D.Impulse);
			}
			else if(waterTop)
			{
                Data.SwimSpeed = 10f;
                LastOnGroundTime = Data.coyoteTime;
                SetGravityScale(Data.gravityScale);
                RB.AddForce(transform.rotation * Vector2.up * Data.SwimSpeed, ForceMode2D.Impulse);
            }
		}
    }

	public void OnJumpUpInput()
	{
		if (CanJumpCut() || CanWallJumpCut())
			_isJumpCut = true;

		if(Swimming)
		{
            Data.SwimSpeed = 5f;
        }
		else
		{
            Data.SwimSpeed = 5f;
        }
	}

	public void OnDashInput()
	{
		if (!isDead)
		{
			LastPressedDashTime = Data.dashInputBufferTime;
			isClimbing = false;
		}
    }
    #endregion

    #region GENERAL METHODS
    public void SetGravityScale(float scale)
	{
		RB.gravityScale = scale;
	}

	private void Sleep(float duration)
    {
		//Method used so we don't need to call StartCoroutine everywhere
		//nameof() notation means we don't need to input a string directly.
		//Removes chance of spelling mistakes and will improve error messages if any
		StartCoroutine(nameof(PerformSleep), duration);
    }

	private IEnumerator PerformSleep(float duration)
    {
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(duration); //Must be Realtime since timeScale with be 0 
		Time.timeScale = 1;
	}
    #endregion

	//MOVEMENT METHODS
    #region RUN METHODS
    private void Run(float lerpAmount)
	{
		if (!Swimming && !isDead && KnockBackCount <= 0)
        {
			//Calculate the direction we want to move in and our desired velocity
			float targetSpeed = _moveInput.x * Data.runMaxSpeed;
			//We can reduce are control using Lerp() this smooths changes to are direction and speed
			targetSpeed = Mathf.Lerp(RB.velocity.x, targetSpeed, lerpAmount);

			#region Calculate AccelRate
			float accelRate;

			//Gets an acceleration value based on if we are accelerating (includes turning) 
			//or trying to decelerate (stop). As well as applying a multiplier if we're air borne.
			if (LastOnGroundTime > 0)
				accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
			else
				accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount * Data.accelInAir : Data.runDeccelAmount * Data.deccelInAir;
			#endregion

			#region Add Bonus Jump Apex Acceleration
			//Increase are acceleration and maxSpeed when at the apex of their jump, makes the jump feel a bit more bouncy, responsive and natural
			if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
			{
				accelRate *= Data.jumpHangAccelerationMult;
				targetSpeed *= Data.jumpHangMaxSpeedMult;
			}
			#endregion

			#region Conserve Momentum
			//We won't slow the player down if they are moving in their desired direction but at a greater speed than their maxSpeed
			if (Data.doConserveMomentum && Mathf.Abs(RB.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(RB.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
			{
				//Prevent any deceleration from happening, or in other words conserve are current momentum
				//You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
				accelRate = 0;
			}
			#endregion

			//Calculate difference between current velocity and desired velocity
			float speedDif = targetSpeed - RB.velocity.x;
			//Calculate force along x-axis to apply to thr player

			float movement = speedDif * accelRate;

			//Convert this to a vector and apply to rigidbody
			RB.AddForce(movement * Vector2.right, ForceMode2D.Force);

            /*
			 * For those interested here is what AddForce() will do
			 * RB.velocity = new Vector2(RB.velocity.x + (Time.fixedDeltaTime  * speedDif * accelRate) / RB.mass, RB.velocity.y);
			 * Time.fixedDeltaTime is by default in Unity 0.02 seconds equal to 50 FixedUpdate() calls per second
			*/
        }
        else
        {
            if (KnockFromRight)
            {
                RB.velocity = new Vector2(-KnockBack, KnockBack);
            }
            if (!KnockFromRight)
            {
                RB.velocity = new Vector2(KnockBack, KnockBack);
            }
            KnockBackCount -= Time.deltaTime;
        }

		if(waterTop)
		{
            //Calculate the direction we want to move in and our desired velocity
            float targetSpeed = _moveInput.x * Data.runMaxSpeed;
            //We can reduce are control using Lerp() this smooths changes to are direction and speed
            targetSpeed = Mathf.Lerp(RB.velocity.x, targetSpeed, lerpAmount);

            #region Calculate AccelRate
            float accelRate;

            //Gets an acceleration value based on if we are accelerating (includes turning) 
            //or trying to decelerate (stop). As well as applying a multiplier if we're air borne.
            if (LastOnGroundTime > 0)
                accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
            else
                accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount * Data.accelInAir : Data.runDeccelAmount * Data.deccelInAir;
            #endregion

            #region Add Bonus Jump Apex Acceleration
            //Increase are acceleration and maxSpeed when at the apex of their jump, makes the jump feel a bit more bouncy, responsive and natural
            if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
            {
                accelRate *= Data.jumpHangAccelerationMult;
                targetSpeed *= Data.jumpHangMaxSpeedMult;
            }
            #endregion

            #region Conserve Momentum
            //We won't slow the player down if they are moving in their desired direction but at a greater speed than their maxSpeed
            if (Data.doConserveMomentum && Mathf.Abs(RB.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(RB.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
            {
                //Prevent any deceleration from happening, or in other words conserve are current momentum
                //You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
                accelRate = 0;
            }
            #endregion

            //Calculate difference between current velocity and desired velocity
            float speedDif = targetSpeed - RB.velocity.x;
            //Calculate force along x-axis to apply to thr player

            float movement = speedDif * accelRate;

            //Convert this to a vector and apply to rigidbody
            RB.AddForce(movement * Vector2.right, ForceMode2D.Force);

            /*
			 * For those interested here is what AddForce() will do
			 * RB.velocity = new Vector2(RB.velocity.x + (Time.fixedDeltaTime  * speedDif * accelRate) / RB.mass, RB.velocity.y);
			 * Time.fixedDeltaTime is by default in Unity 0.02 seconds equal to 50 FixedUpdate() calls per second
			*/
        }
    }

	private void Turn()
	{
		//stores scale and flips the player along the x axis, 
		Vector3 scale = transform.localScale; 
		scale.x *= -1;
		transform.localScale = scale;
		facingRight = !facingRight;
        IsFacingRight = !IsFacingRight;
	}
    #endregion

    #region JUMP METHODS
    private void Jump()
	{
		//Audio
		AudioManager.instance.PlayOneShot(FMODEvents.instance.playerJump);

        //Ensures we can't call Jump multiple times from one press
        LastPressedJumpTime = 0;
		LastOnGroundTime = 0;

		#region Perform Jump
		//We increase the force applied if we are falling
		//This means we'll always feel like we jump the same amount 
		//(setting the player's Y velocity to 0 beforehand will likely work the same, but I find this more elegant :D)
		float force = Data.jumpForce;
		if (RB.velocity.y < 0)
			force -= RB.velocity.y;

		RB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
		#endregion
	}

	private void WallJump(int dir)
	{
		//Ensures we can't call Wall Jump multiple times from one press
		LastPressedJumpTime = 0;
		LastOnGroundTime = 0;
		LastOnWallRightTime = 0;
		LastOnWallLeftTime = 0;

		#region Perform Wall Jump
		Vector2 force = new Vector2(Data.wallJumpForce.x, Data.wallJumpForce.y);
		force.x *= dir; //apply force in opposite direction of wall

		if (Mathf.Sign(RB.velocity.x) != Mathf.Sign(force.x))
			force.x -= RB.velocity.x;

		if (RB.velocity.y < 0) //checks whether player is falling, if so we subtract the velocity.y (counteracting force of gravity). This ensures the player always reaches our desired jump force or greater
			force.y -= RB.velocity.y;

		//Unlike in the run we want to use the Impulse mode.
		//The default mode will apply are force instantly ignoring masss
		RB.AddForce(force, ForceMode2D.Impulse);
		#endregion
	}
	#endregion

	#region DASH METHODS
	//Dash Coroutine
	private IEnumerator StartDash(Vector2 dir)
	{
		//Overall this method of dashing aims to mimic Celeste, if you're looking for
		// a more physics-based approach try a method similar to that used in the jump

		LastOnGroundTime = 0;
		LastPressedDashTime = 0;

		float startTime = Time.time;

		_dashesLeft--;
		_isDashAttacking = true;

		SetGravityScale(0);

		//We keep the player's velocity at the dash speed during the "attack" phase (in celeste the first 0.15s)
		while (Time.time - startTime <= Data.dashAttackTime)
		{
			RB.velocity = dir.normalized * Data.dashSpeed;
			//Pauses the loop until the next frame, creating something of a Update loop. 
			//This is a cleaner implementation opposed to multiple timers and this coroutine approach is actually what is used in Celeste :D
			yield return null;
		}

		startTime = Time.time;

		_isDashAttacking = false;

		//Begins the "end" of our dash where we return some control to the player but still limit run acceleration (see Update() and Run())
		SetGravityScale(Data.gravityScale);
		RB.velocity = Data.dashEndSpeed * dir.normalized;

		while (Time.time - startTime <= Data.dashEndTime)
		{
			yield return null;
		}

		//Dash over
		IsDashing = false;
	}

	//Short period before the player is able to dash again
	private IEnumerator RefillDash(int amount)
	{
		//SHoet cooldown, so we can't constantly dash along the ground, again this is the implementation in Celeste, feel free to change it up
		_dashRefilling = true;
		yield return new WaitForSeconds(Data.dashRefillTime);
		_dashRefilling = false;
		_dashesLeft = Mathf.Min(Data.dashAmount, _dashesLeft + 1);
	}
	#endregion

	#region OTHER MOVEMENT METHODS
	private void Slide()
	{
		//Works the same as the Run but only in the y-axis
		//THis seems to work fine, buit maybe you'll find a better way to implement a slide into this system
		float speedDif = Data.slideSpeed - RB.velocity.y;	
		float movement = speedDif * Data.slideAccel;
		//So, we clamp the movement here to prevent any over corrections (these aren't noticeable in the Run)
		//The force applied can't be greater than the (negative) speedDifference * by how many times a second FixedUpdate() is called. For more info research how force are applied to rigidbodies.
		movement = Mathf.Clamp(movement, -Mathf.Abs(speedDif)  * (1 / Time.fixedDeltaTime), Mathf.Abs(speedDif) * (1 / Time.fixedDeltaTime));

		RB.AddForce(movement * Vector2.up);
	}
    #endregion


    #region CHECK METHODS
    public void CheckDirectionToFace(bool isMovingRight)
	{
		if (isMovingRight != IsFacingRight)
			Turn();
	}

    private bool CanJump()
    {
		return LastOnGroundTime > 0 && !IsJumping;
    }

	private bool CanWallJump()
    {
		return LastPressedJumpTime > 0 && LastOnWallTime > 0 && LastOnGroundTime <= 0 && (!IsWallJumping ||
			 (LastOnWallRightTime > 0 && _lastWallJumpDir == 1) || (LastOnWallLeftTime > 0 && _lastWallJumpDir == -1));
	}

	private bool CanJumpCut()
    {
		return IsJumping && RB.velocity.y > 0;
    }

	private bool CanWallJumpCut()
	{
		return IsWallJumping && RB.velocity.y > 0;
	}

	private bool CanDash()
	{
		if (!IsDashing && _dashesLeft < Data.dashAmount && LastOnGroundTime > 0 && !_dashRefilling)
		{
			StartCoroutine(nameof(RefillDash), 1);
		}

		return _dashesLeft > 0;
	}

	public bool CanSlide()
    {
		if (LastOnWallTime > 0 && !IsJumping && !IsWallJumping && !IsDashing && LastOnGroundTime <= 0)
			return true;
		else
			return false;
	}
    #endregion


    #region EDITOR METHODS
    private void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(_frontWallCheckPoint.position, _wallCheckSize);
		Gizmos.DrawWireCube(_backWallCheckPoint.position, _wallCheckSize);
	}
    #endregion

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("water"))
        {
            Data.SwimSpeed = 5f;
            Swimming = true;
			isOnWater = true;
			waterTop = false;
			RB.drag = 5f;
            SetGravityScale(Data.SwimGravity);
            Bubble.Play();
        }
        if (col.gameObject.CompareTag("waterTop"))
        {
            waterTop = true;
            isOnWater = true;
            SetGravityScale(Data.gravityScale);
        }

        if (col.gameObject.CompareTag("Ladder"))
        {
            isLadder = true;
        }

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("water"))
        {
            //cameraZoom.ZoomOut();
            isOnWater = true;
			waterTop = false;
            Bubble.Play();
        }
        if (col.gameObject.CompareTag("waterTop"))
        {
			waterTop = true;
            isOnWater = true;
            SetGravityScale(Data.gravityScale);
        }
        if (col.gameObject.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("water") == true)
        {
			//cameraZoom.ZoomIn();
			isOnWater = false;
            Swimming = false;
            RB.drag = 0f;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            Bubble.Stop();
        }

        if (col.gameObject.CompareTag("waterTop"))
        {
            waterTop = false;
			if(!Swimming)
			{
                isOnWater = false;
            }
            SetGravityScale(Data.gravityScale);
        }
        if (col.gameObject.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }

    }

    public void TrampolineGo()
    {
        RB.velocity = transform.up * Data.TrampolineJumpForce;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "MovingPlatform")
        {
            transform.parent = col.gameObject.transform;
        }

        if (col.gameObject.tag == "EnemyPlatform")
        {
            transform.parent = col.gameObject.transform;
        }


    }

    private void OnCollisionExit2D(Collision2D col)
    {

        if (col.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
        }

        if (col.gameObject.tag == "EnemyPlatform")
        {
            transform.parent = null;
        }

    }

    //Platforms
    public void SetParent(Transform newParent)
    {
        _originalParent = transform.parent;
        transform.parent = newParent;
    }

    public void ResetParent()
    {
        transform.parent = _originalParent;
    }


    //Persistence save data player position
    public void LoadData(GameData data)
    {
        if (getposition)
        {
            this.transform.position = data.playerPosition;
        }
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
    }

    private void UpdateSound()
    {
		if (RB.velocity.x != 0 && _moveInput.x != 0 && isGrounded)
        {
            if (isForest)
            {
                PLAYBACK_STATE playbackState;
                playerFootsteps.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
                {
                    playerFootsteps.start();
                }
            }
        }
        else
        {
            playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}