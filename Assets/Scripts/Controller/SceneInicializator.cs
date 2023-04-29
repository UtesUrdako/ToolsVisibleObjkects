using System;
using Tools.ObjectVisible;
using UnityEngine;

public class SceneInicializator : MonoBehaviour
{
    [SerializeField] private ObjectToolsSlot slotPrefab;
    [SerializeField] private ObjectVisibleToolsInGame toolsInGame;

    private void Awake()
    {
        IObjectVisibleController controller = new ObjectVisibleController();
        toolsInGame.Initialize(controller);
        var renderers = FindObjectsOfType<Renderer>();

        foreach (var renderer in renderers)
        {
            Guid id = Guid.NewGuid();

            IGameObjectView gameObjectView;
            if (!renderer.gameObject.TryGetComponent(out GameObjectInGameView view))
                gameObjectView = renderer.gameObject.AddComponent<GameObjectInGameView>();
            else
                gameObjectView = view;
            ObjectToolsSlot slot = Instantiate(slotPrefab, toolsInGame.Context);
            ISlotModel slotModel = new GameObjectSlotModel(id, gameObjectView.Name);

            gameObjectView.Initialize(slotModel);
            slot.Initialize(id, slotModel, controller);
            controller.AddModel(slotModel);
        }
    }
}
