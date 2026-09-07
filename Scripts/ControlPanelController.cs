using UnityEngine;
using UnityEngine.InputSystem;

public class ControlPanelController : MonoBehaviour
{
    public GameObject controlPanel;

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.tabKey.wasPressedThisFrame)
        {
            if (controlPanel != null)
            {
                controlPanel.SetActive(!controlPanel.activeSelf);
            }
        }
    }
}