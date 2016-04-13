using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace RunnerGame
{
	/// <summary>
	/// Level manager, spawns the player
	/// </summary>
	public class LevelManager : MonoBehaviour 
	{

	    //singleton pattern
	    static public LevelManager Instance { get { return instance; } }
	    static protected LevelManager instance;

		/// <summary>
		/// Awake this instance.
		/// </summary>
	    protected virtual void Awake()
	    {
	        instance = this;
	    }

		/// <summary>
		/// Controls.
		/// </summary>
	    public enum Controls { SingleButton, LeftRight, Jump }

		///the current speed the level is traveling at
		public float Speed { get; protected set;}
		///distance traveled since the start of the level
		public float DistanceTraveled { get;  protected set; }


		//prefrab for the main player
		[Header("Prefabs")]
		public GameObject startingPosition;
		///list of players
		public List<PlayableCharacter> PlayablePlayers;
		///list of instantiated players
		public List<PlayableCharacter> CurrentPlayablePlayers {get; set; }
		/// x distance between players
		public float DistanceBetweenPlayers = 1.0f;
		///elapsed time since start of level
		public float TimeSinceStart {	get;	protected set;	}
		///amount of points a player gets per second
		public float GamePointsPerSecond = 20.0f;

		///if not empty text object will be shown at the start of the level
		[Multiline]
		public String InstructionsText;

		[Space(10)]
		[Header("Level Bounds")]
		///recycle line
		public Bounds RecycleBounds;

		[Space(10)]
		[Header("Death Bounds")]
		///death zone, players that enter will die, if = 0 it isnt used
		public Bounds DeathBounds;

		[Space(10)]
		[Header("speeed")]
		///initial speed of the level
		public float InitialSpeed = 10.0f;
		///max speed of level
		public float MaximumSpeed = 50.0f;
		///acceleration per second at which the level will go from initial speed to the maximum speed
		public float SpeedAcceleration = 1.0f;

		[Space(10)]
		[Header("into/outro duration")]
		///duration of the initial fade in
		public float IntroFadeDuration = 1.0f;
		//duration of the fade out, at the end of the level
		public float OutroFadeDuration = 1.0f;

		[Space(10)]
		[Header("Start")]
		///the amount of seconds for the initial countdown
		public int StartInitialCountdown;
		///text displayed at the end of the countdown
		public string StartText;

		[Space(10)]
		[Header("Mobile controls")]
		///mobile control scheme for this level
		public Controls ControlProfile;

		[Space(10)]
		[Header("Life lost")]
		//initi effect upon life lost
		public GameObject Explosion;

		//protected stuff
		protected DateTime dateTimeStarted;
		protected float savedPoints;
		protected float recycleX;
		protected Bounds tempRecycleBounds;

	    /// <summary>
	    /// Start this instance.
	    /// </summary>
	    protected virtual void Start () {
			Speed = InitialSpeed;
			DistanceTraveled = 0;

			InitGamePlayers ();
			ManageControlProile ();

			//storage
			savedPoints = GameManager.Instance.GamePoints;
			dateTimeStarted = DateTime.UtcNow;
			GameManager.Instance.SetGameStatus (GameManager.GameStateStatus.GameBefore);
			GameManager.Instance.SetGamePointsPerSecond (GamePointsPerSecond);

			if (GUIManager.Instance != null) {
				//set the name of the level in the GUI
				GUIManager.Instance.SetLevelName(SceneManager.GetActiveScene().name);
				//fade in 
				GUIManager.Instance.FadeOn(false, IntroFadeDuration);
			}

			PrepareGameStart ();

		}

		/// <summary>
		/// Prepares the game start.
		/// </summary>
		protected virtual void PrepareGameStart(){
			//if showing countdown start it, otherwise start level
			if (StartInitialCountdown > 0) {
				GameManager.Instance.SetGameStatus (GameManager.GameStateStatus.GameBefore);
				StartCoroutine (PrepareGameStartCountdown ());
			} else {
				GameLevelStart ();
			}
		}

		/// <summary>
		/// Prepares the game start countdown.
		/// </summary>
		/// <returns>The game start countdown.</returns>
		protected virtual IEnumerator PrepareGameStartCountdown()
		{
			int countdown = StartInitialCountdown;
			GUIManager.Instance.SetGameCountdownActive (true);

			//while countdown > 0 display current game value, wait for a second and show the next value
			while (countdown > 0) {
				if (GUIManager.Instance.CountDownText != null) {
					GUIManager.Instance.SetGameCountDownText (countdown.ToString ());
				}
				countdown--;
				yield return new WaitForSeconds (1.0f);
			}

			//when countdown = 0 set text to start text and display it
			if ((countdown == 0) && (StartText != "")) {
				GUIManager.Instance.SetGameCountDownText (StartText);
				yield return new WaitForSeconds (1.0f);
			}

			//set countdown inactive and start level
			GUIManager.Instance.SetGameCountdownActive(false);
			GameLevelStart ();
		}

		/// <summary>
		/// start of the level
		/// </summary>
		public virtual void GameLevelStart()
		{
			GameManager.Instance.SetGameStatus (GameManager.GameStateStatus.GameRunning);
			GameManager.Instance.AutoIncrementGameScore (true);
			EventManager.StartEvent ("GameStart");
		}

		/// <summary>
		/// Inits the game players.
		/// </summary>
		protected virtual void InitGamePlayers()
		{
			CurrentPlayablePlayers = new List<PlayableCharacter> ();


			for (int i = 0; i < PlayablePlayers.Count; i++) {
				//init prefav
				PlayableCharacter instance = (PlayableCharacter)Instantiate(PlayablePlayers[i]);
				//position based of starting position object point
				instance.transform.position = new Vector2(startingPosition.transform.position.x + i * DistanceBetweenPlayers, startingPosition.transform.position.y);
				//manually set initial position
				instance.SetInitialPosition(instance.transform.position);
				//send it to the game manager
				CurrentPlayablePlayers.Add(instance);
			}
			EventManager.StartEvent ("PlayablePlayersInstantiated");
		}

		/// <summary>
		/// Resets the game level.
		/// </summary>
		public virtual void ResetGameLevel()
		{
			InitGamePlayers ();
			PrepareGameStart ();
		}

		/// <summary>
		/// Manages the control proile.
		/// </summary>
		protected virtual void ManageControlProile()
		{
			String buttonPath = "";
			switch (ControlProfile) 
			{
				case Controls.SingleButton:
					buttonPath = "Canvas/MainActionbutton";
					if (GUIManager.Instance == null) {return;}
					if (GUIManager.Instance.transform.Find(buttonPath) == null) { return; }
					GUIManager.Instance.transform.Find(buttonPath).gameObject.SetActive(true);
					break;

			case Controls.LeftRight:
				buttonPath = "Canvas/LeftRight";
				if (GUIManager.Instance == null) { return; } 
				if (GUIManager.Instance.transform.Find(buttonPath) == null) {return; }
				GUIManager.Instance.transform.Find(buttonPath).gameObject.SetActive(true);
				break;
			
			}
		}

		/// <summary>
		/// Update this instance every frame
		/// </summary>
		public virtual void Update()
		{
			savedPoints = GameManager.Instance.GamePoints;
			dateTimeStarted = DateTime.UtcNow;

			//increment the total distance traveled so far
			DistanceTraveled = DistanceTraveled + Speed * Time.fixedDeltaTime;

			//if we can still accelerate apply speed acccelation of the level
			if (Speed < MaximumSpeed)
			{
				Speed += SpeedAcceleration * Time.deltaTime;
			}

			TimeSinceStart += Time.deltaTime;

		}

		/// <summary>
		/// Sets the game speed.
		/// </summary>
		/// <param name="newGameSpeed">New game speed.</param>
		public virtual void SetGameSpeed(float newGameSpeed)
		{
			Speed = newGameSpeed;
		}

		/// <summary>
		/// Adds the game speed.
		/// </summary>
		/// <param name="GameSpeedAdded">Game speed added.</param>
		public virtual void AddGameSpeed (float GameSpeedAdded)
		{
			Speed += GameSpeedAdded;
		}

		/// <summary>
		/// Temps the multi game speed.
		/// </summary>
		/// <param name="facot">Facot.</param>
		/// <param name="duration">Duration.</param>
		public virtual void TempMultiGameSpeed (float  factor, float duration)
		{
			StartCoroutine (TempMultiGameSpeedCoroutine (factor, duration));
		}

		/// <summary>
		/// Temps the multi game speed coroutine.
		/// </summary>
		/// <returns>The multi game speed coroutine.</returns>
		/// <param name="factor">Factor.</param>
		/// <param name="IntroFadeDuration">Intro fade duration.</param>
		protected virtual IEnumerator TempMultiGameSpeedCoroutine (float factor, float IntroFadeDuration)
		{
			float saveSpeed = Speed;
			Speed = Speed * factor;
			yield return new WaitForSeconds (1.0f);
			Speed = saveSpeed;
		}

		/// <summary>
		/// Checks the recycle object bounds.
		/// </summary>
		/// <returns><c>true</c>, if recycle object bounds was checked, <c>false</c> otherwise.</returns>
		/// <param name="objectBounds">Object bounds.</param>
		/// <param name="destroyDistance">Destroy distance.</param>
		public virtual bool CheckRecycleObjectBounds (Bounds objectBounds, float destroyDistance)
		{
			tempRecycleBounds = RecycleBounds;
			tempRecycleBounds.extents += Vector3.one * destroyDistance;

			if (objectBounds.Intersects (tempRecycleBounds)) {
				return false;
			} else {
				return true;
			}
		}

		/// <summary>
		/// Checks the death object bounds.
		/// </summary>
		/// <returns><c>true</c>, if death object bounds was checked, <c>false</c> otherwise.</returns>
		/// <param name="objectBounds">Object bounds.</param>
		public virtual bool CheckDeathObjectBounds (Bounds objectBounds)
		{
			if (objectBounds.Intersects(DeathBounds))
			{
				return false;
			} else {
				return true;
			}
		}


		/// <summary>
		/// Gos to game level.
		/// </summary>
		/// <param name="levelName">Level name.</param>
		public virtual void GoToGameLevel (string levelName)
		{
			GUIManager.Instance.FadeOn (true, OutroFadeDuration);
			StartCoroutine (GoToGameLevelIE (levelName));
		}

		/// <summary>
		/// Gos to game level I.
		/// </summary>
		/// <returns>The to game level I.</returns>
		/// <param name="levelName">Level name.</param>
		protected virtual IEnumerator GoToGameLevelIE (string levelName)
		{
			if (Time.timeScale > 0.0f) {
				yield return new WaitForSeconds (OutroFadeDuration);
			}
			GameManager.Instance.UnPauseGame ();

			if (string.IsNullOrEmpty (levelName)) {
				LoadingSceneManager.LoadGameScene ("StartScreen");
			} else {
				LoadingSceneManager.LoadGameScene(levelName);
			}
		}


		/// <summary>
		/// triggers the game over event when all lives are lost and when the player 
		/// presses the main action button
		/// </summary>
		public virtual void GameOverEvent()
		{
			GameManager.Instance.UnPauseGame ();
			GoToGameLevel (SceneManager.GetActiveScene ().name);
		}

		/// <summary>
		/// triggers the life lost event when the player loses a life and when the player
		/// presses the main action button
		/// </summary>
		public virtual void LifeLostEvent()
		{
			ResetGameLevel ();
		}


		/// <summary>
		/// Kills the player.
		/// </summary>
		/// <param name="player">Player.</param>
		public virtual void KillPlayer (PlayableCharacter player)
		{
			StartCoroutine (killPlayerIE (player));
		}


		/// <summary>
		/// coroutine that Kills the player, stops the camera and resets the game points
		/// </summary>
		/// <returns>The player I.</returns>
		/// <param name="player">Player.</param>
		protected virtual IEnumerator killPlayerIE (PlayableCharacter player)
		{
			LevelManager.Instance.CurrentPlayablePlayers.Remove (player);
			player.Die ();

			yield return new WaitForSeconds (0.0f);

			//if last player trigger all players are dead coroutine
			if (LevelManager.Instance.CurrentPlayablePlayers.Count == 0) {
				AllPlayersDead ();
			}
		}

		/// <summary>
		/// Alls the players dead.
		/// </summary>
		protected virtual void AllPlayersDead()
		{
			//instantiate effect for players death if it exists
			if (Explosion != null) {
				GameObject explosion = Instantiate (Explosion);
				explosion.transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
			}

			//lost a life
			GameManager.Instance.SetGameStatus(GameManager.GameStateStatus.GameLifeLost);
			EventManager.StartEvent ("Lifelost");
			dateTimeStarted = DateTime.UtcNow;
			GameManager.Instance.SetGamePoints (savedPoints);
			GameManager.Instance.LoseGameLives (1);

			if (GameManager.Instance.CurrentLives <= 0) {
				GUIManager.Instance.SetGameGameOverScreen (true);
				GameManager.Instance.SetGameStatus (GameManager.GameStateStatus.GameOver);
				EventManager.StartEvent ("GameOver");
			}
		}


		/// <summary>
		/// Override this if needed
		/// </summary>
		protected virtual void OnEnable()
		{

		}

		/// <summary>
		/// Override this if needed
		/// </summary>
		protected virtual void OnDisable()
		{

		}

	}
}

