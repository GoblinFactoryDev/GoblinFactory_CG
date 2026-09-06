using UnityEngine;

public class RingManager : MonoBehaviour
{
    private static RingManager _instance;

    public static RingManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<RingManager>();
            }

            if (!_instance)
            {
                Debug.LogError("No Ring Manager Present!");
            }

            return _instance;
        }
    }

    private int p1NumberOfRings;
    private int p2NumberOfRings;
    private int totalNumberOfRings;

    [SerializeField]
    private GameObject ringGameObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeRingObjectPool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeRingObjectPool()
    {
        p1NumberOfRings += GameManager.Instance.player1.GetNumberOfFingers();
        p2NumberOfRings += GameManager.Instance.player2.GetNumberOfFingers();

        totalNumberOfRings = p1NumberOfRings + p2NumberOfRings;

        for (int i = 0; i < totalNumberOfRings; i++)
        {
            Instantiate(ringGameObject, this.transform);
        }
    }
}
