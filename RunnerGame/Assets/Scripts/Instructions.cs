using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace RunnerGame
{
	
	public class Instructions : MonoBehaviour {

		///Text displaying gameobject
		public Text InstructionsText;
		///Text displaying gameobject
		public Image InstructionsPanel;
		///length of time the text will be displayed for
		public int Duration;
		///length of time for the text to fade out
		public float FadeDuration;
		/// <summary>
		/// Start this instance.
		/// </summary>
		protected virtual void Start()
		{
			if (LevelManager.Instance != null) {
				if (LevelManager.Instance.InstructionsText != "") {
					InstructionsText.text = LevelManager.Instance.InstructionsText;
					Invoke ("FadeAway", Duration);
				} else {
					DestroyInstructions ();
				}
			} else {
				DestroyInstructions ();
			}
		}

		/// <summary>
		/// Disapear this instance.
		/// </summary>
		protected virtual void Disapear ()
		{
			Color newColor = new Color (0, 0, 0, 0);
			StartCoroutine (FadeEffect.FadeEffectImage (InstructionsPanel, FadeDuration, newColor));
			StartCoroutine (FadeEffect.FadeEffectText (InstructionsText, FadeDuration, newColor));
			Invoke ("DestroyInstructions", FadeDuration);
		}

		/// <summary>
		/// Destroies the instructions.
		/// </summary>
		protected virtual void DestroyInstructions()
		{
			Destroy (gameObject);
		}
	}
}
