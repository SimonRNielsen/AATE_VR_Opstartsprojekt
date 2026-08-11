using UnityEngine;
using UnityEngine.Localization.Settings;

public class MainRoomScript : MonoBehaviour
{

    [SerializeField] private Canvas first, second, third;
    private bool initialized = false, running = false;
    private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created, used for debugging and initializing
    void Start()
    {

        if (!first || !second || !third)
            Debug.LogWarning("One or more canvases were not assigned");

        if (Camera.main)
            cam = Camera.main;
        else
            Debug.LogWarning("No main camera found for use in " + gameObject.name);

    }

    // Update is called once per frame, used to rotate canvas' towards player
    void Update()
    {

        if (!running || !first || !second || !third || !cam)
            return;

        Vector3 direction = first.transform.position - cam.transform.position;
        direction.y = 0f;

        Quaternion rotation = Quaternion.LookRotation(direction);

        first.transform.rotation = rotation;
        second.transform.rotation = rotation;
        third.transform.rotation = rotation;

    }

    /// <summary>
    /// Detects if player enters trigger area, and displays canvas' if so
    /// </summary>
    /// <param name="other">Colliding trigger</param>
    private void OnTriggerEnter(Collider other)
    {

        if (initialized || !other.gameObject.CompareTag("Player"))
            return;

        initialized = true;
        running = true;

        ShowCanvas();

    }

    /// <summary>
    /// Ensures Localization is initialized
    /// </summary>
    private async void OnEnable()
    {

        await LocalizationSettings.InitializationOperation.Task;

    }

    /// <summary>
    /// Used to change localization dynamically
    /// </summary>
    /// <param name="language">Abbreviation for language to change to</param>
    public void SetLanguage(string language) => LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(language);

    /// <summary>
    /// Function to transition between canvas'
    /// </summary>
    /// <param name="index">Canvas to display (1-3)</param>
    public void ShowCanvas(int index = 1)
    {

        if (!first || !second || !third)
            return;

        switch (index)
        {
            default:
            case 1:
                SetCanvasVisibility(first, true);
                SetCanvasVisibility(second, false);
                break;
            case 2:
                SetCanvasVisibility(first, false);
                SetCanvasVisibility(second, true);
                SetCanvasVisibility(third, false);
                break;
            case 3:
                SetCanvasVisibility(second, false);
                SetCanvasVisibility(third, true);
                break;
        }

    }

    /// <summary>
    /// Changes alpha value and interactability of a canvas
    /// </summary>
    /// <param name="canvas">Canvas to modify</param>
    /// <param name="visible">True for visible, else false</param>
    private void SetCanvasVisibility(Canvas canvas, bool visible)
    {

        CanvasGroup group = canvas.GetComponent<CanvasGroup>();

        if (!group)
            return;

        group.alpha = visible ? 1f : 0f;
        group.interactable = visible;
        group.blocksRaycasts = visible;

    }

    /// <summary>
    /// Function to "close" the final menu
    /// </summary>
    public void CloseMenu()
    {

        SetCanvasVisibility(third, false);
        running = false;

    }

}
