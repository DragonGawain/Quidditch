using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchBountaries : MonoBehaviour
{

    // Notify the player when they are back in the allowed game zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Debug.Log("Back in the allowed game zone!");
    }

    // Notify the player when they exit the pitch boundaries
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            Debug.Log("Exited the pitch boundaries, please return back!");
    }
}
