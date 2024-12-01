using Managers.Time;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeBarManager : MonoBehaviour
{
    Vector2 originalPosition;
    RectTransform rect;
    [SerializeField] private int shakeIntensity;

    [SerializeField] private TimeManager timer;

    private float newValue;
    private float currentSpecialPoints;

    [SerializeField] private Gradient gradient;

    [SerializeField] private int numberOfIterations = 100;
    [SerializeField] private float UpdateSpeed = 1.0f;
    [SerializeField] private bool shake = false;

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        rect = GetComponent<RectTransform>();

        float startingValue = 0.0f;
        newValue = startingValue;
        currentSpecialPoints = startingValue;
        slider.value = startingValue;

        originalPosition = rect.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        newValue = timer.CurrentTime / timer.EndTime;
        if (newValue != currentSpecialPoints)
        {
            slider.fillRect.GetComponent<Image>().color = gradient.Evaluate(newValue);
            StartCoroutine(UpdateBar());
            currentSpecialPoints = newValue;
        }
    }

    private IEnumerator UpdateBar()
    {
        float iterationLength = UpdateSpeed / numberOfIterations;
        float delta = (newValue - currentSpecialPoints);
        float iterationDelta = (2 * delta) / (numberOfIterations * (numberOfIterations + 1));

        for (int i = numberOfIterations; i > 0; i--)
        {
            float currentIterationDelta = i * iterationDelta;

            slider.value += currentIterationDelta;
            if (shake)
            {
                BarShake(currentIterationDelta, i);
            }
            yield return new WaitForSeconds(iterationLength);
            rect.anchoredPosition = originalPosition;
        }
    }

    private void BarShake(float delta, int i)
    {
        float rand = Random.Range(-1, 1);
        float Shake = Mathf.Sin(i + rand);
        rect.anchoredPosition = new Vector2(originalPosition.x + Shake * (delta * i) * shakeIntensity, originalPosition.y + rand);
    }
}
