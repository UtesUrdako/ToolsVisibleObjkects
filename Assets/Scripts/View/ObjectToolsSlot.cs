using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.ObjectVisible
{
    public class ObjectToolsSlot : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameField;
        [SerializeField] private Toggle selected;
        [SerializeField] private Toggle visibled;

        private Guid id;
        private ISlotModel model;
        private IObjectVisibleController controller;

        public void Initialize(Guid id, ISlotModel model, IObjectVisibleController controller)
        {
            this.id = id;
            this.model = model;
            this.controller = controller;
            nameField.text = model.Name;
            SetViewSelected(false);
            SetViewVisible(true);

            model.onSetVisible += SetViewVisible;
            model.onSetSelect += SetViewSelected;

            selected.onValueChanged.AddListener(ChangeSelected);
            visibled.onValueChanged.AddListener(ChangeVisible);
        }

        public void ChangeSelected(bool isOn)
        {
            controller.SetSelected(id, isOn);
        }

        public void ChangeVisible(bool isOn)
        {
            controller.SetVisible(id, isOn);
        }

        public void SetViewSelected(bool isSelected)
        {
            selected.isOn = model.IsSelected;
        }

        public void SetViewVisible(bool isVisible)
        {
            visibled.isOn = model.IsVisible;
        }
    }
}