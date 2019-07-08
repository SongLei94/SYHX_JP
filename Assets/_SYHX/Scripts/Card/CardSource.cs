﻿using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;

namespace SYHX.Cards
{

    public abstract class CardSource : ScriptableObject, Source
    {
        #region 序列化数据
        [SerializeField] protected int mID;
        [SerializeField] protected string mName;
        [Multiline] [SerializeField] protected string mDesc;
        [SerializeField] protected int mEP;
        [SerializeField] public ConnectionType connectionType;
        [SerializeField] public CardType cardType;
        [SerializeField] public Rarity rarity;
        [SerializeField] public List<CardSource> upgradeList;
        [SerializeField] public List<CardKeyWord> keyWords;
        #endregion

        #region 获取用数据
        public int ID { get => mID; }
        public string Name { get => mName; }
        public string Desc { get => mDesc; }
        public int EP { get => mEP; }
        #endregion

        public abstract void Init();

        #region 卡牌生成相关
        public abstract CardContent GenerateCard();

        [Button(ButtonSizes.Large)]
        [Conditional("UNITY_EDITOR")]
        public virtual void GenerateToDeck()
        {
            var cc = GenerateCard();
            BattleCardManager.Ins.GenerateCardTo(cc,CardPosition.Deck);
            cc.RefreshUI();
        }

        [Button(ButtonSizes.Large)]
        [Conditional("UNITY_EDITOR")]
        public void DrawCard()
        {
            BattleCardManager.Ins.Draw(1);
        }
        #endregion
    }

    public class CardSource<T> : CardSource
    where T : CardContent, new()
    {
        [SerializeField] T origin;

        private Dictionary<string, PropertyInfo> descOption;
        public override void Init() => descOption = this.InitDescOption<CardSource, T>();
        public override CardContent GenerateCard()
        {
            var cc = this.GenerateContent<CardSource, T>(origin);
            cc.SetOwnerWithDic(this, this.descOption);
            return cc;
        }

    }
    public enum ConnectionType
    {
        强袭技, 灵巧技, 神秘技, 连接技,无
    }

    public enum CardType
    {
        攻击,技能,防御
    }

    public enum Rarity
    {
        初始,初级,中级,高级
    }
}


