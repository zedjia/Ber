using System;

namespace WC.Lib.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum CommonCommands
    {
        Test=0,
        Beat=1,
        VersionCheck=2,
        Login=3,
        Download=4,
        OnlineUser=5,
        Voice=6,
        VoiceEnd=7,
        IMFile = 8

    }
}
