using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTime : MonoBehaviour
{
    [SerializeField] private Gradient directionalLightGradient;
    [SerializeField] private Gradient ambientLightGradient;
    [SerializeField, Range(1,3600)] private float timeDayInSeconds = 60;
    [SerializeField, Range(0f,1f)] private float timeProgress;
    [SerializeField] private Light dirLight;
    private Vector3 _defaultAngles;
    void Start() => _defaultAngles = dirLight.transform.localEulerAngles;
    
    void Update()
    {
        timeProgress += Time.deltaTime/timeDayInSeconds;
        if (timeProgress > 1f) timeProgress = 0f;
        dirLight.color = directionalLightGradient.Evaluate(timeProgress);
        RenderSettings.ambientLight = ambientLightGradient.Evaluate(timeProgress);
        dirLight.transform.localEulerAngles = new Vector3(360f * timeProgress - 90,
            _defaultAngles.x, _defaultAngles.z);
    }

    public float DayProgress()
    {
        return timeProgress;
    }
}
