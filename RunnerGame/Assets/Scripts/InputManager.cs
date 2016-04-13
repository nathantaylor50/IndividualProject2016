using UnityEngine;
using System.Collections;

namespace RunnerGame
{
	/// <summary>
	/// Input manager.  This class acts as a persistent singleton, a singleton in unity allows a 
	/// game object to be accessed anywhere 1. cannot be immutable 
	/// prevents destruction when loading different levels for use of stuff such as player score or
	/// health that you wouldnt want to reset each time you build the level
	/// </summary>
    public class InputManager : MonoBehaviour
    {
        //singleton pattern
        static public InputManager Instance { get { return instance; } }
        static protected InputManager instance;
		/// <summary>
		/// Awake this instance.
		/// </summary>
        public void Awake()
        {
            instance = this;
        }

		/// <summary>
		/// every frame, we get the various inputs and process them
		/// </summary>
        protected virtual void Update()
        {
            HandleKeyboard();
        }

		/// <summary>
		//called at each Update(), it checks for various key presses
		/// </summary>
        protected virtual void HandleKeyboard()
        {
            //pause button
            if (Input.GetButtonDown("Pause")) { PauseButtonDown(); }
            if (Input.GetButtonUp("Pause")) { PauseButtonUp(); }
            if (Input.GetButton("Pause")) { PauseButtonPressed(); }
            //main action button
            if (Input.GetButtonDown("MainAction")) { MainActionButtonDown(); }
            if (Input.GetButtonUp("MainAction")) { MainActionButtonUp(); }
            if (Input.GetButton("MainAction")) { MainActionButtonPressing(); }
            //Left button
            if (Input.GetButtonDown("Left")) { LeftButtonDown(); }
            if (Input.GetButtonUp("Left")) { LeftButtonUp(); }
            if (Input.GetButton("Left")) { LeftButtonPressing(); }
            //Right Button
            if (Input.GetButtonDown("Right")) { RightButtonDown(); }
            if (Input.GetButtonUp("Right")) { RightButtonUp(); }
            if (Input.GetButton("Right")) { RightButtonPressing(); }
            //Up Button
            if (Input.GetButtonDown("Up")) { UpButtonDown(); }
            if (Input.GetButtonUp("Up")) { UpButtonUp(); }
            if (Input.GetButton("Up")) { UpButtonPressing(); }
            //Down Button
            if (Input.GetButtonDown("Down")) { DownButtonDown(); }
            if (Input.GetButtonUp("Down")) { DownButtonUp(); }
            if (Input.GetButton("Down")) { DownButtonPressing(); }
        }


		///PAUSE BUTTON --------------------------------------------------------------------------------------
        /// <summary>
		/// riggered when the Pause button is pressed down
        /// </summary>
        public virtual void PauseButtonDown() { GameManager.Instance.PauseGame(); }
        // Triggered when the Pause button is released
        public virtual void PauseButtonUp() { }
        // Triggered while the Pause button is being pressed
        public virtual void PauseButtonPressed() { }



		///  MAIN ACTION BUTTON--------------------------------------------------------------------------------
		///<summary>
		/// Triggered when the Main Action button is pressed down
		/// </summary>
        public virtual void MainActionButtonDown()
        {   
            //control state is set to singlebutton then any press will do the following
            if(LevelManager.Instance.ControlProfile == LevelManager.Controls.SingleButton)
            {
                //if the gamemanager state is gameover, any press will incur the game over action
				if(GameManager.Instance.StateStatus == GameManager.GameStateStatus.GameOver)
                {
                    LevelManager.Instance.GameOverEvent();
                    return;
                }
                //if the player died. reset the game
				if(GameManager.Instance.StateStatus == GameManager.GameStateStatus.GameLifeLost)
                {
                    LevelManager.Instance.LifeLostEvent();
                    return;
                }
            }

			for (int i = 0; i < LevelManager.Instance.CurrentPlayablePlayers.Count; ++i) {
				LevelManager.Instance.CurrentPlayablePlayers [i].MainActionPressed ();
			}
        }


        // Triggered when the Main Action button is released
        public virtual void MainActionButtonUp() {
			for (int i = 0; i < LevelManager.Instance.CurrentPlayablePlayers.Count; i++)
			{
				LevelManager.Instance.CurrentPlayablePlayers[i].MainActionReleased();
			}
		}
        // Triggered while the Main Action button is being pressed
        public virtual void MainActionButtonPressing() {
			for (int i = 0; i < LevelManager.Instance.CurrentPlayablePlayers.Count; i++) {
				LevelManager.Instance.CurrentPlayablePlayers [i].MainActionPressing ();
			}
		}


