using UnityEngine;
using UnityEngine.UI;
using System;

namespace RunnerGame {
	public class FPSCounter : MonoBehaviour
	{
		const float fpsMeasureAmount = 0.5f;
		private int fpsIncrement;
		private float fpsNextAmount;
		private int currentFps;
		const string display = "{0} FPS";
		Text fpsText;

		private void Start()
	{
		fpsNextAmount = Time.realtimeSinceStartup + fpsMeasureAmount;
		fpsText = GetComponent<Text> ();
	}

		private void Update()
		{
			//measure average frame rate per second
			fpsIncrement++;
			if (Time.realtimeSinceStartup > fpsNextAmount) {
				currentFps = (int)(fpsIncrement / fpsMeasureAmount);
				fpsIncrement = 0;
				fpsNextAmount += fpsMeasureAmount;
				fpsText.text = string.Format (display, currentFps);
			}

		}
	}
}
