using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager puzzleManagerInstance;
    [SerializeField] private List<PuzzleAltar> listOfAltar = null;
    [SerializeField] private Transform key = null;
    [SerializeField] private Transform spawnPosition = null;

    void Awake()
    {
        if (puzzleManagerInstance != null && puzzleManagerInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            puzzleManagerInstance = this;
        }
    }

    public void CheckIfAltarsAreCompleted()
    {
        foreach (PuzzleAltar altar in listOfAltar)
        {
            if (!altar.IsActivated())
            {
                Debug.Log("Somes Altars are missing...");
                return;
            }

        }
        SpawnKey();
    }

    public void SpawnKey()
    {
        Instantiate(key, spawnPosition.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
    }
}
