using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private string flightSceneName = "AircraftSelection";

    public void startFlight()
    {
        SceneManager.LoadScene(flightSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
