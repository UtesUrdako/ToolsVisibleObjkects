using System;

namespace Tools.ObjectVisible
{
    public interface IObjectVisibleController
    {
        void AddModel(ISlotModel slotModel);
        void SetAlpha(float alpha);
        void SetSelected(bool selected);
        void SetSelected(Guid id, bool selected);
        void SetVisible(bool visible);
        void SetVisible(Guid id, bool visible);
    }
}