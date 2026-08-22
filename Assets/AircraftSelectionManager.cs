using UnityEngine;
using UnityEngine.SceneManagement;

public class AircraftSelectionMaganer : MonoBehaviour
{
    [SerializeField]
    private string flightSceneName = "FlightTest";

    [SerializeField] private Transform aircraftContainer;
    [SerializeField] private float offsetDistance = 15f;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private int totalAircraft = 2;

    private int currentIndex = 0;
    private Vector3 targetPosition;

    private void Start()
    {
        currentIndex = PlayerPrefs.GetInt("SelectedAircraftIndex", 0);

        if (aircraftContainer != null)
        {
            targetPosition = new Vector3(-currentIndex*offsetDistance, aircraftContainer.position.y, aircraftContainer.position.z);
            aircraftContainer.position = targetPosition;
        }
    }

    private void Update()
    {
        if (aircraftContainer != null)
        {
            aircraftContainer.position = Vector3.Lerp(aircraftContainer.position, targetPosition, Time.deltaTime);
        }
    }

    public void NextAircraft()
    {
        if (currentIndex < totalAircraft -1)
        {
            currentIndex++;
            UpdateTargetposition();
        }
    }

    public void PreviousAircraft()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateTargetposition();
        }
    }

    private void UpdateTargetposition()
    {
        targetPosition = new Vector3(-currentIndex*offsetDistance, aircraftContainer.position.y, aircraftContainer.position.z);

        PlayerPrefs.SetInt("SelectedAircraftIndex", currentIndex);
        PlayerPrefs.Save();
    }

    public void startFlight()
    {
        SceneManager.LoadScene(flightSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
