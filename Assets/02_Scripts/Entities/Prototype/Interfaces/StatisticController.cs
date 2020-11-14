using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class StatisticController
{
    public List<GameStatistics> statList;
    public List<Multiplier> multList;

    virtual public void InitStats(StatTemplate template)
    {
        multList = new List<Multiplier>();
        statList = new List<GameStatistics>();
        foreach (FloatReference f in template.statList)
        {
            StatVariable s = (StatVariable)f.Variable;
            statList.Add(new GameStatistics(f.Value, s.statName));
        }
    }
    virtual public void SetStatValue(StatName name, float value)
    {
        statList.Find(x => x.GetName().Equals(name)).SetValue(value);
    }

    virtual public float GetStatValue(StatName stat)
    {
        if (statList.Exists(x => x.GetName().Equals(stat)))
        {
            return statList.Find(x => x.GetName().Equals(stat)).GetValue();
        }
        else
        {
            return 1f;
        }
    }

    virtual public void AddMultiplier(MultiplierName name, float value, float time)
    {
        multList.Add(new Multiplier(value, name));

        //StartCoroutine(MultiplierTimer(time, multList.FindIndex(x => x.GetName().Equals(name))));
        MultiplierTimer(time, multList.FindIndex(x => x.GetName().Equals(name)));
    }

    virtual public float GetMultValue(MultiplierName name)
    {
        float value = 1f;
        if (multList.Exists(x => x.GetName().Equals(name)))
        {
            List<Multiplier> list = multList.FindAll(x => x.GetName().Equals(name));
            foreach (Multiplier mult in list)
            {
                value += mult.GetValue();
            }
            return value;
        }

        return value;
    }

    virtual public void ResetMultipliers()
    {
        multList.Clear();
    }

    virtual public IEnumerator MultipliersTimer(float time, int id)
    {
        yield return new WaitForSeconds(time);
        multList.RemoveAt(id);
    }

    async UniTask MultiplierTimer(float time, int id)
    {
        int miliTime = (int)(time * 1000);
        await UniTask.Delay(miliTime);
        multList.RemoveAt(id);
    }
}
