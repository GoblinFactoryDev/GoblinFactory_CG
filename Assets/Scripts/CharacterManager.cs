using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;

    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<CharacterManager>();
            }

            if (!_instance)
            {
                Debug.LogError("No Colour Manager Present!!!!");
            }

            return _instance;
        }
    }


    [Header("-----DRAGON-----")]
    // Variables for the Dragon
    [SerializeField]
    private GameObject _DragonModel;
    [SerializeField]
    private Material DragonMat;
    private Vector4 _DragonOutline;

    //Getters for the Dragon
    public GameObject dragonModel { get => _DragonModel; }
    public Vector4 dragonOutline { get => DragonMat.GetVector("_OutlineColour"); }

    [Header("-----DWARF-----")]
    // Variables for the Dwarf
    [SerializeField]
    private GameObject _DwarfModel;
    [SerializeField]
    private Material DwarfMat;
    private Vector4 _DwarfOutline;

    //Getters for the Dwarf
    public GameObject dwarfModel { get => _DwarfModel; }
    public Vector4 dwarfOutline { get => DwarfMat.GetVector("_OutlineColour"); }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       _DragonOutline = DragonMat.GetVector("_OutlineColour");
       _DwarfOutline = DwarfMat.GetVector("_OutlineColour");
    }

    
}
