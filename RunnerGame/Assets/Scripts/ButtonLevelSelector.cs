using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace RunnerGame
{
	/// <summary>
	/// Button level selector.
	/// </summary>
	public class ButtonLevelSelector : MonoBehaviour
	{
		/// name for the level
		public string levelName;

		/// <summary>
		/// Gos to level.
		/// </summary>
		public virtual void GoToGameLevel()
		{
			LevelManager.Instance.GoToGameLevel (levelName);
		}

		/// <summary>
		/// Restarts the game level.
		/// </summary>
		public virtual void RestartGameLevel()
		{
			LevelManager.Instance.GoToGameLevel (SceneManager.GetActiveScene ().name);
		}

		/// <summary>
		/// Resumes the game.
		/// </summary>
		public virtual void ResumeGame()
		{
			GameManager.Instance.UnPauseGame ();
		}
		/// <summary>
		/// Resets the game score.
		/// </summary>
		public virtual void ResetGameScore()
		{
	//TODO: class
			//SingleHighScoreManager class 
		}
	}
}
