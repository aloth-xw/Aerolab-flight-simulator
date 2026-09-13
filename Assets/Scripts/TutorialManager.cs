using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialStep[] steps;
    [SerializeField] private TMP_Text tutorialText;
    [SerializeField] private TMP_Text continuePromptText;
    [SerializeField] private string defaultContinueMessage = "[Presiona ESPACIO para continuar]";
    [SerializeField] private Rigidbody aircraftRigidbody;
    [SerializeField] private FlightControls flightControls;

    private int currentStep = 0;
    private bool waitingForContinue = false;

    private void Start()
    {
        ShowStep(0);
    }

    private void ShowStep(int index)
    {
        if (index >= steps.Length)
        {
            EndTutorial();
            return;
        }

        currentStep = index;
        TutorialStep step =steps[index];
        tutorialText.text = step.message;
        tutorialText.gameObject.SetActive(true);

        if (step.freezeAircraft)
        {
            FreezeAircraft(true);
        }

        if (step.type == TutorialStepType.ShowMessage)
        {
            waitingForContinue = true;
            if (continuePromptText != null)
            {
                continuePromptText.text = defaultContinueMessage;
                continuePromptText.gameObject.SetActive(true);
            }
        }
        else
        {
            waitingForContinue = false;
            if (continuePromptText != null) continuePromptText.gameObject.SetActive(false);

            if (step.type == TutorialStepType.RequireCheckpoint)
            {
                FreezeAircraft(false);
            }
        }
    }

    private void Update()
    {
        if (waitingForContinue && UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            waitingForContinue = false;
            if (continuePromptText != null) continuePromptText.gameObject.SetActive(false);
            
            FreezeAircraft(false);
            ShowStep(currentStep + 1);
        }
    }

    private void FreezeAircraft(bool freeze)
    {
        if (aircraftRigidbody != null)
        {
            if (freeze)
            {
                aircraftRigidbody.linearVelocity = Vector3.zero;
                aircraftRigidbody.angularVelocity = Vector3.zero;
                aircraftRigidbody.isKinematic = true;
            }
            else
            {
                aircraftRigidbody.isKinematic = false;
                aircraftRigidbody.linearVelocity = aircraftRigidbody.transform.forward * 50f; // restaura velocidad de vuelo
            }
        }

        if (flightControls != null)
        {
            if (freeze) flightControls.ResetControllers();
            flightControls.enabled = !freeze;
            if (!freeze) flightControls.ResetControllers();
        }
    }

    public void OnCheckpointReached()
    {
        {
            if (currentStep < steps.Length && steps[currentStep].type == TutorialStepType.RequireCheckpoint)
            {
                ShowStep(currentStep + 1);
            }
        }
    }

    private void EndTutorial()
    {
        tutorialText.gameObject.SetActive(false);
        if (continuePromptText != null) continuePromptText.gameObject.SetActive(false);
        FreezeAircraft(false);
    }

}
