using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SocketInteraction : MonoBehaviour
{

    public Renderer sphereRenderer;

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

}
