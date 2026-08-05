using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketInteraction : MonoBehaviour
{

    public Renderer sphereRenderer;
    public XRSocketInteractor socket;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {



    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<XRGrabInteractable>() != null)
        {

            sphereRenderer.material.color = Color.blue;

        }

    }


    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.GetComponent<XRGrabInteractable>() != null)
        {

            sphereRenderer.material.color = Color.white;

        }

    }


    public void CheckObject(SelectEnterEventArgs args)
    {

        GameObject obj = args.interactableObject.transform.gameObject;

        if (obj.CompareTag("Box"))
            socket.interactionManager.SelectExit(socket, args.interactableObject);
        else
            return;

        obj.GetComponent<BoxInteraction>().StartAnimation();

    }

}
