using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected int _gold;

    public int Exp 
    { 
        get { return _exp; } 
        set 
        {
            _exp = value;

            // 레벨업 체크 (레벨 다운 등을 체크하기 위해 Level 1부터 체크)
            int level = 1;
            while(true)
            {
                Data.Stat stat;
                if(Managers.Data.StatDict.TryGetValue(level + 1, out stat) == false)
                {
                    break;
                }

                if(_exp < stat.totalExp)
                {
                    break;
                }

                // Levelup Effect 여기서 반복 호출할지
                // Stat Setting에서 호출 한 번만 할지
                level++;
            }

            if(level != Level)
            {
                Level = level;
                SetStat(Level);
            }
        } 
    }

    public int Gold { get { return _gold; } set { _gold = value; } }

    private void Start()
    {
        _level = 1;
        _exp = 0;
        _defense = 5;
        _moveSpeed = 5.0f;
        _gold = 0;

        SetStat(_level);
    }

    public void SetStat(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        Data.Stat stat = dict[level];

        _hp = stat.maxHp;
        _maxHp = stat.maxHp;
        _attack = stat.attack;
    }

    protected override void OnDead(Stat attacker)
    {
        // base.OnDead();

        // TODO : Player Dead 구현
    }
}
