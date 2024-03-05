using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;
    [SerializeField] private List<TimerImageStageColorPair> timerImageStageColorMaps;
    private int _timerImageStageColorMapIndex;


    private void Update()
    {
        var gamePlayingTimerNormalized = GameManager.Instance.GamePlayingTimerNormalized;

        timerImage.fillAmount = gamePlayingTimerNormalized;


        if (_timerImageStageColorMapIndex >= timerImageStageColorMaps.Count)
        {
            return;
        }

        if (gamePlayingTimerNormalized <= timerImageStageColorMaps[_timerImageStageColorMapIndex].percent)
        {
            timerImage.color = timerImageStageColorMaps[_timerImageStageColorMapIndex].color;


            _timerImageStageColorMapIndex++;
        }
    }

    private void OnValidate()
    {
        SortTimerImageStageColorMaps();
    }

    private void SortTimerImageStageColorMaps()
    {
        timerImageStageColorMaps = timerImageStageColorMaps.OrderByDescending(x=>x.percent).ToList();


        for (var i = 0; i < timerImageStageColorMaps.Count - 1; i++)
        {
            if (Mathf.Approximately(timerImageStageColorMaps[i].percent, timerImageStageColorMaps[i + 1].percent))
            {
                Debug.LogWarning(
                    $"TimerImageStageColorMaps的percent[{i}]與[{i + 1}]都為 {timerImageStageColorMaps[i].percent}");
            }
        }
    }


    [Serializable]
    public class TimerImageStageColorPair
    {
        public Color color;
        public float percent;
    }
}