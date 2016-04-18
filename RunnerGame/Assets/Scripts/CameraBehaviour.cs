using System.Collections;
using UnityEngine;

namespace RunnerGame
{
	/// <summary>
	/// Camera behaviour.
	/// This gives the main camera the ability to zoom in and out
	/// shakes the camera
	/// and follow the player within the bounds of the level.
	/// </summary>
	public class CameraBehaviour : MonoBehaviour
	{
		///Adds a header above the fields in this section in the inspector
		[Header ("Zoom range and position")]
		///min possible position for the camera
		public Vector3 MinZoom;
		///max possible position for the camera
		public Vector3 MaxZoom;
		///min orthographic zoom
		public float MinOthographicZoom = 3.0f;
		///max orthgraphuc zoom
		public float MaxOrthographicZoom = 5.0f;
		//Space attribute adds spacing to this section in the inspector
		[Space (10)] [Header ("Follow the Player")]
		///true if the camera should follow the object on the x axis 
		public bool FollowObjectX = false;
		///true if camera should follow the object on the y axis
		public bool FollowObjectY = true;
		///true if camera should follow the object on the z axis
		public bool FollowObjectZ = false;
		/// The camera follow speed.
		public float CameraFollowSpeed;

		///Bounds the camera is restricted to
		[Space (10)] [Header ("Camera Bounds")]
		///if true camera cant go beyond its bound box
		public bool CameraWithinBoundsOnly = true;
		///position of camera bound's bottom left corner
		public Vector2 BoundsBottomLeft;
		///position of camera bound's top right corner
		public Vector2 BoundsTopRight;

		///InitialPosition
		protected Vector3 initPos;
		/// Initial size
		protected float initSize;
		/// Camera Component
		protected Camera camera;
		///Object position
		protected Vector3 objectPosition;

		protected Vector3 currentZoomOffset;

		protected float xMin, xMax;
		protected float yMin, yMAx;
		protected Vector3 newCameraPos;
	
		/// Bounding box corner vectors
		protected Vector2 topRightBounds;
		protected Vector2 bottomLeftBounds;

		protected Vector2 boundsTopRight;
		protected Vector2 boundsTopLeft;
		protected Vector2 boundsBottomRight;
		protected Vector2 boundsBottomLeft;

		/// The camera shake intensity.
		protected float camShakeIntensity;
		/// How long for the shake to weaken
		protected float camShakeDecay;
		/// How long the camera will 'shake' for
		protected float camShakeLength;

		/// <summary>
		/// Start this instance.
		/// </summary>
		protected virtual void Start ()
		{
			Init ();
		}

		/// <summary>
		/// Initialize the different components of the Camera script
		/// </summary>
		protected virtual void Init ()
		{
			//get camera movement
			camera = GetComponent<Camera> ();
			//store the cameras initial position
			initPos = transform.position;
			//store the cameras initial size
			initSize = MinOthographicZoom;
			//store the players position
			objectPosition = LevelManager.Instance.startingPosition.transform.position;

			//set up info based on inspector set variables
			//cameras size
			camera.orthographicSize = initSize;
			//camera position
			camera.transform.position = MinZoom;
		}


