using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlayer : MonoBehaviour
{
    private static DoorPlayer instance = null;

    [SerializeField, ReadOnly]
    private int _inArenaPlayerCount = 0;

    public static DoorPlayer Instance { get => instance; set => instance = value; }
    public int InArenaPlayerCount { get => _inArenaPlayerCount; set => _inArenaPlayerCount = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<NeutralControl>()?.PlayerGoArena(gameObject);
    }

}
