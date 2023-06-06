using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class DoorFake : MonoBehaviour
{
    private static DoorFake instance = null;
    

    [SerializeField, ReadOnly]
    private int _inArenaFakeCount = 0;

    public int InArenaFakeCount { get => _inArenaFakeCount; set => _inArenaFakeCount = value; }
    public static DoorFake Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<NeutralControl>()?.FakeGoArena(gameObject);
    }
}
