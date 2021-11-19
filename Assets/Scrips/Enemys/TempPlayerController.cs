using UnityEngine.UI;
using UnityEngine;

public class TempPlayerController : MonoBehaviour, IBeatable, IAgentTarget
{
    public Image fillLife;
    public BaseLifeSystem playerLife;
    
    [SerializeField] BoxCollider _boxCollider;
    public Vector3 GetClosestPoint(Vector3 objectPos)
    {
       return _boxCollider.ClosestPointOnBounds(objectPos);
    }

    public void Hit(float value)
    {
        if (playerLife.Life <= 0) return;

        playerLife.Life  -= value;

        if (playerLife.Life < 0)
            playerLife.Life = 0;

        fillLife.fillAmount = ((1 / playerLife.MaxLife) * playerLife.Life);
    }


}
