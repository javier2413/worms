using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormIdentity : MonoBehaviour
{
    [Header("Identity")]
    public int wormID;
    public int playerID;   //dueño
    public int teamID;     //Equipo 

    [Header("State")]
    public bool isAlive = true;
}
