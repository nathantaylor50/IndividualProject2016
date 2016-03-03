using UnityEngine;
using System.Collections;
/*
    t
*/
public class LevelManager : MonoBehaviour {

    //singleton pattern
    static public LevelManager Instance { get { return _instance; } }
    static protected LevelManager _instance;

    protected virtual void Awake()
    {
        _instance = this;
    }

    public enum Controls { SingleButton, LeftRight, Jump }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    [Space(10)]
    [Header("Mobile Controls")]
    //the mobile control scheme applied to this level
    public Controls ControlScheme;

    /*
        Triggered when all lives are lost and the player presses the main action button down
    */ 
    public virtual void GameOverAction()
    {
        //Application.LoadLevel(Application.LoadedLevel);
    }

    //
    public virtual void LifeLostAction()
    {
        
    }
}


