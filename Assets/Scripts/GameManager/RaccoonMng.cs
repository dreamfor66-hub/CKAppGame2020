﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class RaccoonMng : MonoBehaviour
{
    GameMng GMng;
    private int selectedRC;
    private static int RaccoonCount = 7;
    private static int RaccoonRankCount = 5;
    public GameObject[] RC = new GameObject[RaccoonCount];
    bool[] RaccoonExist = new bool[RaccoonCount];
    public bool[] RaccoonUnlock = new bool[RaccoonCount];
    int[] RaccoonRank = new int[RaccoonCount];
    public float[,] RCEfficiency = new float[RaccoonCount, RaccoonRankCount];

    /*
     * 라쿤의 장사 금액 효율을 반환한다
     * ex) 1.0f, 1.5f
     */
    public float GetRCEfficiency(int RCIndex, int RankIndex)
    {
        if (RCIndex < 0 && RCIndex >= RaccoonCount && RankIndex < 0 && RankIndex >= RaccoonRankCount)
            return 1f;
        return RCEfficiency[RCIndex, RankIndex];
    }

    // Start is called before the first frame update
    void Start()
    {
        GMng = GameObject.Find("GameManager").GetComponent<GameMng>();
        UnlockRC(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(isRCOnDrag())
        {
            Camera.main.GetComponent<CameraController>().MoveScreenEdge();
        }
    }

    public void GenerateRaccoon(int RCindex)
    {
        if (!GameObject.Find("GameManager").GetComponent<GameMng>().getOpenData)
        {
            Debug.Log(RCindex); 
            
            if (!RaccoonExist[RCindex])
            {
                RC[RCindex].transform.position = new Vector3(5, 0, 5);
                RC[RCindex].GetComponent<RaccoonController>().SetRCActive(true);
                RaccoonExist[RCindex] = true;
                Debug.Log("Raccoon Created!");
            }
        }
    }

    public bool isRCOnDrag()
    {
        for (int i = 0; i < RaccoonCount; i++)
        {
            if (RC[i].GetComponent<RaccoonController>().GetIsDrag())
            {
                return true;
            }
        }
        return false;
    }

    public bool GetRCUnlockData(int index)
    {
        return RaccoonUnlock[index];
    }

    public int GetRCRank(int index)
    {
        return RaccoonRank[index];
    }

    public void UpgradeRC(int index)
    {
        int cost = RetCost(index, RaccoonRank[index]);
        if (RaccoonRank[index] < RaccoonRankCount && RaccoonUnlock[index] && GMng.money >= cost && !GameObject.Find("GameManager").GetComponent<GameMng>().getOpenData)
        {
            RaccoonRank[index]++;
            RC[index].GetComponent<RaccoonController>().CallUpgradeTrigger();
            GMng.money -= cost;
        }
    }

    public void UnlockRC(int index)
    {
        int cost = RetCost(index, 0);
        if(!RaccoonUnlock[index] && GMng.money >= cost && !GameObject.Find("GameManager").GetComponent<GameMng>().getOpenData)
        {
            RaccoonUnlock[index] = true;
            RaccoonRank[index] = 1;
            GMng.money -= cost;
            GenerateRaccoon(index);
        }
    }

    public void StartRCWork()
    {
        for(int i =0;i<RaccoonCount;i++)
        {
            if (RaccoonExist[i])
                RC[i].GetComponent<RaccoonController>().StartWork();
        }
    }

    public void StopRCWork()
    {
        for (int i = 0; i < RaccoonCount; i++)
        {
            if (RaccoonExist[i])
                RC[i].GetComponent<RaccoonController>().StopWork();
        }
    }

    public int RetCost(int RcIndex, int UpgradeIndex)
    {
        if (UpgradeIndex >= 0 && UpgradeIndex < RaccoonRankCount)
            return RC[RcIndex].GetComponent<RaccoonController>().Cost[UpgradeIndex];
        return -1;
    }
}
