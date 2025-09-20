using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardObjectPool : MonoBehaviour
{
    private static CardObjectPool _instance;

    public static CardObjectPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<CardObjectPool>();
            }

            if (!_instance)
            {
                Debug.LogError("No Round Manager Present!!!!");
            }

            return _instance;
        }
    }

    /// <summary>
    /// The pool of card GameObjects available for reuse.
    /// </summary>
    public List<GameObject> cardPool = new List<GameObject>();
    public GameObject cardPrefab;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject card = Instantiate(cardPrefab);
            card.SetActive(false);
            cardPool.Add(card);
        }
    }

    public GameObject GetCardFromPool()
    {
        foreach (GameObject card in cardPool)
        {
            if (!card.activeInHierarchy)
            {
                card.SetActive(true);
                return card;
            }
        }

        // If no inactive cards are available, instantiate a new one
        GameObject newCard = Instantiate(cardPrefab);
        cardPool.Add(newCard);
        return newCard;
    }
}
