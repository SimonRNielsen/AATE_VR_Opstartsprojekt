using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketInteraction : MonoBehaviour
{

    [SerializeField] private Renderer sphereRenderer;
    [SerializeField] private XRSocketInteractor socket;

    // Start is called once before the first execution of Update after the MonoBehaviour is created, used for debugging
    void Start()
    {

        if (!sphereRenderer)
            Debug.LogWarning("No renderer found/assigned for " + gameObject.name);

        if (!socket)
            Debug.LogWarning("No socket found/assigned for " + gameObject.name);

    }

    /// <summary>
    /// Changes color of material to blue if a collidee is Grabable
    /// </summary>
    /// <param name="other">Collidee</param>
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<XRGrabInteractable>() && sphereRenderer)
            sphereRenderer.material.color = Color.blue * 0.9f;

    }

    /// <summary>
    /// Changes color of material to white if a collidee is Grabable
    /// </summary>
    /// <param name="other">Collidee</param>
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.GetComponent<XRGrabInteractable>() && sphereRenderer)
            sphereRenderer.material.color = Color.white * 0.9f;

    }

    /// <summary>
    /// Checks if a deposited item is a "box" and if so run interaction logic on it
    /// </summary>
    /// <param name="args">Arguements given by deposited item</param>
    public void CheckObject(SelectEnterEventArgs args)
    {

        GameObject obj = args.interactableObject.transform.gameObject;

        if (obj.CompareTag("Box"))
        {

            BoxInteraction box = obj.GetComponent<BoxInteraction>();
            if (!box || !box.IsArmed || !socket)
                return;

            socket.interactionManager.SelectExit(socket, args.interactableObject);
            box.StartAnimation();

        }

    }

}
