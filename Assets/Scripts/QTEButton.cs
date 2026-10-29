using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QTEButton : MonoBehaviour
{
    private Image imageRenderer;

    [SerializeField] private List<Sprite> buttonTypeSpritesBase;
    [SerializeField] private List<Sprite> buttonTypeSpritesCorr;
    [SerializeField] private List<Sprite> buttonTypeSpritesFail;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageRenderer = GetComponent<Image>();

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
