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
            var (_, par, hitCount) = getScore(i + 1) ?? (0, 0, 0);
            panels[i].par.text = par.ToString();
            panels[i].stroke.text = hitCount.ToString();
            panels[i].score.text = (hitCount - par).ToString();
        }
    }
    
    public void SetScore(int id, int score)
    {
        panels[id - 1].score.text = score.ToString();
    }

    public void SetStroke(int id, int stroke)
    {
        panels[id - 1].stroke.text = stroke.ToString();
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
    public TMP_Text stroke;
    public TMP_Text score;
}


