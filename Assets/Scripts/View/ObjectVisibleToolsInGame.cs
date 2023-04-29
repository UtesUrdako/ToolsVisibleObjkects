using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.ObjectVisible
{
    public class ObjectVisibleToolsInGame : MonoBehaviour
    {
        [SerializeField] private float speedAlphaPanel = 2f;
        [SerializeField] private float speedWidthPanel = 200f;

        [SerializeField] private RectTransform mainPanel;
        [SerializeField] private CanvasGroup canvasGroupPanel;
        [SerializeField] private Transform context;
        
        [SerializeField] private Button openCloseButton;
        [SerializeField] private Toggle checkAllButton;
        [SerializeField] private Toggle visibleAllButton;
        [SerializeField] private Slider alphaSlider;

        private float widthPanel;

        public Transform Context => context;

        public void Initialize(IObjectVisibleController controller)
        {
            widthPanel = mainPanel.sizeDelta.x;
            StartCoroutine(ClosePanel());

            openCloseButton.onClick.AddListener(OnActivatePanel);
            checkAllButton.onValueChanged.AddListener(isOn => controller.SetSelected(isOn));
            visibleAllButton.onValueChanged.AddListener(isOn => controller.SetVisible(isOn));
            alphaSlider.onValueChanged.AddListener(alpha => controller.SetAlpha(alpha));
        }

        public void SetViewSelected(bool isSelected)
        {
            checkAllButton.isOn = isSelected;
        }

        public void SetViewVisible(bool isVisible)
        {
            visibleAllButton.isOn = isVisible;
        }

        public void OnActivatePanel()
        {
            if (mainPanel.gameObject.activeSelf)
                StartCoroutine(ClosePanel());
            else
                StartCoroutine(OpenPanel());
        }

        private IEnumerator OpenPanel()
        {
            openCloseButton.interactable = false;
            mainPanel.gameObject.SetActive(true);

            while (mainPanel.rect.width < widthPanel)
            {
                yield return null;
                Vector2 size = mainPanel.sizeDelta;
                size.x += speedWidthPanel * Time.deltaTime;
                mainPanel.sizeDelta = size;
            }

            while (canvasGroupPanel.alpha < 1f)
            {
                yield return null;
                canvasGroupPanel.alpha += speedAlphaPanel * Time.deltaTime;
            }
            canvasGroupPanel.alpha = 1f;

            canvasGroupPanel.interactable = true;
            openCloseButton.interactable = true;
        }

        private IEnumerator ClosePanel()
        {
            canvasGroupPanel.interactable = false;
            openCloseButton.interactable = false;

            while (canvasGroupPanel.alpha > 0f)
            {
                yield return null;
                canvasGroupPanel.alpha -= speedAlphaPanel * Time.deltaTime;
            }
            canvasGroupPanel.alpha = 0f;

            while (mainPanel.rect.width > 0f)
            {
                yield return null;
                Vector2 size = mainPanel.sizeDelta;
                size.x -= speedWidthPanel * Time.deltaTime;
                mainPanel.sizeDelta = size;
            }

            mainPanel.gameObject.SetActive(false);
            openCloseButton.interactable = true;
        }
    }
}