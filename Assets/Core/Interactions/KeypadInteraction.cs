using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeypadInteraction : MonoBehaviour
{

    [SerializeField] private TMP_Text text;
    [SerializeField] private BoxInteraction box;
    private Coroutine coroutine;
    private Color originalColor;
    private const int maxDigits = 4;
    private List<int> digits = new List<int>(maxDigits);
    private string currentInput = string.Empty;

    // Start is called once before the first execution of Update after the MonoBehaviour is created, used for debugging and initializing
    void Start()
    {

        if (!text)
            Debug.LogWarning("No TextMesh found/assigned for " + gameObject.name);
        else
            originalColor = text.color;

        if (!box)
            Debug.LogWarning("No BoxIntereaction found/assigned for " + gameObject.name);

        SetText();

    }

    /// <summary>
    /// Adds a digit to the "digits" list, if less than 4
    /// </summary>
    /// <param name="value"></param>
    public void AddDigit(int value)
    {

        if (digits.Count >= maxDigits)
            return;

        digits.Add(value);

        SetText();

    }

    /// <summary>
    /// Removes latest entry to "digits" list
    /// </summary>
    public void RemoveLastDigit()
    {

        if (digits.Count == 0)
            return;

        digits.RemoveAt(digits.Count - 1);

        SetText();

    }

    /// <summary>
    /// Sets current digits displayed according to cache
    /// </summary>
    private void SetText()
    {

        currentInput = string.Join(" ", digits);

        for (int i = digits.Count; i < maxDigits; i++)
            currentInput += i != 0 ? " _" : "_";

        if (text)
            text.text = currentInput;

    }

    /// <summary>
    /// Tries to check if the code was valid for disabling return animation, text changes color to green on success, red on failure
    /// </summary>
    public void EnterCode()
    {

        if (digits.Count < maxDigits || !box)
            return;

        if (text && box)
            text.color = !box.AttemptDisarm(string.Join("", digits)) ? Color.green : Color.red;

        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(ChangeColorBackCoroutine());

    }

    /// <summary>
    /// Coroutine function to change color back after a predetermined amount of time
    /// </summary>
    /// <param name="time">Time before changing color back</param>
    /// <returns>Coroutine "wait for seconds"</returns>
    private IEnumerator ChangeColorBackCoroutine(float time = 1f)
    {

        yield return new WaitForSeconds(time);

        if (text)
            text.color = originalColor;

        coroutine = null;

    }

}
