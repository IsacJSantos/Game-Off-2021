
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
}
