using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour
{
    [SerializeField] Canvas _cv;
    [SerializeField] GameObject[] cards;
    [SerializeField] Transform _container;
    private void Awake()
    {
        Events.OnShowCards += ShowCard;
        Events.OnCardClicked += HidePanel;
    }
    private void OnDestroy()
    {
        Events.OnShowCards -= ShowCard;
        Events.OnCardClicked -= HidePanel;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            Events.OnShowCards?.Invoke();
        else if(Input.GetKeyDown(KeyCode.J))
            Events.OnCardClicked?.Invoke();
    }
    void ShowCard()
    {
        InstantiateCards();
        _cv.enabled = true;
    }

    void HidePanel() 
    {
        _cv.enabled = false;
        desableOldCards();
    }

    void InstantiateCards()
    {
        List<int> spawnedIds = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            int x = 0;
            do
            {
                x = Random.Range(0, cards.Length);
            }
            while (spawnedIds.Count > 0 && spawnedIds.Contains(x));
            spawnedIds.Add(x);
            Instantiate(cards[x], _container);
        }
    }

    void desableOldCards() 
    {
        for (int i = 0; i < _container.childCount; i++)
        {
           Destroy( _container.GetChild(i).gameObject);
        }
    }
}
