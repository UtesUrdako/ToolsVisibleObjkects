namespace Tools.ObjectVisible
{
    public interface ISlotModel
    {
        float Alpha { get; }
        bool IsSelected { get; }
        bool IsVisible { get; }
        string Name { get; }

        void ChangeSelect();
        void ChangeVisible();
        void SetAlpha(float alpha);
    }
}