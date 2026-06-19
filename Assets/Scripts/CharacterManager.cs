using System.Collections.Generic;
using UnityEditor.UIElements;
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
    private Color _DragonColour;
    [SerializeField]
    private GameObject _DragonMap;
    [SerializeField, Tooltip("The Dragons characters cards")]
    private List<CardData> _DragonCards = new List<CardData>();

    //Getters for the Dragon
    public GameObject dragonModel { get => _DragonModel; }
    public Vector4 dragonOutline { get => _DragonColour; }
    public GameObject dragonMap { get => _DragonMap; }
    public List<CardData> dragonCards { get => _DragonCards; }

    [Header("-----DWARF-----")]
    // Variables for the Dwarf
    [SerializeField]
    private GameObject _DwarfModel;
    [SerializeField]
    private Color _DwarfColour;
    [SerializeField]
    private GameObject _DwarfMap;
    [SerializeField, Tooltip("The Dwarf characters cards")]
    private List<CardData> _DwarfCards = new List<CardData>();

    //Getters for the Dwarf
    public GameObject dwarfModel { get => _DwarfModel; }
    public Vector4 dwarfOutline { get => _DwarfColour; }
    public GameObject dwarfMap { get => _DwarfMap; }
    public List<CardData> dwarfCards { get => _DwarfCards; }
    
}
