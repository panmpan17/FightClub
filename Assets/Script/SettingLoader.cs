using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingLoader
{
    public SettingLoader() {
        TextAsset _text = Resources.Load<TextAsset>("Setting.json");
        TempData _data = JsonUtility.FromJson<TempData>(_text.text);
        Resources.UnloadAsset(_text);

        _data.GetDictionary(this);
    }

    [System.Serializable]
    private sealed class TempData {
        public int PlayerHealth;
        public int[] PunchDamage;
        public int[] TwistArmPunch;
        public int[] MetaPunch;

        public void GetDictionary(SettingLoader loader) {

        }
    }
}
