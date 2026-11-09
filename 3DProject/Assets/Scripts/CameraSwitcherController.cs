using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcherController : MonoBehaviour
{
    [SerializeField] private List<Camera> cameras;

    void Start()
    {
        ActiveCameras(0);
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        int maxCheck = Mathf.Min(cameras.Count, 9);

        for (int i = 0; i < maxCheck; i++)
        {
            Key targetKey = Key.Digit1 + i;

            if (Keyboard.current[targetKey].wasPressedThisFrame)
            {
                ActiveCameras(i);
                break;
            }

        }
    }
    private void ActiveCameras(int index)
    {
        if (index < 0 || index >= cameras.Count) return;

        for (int i = 0; i < cameras.Count; i++)
        {

            if(cameras[i] != null)
            {
                cameras[i].gameObject.SetActive(i == index);
            }
        }
    }
}
