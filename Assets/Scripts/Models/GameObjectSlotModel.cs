using System;
using UnityEditor;
using UnityEngine;

namespace Tools.ObjectVisible
{
    public class GameObjectSlotModel : ISlotModel
    {
        private string id;
        private bool isSelected;
        private bool isVisible;
        private float alpha;
        private string name;

        public GameObjectSlotModel(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public event Action<float> onSetAlpha;
        public event Action<bool> onSetVisible;

        public bool IsSelected => isSelected;
        public bool IsVisible => isVisible;
        public float Alpha => alpha;
        public string Name => name;

        public void ChangeSelect()
        {
            isSelected = !isSelected;
        }

        public void ChangeVisible()
        {
            isVisible = !isVisible;
        }

        public void SetAlpha(float alpha)
        {
            this.alpha = alpha;
        }

        public override bool Equals(object obj)
        {
            if (obj is string id)
                return this.id == id;
            else
                return false;
        }
    }
}