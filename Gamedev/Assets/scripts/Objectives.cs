using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Objectives : MonoBehaviour
{
    private Label dayNumberLabel;
    private Label moneyGoalLabel;
    private Label currentMoneyLabel;
    private Label warningMessageLabel;
    private Button continueButton;

    private int dayNumber = 1;
    private int moneyGoal = 40;
    private int currentMoney = 20;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        dayNumberLabel = root.Q<Label>("dayNumber");
        moneyGoalLabel = root.Q<Label>("moneyGoal");
        currentMoneyLabel = root.Q<Label>("currentMoney");
        warningMessageLabel = root.Q<Label>("warningMessage");
        continueButton = root.Q<Button>("continueButton");

        // Set UI text dynamically
        dayNumberLabel.text = $"Day {dayNumber} Objectives: ";
        moneyGoalLabel.text = moneyGoal.ToString() + " Coins";
        currentMoneyLabel.text = currentMoney.ToString() + " Coins";

        // Add button functionality with transition
        continueButton.clicked += OnContinueButtonClicked;
    }

    private void OnContinueButtonClicked()
    {
        StartCoroutine(TransitionToGameScene());
    }

    private IEnumerator TransitionToGameScene()
    {
        yield return new WaitForSeconds(0.5f); // Optional delay for smooth transition

        // This ensures that GameScene REPLACES ObjectivesScene
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
