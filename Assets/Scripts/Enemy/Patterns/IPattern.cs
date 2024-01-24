using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPattern
{
    public void SetPattern();
    public IEnumerator Attack(float noteSpeed);
}