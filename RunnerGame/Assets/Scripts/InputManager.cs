using UnityEngine;
using System.Collections;

namespace RunnerGame
{
    /*
    
        This class acts as a persistent singleton, a singleton in unity allows a 
        game object to be accessed anywhere 1. cannot be immutable 
        prevents destruction when loading different levels for use of stuff such as player score or
        health that you wouldnt want to reset each time you build the level
    */
    public class InputManager : MonoBehaviour
    {
        //singleton pattern
        static public InputManager Instance { get { return _instance; } }
        static protected InputManager _instance;

        public void Awake()
        {
            _instance = this;
        }

        /*
            every frame, we get the various inputs and process them
        */
        protected virtual void Update()
        {
            HandleKeyboard();
        }

        /*
            called at each Update(), it checks for various key presses
        */
        protected virtual void HandleKeyboard()
        {
            //pause button
            if (Input.GetButtonDown("Pause")) { PauseButtonDown(); }
            if (Input.GetButtonUp("Pause")) { PauseButtonUp(); }
            if (Input.GetButton("Pause")) { PauseButtonPressed(); }
            //main action button
            if (Input.GetButtonDown("MainAction")) { MainActionButtonDown(); }
            if (Input.GetButtonUp("MainAction")) { MainActionButtonUp(); }
            if (Input.GetButton("MainAction")) { MainActionButtonPressed(); }
            //Left button
            if (Input.GetButtonDown("Left")) { LeftButtonDown(); }
            if (Input.GetButtonUp("Left")) { LeftButtonUp(); }
            if (Input.GetButton("Left")) { LeftButtonPressed(); }
            //Right Button
            if (Input.GetButtonDown("Right")) { RightButtonDown(); }
            if (Input.GetButtonUp("Right")) { RightButtonUp(); }
            if (Input.GetButton("Right")) { RightButtonPressed(); }
            //Up Button
            if (Input.GetButtonDown("Up")) { UpButtonDown(); }
            if (Input.GetButtonUp("Up")) { UpButtonUp(); }
            if (Input.GetButton("Up")) { UpButtonPressed(); }
            //Down Button
            if (Input.GetButtonDown("Down")) { DownButtonDown(); }
            if (Input.GetButtonUp("Down")) { DownButtonUp(); }
            if (Input.GetButton("Down")) { DownButtonPressed(); }
            //Jump Button
            if (Input.GetButtonDown("Jump")) { JumpButtonDown(); }
            if (Input.GetButtonUp("Jump")) { JumpButtonUp(); }
            if (Input.GetButton("Jump")) { JumpButtonPressed(); }
        }

        /*
            Pause -----------------------------------------------------------------------------------------
        */
        // Triggered when the Pause button is pressed down
        public virtual void PauseButtonDown() { GameManager.Instance.Pause(); }
        // Triggered when the Pause button is released
        public virtual void PauseButtonUp() { }
        // Triggered while the Pause button is being pressed
        public virtual void PauseButtonPressed() { }


        /*
            Main Action -----------------------------------------------------------------------------------------
        */
        // Triggered when the Main Action button is pressed down
        public virtual void MainActionButtonDown()
        {   
            //control state is set to singlebutton then any press will do the following
            if(LevelManager.Instance.ControlScheme == LevelManager.Controls.SingleButton)
            {
                //if the gamemanager state is gameover, any press will incur the game over action
                if(GameManager.Instance.Status == GameManager.Gamestatus.GameOver)
                {
                    LevelManager.Instance.GameOverAction();
                    return;
                }
                //if the player died. reset the game
                if(GameManager.Instance.Status == GameManager.Gamestatus.LifeLost)
                {
                    LevelManager.Instance.LifeLostAction();
                    return;
                }
            }
        }
        // Triggered when the Main Action button is released
        public virtual void MainActionButtonUp() { }
        // Triggered while the Main Action button is being pressed
        public virtual void MainActionButtonPressed() { }


        /*
            Left -----------------------------------------------------------------------------------------
        */
        // Triggered when the Left button is pressed down
        public virtual void LeftButtonDown() { }
        // Triggered when the Left button is released
        public virtual void LeftButtonUp() { }
        // Triggered while the Left button is being pressed
        public virtual void LeftButtonPressed() { }


        /*
             Right -----------------------------------------------------------------------------------------
        */
        // Triggered when the Right button is pressed down
        public virtual void RightButtonDown() { }
        // Triggered when the Right button is released
        public virtual void RightButtonUp() { }
        // Triggered while the Right button is being pressed
        public virtual void RightButtonPressed() { }


        /*
             Up -----------------------------------------------------------------------------------------
        */
        // Triggered when the Up button is pressed down
        public virtual void UpButtonDown() { }
        // Triggered when the Up button is released
        public virtual void UpButtonUp() { }
        // Triggered while the Up button is being pressed
        public virtual void UpButtonPressed() { }


        /*
             Down -----------------------------------------------------------------------------------------
        */
        // Triggered when the Down button is pressed down
        public virtual void DownButtonDown() { }
        // Triggered when the Down button is released
        public virtual void DownButtonUp() { }
        // Triggered while the Down button is being pressed
        public virtual void DownButtonPressed() { }

        /*
            Jump -----------------------------------------------------------------------------------------
        */
        // Triggered when the Down button is pressed down
        public virtual void JumpButtonDown() { }
        // Triggered when the Down button is released
        public virtual void JumpButtonUp() { }
        // Triggered while the Down button is being pressed
        public virtual void JumpButtonPressed() { }


    }
}
        
        
        




