using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_10001_Option : OptionSource
{
    public int count;
    public override void Effect()
    {
        int f = CharacterInDungeon.Ins.Fortune.Get_xDy(2, 0);
        string result = "";
        if (f > 9)
        {
            CharacterInDungeon.Ins.IncreaseEp(1 * count);
            result += "強い力が感じました、エネルギー上限を" + count + "アップ";
        }
        else if (f == 9)
        {
            CharacterInDungeon.Ins.IncreaseAttack(3 * count);
            result += "力が湧いてくる、攻撃を" + 3 * count + "アップ";
        }
        else if (f == 8)
        {
            CharacterInDungeon.Ins.IncreaseDefend(3 * count);
            result += "力が湧いてくる、防御を" + 3 * count + "アップ";
        }
        else if (f == 7)
        {
            CharacterInDungeon.Ins.IncreaseHpMax(10 * count);
            result += "体力がどんどん増えてくる、HP上限を" + 10 * count + "アップ";
        }
        else if (f == 6)
        {
            CharacterInDungeon.Ins.IncreaseAttack(1 * count);
            result += "力が湧いてくる、攻撃を" + count + "アップ";
        }
        else if (f == 5)
        {
            CharacterInDungeon.Ins.IncreaseDefend(1 * count);
            result += "力が湧いてくる、防御を" + count + "アップ";
        }
        else if (f == 4)
        {
            int hp = Mathf.FloorToInt(0.1f * CharacterInDungeon.Ins.currentHp * count);
            CharacterInDungeon.Ins.IncreaseHpCurrect(hp);
            result += "体力が少々回復している、HPを" + hp + "回復";
        }
        else if (f == 3)
        {
            result += "何の感じもない";
        }
        else if (f == 2)
        {
            int hp = Mathf.FloorToInt(0.1f * CharacterInDungeon.Ins.currentHp * count);
            CharacterInDungeon.Ins.DecreaseHpCurrect(hp);
            result += "めまい感を感じしました，HPを" + hp + "ダウン";
        }
        else if (f == 1)
        {
            CharacterInDungeon.Ins.DecreaseHpMax(5 * count);
            result += "お腹が痛みを感じます、HP上限を" + 5 * count + "ダウン";
        }
        else if (f == 0)
        {
            CharacterInDungeon.Ins.DecreaseAttack(1 * count);
            result += "気持ち悪いの味だ、攻撃を" + count + "ダウン";
        }
        EventManager.Ins.EUI.ShowResultPanel(result);
    }
}
