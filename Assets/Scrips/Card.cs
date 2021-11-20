using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] CardType cardType;
    public void Click() 
    {
        switch (cardType)
        {
            case CardType.ImprovePlayerLife:
                Events.OnImprovePlayerLife?.Invoke(20);
                break;
            case CardType.ImproveAnthillLife:
                Events.OnImproveAnthillLife?.Invoke(20);
                break;
            case CardType.DecreaseAbilityCooldown:
                Events.OnDecreaseAbilityCooldown?.Invoke();
                break;
            case CardType.DecreaseReloadDelay:
                Events.OnDecreaseReloadDelay?.Invoke();
                break;
            case CardType.ImproveMagazine:
                Events.OnImproveMagazine?.Invoke();
                break;
            case CardType.ImproveDamage:
                Events.OnImproveDamage?.Invoke();
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
    DecreaseAbilityCooldown,
    DecreaseReloadDelay,
    ImproveMagazine,
    ImproveDamage
}