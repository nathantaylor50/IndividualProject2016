using UnityEngine;
using UnityEngine.UI;
using System;

namespace RunnerGame {
	public class FPSCounter : MonoBehaviour
	{
		const float fpsMeasurePeriod = 0.5f;
		private int fpsAccumulator;
		private float fpsNextPeriod;
		private int currentFps;
		const string display = "{0} FPS";
		Text fpsText;

		private void Start()
	{
		fpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
		fpsText = GetComponent<Text> ();
	}

		private void Update()
		{
			//
			fpsAccumulator++;
			if (Time.realtimeSinceStartup > fpsNextPeriod) {
				currentFps = (int)(fpsAccumulator / fpsMeasurePeriod);
				fpsAccumulator = 0;
				fpsNextPeriod += fpsMeasurePeriod;
				fpsText.text = string.Format (display, currentFps);
			}

		}
	}
}
