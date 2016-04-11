using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RunnerGame
{
	/// <summary>
	/// Game manager. persistent singleton 
	/// </summary>
	public class GameManager : MonoBehaviour {

		///number of lives
		public int TotalLives = 3;
		///current number of lives
		public int CurrentLives {	get;	protected set; }
		///number of gamepoints
		public float GamePoints {	get;	protected set; }
		//time scale
		public float GameTimeScale;
		///states the game can be in
		public enum GameStateStatus { GameBefore, GameRunning, GamePaused, GameOver, GameLifeLost };
		///the current game state the game is currently in
		public GameStateStatus StateStatus{	get;	protected set; }

		public delegate void GameManagerInspectRedraw ();
		//event where the editor code will hook itself
		public event GameManagerInspectRedraw GameManagerInspectNeedRedrawing;

		//storage
		protected float savedGameTimeScale;
		protected IEnumerator scoreCoroutine;
		protected float GamePointsPerSecond;
		protected GameStateStatus stateStatusBeforePause;
	

	    ///singleton pattern
	    static public GameManager Instance { get { return instance; } }
	    static protected GameManager instance;

		/// <summary>
		/// Awake this instance.
		/// </summary>
	    void Awake () 
		{
			instance = this;
		}
		
		/// <summary>
		/// Start this instance.
		/// </summary>
		void Start () 
		{
			CurrentLives = TotalLives;
			savedGameTimeScale = GameTimeScale;
			Time.timeScale = GameTimeScale;

			if (GUIManager.Instance != null) {
				GUIManager.Instance.Initialize ();
			}
		}

		/// <summary>
		/// Sets the game points per second.
		/// </summary>
		/// <param name="newGamePointsPerSecond">New game points per second.</param>
		public virtual void SetGamePointsPerSecond (float newGamePointsPerSecond)
		{
			GamePointsPerSecond = newGamePointsPerSecond;
		}

		/// <summary>
		/// Sets the game status.
		/// </summary>
		/// <param name="newGameStateStatus">New game state status.</param>
		public virtual void SetGameStatus(GameStateStatus newGameStateStatus)
		{
			StateStatus = newGameStateStatus;
			if (GameManagerInspectNeedRedrawing != null) {
				GameManagerInspectNeedRedrawing ();
			}
		}


		/// <summary>
		/// Resets the manager.
		/// </summary>
		public virtual void ResetManager(){
			GamePoints = 0;
			GameTimeScale = 1.0f;
			GameManager.instance.SetGameStatus (GameStateStatus.GameRunning);
			EventManager.StartEvent ("Gamestart");
			GUIManager.Instance.RefreshGamePoints ();
		}

		/// <summary>
		/// Increments the game score.
		/// </summary>
		/// <param name="status">If set to <c>true</c> status.</param>
		public virtual void AutoIncrementGameScore(bool status)
		{
			if (status) {
				StartCoroutine (IncrementGameScore ());
			} else {
				StopCoroutine (IncrementGameScore ());
			}
		}

		/// <summary>
		/// Increments the game score.
		/// </summary>
		/// <returns>The game score.</returns>
		public virtual IEnumerator IncrementGameScore()
		{
			while (true) {
				if ((GameManager.Instance.StateStatus == GameStateStatus.GameRunning) && (GamePointsPerSecond != 0)) {
					AddGamePoints (GamePointsPerSecond / 100);
				}

				yield return new WaitForSeconds (0.01f);
			}
		}


		/// <summary>
		/// Adds the game points.
		/// </summary>
		/// <param name="gamePointsToAdd">Game points to add.</param>
		public virtual void AddGamePoints (float gamePointsToAdd)
		{
			GamePoints += gamePointsToAdd;
			if (GUIManager.Instance != null) {
				GUIManager.Instance.RefreshGamePoints ();
			}

		}

		/// <summary>
		/// Sets the game points.
		/// </summary>
		/// <param name="gamePoints">Game points.</param>
		public virtual void SetGamePoints (float gamePoints)
		{
			GamePoints = gamePoints;
			if (GUIManager.Instance != null) {
				GUIManager.Instance.RefreshGamePoints ();
			}
		}

		/// <summary>
		/// Sets the game lives.
		/// </summary>
		/// <param name="gameLives">Game lives.</param>
		public virtual void SetGameLives (int gameLives)
		{
			CurrentLives = gameLives;
			if (GUIManager.Instance != null) {
				GUIManager.Instance.InitLives ();
			}
		}

		/// <summary>
		/// Loses the game lives.
		/// </summary>
		/// <param name="gameLives">Game lives.</param>
		public virtual void LoseGameLives (int gameLives)
		{
			CurrentLives -= gameLives;
			if (GUIManager.Instance != null) {
				GUIManager.Instance.InitLives ();
			}
		}

		/// <summary>
		/// Sets the game time scale.
		/// </summary>
		/// <param name="newGameTimeScale">New game time scale.</param>
		public virtual void SetGameTimeScale (float newGameTimeScale)
		{
			savedGameTimeScale = Time.timeScale;
			Time.timeScale = newGameTimeScale;
		}

		/// <summary>
		/// Resets the game time scale.
		/// </summary>
		public virtual void ResetGameTimeScale()
		{
			Time.timeScale = savedGameTimeScale;
		}

		/// <summary>
		/// Pauses the game.
		/// </summary>
		public virtual void PauseGame()
		{
			//if time is not already stopped
			if (Time.timeScale > 0.0f) {
				Instance.SetGameTimeScale (0.0f);
				stateStatusBeforePause = Instance.StateStatus;
				Instance.SetGameStatus (GameStateStatus.GamePaused);
				EventManager.StartEvent ("Pause");
				if (GUIManager.Instance != null) {
					GUIManager.Instance.SetGamePause (true);
				}
			} else {
				UnPauseGame ();
			}
		}

		/// <summary>
		/// Unpause the game.
		/// </summary>
		public virtual void UnPauseGame(){
			Instance.ResetGameTimeScale ();
			Instance.SetGameStatus (stateStatusBeforePause);
			if (GUIManager.Instance != null) {
				GUIManager.Instance.SetGamePause (false);
			}
		}
	}
}
