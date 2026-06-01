using System.Collections.Generic;
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

    [Header("-----All-----")]
    // Variables for the Everyone
    [SerializeField, Tooltip("Cards every character has")] 
    private List<CardData> _BasicCards = new List<CardData>();
    public List<CardData> basicCards { get => _BasicCards; }

    [Header("-----DRAGON-----")]
    // Variables for the Dragon
    [SerializeField]
    private GameObject _DragonModel;
    [SerializeField]
    private Material _DragonMat;
    private Vector4 _DragonOutline;
    [SerializeField]
    private GameObject _DragonMap;
    [SerializeField, Tooltip("The Dragons characters cards")]
    private List<CardData> _DragonCards = new List<CardData>();

    //Getters for the Dragon
    public GameObject dragonModel { get => _DragonModel; }
    public Vector4 dragonOutline { get => _DragonMat.GetVector("_OutlineColour"); }
    public GameObject dragonMap { get => _DragonMap; }
    public List<CardData> dragonCards { get => _DragonCards; }

    [Header("-----DWARF-----")]
    // Variables for the Dwarf
    [SerializeField]
    private GameObject _DwarfModel;
    [SerializeField]
    private Material _DwarfMat;
    private Vector4 _DwarfOutline;
    [SerializeField]
    private GameObject _DwarfMap;
    [SerializeField, Tooltip("The Dwarf characters cards")]
    private List<CardData> _DwarfCards = new List<CardData>();

    //Getters for the Dwarf
    public GameObject dwarfModel { get => _DwarfModel; }
    public Vector4 dwarfOutline { get => _DwarfMat.GetVector("_OutlineColour"); }
    public GameObject dwarfMap { get => _DwarfMap; }
    public List<CardData> dwarfCards { get => _DwarfCards; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       _DragonOutline = _DragonMat.GetVector("_OutlineColour");
       _DwarfOutline = _DwarfMat.GetVector("_OutlineColour");
    }

    
}
