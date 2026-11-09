using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    private int isScaleHash;
    private int isRotateHash;

    void Start()
    {
        animator = GetComponent<Animator>();

        isScaleHash = Animator.StringToHash("Scale");
        isRotateHash = Animator.StringToHash("Rotate");
    }

    private void Update()
    {
        if(Keyboard.current == null) return;

        bool isPressed = Keyboard.current.pKey.isPressed;

        bool isPressed2 = Keyboard.current.rKey.isPressed;

        animator.SetBool(isScaleHash, isPressed);
        animator.SetBool(isRotateHash, isPressed2);

    }
}
