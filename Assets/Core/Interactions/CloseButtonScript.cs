using UnityEngine;
using UnityEditor;

public class CloseButtonScript : MonoBehaviour
{
    
    public void CloseApp() => EditorApplication.isPlaying = false;

}
