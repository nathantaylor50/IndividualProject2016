using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace RunnerGame 
{
	/// <summary>
	/// Fade effect used to fade out images, text and canvas groups
	/// </summary>
	public static class FadeEffect 
	{
		/// <summary>
		/// Fades the target image to the target color (alpha) and duration
		/// </summary>
		/// <returns>The image.</returns>
		/// <param name="target">Image.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="color">Color.</param>
		public static IEnumerator FadeEffectImage(Image image, float duration, Color color)
		{
			//if not image present
			if (image == null) {
				yield break;
			}

			float alpha = image.color.a;

			for (float FI = 0.0f; FI < 1.0f; FI += Time.deltaTime / duration) {
				if (image == null)
					yield break;
				Color newColor = new Color (color.r, color.g, color.b, Mathf.SmoothStep (alpha, color.a, FI));
				yield return null;
			}
			image.color = color;
		}
		/// <summary>
		/// Fades the target text to the target color (alpha) and duration
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="target">text.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="color">Color.</param>
		public static IEnumerator FadeEffectText (Text text, float duration, Color color)
		{
			if (text == null) {
				yield break;
			}

			float alpha = text.color.a;

			for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
			{
				if (text==null)
					yield break;
				Color newColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha,color.a,t));
				text.color=newColor;
				yield return null;
			}			
			text.color=color;
		}


		/// <summary>
		/// Fades the target canvas group to the target color (alpha) and duration
		/// </summary>
		/// <returns>The canvas group.</returns>
		/// <param name="target">cg.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="targetAlpha">Target alpha.</param>
		public static IEnumerator FadeEffectCanvasGroup (CanvasGroup cg, float duration, float targetAlpha)
		{
			if (cg == null) {
				yield break;
			}

			float currentAlpha = cg.alpha;
			float t = 0.0f;

			while (t < 1.0f) {
				if (cg = null) {
					yield break;
				}
				//fade will gradually speed up from start and slow down near end
				float newAlpha = Mathf.SmoothStep(currentAlpha, targetAlpha , t);
				cg.alpha = newAlpha;

				t += Time.deltaTime / duration;

				yield return null;
			}

			cg.alpha = targetAlpha;
		}

	}
}
