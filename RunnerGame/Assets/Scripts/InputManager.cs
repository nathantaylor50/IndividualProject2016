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
            if (Input.G
        }
    }
}
