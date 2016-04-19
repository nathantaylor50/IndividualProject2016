using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace RunnerGame
{
	/// <summary>
	/// GUI manager.
	/// </summary>
	public class GUIManager : MonoBehaviour {

		///pause screen gameObject
		public GameObject PauseScreen;
		///gameover gameObject
		public GameObject GameOverScreen;
		///gameObject for the lives
		public GameObject HealthContainer;
		///points counter text
		public Text PointsText;
		///level text
		public Text LevelText;
		///countdown text
		public Text CountDownText;
		///screen image to fade in/out 
		public Image FaderEffect;

		//singleton pattern
		static public GUIManager Instance { get { return instance; }}
		static protected GUIManager instance;

		/// <summary>
		/// Awake this instance.
		/// </summary>
		public void Awake()
		{
			instance = this;
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		public virtual void Initialize()
		{
			RefreshGamePoints ();
			InitLives ();

			if (CountDownText != null) {
				CountDownText.enabled = false;
			}
		}

		/// <summary>
		/// Inits the lives.
		/// </summary>
		public virtual void InitLives()
		{
			if (HealthContainer == null) {
				return;
			}
			//remove everything in the HealthContainer
			foreach (Transform child in HealthContainer.transform) {
				Destroy (child.gameObject);
			}

			int deadGameLives = GameManager.Instance.TotalLives - GameManager.Instance.CurrentLives;
			// for each game life in the total number of lives possible
			for (int i = 0; i < GameManager.Instance.TotalLives; i++) {
				//display empty heart if life is already lost
				string resourceURL = "";
				if (deadGameLives > 0) {
					resourceURL = "GUI/GUIHeartEmpty";
				} else {
					//display a full heart if there is life
					resourceURL = "GUI/GUIHeartFull";
				}
				//init the heart gameobject and position it
				GameObject gameHeart = (GameObject)Instantiate(Resources.Load(resourceURL));
				gameHeart.transform.SetParent (HealthContainer.transform, false);
				HealthContainer.GetComponent<RectTransform> ().localPosition 
				= new Vector3 (HealthContainer.GetComponent<RectTransform> ().sizeDelta.x / 2 - i
				* (gameHeart.GetComponent<RectTransform> ().sizeDelta.x * 75.0f), 0, 0);
				deadGameLives--;

			}
		}

		/// <summary>
		/// Raises the game start event.
		/// override this
		/// </summary>
		public virtual void OnGameStart()
		{

		}

		/// <summary>
		/// Sets the game pause.
		/// </summary>
		/// <param name="state">If set to <c>true</c> state.</param>
		public virtual void SetGamePause(bool state)
		{
			PauseScreen.SetActive (state);
		}

		/// <summary>
		/// Set the game countdown active.
		/// </summary>
		/// <param name="state">If set to <c>true</c> state.</param>
		public virtual void SetGameCountdownActive(bool state)
		{
			if (CountDownText == null) {	return;	}
			CountDownText.enabled = state;
		}

		/// <summary>
		/// Sets the game count down text.
		/// </summary>
		/// <param name="newText">New text.</param>
		public  virtual void SetGameCountDownText(string newText)
		{
			if (CountDownText == null) {	return; }
			CountDownText.text = newText;
		}

		/// <summary>
		/// Sets the game's game over screen.
		/// </summary>
		/// <param name="state">If set to <c>true</c> state.</param>
		public virtual void SetGameGameOverScreen(bool state)
		{
			GameOverScreen.SetActive (state);
			Text gameOverScreenText = GameOverScreen.transform.Find ("GameOverScreenText").GetComponent<Text> ();
			if (gameOverScreenText != null) {
				gameOverScreenText.text = "GAME OVER\nYOUR SCORE : " + Mathf.Round (GameManager.Instance.GamePoints);
			}
		}

		/// <summary>
		/// sets the text object to the game manager's game points
		/// </summary>
		public virtual void RefreshGamePoints()
		{
			if (PointsText == null) {	return;}
			PointsText.text = GameManager.Instance.GamePoints.ToString ("0000 000 000");
		}

		/// <summary>
		/// Sets the name of the level.
		/// </summary>
		/// <param name="name">Name.</param>
		public virtual void SetLevelName(string name)
		{
			if (LevelText == null) {
				return;
			}
			LevelText.text = name;
		}


		public virtual void FadeEffectOn(bool state, float duration)
		{
			if (FaderEffect == null) {
				return;
			}
			FaderEffect.gameObject.SetActive (true);

			if (state) {
				StartCoroutine (FadeEffect.FadeEffectImage (FaderEffect, duration, new Color (0, 0, 0, 1f)));
			} else {
				StartCoroutine (FadeEffect.FadeEffectImage (FaderEffect, duration, new Color (0, 0, 0, 0f)));
			}
		}

		/// <summary>
		/// Fades the fader.
		/// </summary>
		/// <param name="newcolor">Newcolor.</param>
		/// <param name="duration">Duration.</param>
		public virtual void FadeFader (Color newcolor, float duration)
		{
			if (FaderEffect == null) {
				return;
			}
			FaderEffect.gameObject.SetActive (true);
			StartCoroutine (FadeEffect.FadeEffectImage (FaderEffect, duration, newcolor));
		}

		/// <summary>
		/// Raises the enable event.
		/// </summary>
		protected virtual void OnEnable()
		{
			EventManager.StartListener("GameStart", OnGameStart);
		}

		/// <summary>
		/// Raises the disable event.
		/// </summary>
		protected virtual void OnDisable()
		{
			EventManager.StopListener ("GameStart", OnGameStart);
		}
	}
}
