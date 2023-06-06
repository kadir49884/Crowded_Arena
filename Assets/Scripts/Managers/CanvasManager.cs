using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private static CanvasManager instance;


    [SerializeField]
    public VariableJoystick JoystickObject;

    public static CanvasManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

}
