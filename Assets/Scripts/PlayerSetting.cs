public enum Languages
{
    KOR,
    ENG,
    JAP
}

public class PlayerSetting
{
    // Player Setting
    public Languages lang { get; set; }
    public int backgroundVolume // 0 ~ 100
    {
        get => backgroundVolume;
        set
        {
            if (0 <= value && value <= 100)
                backgroundVolume = value;
        }
    }

    private static PlayerSetting _instance;
    private static object _synLock = new object();

    private static void InitSetting(PlayerSetting setting)
    {
        setting.lang = Languages.KOR;
        setting.backgroundVolume = 100;
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
