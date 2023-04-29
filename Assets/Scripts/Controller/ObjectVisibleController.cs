using System;
using System.Collections.Generic;
using Tools.ObjectVisible;

public class ObjectVisibleController : IObjectVisibleController
{
    private List<ISlotModel> slotModels = new List<ISlotModel>();

    public void AddModel(ISlotModel slotModel)
    {
        slotModels.Add(slotModel);
    }

    public void SetAlpha(float alpha)
    {
        foreach (var slot in slotModels)
        {
            if (slot.IsSelected)
                slot.SetAlpha(alpha);
        }
    }

    public void SetSelected(bool selected)
    {
        foreach (var slot in slotModels)
        {
            if (slot.IsSelected != selected)
                slot.ChangeSelect();
        }
    }

    public void SetSelected(Guid id, bool selected)
    {
        foreach (var slot in slotModels)
        {
            if (slot.Id.Equals(id))
            {
                if (slot.IsSelected != selected)
                    slot.ChangeSelect();
                break;
            }
        }
    }

    public void SetVisible(bool visible)
    {
        foreach (var slot in slotModels)
        {
            if (slot.IsSelected)
                if (slot.IsVisible != visible)
                    slot.ChangeVisible();
        }
    }

    public void SetVisible(Guid id, bool visible)
    {
        foreach (var slot in slotModels)
        {
            if (slot.Id.Equals(id))
            {
                if (slot.IsVisible != visible)
                    slot.ChangeVisible();
                break;
            }
        }
    }
}
