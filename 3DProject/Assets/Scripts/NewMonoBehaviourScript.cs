using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;

    void Start()
    {
        camera1.enabled = false;
        camera2.enabled = true;
        camera3.enabled = false;
    }

    void Update()
    {
        // Klavyenin bağlı olup olmadığını kontrol ediyoruz
        if (Keyboard.current == null) return;

        // 1 Tuşu
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            camera1.enabled = true;
            camera2.enabled = false;
            camera3.enabled = false;
        }
        // 2 Tuşu
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            camera1.enabled = false;
            camera2.enabled = true;
            camera3.enabled = false;
        }
        // 3 Tuşu
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            camera1.enabled = false;
            camera2.enabled = false;
            camera3.enabled = true;
        }
    }
}