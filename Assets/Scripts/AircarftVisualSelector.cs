using UnityEngine;

public class AircarftVisualSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] aircraftModels;

    void Awake()
    {
        int selectedindex = PlayerPrefs.GetInt("SelectedAircarftIndex", 0);

        for (int i = 0; i< aircraftModels.Length; i++)
        {
            if (aircraftModels[i] != null)
                aircraftModels[i].SetActive(i==selectedindex);
        }
    }
}
