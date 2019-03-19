using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*----Singleton----*/
public class GameManager : MonoBehaviour
{
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    protected GameManager()
    {
        GameState = GameState.Start;
        CanMove = false;
    }

    public GameState GameState { get; set; }

    protected bool CanMove { get; set; }

    public void Die()
    {
        //UIManager.Instance.SetStatus(Constants.StatusDeadClickToStart);
        this.GameState = GameState.Dead;
    }

}
