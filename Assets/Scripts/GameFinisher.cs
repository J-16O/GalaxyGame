using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class GameFinisher : MonoBehaviour
{
    [SerializeField] private Button finishButton;

    void Start()
    {
        finishButton.onClick.AddListener(OnFinishClicked);
    }

    void OnFinishClicked()
    {
        Debug.Log("Finish button clicked!");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}