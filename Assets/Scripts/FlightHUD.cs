using UnityEngine;
using TMPro;

public class FlightHUD : MonoBehaviour
{
    [SerializeField]
    private TMP_Text flightDataText;

    [SerializeField]
    private Aircraft aircraft;

    [Header("Configuración de Stall")]
    [SerializeField] private float stallAoAThreshold = 15f;
    [SerializeField] private float minStallSpeed = 15f;
    [SerializeField] private TMP_Text stallWarningText;
    [SerializeField] private GameObject stallWarningPanel;
    [SerializeField] private float flashSpeed = 10f;


    private void Update()
    {
        if (aircraft == null) return;

        float currentAoA = aircraft.GetCurrentAoA();
        float currentSpeed = aircraft.GetSpeed();

        flightDataText.text = "SPEED: "+ currentSpeed.ToString("F1")+"m/s\n"
        +"THROTTLE: "+(aircraft.GetThrottle()*100f).ToString("F0")+"\n"
        +"ALTITUDE: "+aircraft.GetAltitude().ToString("F1")+" m"+"\n"
        +"AOA: "+currentAoA.ToString("F1")+"°"+"\n"
        + "FUEL: "+(aircraft.GetFuelPercent()*100f).ToString("F0")+"%";

        bool isStalling = Mathf.Abs(currentAoA) >= stallAoAThreshold || 
                         (currentSpeed < minStallSpeed && aircraft.GetAltitude() > 2f);
                    
        UpdateStallVisuals(isStalling);
    }

    private void UpdateStallVisuals(bool isStalling)
    {
        if (stallWarningPanel != null)
        {
            stallWarningPanel.SetActive(isStalling);
        }

        if (stallWarningText != null)
        {
          if(isStalling)
            {
               stallWarningText.gameObject.SetActive(true);
                // PARPADEO
                float alpha = Mathf.Abs(Mathf.Sin(Time.time * flashSpeed));
                stallWarningText.color = new Color(1f, 0.1f, 0.1f, alpha);
                stallWarningText.text = "¡ STALL / PÉRDIDA !"; 
            }
            else
            {
                stallWarningText.gameObject.SetActive(false);
            }
        }
    }
}
