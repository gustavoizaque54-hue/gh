using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gx_cheat
{
internal static class Config
{
    internal static bool SpawnKill = false;
    internal static bool TpEnemy = false;
    internal static bool TeleKill = false;
    internal static bool UpPlayer = false;
    internal static bool DowmPlayer = false;
    internal static bool UnderPlayer = false;
    internal static bool Frente = false;
    internal static bool Traz = false;
    internal static bool Direita = false;
    internal static bool Esquerda = false;

    internal static bool Ghost = false;
    internal static bool NoRecoil = false;
    internal static bool FastReload = false;

    internal static int AimBotMaxDistance = 1000;
    internal static bool SilentKill = false;
    internal static Keys AimSilentKey = Keys.LButton;












    public static float AimBotMaxByDistanceV2 = 0f;
    public static float AimSmoothV2 = 0f;
    public static float AimfovV2 = 0f;

    public static bool AimbotV2 = false;
    public static bool IgnoreKnockedV2 = true;

    internal static bool ESPWeaponIcon = false;
    internal static bool ESPWeapon = false;

    internal static bool ESPLineBottom = false;
    internal static bool ESPLineTop = false;

    internal static int fov = 999999999;
    internal static Color colorlinee = Color.White;
    internal static Color WarningColor = Color.White;

    internal static float WarningDistance = 50f; // por exemplo 50 metros

    internal static bool ShowEnemyWarning = false;
    internal static bool FixEsp = false;
    internal static bool Chams = false;


    internal static bool ESPLine = false;
    internal static Color ESPLineColor = Color.White;
    internal static bool ESPBox = false;
    internal static Color ESPBoxColor = Color.White;
    internal static bool ESPBox2 = false;
    internal static Color ESPFillBoxColor = Color.White;
    internal static bool ESPName = false;
    internal static Color ESPNameColor = Color.White;
    internal static bool ESPHealth = false;
    internal static Color ESPHeath = Color.White;
    internal static bool ESPSkeleton = false;
    internal static Color ESPSkeletonColor = Color.White;
    internal static Color NameCheat = Color.White;
    internal static bool ESPFillBox = false;
    internal static bool ESPCorner = false;
    internal static bool ESPCornerColor = false;
    internal static bool ESPInfo = false;
    internal static bool ESPFove = false;
    internal static bool espbg = false;
    internal static Color Aimfovcolor = Color.White;
    internal static bool espcfx = false;
    internal static bool sound = false;
    internal static bool ESPDistance = false;
    internal static int espran = 100;
    internal static bool AimBot = false;
    internal static bool Aimfovc = false;
    // New field for line position control
    internal static string linePosition = "Up";  // Default to "Up"
    internal static bool RGB = false;

    internal static bool StreamMode = false;
    internal static bool esptotalplyer = false;
    // AIM BOT
    internal static bool Aimbot = false;
    internal static int Aimfov = 999999999;
    public static float AimBotMaxByDistance = 999999999999999999;
    public static bool IgnoreKnocked = true;

    // INTERNAL MEMORY
    internal static bool NoCache = false;


    public enum TargetingMode
    {
        ClosestToCrosshair,
        Target360,
        ClosestToPlayer,
        LowestHealth,
    }
    public enum AimBotType
    {
        Silent,
        Rage
    }
    public enum LinePosition
    {
        Top,
        Center,
        Bottom
    }
}
}

