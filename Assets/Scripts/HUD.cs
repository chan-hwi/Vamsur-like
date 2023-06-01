using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health };
    public InfoType Type;

    public Slider slider;
    public Text text;

    private void LateUpdate()
    {
        GameManager gm = GameManager.instance;
        switch (Type)
        {
            case InfoType.Exp:
                if (gm.level == gm.nextExp.Length) slider.value = 1f;
                else slider.value = (float)gm.exp / gm.nextExp[gm.level];
                break;
            case InfoType.Level:
                text.text = string.Format("LV. {0:F0}", gm.level);
                break;
            case InfoType.Kill:
                text.text = string.Format("{0:F0}", gm.kill);
                break;
            case InfoType.Time:
                int min = Mathf.FloorToInt(gm.gameTime / 60f);
                int sec = Mathf.FloorToInt(gm.gameTime % 60f);
                text.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            case InfoType.Health:
                Player playerScript = gm.player.GetComponent<Player>();
                slider.value = Mathf.Clamp01(playerScript.health / playerScript.maxHealth);
                break;
        }
    }
}
