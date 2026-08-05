using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class BoxInteraction : MonoBehaviour
{

    private Vector3 startPosition, returnFromPosition;
    private Quaternion startRotation, returnFromRotation;
    public XRGrabInteractable grab;
    public XRGeneralGrabTransformer transformer;
    public Rigidbody rb;
    public Animator animator;
    private const int twoHanded = 2;
    private const string flyAnimation = "Box_Flying", stopFlyAnimation = "exitFly", playFlyAnimation = "PlayFlyAnimation";
    private float returnDuration = 3, returnTime = 0;
    private bool returning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        startPosition = gameObject.transform.position;
        startRotation = gameObject.transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {

        if (grab.interactorsSelecting.Count != 0)
            DisableReturn();

        transformer.enabled = grab.interactorsSelecting.Count == twoHanded;

        if (returning)
            ReturnToStart();

    }


    private void ReturnToStart()
    {

        returnTime += Time.deltaTime;

        if (returnTime >= returnDuration)
        {

            returning = false;
            rb.useGravity = true;
            rb.isKinematic = false;
            gameObject.transform.position = startPosition;
            gameObject.transform.rotation = startRotation;
            animator.SetBool(stopFlyAnimation, true);
            animator.Play(flyAnimation, 0, 0f);

        }
        else
        {

            gameObject.transform.position = Vector3.Lerp(returnFromPosition, startPosition, returnTime / returnDuration);
            gameObject.transform.rotation = Quaternion.Lerp(returnFromRotation, startRotation, returnTime / returnDuration);

        }

    }


    public void InitiateReturn()
    {

        returning = true;
        returnTime = 0;
        returnFromPosition = gameObject.transform.position;
        returnFromRotation = gameObject.transform.rotation;
        rb.isKinematic = true;

    }


    private void DisableReturn()
    {

        if (!returning)
            return;

        returning = false;
        rb.useGravity = true;
        rb.isKinematic = false;
        animator.ResetTrigger(playFlyAnimation);
        animator.SetBool(stopFlyAnimation, true);

    }


    public void StartAnimation()
    {

        animator.SetBool(stopFlyAnimation, false);
        animator.ResetTrigger(playFlyAnimation);
        animator.SetTrigger(playFlyAnimation);
        animator.Play(flyAnimation, 0, 0f);

    }

}
