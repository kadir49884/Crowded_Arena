using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaPlayerPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<NeutralControl>()?.PlayerGoArenaPoint();
    }

}
