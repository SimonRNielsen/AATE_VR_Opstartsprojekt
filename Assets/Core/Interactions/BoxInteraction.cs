using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class BoxInteraction : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private TMP_Text text;
    [SerializeField] private XRGeneralGrabTransformer transformer;
    [SerializeField] private XRGrabInteractable grab;
    private Quaternion startRotation, returnFromRotation;
    private Vector3 startPosition, returnFromPosition;
    private const float returnDuration = 3f;
    private const int twoHanded = 2, passwordLength = 4;
    private const string flyAnimation = "Box_Flying", stopFlyAnimation = "exitFly", playFlyAnimation = "PlayFlyAnimation";
    private bool returning = false, armed = true;
    private float returnTime = 0;
    private string password = string.Empty;

    /// <summary>
    /// Gets value for "armed"
    /// </summary>
    public bool IsArmed { get => armed; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created, used for debugging and initialization
    void Start()
    {

        for (int i = 0; i < passwordLength; i++)
            password += Random.Range(0, 10).ToString();

        if (!animator)
            Debug.LogWarning("No Animator found/assigned for " + gameObject.name);

        if (!rb)
            Debug.LogWarning("No Rigidbody found/assigned for " + gameObject.name);

        if (!text)
            Debug.LogWarning("No TextMesh found/assigned for " + gameObject.name);
        else
            text.text = password;

        if (!transformer)
            Debug.LogWarning("No Grab Transformer found/assigned for " + gameObject.name);

        if (!grab)
            Debug.LogWarning("No Grab Interactable found/assigned for " + gameObject.name);

        startPosition = gameObject.transform.position;
        startRotation = gameObject.transform.rotation;
        
    }

    // Update is called once per frame, used for checking interactability and animation
    void Update()
    {

        if (grab && grab.interactorsSelecting.Count != 0)
            DisableReturn();

        if (transformer && grab)
            transformer.enabled = grab.interactorsSelecting.Count == twoHanded;

        if (returning)
            ReturnToStart();

    }

    /// <summary>
    /// Makes the gameobject do a lerp-animation (concurrently with a animation on the object)
    /// </summary>
    private void ReturnToStart()
    {

        returnTime += Time.deltaTime;

        if (returnTime >= returnDuration)
        {

            returning = false;
            gameObject.transform.position = startPosition;
            gameObject.transform.rotation = startRotation;

            if (rb)
            {

                rb.useGravity = true;
                rb.isKinematic = false;

            }

            if (animator)
            {

                animator.SetBool(stopFlyAnimation, true);
                animator.Play(flyAnimation, 0, 0f);

            }

        }
        else
        {

            gameObject.transform.position = Vector3.Lerp(returnFromPosition, startPosition, returnTime / returnDuration);
            gameObject.transform.rotation = Quaternion.Lerp(returnFromRotation, startRotation, returnTime / returnDuration);

        }

    }

    /// <summary>
    /// Run by animation event to signal return animation has begun
    /// </summary>
    public void InitiateReturn()
    {

        returning = true;
        returnTime = 0;
        returnFromPosition = gameObject.transform.position;
        returnFromRotation = gameObject.transform.rotation;
        if (rb)
            rb.isKinematic = true;

    }

    /// <summary>
    /// Used in case return to origin animation has to be stopped during animation
    /// </summary>
    private void DisableReturn()
    {

        if (!returning)
            return;

        returning = false;

        if (!rb || !animator)
            return;

        rb.useGravity = true;
        rb.isKinematic = false;
        animator.ResetTrigger(playFlyAnimation);
        animator.SetBool(stopFlyAnimation, true);

    }

    /// <summary>
    /// Triggers animation, which initializes further logic upon state change
    /// </summary>
    public void StartAnimation()
    {

        if (!armed || !animator)
            return;

        animator.SetBool(stopFlyAnimation, false);
        animator.ResetTrigger(playFlyAnimation);
        animator.SetTrigger(playFlyAnimation);
        animator.Play(flyAnimation, 0, 0f);

    }

    /// <summary>
    /// Compares a given string to the password, and sets armed accordingly
    /// </summary>
    /// <param name="input">String for comparison</param>
    /// <returns>false if strings are identical, else true</returns>
    public bool AttemptDisarm(string input) => armed = input != password;

}
