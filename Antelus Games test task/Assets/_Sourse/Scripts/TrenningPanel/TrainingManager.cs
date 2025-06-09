using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    public static TrainingManager Instance { get; private set; }
    [SerializeField] private PlayerController playerController;

    [SerializeField] private GameObject trainingPanel;
    private bool isTrainingActive = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        trainingPanel.SetActive(false);
    }

    private void Update()
    {
        if (isTrainingActive && Input.GetKeyDown(KeyCode.Escape))
        {
            EndTraining();
        }
    }

    public void StartTraining()
    {
        Time.timeScale = 0f; // Ставим игру на паузу
        Debug.Log("Game paused");
        trainingPanel.SetActive(true);
        isTrainingActive = true;
        playerController.SetMovementEnabled(false);
        // Можно отключить управление игроком, если нужно
    }

    public void EndTraining()
    {
        trainingPanel.SetActive(false);
        Time.timeScale = 1f; // Возобновляем игру
        Debug.Log("Game continue");
        isTrainingActive = false;
        playerController.SetMovementEnabled(true);
        // Можно вернуть управление игроку
    }
}