using UnityEngine;

public class ColourManager : MonoBehaviour
{
    private static ColourManager _instance;

    public static ColourManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<ColourManager>();
            }

            if (!_instance)
            {
                Debug.LogError("No Colour Manager Present!!!!");
            }

            return _instance;
        }
    }


    [SerializeField]
    private Material DragonMat, DwarfMat;

    private Vector4 _DragonOutline, _DwarfOutline;

    public Vector4 dragonOutline { get => _DragonOutline; }
    public Vector4 dwarfOutline { get => _DwarfOutline; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       _DragonOutline = DragonMat.GetVector("_OutlineColour");
       _DwarfOutline = DwarfMat.GetVector("_OutlineColour");
    }

    
}
