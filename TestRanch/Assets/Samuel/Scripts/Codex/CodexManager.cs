using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodexManager : MonoBehaviour
{
    public List<CodexObject> codexList = new List<CodexObject>();
    public static CodexManager codexInstance;
    [SerializeField] private CameraControl cameraControl = null;
    [SerializeField] private Transform codexInterface = null;
    [SerializeField] private GameObject player = null;
    private bool isOpen = false;


    void Awake()
    {  
    
        if (codexInstance != null && codexInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            codexInstance = this;
        }
    }
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("nop");
            Interact(player.GetComponent<Player>());
        }
    }
    public void Interact(Player joueur)
    {
        if (isOpen)
            ClosePanel();
        else
            OpenPanel();
    }
    public void OpenPanel()
    {
        codexInterface.gameObject.SetActive(true);
        cameraControl.LockCamera();
        isOpen = true;
    }
    public void ClosePanel()
    {
        codexInterface.gameObject.SetActive(false);
        cameraControl.UnlockCamera();
        isOpen = false;
    }
    public void DiscoverCodex(CodexScriptable codexScriptable)
    {
        foreach (CodexObject codex in codexList)
        {
            if(codexScriptable == codex.GetCodex())
            {
                codex.Discover();
                break;
            }
        }
    }



}
