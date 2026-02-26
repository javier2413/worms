using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;



    [Header("Turn Settings")]
    public float turnDuration = 20f;
    private float currentTime;

    [Header("Worms")]
    public WormMovement[] worms;
    private int currentWormIndex = 0;

    [Header("UI")]
    public Text timerText;

    private bool turnActive = true;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartTurn();
    }

    void Update()
    {
        if (!turnActive) return;

        currentTime -= Time.deltaTime;
        UpdateUI();

        if (currentTime <= 0f)
        {
            EndTurn();
        }
    }

    void StartTurn()
    {
        turnActive = true;
        currentTime = turnDuration;

        for (int i = 0; i < worms.Length; i++)
        {
            bool isActive = (i == currentWormIndex) && worms[i].identity.isAlive;
            worms[i].SetTurn(isActive);
        }
    }

    public void EndTurn()
    {
        turnActive = false;

        worms[currentWormIndex].SetTurn(false);

        do
        {
            currentWormIndex++;
            if (currentWormIndex >= worms.Length)
                currentWormIndex = 0;
        }
        while (!worms[currentWormIndex].identity.isAlive);

        Invoke(nameof(StartTurn), 1f);
    }

    void UpdateUI()
    {
        if (timerText != null)
            timerText.text = Mathf.Ceil(currentTime).ToString();
    }

    //dispara o recibe daño
    public void ForceEndTurn()
    {
        if (!turnActive) return;
        EndTurn();
    }

    //saber de que jugador es e turno actual
    public int GetCurrentPlayerID()
    {
        return worms[currentWormIndex].identity.playerID;
    }
}
