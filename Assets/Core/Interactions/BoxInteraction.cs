using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class BoxInteraction : MonoBehaviour
{

    public XRGrabInteractable grab;
    public XRGeneralGrabTransformer transformer;
    private Transform startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        startPosition = transform;

    }

    // Update is called once per frame
    void Update()
    {

        if (grab.interactorsSelecting.Count < 2)
            transformer.enabled = false;
        else
            transformer.enabled = true;

    }

}
