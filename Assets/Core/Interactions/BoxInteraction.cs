using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class BoxInteraction : MonoBehaviour
{

    public Transform leftHandAttach;
    public Transform rightHandAttach;
    public XRGrabInteractable grab;
    public XRGeneralGrabTransformer transformer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (grab.interactorsSelecting.Count < 2)
            transformer.enabled = false;
        else
            transformer.enabled = true;

    }


    private void OnEnable()
    {

        grab.selectEntered.AddListener(OnSelectEnter);

    }

    private void OnDisable()
    {

        grab.selectEntered.RemoveListener(OnSelectEnter);

    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {

        if (args.interactorObject.transform.name.Contains("Left"))
            grab.attachTransform = leftHandAttach;
        else
            grab.attachTransform = rightHandAttach;

    }

}