        /*
            Left -----------------------------------------------------------------------------------------
        */
        // Triggered when the Left button is pressed down
        public virtual void LeftButtonDown() {
			if (LevelManager.Instance.ControlProfile == LevelManager.Controls.LeftRight) {
				if (GameManager.Instance.StateStatus == GameManager.GameStateStatus.GameOver) {
					LevelManager.Instance.GameOverEvent ();
					return;
				}
				if (GameManager.Instance.StateStatus == GameManager.GameStateStatus.GameLifeLost) {
					LevelManager.Instance.LifeLostEvent ();
					return;
				}
			}

			for (int i = 0; i < LevelManager.Instance.CurrentPlayablePlayers.Count; i++) {
				LevelManager.Instance.CurrentPlayablePlayers [i].LeftPressed ();
			}
		}
        // Triggered when the Left button is released
        public virtual void LeftButtonUp() { 
			for (int i = 0; i < LevelManager.Instance.CurrentPlayablePlayers.Count; i++) {
				LevelManager.Instance.CurrentPlayablePlayers [i].LeftReleased ();
			}
		}
        // Triggered while the Left button is being pressed
        public virtual void LeftButtonPressing() {
			for (int i = 0; i < LevelManager.Instance.CurrentPlayablePlayers.Count; i++) {
				LevelManager.Instance.CurrentPlayablePlayers [i].LeftPressing ();
			}
		}


        /*
             Right -----------------------------------------------------------------------------------------
        */
        // Triggered when the Right button is pressed down
        public virtual void RightButtonDown() { 
			for (int i = 0; i < LevelManager.Instance.CurrentPlayablePlayers.Count; i++) {
				LevelManager.Instance.CurrentPlayablePlayers [i].RightPressed ();
			}
		
		}
        // Triggered when the Right button is released
        public virtual void RightButtonUp() { 
			for (int i = 0; i < LevelManager.Instance.CurrentPlayablePlayers.Count; i++) {
				LevelManager.Instance.CurrentPlayablePlayers [i].RightReleased ();
			}
		}
        // Triggered while the Right button is being pressed
        public virtual void RightButtonPressing() {
			for (int i = 0; i < LevelManager.Instance.CurrentPlayablePlayers.Count; i++) {
				LevelManager.Instance.CurrentPlayablePlayers [i].RightPressing ();
			}
		}


        /*
             Up -----------------------------------------------------------------------------------------
        */
        // Triggered when the Up button is pressed down
        public virtual void UpButtonDown() {
			for (int i = 0; i < LevelManager.Instance.CurrentPlayablePlayers.Count; i++) {
				LevelManager.Instance.CurrentPlayablePlayers [i].UpPressed ();
			}
		}
        // Triggered when the Up button is released
        public virtual void UpButtonUp() { 
			for (int i = 0; i < LevelManager.Instance.CurrentPlayablePlayers.Count; i++) {
				LevelManager.Instance.CurrentPlayablePlayers [i].UpReleased ();
			}
		}
        // Triggered while the Up button is being pressed
        public virtual void UpButtonPressing() {
			for (int i = 0; i < LevelManager.Instance.CurrentPlayablePlayers.Count; i++) {
				LevelManager.Instance.CurrentPlayablePlayers [i].UpPressing ();
			}
		}


        /*
             Down -----------------------------------------------------------------------------------------
        */
        // Triggered when the Down button is pressed down
        public virtual void DownButtonDown() {
			for (int i = 0; i < LevelManager.Instance.CurrentPlayablePlayers.Count; i++) {
				LevelManager.Instance.CurrentPlayablePlayers [i].DownPressed ();
			}
		}
        // Triggered when the Down button is released
        public virtual void DownButtonUp() { 
			for (int i = 0; i < LevelManager.Instance.CurrentPlayablePlayers.Count; i++) {
				LevelManager.Instance.CurrentPlayablePlayers [i].Downreleased ();
			}
		}
        // Triggered while the Down button is being pressed
        public virtual void DownButtonPressing() { 
			for (int i = 0; i < LevelManager.Instance.CurrentPlayablePlayers.Count; i++) {
				LevelManager.Instance.CurrentPlayablePlayers [i].DownPressing ();
			}
		}
    }
}
        
        
        




