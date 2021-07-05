using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour
{
    protected MyTimeManager timeManager;
    public static Ocean oceanInstance;
    [SerializeField] private Animator animator = null;

    [Header("High Tide")]
    [SerializeField] private int highTideHour = 2;
    [SerializeField] private float highTideHeight = 2;

    [Header("Low Tide")]
    [SerializeField] private int lowTideHour = 4;
    [SerializeField] private float lowTideHeight = 2;

    private bool highTide = false;

    void Awake()
    {
        if (oceanInstance != null && oceanInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            oceanInstance = this;
        }
    }

    private void Start()
    {
        timeManager = MyTimeManager.timeInstance;
        timeManager.GHourPassed += OnGHourPassed;
    }

    public void OnGHourPassed(object source)
    {
        if (timeManager.Hour == highTideHour)
            IncreaseWaterLevel();

        if (timeManager.Hour == lowTideHour)
            DecreaseWaterLevel();
    }
    public void IncreaseWaterLevel()
    {
        animator.Play("WaterRaise");
        highTide = true;
        transform.position = new Vector3(transform.position.x, highTideHeight, transform.position.z);
    }
    public void DecreaseWaterLevel()
    {
        animator.Play("WaterDecrease");
        highTide = false;
        transform.position = new Vector3(transform.position.x, lowTideHeight, transform.position.z);
    }
    public float GetWaterLevel()
    {
        if (highTide)
            return highTideHeight;
        else
            return lowTideHeight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<HealthModule>().DecreaseHealth(1000);
        }
    }
}
