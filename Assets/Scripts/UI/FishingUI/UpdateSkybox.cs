using Managers.Time;
using TMPro;
using UnityEngine;

public class UpdateSkybox : MonoBehaviour
{
    [SerializeField]
    private Light _sun;

    [SerializeField]
    private float _range = 180f;
    [SerializeField]
    private float _startingAngle = 4f;

    [SerializeField]
    private Gradient _sunColourGradient;

    [SerializeField]
    private TimeManager _Timer;
    [SerializeField] 
    private TextMeshProUGUI timeText;

    public void Awake()
    {
        RotateX(_startingAngle);
        SetColour(0f);
    }

    public void SetSkyboxTime()
    {
        float completion = _Timer.CurrentTime / _Timer.EndTime;

        RotateX(_startingAngle + completion * _range);
        SetColour(completion);


    }

    private void RotateX(float angle)
    {
        _sun.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0.5f, 0.2f, 0.5f));
    }

    private void SetColour(float sampleTime)
    {
        _sun.color = _sunColourGradient.Evaluate(sampleTime);
    }
}
