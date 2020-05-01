using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public float timeLeft = 60;
    private float minutes = float.MinValue;
    private float seconds = float.MinValue;
    public float roundedTime;
    public TMP_Text textBox;


    // Start is called before the first frame update
    void Start()
    {
        textBox.text = timeLeft.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        roundedTime = Mathf.Round(timeLeft);

        minutes = Mathf.Floor(roundedTime / 60);
        seconds = roundedTime % 60;

        textBox.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
