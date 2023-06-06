using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaFakePoint : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<NeutralControl>()?.FakeGoArenaPoint();
    }
}
