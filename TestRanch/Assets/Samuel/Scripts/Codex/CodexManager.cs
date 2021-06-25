using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodexManager : MonoBehaviour
{
    public static CodexManager codexInstance;

    [SerializeField] private GameObject player = null;

    [Header("UI")]
    public List<CodexObject> codexList = new List<CodexObject>();
    [SerializeField] private Transform codexInterface = null;
    [SerializeField] private Transform codexNewDiscoveryInterface = null;
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
        player.GetComponentInChildren<CameraControl>().LockMouse();
        isOpen = true;
    }
    public void ClosePanel()
    {
        codexInterface.gameObject.SetActive(false);
        player.GetComponentInChildren<CameraControl>().UnlockMouse();
        isOpen = false;
    }
    public void DiscoverCodex(CodexScriptable codexScriptable)
    {
        foreach (CodexObject codex in codexList)
        {
            if(codexScriptable == codex.GetCodex())
            {
                StartCoroutine(TemporaryVisual(codexScriptable));
                codex.Discover();
                break;
            }
        }
    }
    private IEnumerator TemporaryVisual(CodexScriptable codexScriptable)
    {
        codexNewDiscoveryInterface.gameObject.SetActive(true);
        codexNewDiscoveryInterface.GetComponent<Text>().text = "Discover : " + codexScriptable.GetName() + "\n" + "Upgrade Unlocked : " + codexScriptable.GetListOfUpgrade()[0].GetName();

        yield return new WaitForSeconds(5);
        codexNewDiscoveryInterface.gameObject.SetActive(false);

    }

}
