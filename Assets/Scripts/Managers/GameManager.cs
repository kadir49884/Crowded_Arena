using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Status1,
    Status2
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private State currentState;
    public State CurrentState { get => currentState; set => currentState = value; }


}
