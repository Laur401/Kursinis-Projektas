using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePanelManager : MonoBehaviour
{
    [SerializeField] private List<LevelPanel> panels;

    public void LoadWholePanel(Func<int, (int levelID, int par, int hitCount)?> getScore)
    {
        for (int i = 0; i < panels.Count; i++)
        {
            var result = getScore(i + 1);
            if (result == null) continue;
            var (_, par, hitCount) = getScore(i + 1)!.Value;
            panels[i].par.text = par.ToString();
            panels[i].score.text = hitCount.ToString();
        }
    }
    
    public void SetStroke(int id, int stroke)
    {
        panels[id - 1].score.text = stroke.ToString();
    }
    public void SetPar(int id, int par)
    {
        panels[id - 1].par.text = par.ToString();
    }
}

[Serializable]
public class LevelPanel
{
    public TMP_Text par;
    public TMP_Text score;
}


