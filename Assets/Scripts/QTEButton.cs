using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class QTEButton : MonoBehaviour
{
    private Image imageRenderer;

    [SerializeField] private List<Sprite> buttonTypeSpritesBase;
    [SerializeField] private List<Sprite> buttonTypeSpritesCorr;
    [SerializeField] private List<Sprite> buttonTypeSpritesFail;

    private QTEButtonState currentButtonState;

    private QTEType currentType = QTEType.NONE;
    public QTEType getCurrentType { get {  return currentType; } }
    public void SetCurrentType(QTEType setValue) { currentType = setValue; }

    private InputAction requiredAction;

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

    public void SetInputType(QTEType type)
    {
        currentType = type;

        switch (currentType)
        {
            case QTEType.NONE:
                imageRenderer.sprite = null;
                Debug.LogWarning("Button Not Assigned InputType (Check QTE Setup)");
                break;

            case QTEType.dirDOWN:

                if(currentButtonState == QTEButtonState.Default)
                    imageRenderer.sprite = buttonTypeSpritesBase[0];
                else if(currentButtonState == QTEButtonState.Success)
                    imageRenderer.sprite = buttonTypeSpritesCorr[0];
                else
                    imageRenderer.sprite = buttonTypeSpritesFail[0];

                    break;

            case QTEType.dirLEFT:
                if (currentButtonState == QTEButtonState.Default)
                    imageRenderer.sprite = buttonTypeSpritesBase[1];
                else if (currentButtonState == QTEButtonState.Success)
                    imageRenderer.sprite = buttonTypeSpritesCorr[1];
                else
                    imageRenderer.sprite = buttonTypeSpritesFail[1];

                break;

            case QTEType.dirRIGHT:
                if (currentButtonState == QTEButtonState.Default)
                    imageRenderer.sprite = buttonTypeSpritesBase[2];
                else if (currentButtonState == QTEButtonState.Success)
                    imageRenderer.sprite = buttonTypeSpritesCorr[2];
                else
                    imageRenderer.sprite = buttonTypeSpritesFail[2];
                break;

            case QTEType.dirUP:
                if (currentButtonState == QTEButtonState.Default)
                    imageRenderer.sprite = buttonTypeSpritesBase[3];
                else if (currentButtonState == QTEButtonState.Success)
                    imageRenderer.sprite = buttonTypeSpritesCorr[3];
                else
                    imageRenderer.sprite = buttonTypeSpritesFail[3];

                break;

            case QTEType.A:
                if (currentButtonState == QTEButtonState.Default)
                    imageRenderer.sprite = buttonTypeSpritesBase[4];
                else if (currentButtonState == QTEButtonState.Success)
                    imageRenderer.sprite = buttonTypeSpritesCorr[4];
                else
                    imageRenderer.sprite = buttonTypeSpritesFail[4];

                break;

            case QTEType.B:
                if (currentButtonState == QTEButtonState.Default)
                    imageRenderer.sprite = buttonTypeSpritesBase[5];
                else if (currentButtonState == QTEButtonState.Success)
                    imageRenderer.sprite = buttonTypeSpritesCorr[5];
                else
                    imageRenderer.sprite = buttonTypeSpritesFail[5];

                break;

            case QTEType.X:
                if (currentButtonState == QTEButtonState.Default)
                    imageRenderer.sprite = buttonTypeSpritesBase[6];
                else if (currentButtonState == QTEButtonState.Success)
                    imageRenderer.sprite = buttonTypeSpritesCorr[6];
                else
                    imageRenderer.sprite = buttonTypeSpritesFail[6];

                break;

            case QTEType.Y:
                if (currentButtonState == QTEButtonState.Default)
                    imageRenderer.sprite = buttonTypeSpritesBase[7];
                else if (currentButtonState == QTEButtonState.Success)
                    imageRenderer.sprite = buttonTypeSpritesCorr[7];
                else
                    imageRenderer.sprite = buttonTypeSpritesFail[7];
                break;

        }
    }

    public void SetButtonState(QTEButtonState buttonState)
    { 
        switch (buttonState)
        {
            case QTEButtonState.Default:
                currentButtonState = buttonState;
                break;

            case QTEButtonState.Success:
                currentButtonState = buttonState;
                break;

            case QTEButtonState.Failure:
                currentButtonState = buttonState;
                break;

        }

        SetInputType(currentType);
    }
}
