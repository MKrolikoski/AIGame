using Pada1.BBCore;
using Pada1.BBCore.Tasks;

[Action("Custom/Actions/SelectOgre")]
[Help("Selects ogre")]
public class SelectOgre : SelectUnit
{
    public override void OnStart()
    {
        base.OnStart();
        type = typeof(Ogre);
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
