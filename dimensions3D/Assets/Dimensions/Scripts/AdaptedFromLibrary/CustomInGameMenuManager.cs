using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    /*
     * simplified menu manager from microgame 
     */
    public class CustomInGameMenuManager : MonoBehaviour
    {
        [Tooltip("Root GameObject of the menu used to toggle its activation")]
        public GameObject MenuRoot;

        [Tooltip("Master volume when menu is open")] [Range(0.001f, 1f)]
        public float VolumeWhenMenuOpen = 0.5f;

        void Start()
        {
            MenuRoot.SetActive(false);
        }

        void Update()
        {
            // Lock cursor when clicking outside of menu
            if (!MenuRoot.activeSelf && Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            if (Input.GetButtonDown(GameConstants.k_ButtonNamePauseMenu))
            {
                SetPauseMenuActivation(!MenuRoot.activeSelf);

            }

            if (Input.GetAxisRaw(GameConstants.k_AxisNameVertical) != 0)
            {
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                }
            }
        }

        public void ClosePauseMenu()
        {
            SetPauseMenuActivation(false);
        }

        void SetPauseMenuActivation(bool active)
        {
            MenuRoot.SetActive(active);

            if (MenuRoot.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //Stop the game
                Time.timeScale = 0f;
                //Dim the volume
                AudioUtility.SetMasterVolume(VolumeWhenMenuOpen);
                EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                // lock cursor to the center
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                // resume the game
                Time.timeScale = 1f;
                AudioUtility.SetMasterVolume(1);
            }

        }
    }
}