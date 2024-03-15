using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private int score;
    public Text textScore;
    public void AddScore()
    {
        score++;
        textScore.text = score.ToString();
    }
}
