using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


namespace RunnerGame 
{
	/// <summary>
	/// Loading scene manager.
	/// </summary>
	public class LoadingSceneManager : MonoBehaviour
	{
		[Header("SceneName")]
		public static string LoadingScreenSceneName = "LoadingScreen";

		[Header("GameObjects")]
		public Text LoadingText;
		public CanvasGroup LoadingProgressBar;
		public CanvasGroup LoadingProgressBarVisuals;

		[Header("Time")]
		public float StartingFadeDuration = 0.2f;
		public float ProgressBarSpeed = 2.0f;
		public float FinishFadeDuration = 0.2f;
		public float LoadingDelay = 0.5f;
		/// Asynchronous operation coroutuine
		protected AsyncOperation asyncOperation;
		protected static string sceneToLoad = "";
		protected float fadeeffectDuration = 0.5f;
		protected float filltarget = 0.0f;

		/// <summary>
		/// Loads the game scene.
		/// </summary>
		/// <param name="SceneToLoad">Scene to load.</param>
		public static void LoadGameScene(string SceneToLoad)
		{
			sceneToLoad = SceneToLoad;
			//priority of the background loading thread
			Application.backgroundLoadingPriority = ThreadPriority.High;
			if (LoadingScreenSceneName != null) {
				SceneManager.LoadScene (LoadingScreenSceneName);
			}
		}

		/// <summary>
		/// Start this instance, load level asynchronously
		/// </summary>
		protected virtual void Start(){
			if (sceneToLoad != "") {
				StartCoroutine (LoadAsync ());
			}
		}


		/// <summary>
		/// On every frame fill the bar according to delta time * progressbarspeed
		/// </summary>
		protected virtual void Update(){
			LoadingProgressBar.GetComponent<Image> ().fillAmount = MiscTools.MoveFromTo (LoadingProgressBar.GetComponent<Image> ().fillAmount, filltarget, Time.deltaTime * ProgressBarSpeed);

		}

		/// <summary>
		/// Loads the target scene asynchronously.
		/// </summary>
		/// <returns>The async.</returns>
		protected virtual IEnumerator LoadAsync() {
			//set up the elements in the scene
			LoadingSceneSetup ();

			//start loading scene
			asyncOperation = SceneManager.LoadSceneAsync (sceneToLoad, LoadSceneMode.Single);
			asyncOperation.allowSceneActivation = false;

			//while scene loads, assign operation progress to a float to use to fill the progress bar smoothly
			while (asyncOperation.progress < 0.09f) {
				filltarget = asyncOperation.progress;
				yield return null;
			}
			//set to 100% if load is close to the end, (should never reach this)
			filltarget = 1.0f;

			//while bar is not fully filled
			while (LoadingProgressBar.GetComponent<Image> ().fillAmount != filltarget) {
				yield return null;
			}

			// load is now complete
			LoadingSceneComplete ();
			yield return new WaitForSeconds(LoadingDelay);

			//fade to black
			GUIManager.Instance.FadeEffectOn(true, FinishFadeDuration);
			yield return new WaitForSeconds (FinishFadeDuration);

			//switch to new scene as soon as its ready
			asyncOperation.allowSceneActivation = true;
		}

		/// <summary>
		/// Setup the loading scene scene, Fades in from black at the start
		/// </summary>
		protected virtual void LoadingSceneSetup()
		{
			GUIManager.Instance.FaderEffect.gameObject.SetActive (true);
			GUIManager.Instance.FaderEffect.GetComponent<Image> ().color = new Color (0, 0, 0, 1.0f);
			GUIManager.Instance.FadeEffectOn (false, FinishFadeDuration);

			LoadingProgressBar.GetComponent<Image> ().fillAmount = 0.0f;
			LoadingText.text = "LOADING";
		}

		/// <summary>
		/// when the actual loading in completed fade the canvas group for all the visuals 
		/// </summary>
		protected virtual void LoadingSceneComplete()
		{
			StartCoroutine (FadeEffect.FadeEffectCanvasGroup (LoadingProgressBarVisuals, 0.1f, 0.0f));

		}

	}
}