		/// <summary>
		/// Update this instance on every frame
		/// </summary>
		protected virtual void Update ()
		{
			//If the current game stare is paused then do nothing
			if (GameManager.Instance != null) {
				if (GameManager.Instance.StateStatus == GameManager.GameStateStatus.GamePaused) {
					return;
				}
			}

			//if there is more than one player
			if (LevelManager.Instance.CurrentPlayablePlayers.Count > 0) {
				//get position of thge first player
				objectPosition = LevelManager.Instance.CurrentPlayablePlayers [0].transform.position;
			}
			//if the camera's not supposed to follow the player on one of its axis, we reset these values.
			if (!FollowObjectX) {
				objectPosition.x = 0;
			}
			if (!FollowObjectY) {
				objectPosition.y = 0;
			}
			if (!FollowObjectZ) {
				objectPosition.z = 0;
			}
			//move the camera pos to the levels current speed
			camera.orthographicSize = MinOthographicZoom + (MaxOrthographicZoom - MinOthographicZoom) * (LevelManager.Instance.Speed / LevelManager.Instance.MaximumSpeed);
			//zoom from min to max to the levels current speed
			currentZoomOffset = MinZoom + (MaxZoom - MinZoom) * (LevelManager.Instance.Speed / LevelManager.Instance.MaximumSpeed);
		
			//get the bounds of the level
			GetLevelBounds ();
			// we interpolate the camera's position towards the new player's position, taking into account the maximum zoom.
			newCameraPos = Vector3.Lerp (transform.position, currentZoomOffset + objectPosition, CameraFollowSpeed * Time.deltaTime);

			//if required limit the camera within the level bounds
			float posX = newCameraPos.x;
			float posY = newCameraPos.y;
			float posZ = newCameraPos.z;
			if (CameraWithinBoundsOnly) {
				posX = Mathf.Clamp (newCameraPos.x, xMin, xMax);
				posY = Mathf.Clamp (newCameraPos.y, yMin, yMAx);
			}

			//handle potential shake of the camera
			Vector3 shakeAmountFactor = Vector3.zero;
			//if shake length still active
			if (camShakeLength > 0) {
				shakeAmountFactor = Random.insideUnitSphere * camShakeIntensity * camShakeLength;
				camShakeLength -= camShakeDecay * Time.deltaTime;
			}
			//move the camera by applying new position
			camera.transform.position = new Vector3 (posX, posY, posZ) + shakeAmountFactor;
		}

		/// <summary>
		/// Shake the camera using specified vector3 for intensity, duration and decay 
		/// </summary>
		/// <param name="camShakeParameters">Cam shake parameters.</param>
		public virtual void Shake (Vector3 camShakeParameters)
		{
			//set a few variables to be used in update() to shake the camera
			camShakeIntensity = camShakeParameters.x;
			camShakeLength = camShakeParameters.y;
			camShakeDecay = camShakeParameters.z;
		}

		/// <summary>
		/// Resets the camera component to Initial position and the Initial size
		/// </summary>
		public virtual void CameraReset ()
		{
			transform.position = initPos;
			camera.orthographicSize = initSize;
		}

		/// <summary>
		/// Gets the level bounds for the camera to lock on to 
		/// (remember orthographicSize is half the height of what the camera sees)
		/// </summary>
		protected virtual void GetLevelBounds ()
		{
			//work out camera size
			float camHeight = Camera.main.orthographicSize * 2.0f;
			float camWidth = camHeight * Camera.main.aspect;
			//define coords for each of the cameras corner
			xMin = bottomLeftBounds.x + (camWidth / 2);
			xMax = topRightBounds.x - (camWidth / 2);
			yMin = bottomLeftBounds.y + (camHeight / 2);
			yMAx = topRightBounds.y - (camHeight / 2);
		}

		/// <summary>
		/// Shows the bounding box when the user selects the camera in the scene view
		/// </summary>
		protected virtual void ShowBoundingBox ()
		{
			boundsTopRight = new Vector2 (topRightBounds.x, topRightBounds.y);
			boundsBottomRight = new Vector2 (topRightBounds.x, bottomLeftBounds.y);
			BoundsBottomLeft = new Vector2 (bottomLeftBounds.x, bottomLeftBounds.y);
			boundsTopLeft = new Vector2 (bottomLeftBounds.x, topRightBounds.y);

			//draw a blue rectabngle to show the cames bounds
			Gizmos.color = Color.blue;
			Gizmos.DrawLine (BoundsTopRight, boundsBottomRight);
			Gizmos.DrawLine (boundsBottomRight, BoundsBottomLeft);
			Gizmos.DrawLine (BoundsBottomLeft, boundsBottomLeft);
			Gizmos.DrawLine (boundsTopLeft, BoundsTopRight);
		}


	}
}

