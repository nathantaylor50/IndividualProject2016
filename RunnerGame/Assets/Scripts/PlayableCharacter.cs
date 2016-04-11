using UnityEngine;
using System.Collections;

namespace RunnerGame
{ 
	/// <summary>
	/// Playable character. extend this class to access code for specific playable characters
	/// This code is encapsulated to make debugging less strenous
	/// </summary>

    public class PlayableCharacter : MonoBehaviour {


        ///should default mecanim be used?
        public bool UseDefaultMecanim = true;
        ///returns true if the character is currently grounded
        //if true, the object will try to go back to its starting position
        public bool ShouldResetPosition = true;
        ///the speed of which the object should try to go back to its starting position
        public float ResetPosistionSpeed = 0.5f;
		///the distance between gameObject and the ground
		public float DistanceToGround { get; protected set; }

        protected Vector3 initialPosition;
        protected bool grounded;
		protected Animator animator;
		protected float distanceToTheGroundRayLength = 50.0f;
		protected float groundDistanceTolerance = 0.05f;
		protected LayerMask collisionMask;

	    /// <summary>
		/// Use this for initialization
	    /// </summary>
	    protected virtual void Awake () 
		{
            Initialize();
	    }

		/// <summary>
		/// Start this instance.
		/// </summary>
		protected virtual void Start() 
		{
			
		}
			
        /// <summary>
		/// this initializes animator element
        /// </summary>
        protected virtual void Initialize()
        {
            animator = GetComponent<Animator>();
        }


		/// <summary>
		/// Sets the initial position of the character (used to reset position)
		/// </summary>
		/// <param name="initPos">Initial position.</param>
        public virtual void SetInitialPosition(Vector3 initPos)
        {
            initialPosition = initPos;
        }

        // FixedUpdate is called every physic step and is more consitent 
        //than Update function. so is better for performance
        protected virtual void FixedUpdate()
        {
            //send various states to the animator 
            UpdateAnimator();
            //Lerp position to its initial position (Linearly interpolates between two vectors.)
            ResetPosition();
			//check if player is out of bounds or not
			CheckDeathBounds();
			//distance between ground and player
			CalDistanceToGround();
            
        }

		/// <summary>
		/// Distances between player and ground.
		/// </summary>
		protected virtual void CalDistanceToGround()
		{

			//cast a ray down below to check if current pos is above ground level and checks the distance
			Vector2 raycastOrigin = transform.position;
			//RaycastHit2D raycast = 
		}

		/// <summary>
		/// checks death bounds
		/// </summary>
		protected virtual void CheckDeathBounds()
		{

		}
		/// <summary>
		/// Gets the object bounds.
		/// </summary>
		/// <returns>The object bounds.</returns>
		protected virtual Bounds GetObjectBounds()
		{

		}



           /// called at Update() and sets each animator parameter to its corresponding state values

        protected virtual void UpdateAnimator()
        {
            if(animator ==null)
            { return;  }
            //send various states to the animator.
            if (UseDefaultMecanim)
            {
                UpdateAllMecanimAnimators();
            }
        }

        

        /*
            Update all mecanim animators
        */
        protected virtual void UpdateAllMecanimAnimators()
        {
			MiscTools.updateAnimatorBool(animator, "Grounded", grounded);
            //MiscTools.UpdateAnimatorFloat(animator, "VerticalSpeed", rigidbody.velocity.y);
        }

        /* 
            Called on fixed update, and tries to return the object to its initial position
        */
        protected virtual void ResetPosition()
        {
			if(ShouldResetPosition)
            {
                if (grounded)
                {
                   // rigidbody.velocity = new Vector3((_initialPosition.x - transform.position.x) * (ResetPosistionSpeed), _rigidbody.velocity.y, _rigidbody.velocity.z);
                }
            }
        }

        //what happens then the main action button is pressed
        public virtual void MainActionPresseed() {  }

        //what happens when the main action button is released
        public virtual void MainActionReleased() { }

        //what happens when the main action is being pressed
        public virtual void MainActionPressing() { }

        //what happens when when the down button is pressed
        public virtual void DownPressed() { }

        //what happens when the down button is released
        public virtual void Downreleased() { }

        //what happens when the down button is being pressed
        public virtual void DownPressing() { }

        //what happens when the up button is begin pressed
        public virtual void UpPressed() { }

        //what happens when the up button is released
        public virtual void UpReleased() { }

        //what happens when the up button is being pressed
        public virtual void UpPressing() { }

        //what happens when the left button is pressed
        public virtual void LeftPressed() { }

        //what happens when the left button is released
        public virtual void LeftReleased() { }

        //what happens when the left button is being pressed
        public virtual void LeftPressing() { }

        //what happens when the right button is pressed
        public virtual void RightPressed() { }

        //what happens when the right button is released 
        public virtual void RightReleased() { }

        //what happens when the right button is being pressed
        public virtual void RightPressing() { }


        //disables the playable character
        public virtual void Disable()
        {
            gameObject.SetActive(false);
        }

        //destroy the object (die)
        public virtual void die()
        {
            Destroy(gameObject);
        }

        //Handles enter collision with 3D colliders
        protected virtual void OnCollisionEnter (Collision collidingObject)
        {
            CollisionEnter(collidingObject.collider.gameObject);
        }

        //handles the exit collision with 3D colliders
        protected virtual void OnCollisionExit (Collision collidingObject)
        {
            CollisionExit(collidingObject.collider.gameObject);
        }

        //Detects when the object touches a object on the ground layer
        protected virtual void CollisionEnter(GameObject collidingObject)
        {
            //if we're entering a collision with the ground
            if(collidingObject.layer == LayerMask.NameToLayer ("Ground"))
            {
                if(collidingObject.transform.position.y <= transform.position.y)
                {
                    grounded = true;
                }
            }
        }

        //Detects when the object leaves the ground#
        protected virtual void CollisionExit(GameObject collidingObject)
        {
            //if we're leaving the ground
            if (collidingObject.layer == LayerMask.NameToLayer("Ground"))
            {
                grounded = false;
            }
        }
    }
}