using System;

namespace Tools.ObjectVisible
{
    public interface ISlotModel
    {
        event Action<float> onSetAlpha;
        event Action<bool> onSetVisible;

        float Alpha { get; }
        bool IsSelected { get; }
        bool IsVisible { get; }
        string Name { get; }

        void ChangeSelect();
        void ChangeVisible();
        void SetAlpha(float alpha);
    }
}