using UnityEngine;
using System.Collections;

namespace RunnerGame
{
	/// <summary>
	/// extend playable character class, player switches lanes by moving left or right
	/// </summary>
	public class MainPlayer : PlayableCharacter
	{
		//shows up in unity inspector
		[Space(10)]
		[Header("Lanes")]
		///width of individual lanes
		public float LaneWidth = 3.0f;
		///the number of lanes the main player can run in
		public int NumberOfLanes = 3;
		///the speed in which the main player changes lane in seconds
		public float ChangingLaneSpeed = 1.0f;
		///instanced gameobject when main player dies
		public GameObject Explosion;

		protected int currentLane;
		protected bool isMoving = false;

		/// <summary>
		/// Use this for initialization
		/// </summary>
		protected override void Awake()
		{
			Initialize ();
			//init the current which should be the middle lane
			currentLane = NumberOfLanes / 2;
			//if the number of lanes is odd, we add one to get the moddle one.
			if(NumberOfLanes % 2 ==1) {	currentLane++;	}
		}

		/// <summary>
		/// Update this instance.
		/// </summary>
		//protected override void Update ()
		protected override void Update ()
		{
			//send states to animator
			UpdateAnimator();
			//check if the player is out of the death bounds or not
			CheckDeathBounds();
		}

		/// <summary>
		/// triggered when the player presses right
		/// </summary>
		//public override void RightStart()
		public override void RightPressed()
		{
			//if already in right lane, do nothing and exit
			if (currentLane == NumberOfLanes) {return;}
			//if already moving do nothing and exit
			if (isMoving) { return; }
			//move lane runner to the right
			StartCoroutine(MoveTo(transform.position - Vector3.forward * LaneWidth, ChangingLaneSpeed));
			currentLane++;
		}

		/// <summary>
		/// triggered when the player presses left
		/// </summary>
		//public override void LeftStart()
		public override void LeftPressed()
		{
			//if already in the left lane, do nothing and exit
			if (currentLane <= 1) {	return;	}
			//if the lane runner is already moving do nothing and exit
			if (isMoving) {	return;	}
			//move player left
			StartCoroutine(MoveTo(transform.position + Vector3.forward * LaneWidth, ChangingLaneSpeed));
			currentLane--;
		}



		/// <summary>
		/// Moves to.
		/// </summary>
		/// <returns>The to.</returns>
		/// <param name="destination">Destination.</param>
		/// <param name="movementDuration">Movement duration.</param>
		protected IEnumerator MoveTo(Vector3 dest, float moveDuration)
		{
			//init
			float elaspedTime = 0.0f;
			Vector3 initPos = transform.position;
			isMoving = true;

			float squareRemainingDistance = (transform.position - dest).sqrMagnitude;
			while (squareRemainingDistance > float.Epsilon)
			{
				elaspedTime += Time.deltaTime;
				transform.position = Vector3.Lerp (initPos, dest, elaspedTime / moveDuration);
				squareRemainingDistance = (transform.position - dest).sqrMagnitude;
				yield return null;
			}
			isMoving = false;
		}

		/// <summary>
		/// destroy this instance and initiate explosion
		/// </summary>
		//public override void Die()
		public override void Die()

		{
			if (Explosion != null)
			{
				GameObject explosion = (GameObject)Instantiate (Explosion);
				explosion.transform.position = transform.GetComponent<Renderer> ().bounds.center;
			}
			Destroy (gameObject);
		}
	}
}