using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using RunnerGame.MiscTools;

namespace RunnerGame 
{
	public class LoadingSceneManager : MonoBehaviour
	{
		[Header("SceneName")]
		public static string LoadingScreenSceneName = "LoadingScreen";

		[Header("GameObjects")]
		public Text LoadingText;
		public CanvasGroup LoadingProgressBar;
		public CanvasGroup LoadingAnimation;
		public CanvasGroup LoadingCompleteAnimation;

		[Header("Time")]
		public float StartingFadeDuration = 0.2f;
		public float ProgressBarSpeed = 2.0f;
		public float FinishFadeDuration = 0.2f;
		public float LoadingDelay = 0.5f;

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
			sceneToLoad = sceneToLoad;
			Application.backgroundLoadingPriority = ThreadPriority.High;
			if (LoadingScreenSceneName != null) {
				SceneManager.LoadScene (LoadingScreenSceneName);
			}
		}

		/// <summary>
		/// Start this instance.
		/// </summary>
		protected virtual void Start(){
			if (sceneToLoad != "") {
				StartCoroutine (LoadAsync ());
			}
		}


		/// <summary>
		/// Update this instance.
		/// </summary>
		protected virtual void Update(){
			LoadingProgressBar.GetComponent<Image> ().fillAmount = MiscTools.MoveFromTo (LoadingProgressBar.GetComponent<Image> ().fillAmount, filltarget, Time.deltaTime * ProgressBarSpeed);
		}

		/// <summary>
		/// Loads the async.
		/// </summary>
		/// <returns>The async.</returns>
		protected virtual IEnumerator LoadAsync() {
			//
			LoadingSceneSetUp ();

			//
			asyncOperation = SceneManager.LoadSceneAsync (sceneToLoad, LoadSceneMode.Single);
			asyncOperation.allowSceneActivation = false;

			//
			while (asyncOperation.progress < 0.09f) {
				filltarget = asyncOperation.progress;
				yield return null;
			}
			//
			filltarget = 1.0f;

			//
			while (LoadingProgressBar.GetComponent<Image> ().fillAmount != filltarget) {
				yield return null;
			}

			// 
			LoadingSceneComplete ();
			yield return new WaitForSeconds(LoadingDelay);

			//fade to black
			GUIManager.Instance.FadeOn(true, FinishFadeDuration);
			yield return new WaitForSeconds (FinishFadeDuration);

			//switch to new scene as soon as its ready
			asyncOperation.allowSceneActivation = true;
		}

		/// <summary>
		/// Loadings the scene setup.
		/// </summary>
		protected virtual void LoadingSceneSetup()
		{
			GUIManager.Instance.Fader.gameObject.SetActive (true);
			GUIManager.Instance.Fader.GetComponent<Image> ().color = new Color (0, 0, 0, 1.0f);
			GUIManager.Instance.FadeOn (false, FinishFadeDuration);

			LoadingCompleteAnimation.alpha = 0;
			LoadingProgressBar.GetComponent<Image> ().fillAmount = 0.0f;
			LoadingText.text = "LOADING";
		}


		protected virtual void LoadingSceneComplete()
		{
			LoadingCompleteAnimation.gameObject.SetActive (true);
			StartCoroutine (FadeEffect.FadeCanvasGroup (LoadingProgressBar, 0.1f, 0.0f));
			StartCoroutine (FadeEffect.FadeCanvasGroup (LoadingAnimation, 0.1f, 0.0f));
			StartCoroutine (FadeEffect.FadeCanvasGroup (LoadingAnimation, 0.1f, 0f));
		}

	}
}
