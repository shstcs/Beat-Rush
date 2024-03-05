using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimationData
{
    private string _attack1ParameterName = "Attack1";
    private string _attack2ParameterName = "Attack2";
    private string _attack3ParameterName = "Attack3";
    private string _takeDamageParameterName = "TakeDamage";
    private string _dieParameterName = "Die";

    private List<int> _attackParameterHash = new();
    public int TakeDamageParameterHash { get; private set; }
    public int DieParameterHash { get; private set; }

    public void Init()
    {
        _attackParameterHash.Add(Animator.StringToHash(_attack1ParameterName));
        _attackParameterHash.Add(Animator.StringToHash(_attack2ParameterName));
        _attackParameterHash.Add(Animator.StringToHash(_attack3ParameterName));
        TakeDamageParameterHash = Animator.StringToHash(_takeDamageParameterName);
        DieParameterHash = Animator.StringToHash(_dieParameterName);
    }

    public int GetRandomAttackHash()
    {
        int randomNum = Random.Range(0, 3);
        int _randomAttackHash = _attackParameterHash[randomNum];
        return _randomAttackHash;
    }
}
