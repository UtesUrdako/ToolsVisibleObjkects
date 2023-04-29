using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.ObjectVisible
{
    public class InputManager : MonoBehaviour
    {
        private Camera camera;

        private void Awake()
        {
            camera = Camera.main;
        }

        void Update()
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out IGameObjectView inGameView))
                {

                }
            }
        }
    }
}