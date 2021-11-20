using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] CardType cardType;
    public void Click() 
    {
        print("Click");
        switch (cardType)
        {
            case CardType.ImprovePlayerLife:
                Events.OnImprovePlayerLife?.Invoke(20);
                break;
            case CardType.ImproveAnthillLife:
                Events.OnImproveAnthillLife?.Invoke(20);
                break;
            default:
                break;
        }
        Events.OnCardClicked?.Invoke();
    }
}
[System.Serializable]
public enum CardType
{
    ImprovePlayerLife,
    ImproveAnthillLife,

}