using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaLoginControl : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<NeutralControl>()?.TriggerClose();
    }
}
