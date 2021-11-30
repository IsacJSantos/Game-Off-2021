
public class AnthillLife : BaseLifeSystem
{
    public override void Awake()
    {
        Events.OnImproveAnthillLife += ImproveMaxLife;
    }
    public override void OnDestroy()
    {
        Events.OnImproveAnthillLife -= ImproveMaxLife;
    }

    public override void ImproveMaxLife(float amount)
    {
        Life += MaxLife * (amount / 100);
    }
}
