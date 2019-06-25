﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using SYHX.Cards;

public enum BattleResult
{
    Continue,
    Win,
    Lose,
}

public partial class BattleManager : SingletonMonoBehaviour<BattleManager>
{
    #region 外部调用
    [SerializeField] private BattleHero hero;
    public static Enemy selectedEnemy => BattleCharacterManager.Ins.selectedEnemy;
    public static List<Enemy> enemyList => BattleCharacterManager.Ins.enemyList;
    public static BattleCharacter Hero => BattleCharacterManager.Ins.hero;
    public BattleInfoManager biManager;
    public TurnManager turnManager => TurnManager.Ins;
    public static bool enemyOnGoing = false;
    public static bool canExeNextEnemy = true;
    public static bool finishEnemyAction = false;
    #endregion


    #region  UI
    public TextMeshProUGUI currentEPUI;
    public TextMeshProUGUI maxEPUI;
    public Button result;
    public TextMeshProUGUI resultUI;
    public Text roundText;
    //可能弃用
    public int currentEP;
    public int maxEP;
    #endregion
    protected override void UnityAwake()
    {
        result.gameObject.SetActive(false);
        BattleCharacterManager.Ins.SetHero(hero);
        biManager = new BattleInfoManager(this, currentEP, maxEP);
        biManager.RefreshUI();
    }
    void Start()
    {
        BattleStart(0, null);
    }

    /// <summary>
    /// 战斗开始，战斗场景的入口
    /// </summary>
    /// <param name="id">战斗数据的键值</param>
    /// <param name="context">将GameManager作为上下文传入</param>
    public void BattleStart(int id, GameManager context)
    {
        //读取战斗数据
        BattleCharacterManager.Ins.GenerateEnemyGroup(id);
        biManager.ResetCardType();
        biManager.ResetConnection();
    }

    /// <summary>
    /// 战斗结束，做BattleManager的收尾工作
    /// </summary>
    public void BattleEnd()
    {
        // mBattleStatus = null;
        BattleCharacterManager.Ins.Destroy();
        BattleProgressEvent.Ins.Destroy();
    }


    /// <summary>
    /// 检测战斗是否结束
    /// 检测方式：
    /// 是否所有敌人均已死亡
    /// 是否角色死亡
    /// </summary>
    /// <returns></returns>
    public void EndBattle()
    {
        StartCoroutine(Result());
    }
    private IEnumerator Result()
    {
        yield return new WaitForSeconds(0.1f);
        switch (BattleCharacterManager.Ins.result)
        {
            case BattleResult.Win:
                Win();
                break;
            case BattleResult.Lose:
                Lose();
                break;
            default:
                break;
        }
        yield break;
    }
    private void Win()
    {
        resultUI.text = "you win";
        result.gameObject.SetActive(true);
    }
    private void Lose()
    {
        resultUI.text = "you lose";
        result.gameObject.SetActive(true);
    }
}


public partial class BattleManager
{

    public static void SAddTurn() => Ins.biManager.AddTurn();
    public static int SGetEP() => Ins.biManager.currentEP;
    public static void STurnStartDraw() => SDraw(Ins.biManager.DrawCountPerTurn);
    public static void SDraw(int count) => BattleCardManager.Ins.Draw(count);
    public static void SShuffle() => BattleCardManager.Ins.Shuffle();
    public static void SEnergyPointRegain() => Ins.biManager.EnergyPointRegain();
    public static void SChangeEnergy(int ep) => Ins.biManager.ChangeEnergy(ep);
    public static void sCalculateConnection(CardType type, int count) => Ins.biManager.CalculateConnection(type, count);
    public static void sRegainMoreEnergyPointNextTurn(int count) => Ins.biManager.RegainMoreEnergyPointNextTurn(count);
    public static void sTurnEnd() => Ins.turnManager.EndPlayerTurn();
    public static void sResult() => Ins.turnManager.Result();
    public static void ManagerCoroutine(IEnumerator enumarator) => Ins.StartCoroutine(enumarator);
    public static void sStartEnemyAction() => BattleCharacterManager.Ins.StartEnemyAction();
    public static void sDiscardAll() => BattleCardManager.Ins.DiscardAll();
    public void TurnEnd() => sTurnEnd();
}

