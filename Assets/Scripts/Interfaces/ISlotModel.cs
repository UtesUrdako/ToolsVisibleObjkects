using System;

namespace Tools.ObjectVisible
{
    public interface ISlotModel
    {
        event Action<float> onSetAlpha;
        event Action<bool> onSetVisible;
        event Action<bool> onSetSelect;

        float Alpha { get; }
        bool IsSelected { get; }
        bool IsVisible { get; }
        string Name { get; }
        Guid Id { get; }

        void ChangeSelect();
        void ChangeVisible();
        void SetAlpha(float alpha);
    }
}