using UnityEngine;

// TODO
// Valid Language 인지 체크하고 변경할 수 있는 setter가 필요함
public class PlayerSetting
{
    // Player Setting
    private SystemLanguage _language;
    public SystemLanguage language
    {
        get => _language;
        set { _language = value; }
    }
    private int _backgroundVolume;
    public int backgroundVolume // 0 ~ 100
    {
        get => _backgroundVolume;
        set
        {
            if (0 <= value && value <= 100)
            {
                _backgroundVolume = value;
            }
        }
    }

    private static PlayerSetting _instance;
    private static object _synLock = new object();

    private static void InitSetting(PlayerSetting setting)
    {
        if (Application.systemLanguage == SystemLanguage.Korean
            || Application.systemLanguage == SystemLanguage.English
            || Application.systemLanguage == SystemLanguage.Japanese)
            setting.language = Application.systemLanguage;
        else
            setting.language = SystemLanguage.English;
        setting.backgroundVolume = Constants.BACKGROUND_SOUND_INIT_VALUE;
    }

    protected PlayerSetting() { }
    public static PlayerSetting Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_synLock)
                {
                    _instance = new PlayerSetting();
                    InitSetting(_instance);
                }
            }
            return _instance;
        }
    }
}
