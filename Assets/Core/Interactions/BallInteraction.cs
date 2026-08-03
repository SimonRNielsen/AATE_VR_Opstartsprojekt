using UnityEngine;

public class BallInteraction : MonoBehaviour
{

    public Renderer meshRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        

    }

    // Update is called once per frame
    void Update()
    {
        


    }


    public void OnSelectEnter()
    {

        if (meshRenderer == null)
            return;

        meshRenderer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

    }

}
