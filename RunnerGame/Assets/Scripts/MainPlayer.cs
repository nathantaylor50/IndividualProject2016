using UnityEngine;
using System.Collections;
//namespace = collection
namespace RunnerGame
{
    /*
        This class is for having the player character switch lanes by moving left or right
    */ 
    ///  
    public class MainPlayer : PlayableCharacter
    {
        //space and header allows to add a header above the following fields (variables) to unitys editor
        [Space(10)]
        [Header("Lanes")]
        //the width of each lane
        public float LaneWidth = 3f;
        //the number of lanes the runner can run in
        public int NumberOfLanes = 3;
        // the speed, in seconds, at which the runner changes lanes
        public float ChangingLaneSpeed = 1f;
        //the gameobject instantiated when the runner dies
        public GameObject Explosion;

        protected int _currentLane;
        protected bool _isMoving = false;

        // Use this for initialization
       protected override void Awake()
        {
            Initialize();
            //we initialize the current lane, which should be the middle lane in the center
            _currentLane = NumberOfLanes / 2;
            //if the number of lanes is odd, we add one to get a middle lane.
            if (NumberOfLanes % 2 == 1) { _currentLane++; }
        }

        /* 
            on fixed update we handle the animator's update
        */
        protected override void FixedUpdate()
        {
            //send various states to the animator
            UpdateAnimator();
        }

        //triggered when the player presses left
        public override void LeftStart()
        {
            //if already in the left lane, do nothing and exit
            if (_currentLane <= 1) { return; }
            //if runner is already moving do nothing and exit
            if (_isMoving) { return; }
            //move runner to lane to the left
            StartCoroutine(MoveTo(transform.position - Vector3.forward * LaneWidth, ChangingLaneSpeed));
            _currentLane--;
        }

        //triggered when the player presses right
        public override void RightStart()
        {
            //if already in the right lane, do nothing and exit
            if(_currentLane <= 1) { return; }
            //if runner is already moving do nothing and exit
            if (_isMoving) { return;  }
            //move the runner to the right
            StartCoroutine(MoveTo(transform.position - Vector3.forward * LaneWidth, ChangingLaneSpeed));
            _currentLane++;
        }

        /*
            Moves an object to a destination position in a determined time
        */
        protected IEnumerator MoveTo(Vector3 destination, float movementDuration)
        {
            //initialization
            float elapsedTime = 0f;
            Vector3 initialPosition = transform.position;
            _isMoving = true;

            //squared length of this vector
            float sqrRemainingDistance = (transform.position - destination).sqrMagnitude;
            //using epsilon to deal with The small values that a float can have
            while (sqrRemainingDistance > float.Epsilon)
            {
                elapsedTime += Time.deltaTime;
                transform.position = Vector3.Lerp(initialPosition, destination, elapsedTime / movementDuration);
                sqrRemainingDistance = (transform.position - destination).sqrMagnitude;
                yield return null;
            }
            _isMoving = false;
        }

        /*  
            when the runner dies, instantiate an explosion at the point of impact
        */
        public override void die()
        {
            if (Explosion != null)
            {
                GameObject explosion = (GameObject)Instantiate(Explosion);
                explosion.transform.position = transform.GetComponent<Renderer>().bounds.center;
            }
            Destroy(gameObject);
        }

    }
}
