using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI enemiesLeftText;
    [SerializeField] GameObject youWinText;

    int enemiesLeft = 0;

    const string ENEMIES_LEFT_STRING = "Enemies Left: ";

    public void RestartLevelButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void AdjustEnemyLeft(int amount)
    {
        enemiesLeft += amount;
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();

        if(enemiesLeft <= 0)
        {
            youWinText.SetActive(true);
            StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
            starterAssetsInputs.SetCursorState(false);
            starterAssetsInputs.cursorInputForLook = false;

            CharacterController playerInput = starterAssetsInputs.GetComponent<CharacterController>();
            playerInput.enabled = false;

            ActiveWeapon activeWeapon = starterAssetsInputs.GetComponentInChildren<ActiveWeapon>();
            activeWeapon.AdjustAmmo(-1000);
        }
    }
}
