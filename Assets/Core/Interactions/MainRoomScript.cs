using UnityEngine;
using UnityEngine.Localization.Settings;

public class MainRoomScript : MonoBehaviour
{

    private bool initialized = false, running = false;
    [SerializeField]
    private Canvas first, second, third;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

        if (!running)
            return;

        Vector3 direction = first.transform.position - Camera.main.transform.position;
        direction.y = 0f;

        Quaternion rotation = Quaternion.LookRotation(direction);

        first.transform.rotation = rotation;
        second.transform.rotation = rotation;
        third.transform.rotation = rotation;

    }


    private void OnTriggerEnter(Collider other)
    {

        if (initialized || !other.gameObject.CompareTag("Player"))
            return;

        initialized = true;
        running = true;

        ShowCanvas();

    }


    private async void OnEnable()
    {

        await LocalizationSettings.InitializationOperation.Task;

    }


    public void SetLanguage(string language) => LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(language);


    public void ShowCanvas(int index = 1)
    {

        if (first == null || second == null || third == null)
        {

            Debug.LogWarning("One or more canvases were not assigned");
            return;

        }

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


    private void SetCanvasVisibility(Canvas canvas, bool visible)
    {

        CanvasGroup group = canvas.GetComponent<CanvasGroup>();

        if (group == null)
            return;

        group.alpha = visible ? 1f : 0f;
        group.interactable = visible;
        group.blocksRaycasts = visible;

    }


    public void CloseMenu()
    {

        SetCanvasVisibility(third, false);
        running = false;

    }

}
