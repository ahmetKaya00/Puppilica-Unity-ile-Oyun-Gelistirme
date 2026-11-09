using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras;

    void Start()
    {
        if (cameras != null && cameras.Length > 0)
        {
            SwitchCamera(0);
        }
    }

    void Update()
    {
        if(Keyboard.current == null) return;

        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SwitchCamera(0);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            SwitchCamera(1);
        }
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            SwitchCamera(2);
        }
        else if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            SwitchCamera(3);
        }
    }
    void SwitchCamera(int index)
    {
        if (cameras == null || index < 0 || index >= cameras.Length) return;

        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == index);
        }
    }
}
