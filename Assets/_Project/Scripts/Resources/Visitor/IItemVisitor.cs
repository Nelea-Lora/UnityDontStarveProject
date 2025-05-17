namespace _Project.Scripts.Resources.Visitor
{
    public interface IItemVisitor
    {
        void Visit(FoodItem food);
        void Visit(BuildItem build);
        void Visit(InstrumentItem instrument);
        void Visit(MaterialItem material);
        void Visit(LightItem light);
    }

}