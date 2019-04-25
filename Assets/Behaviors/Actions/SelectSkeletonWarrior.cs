using Pada1.BBCore;
using Pada1.BBCore.Tasks;

[Action("Custom/Actions/SelectSkeletonWarrior")]
[Help("Selects Skeleton Warrior")]
public class SelectSkeletonWarrior : SelectUnit
{
    public override void OnStart()
    {
        base.OnStart();
        type = typeof(SkeletonWarrior);
    }

    public override TaskStatus OnUpdate()
    {
        return base.OnUpdate();
    }

    protected override Unit findUnit()
    {
        Unit[] units = AI.instance.units.ToArray();
        for (int i = 0; i < units.Length; i++)
        {
            if (units[i].GetType() == type && units[i].canBeSelected)
            {
                return units[i];
            }
        }
        return null;
    }
}
