using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    //singleton pattern
    static public GameManager Instance { get { return _instance; } }
    static protected GameManager _instance;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //the various states the game can be in
    public enum Gamestatus { BeforeGameStart, GameInProgress, Paused, GameOver, LifeLost};
    //the current status of the game
    public Gamestatus Status { get; protected set; }

    public virtual void Pause() { }

    public virtual void UnPause() { }
}
