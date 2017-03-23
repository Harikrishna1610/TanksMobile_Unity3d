using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class DeathMatchManager : NetworkBehaviour {

    // Use this for initialization
    static List<TankHealth_DM> tanks = new List<TankHealth_DM>();  //list of tanks playing in the match
    public static void AddTank(TankHealth_DM tank)
    {
        tanks.Add(tank);
    }
    public static bool RemoveTankAndCheckWinner(TankHealth_DM tank)
    {
        tanks.Remove(tank);
        if(tanks.Count == 1)
        {
            return true;
        }
        return false; 
    }
    public static TankHealth_DM GetWinner()
    {
        if(tanks.Count != 1)
        {
            return null;
        }
        return tanks[0];
    }
}
