namespace Tools.ObjectVisible
{
    public interface IGameObjectView
    {
        string Name { get; }
        void Initialize(ISlotModel model);
        void SetAlpha(float alpha);
        void SetVisible(bool value);
    }
}