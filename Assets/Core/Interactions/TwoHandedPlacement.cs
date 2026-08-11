using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TwoHandedPlacement : XRGrabInteractable
{

    [SerializeField] private Transform leftHandTransform, rightHandTransform, bottomTransform;

    /// <summary>
    /// Modified to take several non-dynamic attach transforms into account
    /// </summary>
    /// <param name="args"></param>
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {

        if (!leftHandTransform || !rightHandTransform || !bottomTransform)
        {

            Debug.LogWarning("One or more transforms not found/assigned for " + gameObject.name);

            return;

        }

        if (args.interactorObject.handedness == InteractorHandedness.Left)
            attachTransform = leftHandTransform;
        else if (args.interactorObject.handedness == InteractorHandedness.Right)
            attachTransform = rightHandTransform;
        else
            attachTransform = bottomTransform;

        base.OnSelectEntering(args);

    }

}
