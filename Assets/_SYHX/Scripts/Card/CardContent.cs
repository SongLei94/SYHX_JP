﻿using System;
using System.Collections.Generic;
using System.Reflection;
public abstract class CardContent : SAssitant<CardSource>
{
    public string name { get; private set; }
    private string desc;
    public string Desc => GetDesc(desc);
    public Dictionary<string, PropertyInfo> descOption;
    public CardContent()
    { }
    public void SetOwnerWithDic(CardSource owner, Dictionary<string, PropertyInfo> descOption = null)
    {
        this.owner = owner;
        this.EP = owner.EP;
        this.cardType = owner.cardType;
        this.name = owner.Name;
        this.desc = owner.Desc;
        this.descOption = descOption;
    }
    public CardType cardType { get; private set; }
    public int EP { get; private set; }
    public void OnDraw() => owner.OnDraw();

    public bool CanUse() => TurnManager.Ins.stateManager.playerTurnState.IsCurrent() && BattleManager.Ins.GetEP() >= this.EP && UseOption();
    protected virtual bool UseOption() => true;
    /// <summary>
    /// ❌事件：当卡牌在打出后，经过选择之后的效果
    /// </summary>
    public virtual void OnUse(CardUseTrigger trigger)
    {
        if (CanUse())
        {
            BattleManager.Ins.ChangeEnergy(-this.EP);
            UseEffect(trigger);
        }

    }

    protected abstract void UseEffect(CardUseTrigger trigger);
    public void OnFold() => owner.OnFold();
    public void OnExiled() => owner.OnExiled();
    public void OnOtherCardUse(CardSource context) => owner.OnOtherCardUse(context);
    public virtual string GetDesc(string desc)
    {
        foreach (KeyValuePair<string, PropertyInfo> pairs in descOption)
        {
            desc = desc.Replace("{" + pairs.Key + "}", (string)pairs.Value.GetValue(this));
        }
        return desc;
    }
}

public enum CardUseTrigger
{
    ByUser, ByCard
}

/*
 *用于做卡牌描述
 */
[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
sealed class CardDescAttribute : Attribute
{
    public readonly string descName;
    public CardDescAttribute(string descName)
    {
        this.descName = descName;
    }
}
