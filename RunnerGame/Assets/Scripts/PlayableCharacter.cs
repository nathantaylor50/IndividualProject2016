using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace RunnerGame
{ 
	/// <summary>
	/// Playable character. extend this class to access code for specific playable characters
	/// This code is encapsulated to make debugging less strenous
	/// </summary>

	public class PlayableCharacter :  NetworkBehaviour {


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
		protected RigidbodyInterface rigidbodyInterface; //????
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
			rigidbodyInterface = GetComponent<RigidbodyInterface> ();		
			animator = GetComponent<Animator>();

			if (rigidbodyInterface == null)
			{
				return;
			}
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
        protected virtual void Update()
        {
            //send various states to the animator 
            UpdateAnimator();
            //Lerp position to its initial position (Linearly interpolates between two vectors.)
            ResetPosition();
			//check if player is out of bounds or not
			CheckDeathBounds();

            
        }



		/// <summary>
		/// checks death bounds
		/// </summary>
		protected virtual void CheckDeathBounds()
		{
			if (LevelManager.Instance.CheckDeathObjectBounds (GetObjectBounds ())) {
				LevelManager.Instance.KillPlayer (this);
			}
		}
		/// <summary>
		/// Gets the object bounds.
		/// </summary>
		/// <returns>The object bounds.</returns>
		protected virtual Bounds GetObjectBounds()
		{
			if (GetComponent<Collider> () != null) {
				return GetComponent<Collider> ().bounds;
			}

			return GetComponent<Renderer>().bounds;
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
					rigidbodyInterface.Velocity = new Vector3((initialPosition.x - transform.position.x) * (ResetPosistionSpeed), rigidbodyInterface.Velocity.y, rigidbodyInterface.Velocity.z);
                }
            }
        }

		//disables the playable character
		public virtual void Disable()
		{
			gameObject.SetActive(false);
		}

		///destroy the object (die)
		public virtual void Die()
		{
			Destroy(gameObject);
		}

		public virtual void DisableCollisions()
		{
			rigidbodyInterface.EnableBoxCollider (false);
		}

		public virtual void EnableCollisions()
		{

			rigidbodyInterface.EnableBoxCollider (true);
		}


        //what happens then the main action button is pressed
        public virtual void MainActionPressed() {  }

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



		protected virtual void OnTriggerEnter(GameObject collidingObject)
		{
			TriggerEnter (collidingObject.gameObject);

		}


		protected virtual void OnTriggerExit (GameObject collidingObject)
		{
			TriggerExit (collidingObject.gameObject);
		}

		protected virtual void CollisionEnter(GameObject collidingObject)
		{

		}

		protected virtual void CollisionExit (GameObject collidingObject)
		{

		}

		protected virtual void TriggerEnter(GameObject collidingObject)
		{

		}

		protected virtual void TriggerExit (GameObject collidingObject)
		{

		}
    }
}