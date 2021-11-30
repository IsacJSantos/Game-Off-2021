using UnityEngine;
using UnityEngine.UI;

public class AntHillController : MonoBehaviour, IBeatable, IAgentTarget
{
    [SerializeField] Collider _collider;
    public Image fillLife;
    public BaseLifeSystem anthillLife;
    public void Hit(float value)
    {
        if (anthillLife.Life <= 0) return;

        anthillLife.Life -= value;

        //fillLife.fillAmount = ((1 / anthillLife.MaxLife) * anthillLife.Life);


        if (!anthillLife.IsAlive)
        {
            Events.OnAnthillDie?.Invoke();
        }

    }

    public Vector3 GetClosestPoint(Vector3 objectPos)
    {
        return _collider.ClosestPointOnBounds(objectPos);
    }
}
