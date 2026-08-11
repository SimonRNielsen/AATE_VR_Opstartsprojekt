using UnityEngine;

public class BallInteraction : MonoBehaviour
{

    [SerializeField] private Renderer meshRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created, used for debugging
    void Start()
    {

        if (!meshRenderer)
            Debug.LogWarning("No renderer assigned/found for " + gameObject.name);

    }

    /// <summary>
    /// Changes color of the renderers material to random on all RGB-scales
    /// </summary>
    public void OnSelectEnter()
    {

        if (!meshRenderer)
            return;

        meshRenderer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

    }

}
