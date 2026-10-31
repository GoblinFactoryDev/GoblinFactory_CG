using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class QTEButton : MonoBehaviour
{
    public enum QTETYPE { NONE = -1, dirDOWN, dirLEFT, dirRIGHT, dirUP, A, B, X, Y};
    private enum QTESTATE { IDLE, PASS, FAIL};

    private Image imageRenderer;

    [SerializeField] private List<Sprite> buttonTypeSpritesBase;
    [SerializeField] private List<Sprite> buttonTypeSpritesCorr;
    [SerializeField] private List<Sprite> buttonTypeSpritesFail;

    private QTESTATE currentButtonState;
    private QTETYPE thisQteType = QTETYPE.NONE;

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

    public void SetInputType(QTETYPE type)
    {
        thisQteType = type;

        switch (thisQteType)
        {
            case QTETYPE.NONE:
                imageRenderer.sprite = null;
                Debug.LogWarning("Button Not Assigned InputType (Check QTE Setup)");
                break;

            case QTETYPE.dirDOWN:

                if(currentButtonState == QTESTATE.IDLE)
                    imageRenderer.sprite = buttonTypeSpritesBase[0];
                else if(currentButtonState == QTESTATE.PASS)
                    imageRenderer.sprite = buttonTypeSpritesCorr[0];
                else
                    imageRenderer.sprite = buttonTypeSpritesFail[0];

                    break;

            case QTETYPE.dirLEFT:
                if (currentButtonState == QTESTATE.IDLE)
                    imageRenderer.sprite = buttonTypeSpritesBase[1];
                else if (currentButtonState == QTESTATE.PASS)
                    imageRenderer.sprite = buttonTypeSpritesCorr[1];
                else
                    imageRenderer.sprite = buttonTypeSpritesFail[1];

                break;

            case QTETYPE.dirRIGHT:
                if (currentButtonState == QTESTATE.IDLE)
                    imageRenderer.sprite = buttonTypeSpritesBase[2];
                else if (currentButtonState == QTESTATE.PASS)
                    imageRenderer.sprite = buttonTypeSpritesCorr[2];
                else
                    imageRenderer.sprite = buttonTypeSpritesFail[2];
                break;

            case QTETYPE.dirUP:
                if (currentButtonState == QTESTATE.IDLE)
                    imageRenderer.sprite = buttonTypeSpritesBase[3];
                else if (currentButtonState == QTESTATE.PASS)
                    imageRenderer.sprite = buttonTypeSpritesCorr[3];
                else
                    imageRenderer.sprite = buttonTypeSpritesFail[3];

                break;

            case QTETYPE.A:
                if (currentButtonState == QTESTATE.IDLE)
                    imageRenderer.sprite = buttonTypeSpritesBase[4];
                else if (currentButtonState == QTESTATE.PASS)
                    imageRenderer.sprite = buttonTypeSpritesCorr[4];
                else
                    imageRenderer.sprite = buttonTypeSpritesFail[4];

                break;

            case QTETYPE.B:
                if (currentButtonState == QTESTATE.IDLE)
                    imageRenderer.sprite = buttonTypeSpritesBase[5];
                else if (currentButtonState == QTESTATE.PASS)
                    imageRenderer.sprite = buttonTypeSpritesCorr[5];
                else
                    imageRenderer.sprite = buttonTypeSpritesFail[5];

                break;

            case QTETYPE.X:
                if (currentButtonState == QTESTATE.IDLE)
                    imageRenderer.sprite = buttonTypeSpritesBase[6];
                else if (currentButtonState == QTESTATE.PASS)
                    imageRenderer.sprite = buttonTypeSpritesCorr[6];
                else
                    imageRenderer.sprite = buttonTypeSpritesFail[6];

                break;

            case QTETYPE.Y:
                if (currentButtonState == QTESTATE.IDLE)
                    imageRenderer.sprite = buttonTypeSpritesBase[7];
                else if (currentButtonState == QTESTATE.PASS)
                    imageRenderer.sprite = buttonTypeSpritesCorr[7];
                else
                    imageRenderer.sprite = buttonTypeSpritesFail[7];

                break;

        }
    }
}
