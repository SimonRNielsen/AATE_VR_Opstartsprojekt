using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{

    [SerializeField] private InputActionProperty triggerValue;
    [SerializeField] private InputActionProperty gripValue;
    [SerializeField] private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created, used for debugging
    void Start()
    {

        if (!animator)
            Debug.LogWarning("No animator found/assigned for " + gameObject.name);

    }

    // Update is called once per frame, used for animation parameters/blend values
    void Update()
    {

        if (!animator)
            return;

        animator.SetFloat("Trigger", triggerValue.action.ReadValue<float>());
        animator.SetFloat("Grip", gripValue.action.ReadValue<float>());

    }
}
