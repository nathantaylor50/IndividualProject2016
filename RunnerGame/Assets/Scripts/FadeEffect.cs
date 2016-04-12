using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace RunnerGame 
{
	/// <summary>
	/// Fade effect.
	/// </summary>
	public static class FadeEffect 
	{
		/// <summary>
		/// Fades the image.
		/// </summary>
		/// <returns>The image.</returns>
		/// <param name="target">Target.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="color">Color.</param>
		public static IEnumerator FadeImage(Image target, float duration, Color color)
		{
			if (target == null) {
				yield break;
			}

			float alpha = target.color.a;

			for (float FI = 0.0f; FI < 1.0f; FI += Time.deltaTime / duration) {
				if (target == null)
					yield break;
				Color newColor = new Color (color.r, color.g, color.b, Mathf.SmoothStep (alpha, color.a, FI));
				yield return null;
			}
			target.color = color;
		}

		/// <summary>
		/// Fades the sprite.
		/// </summary>
		/// <returns>The sprite.</returns>
		/// <param name="target">Target.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="color">Color.</param>
		public static IEnumerator FadeSprite(SpriteRenderer target, float duration, Color color)
		{
			if (target==null)
				yield break;

			float alpha = target.material.color.a;

			float t=0f;
			while (t<1.0f)
			{
				if (target==null)
					yield break;

				Color newColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha,color.a,t));
				target.material.color=newColor;

				t += Time.deltaTime / duration;

				yield return null;

			}
			Color finalColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha,color.a,t));
			target.material.color=finalColor;
		}


		/// <summary>
		/// Fades the canvas group.
		/// </summary>
		/// <returns>The canvas group.</returns>
		/// <param name="target">Target.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="targetAlpha">Target alpha.</param>
		public static IEnumerator FadeCanvasGroup (CanvasGroup target, float duration, float targetAlpha)
		{
			if (target == null) {
				yield break;
			}

			float currentAlpha = target.alpha;
			float t = 0.0f;

			while (t < 1.0f) {
				if (target = null) {
					yield break;
				}
				//fade will gradually speed up from start and slow down near end
				float newAlpha = Mathf.SmoothStep(currentAlpha, targetAlpha , t);
				target.alpha = newAlpha;

				t += Time.deltaTime / duration;

				yield return null;
			}

			target.alpha = targetAlpha;
		}

	}
}
