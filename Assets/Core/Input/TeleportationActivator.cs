using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TeleportationActivator : MonoBehaviour
{

    [SerializeField] private XRRayInteractor teleportInteractor;
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private InputActionReference teleportActivatorAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created, used for debugging and initialization
    void Start()
    {

        if (!teleportInteractor)
            Debug.LogWarning("No teleport interactor (RayInteractor) set/assigned for " + gameObject.name);
        else
            teleportInteractor.gameObject.SetActive(false);

        if (!rayInteractor)
            Debug.LogWarning("No RayInteractor set/assigned for " + gameObject.name);
        else
            rayInteractor.uiHoverEntered.AddListener(x => DisableTeleport());

        if (!teleportActivatorAction)
            Debug.LogWarning("No InputActionReference for teleporting set/assigned for " + gameObject.name);

    }


    // Update is called once per frame, used for disabling teleportation when not in use
    void Update()
    {

        if (teleportActivatorAction && teleportActivatorAction.action.WasReleasedThisFrame())
            DisableTeleport();

    }

    /// <summary>
    /// Subscribes to action for reenabling teleportation
    /// </summary>
    private void OnEnable()
    {

        if (teleportActivatorAction)
            teleportActivatorAction.action.performed += Action_performed;

    }

    /// <summary>
    /// Unsubscribes to action for reenabling teleportation
    /// </summary>
    private void OnDisable()
    {
        
        if (teleportActivatorAction)
            teleportActivatorAction.action.performed -= Action_performed;

    }

    /// <summary>
    /// Enables teleport-interaction
    /// </summary>
    /// <param name="obj">Action performed</param>
    private void Action_performed(InputAction.CallbackContext obj)
    {

        if (rayInteractor && rayInteractor.IsOverUIGameObject())
            return;

        if (teleportInteractor)
            teleportInteractor.gameObject.SetActive(true);

    }

    /// <summary>
    /// Disables Teleportation
    /// </summary>
    private void DisableTeleport()
    {

        if (teleportInteractor)
            teleportInteractor.gameObject.SetActive(false);

    }

}
