using UnityEngine;
using TMPro;

public class FlightHUD : MonoBehaviour
{
    [SerializeField]
    private TMP_Text flightDataText;

    [SerializeField]
    private Aircraft aircraft;

    private void Update()
    {
        flightDataText.text = "SPEED: "+ aircraft.GetSpeed().ToString("F1")+"m/s\n"
        +"THROTTLE: "+(aircraft.GetThrottle()*100f).ToString("F0")+"\n"
        +"ALTITUDE: "+aircraft.GetAltitude().ToString("F1")+" m"+"\n"
        +"AOA: "+aircraft.GetAoA().ToString("F1")+"°";
    }
}
