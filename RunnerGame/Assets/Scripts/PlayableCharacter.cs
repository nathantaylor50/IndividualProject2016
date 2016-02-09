using UnityEngine;
using System.Collections;

namespace RunnerGame
{ 
    /*
        This class will hold code for the playable classes, 
        this is a seperate class in case I need to encapsulate to have
        another player (multiplayer shadow car) to use the code as well.
    */ 
    public class PlayableCharacter : MonoBehaviour {


        //should default mecanim be used?
        public bool UseDefaultMecanim = true;
        //returns true if the character is currently grounded
        public bool IsGrounded { get { return _grounded; } }
        //if true, the object will try to go back to its starting position
        public bool TryResetPosition = true;
        // the speed of which the object should try to go back to its starting position
        public float ResetPosistionSpeed = 0.5f;

        protected Vector3 _initialPosition;
        protected bool _grounded;
        //animation for wheel (might not need)
        protected Animator _animator;
        //
        protected Rigidbody _rigidbody;

	    // Use this for initialization
	    protected virtual void Awake () {
            Initialize();
	    }

        //this initializes animator element
        protected virtual void Initialize()
        {
            _animator = GetComponent<Animator>();
        }

        //define the initial position of the character (used to reset position)
        //method is virtual to be overriddem
        public virtual void SetInitialPosition(Vector3 initialPosition)
        {
            _initialPosition = initialPosition;
        }

        // FixedUpdate is called every physic step and is more consitent 
        //than Update function. so is better for performance
        void FixedUpdate()
        {
            //send various states to the animator 
            UpdateAnimator();
            //Lerp position to its initial position (Linearly interpolates between two vectors.)
            ResetPosition();
            
        }

        /*
            called at Update() and sets each animator parameter to its corresponding state values
        */
        protected virtual void UpdateAnimator()
        {
            if(_animator ==null)
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
            MiscTools.updateAnimatorBool(_animator, "Grounded", IsGrounded);
            MiscTools.UpdateAnimatorFloat(_animator, "VerticalSpeed", _rigidbody.velocity.y);
        }

        /* 
            Called on fixed update, and tries to return the object to its initial position
        */
        protected virtual void ResetPosition()
        {
            if(TryResetPosition)
            {
                if (IsGrounded)
                {
                    _rigidbody.velocity = new Vector3((_initialPosition.x - transform.position.x) * (ResetPosistionSpeed), _rigidbody.velocity.y, _rigidbody.velocity.z);
                }
            }
        }

        //what happens then the main action button is pressed
        public virtual void MainActionStart() {  }

        //what happens when the main action button is released
        public virtual void MainActionEnd() { }

        //what happens when the main action is being pressed
        public virtual void MainActionOnGoing() { }

        //what happens when when the down button is pressed
        public virtual void DownStart() { }

        //what happens when the down button is released
        public virtual void DownEnd() { }

        //what happens when the down button is being pressed
        public virtual void DownOnGoing() { }

        //what happens when the up button is begin pressed
        public virtual void UpStart() { }

        //what happens when the up button is released
        public virtual void UpEnd() { }

        //what happens when the up button is being pressed
        public virtual void UpOnGoing() { }

        //what happens when the left button is pressed
        public virtual void LeftStart() { }

        //what happens when the left button is released
        public virtual void LeftEnd() { }

        //what happens when the left button is being pressed
        public virtual void LeftOnGoing() { }

        //what happens when the right button is pressed
        public virtual void RightStart() { }

        //what happens when the right button is released 
        public virtual void RightEnd() { }

        //what happens when the right button is being pressed
        public virtual void RightOnGoing() { }


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
                    _grounded = true;
                }
            }
        }

        //Detects when the object leaves the ground#
        protected virtual void CollisionExit(GameObject collidingObject)
        {
            //if we're leaving the ground
            if (collidingObject.layer == LayerMask.NameToLayer("Ground"))
            {
                _grounded = false;
            }
        }
    }
}