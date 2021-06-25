using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodexDiscovery : MonoBehaviour
{
    [SerializeField] CodexScriptable codex = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SendDiscovery();
        }
    }
    private void SendDiscovery()
    {
        CodexManager.codexInstance.DiscoverCodex(codex);
    }
}
