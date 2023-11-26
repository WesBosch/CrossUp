using System;
using System.Numerics;
using Dalamud.Configuration;
using Dalamud.Plugin;
using static CrossUp.Features.Color;
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassCanBeSealed.Global
// ReSharper disable MemberCanBeInternal

namespace CrossUp;

[Serializable]
public class CrossUpConfig : IPluginConfiguration
{
    public int Version                 { get; set; } = 0;
    public Vector2 ConfigWindowSize    { get; set; } = new(500, 450);

    // unique configs per HUD Slot
    public bool UniqueHud              { get; set; }
    public ConfigProfile[] Profiles    { get; set; } = { new(), new(), new(), new(), new() };

    // Separate Expanded Hold Configs
    public int LRborrow                { get; set; } = -1;
    public int RLborrow                { get; set; } = -1;

    // Remapping Configs
    public bool RemapEx                { get; set; }
    public bool RemapW                 { get; set; }
    public int[,] MappingsEx           { get; set; } = { { 0, 0, 0, 0, 0, 0, 0, 0 }, { 1, 1, 1, 1, 1, 1, 1, 1 } };
    public int[,] MappingsW            { get; set; } = { { 0, 0, 0, 0, 0, 0, 0, 0 }, { 1, 1, 1, 1, 1, 1, 1, 1 } };

    [NonSerialized] private DalamudPluginInterface? PluginInterface;
    public void Initialize(DalamudPluginInterface pluginInterface) => PluginInterface = pluginInterface;
    public void Save() => PluginInterface!.SavePluginConfig(this);
}

public class ConfigProfile
{
    // Bar Separation
    public bool SplitOn                { get; set; }
    public int SplitDist               { get; set; } = 100;
    public int CenterPoint             { get; set; }

    // Slot Config (Hotbar Unlimited lite)
    public bool ConfigureSlots { get; set; }
    public bool PSConfig { get; set; }
    public Vector2 Left_LeftOffset { get; set; }
    public Vector2 Right_LeftOffset { get; set; }
    public Vector2 Up_LeftOffset { get; set; }
    public Vector2 Down_LeftOffset { get; set; }
    public Vector2 A_LeftOffset { get; set; }
    public Vector2 B_LeftOffset { get; set; }
    public Vector2 X_LeftOffset { get; set; }
    public Vector2 Y_LeftOffset { get; set; }
    public Vector2 Left_RightOffset { get; set; }
    public Vector2 Right_RightOffset { get; set; }
    public Vector2 Up_RightOffset { get; set; }
    public Vector2 Down_RightOffset { get; set; }
    public Vector2 A_RightOffset { get; set; }
    public Vector2 B_RightOffset { get; set; }
    public Vector2 X_RightOffset { get; set; }
    public Vector2 Y_RightOffset { get; set; }
    public Vector2 Left_LeftOffset_RL { get; set; }
    public Vector2 Right_LeftOffset_RL { get; set; }
    public Vector2 Up_LeftOffset_RL { get; set; }
    public Vector2 Down_LeftOffset_RL { get; set; }
    public Vector2 A_LeftOffset_RL { get; set; }
    public Vector2 B_LeftOffset_RL { get; set; }
    public Vector2 X_LeftOffset_RL { get; set; }
    public Vector2 Y_LeftOffset_RL { get; set; }
    public Vector2 Left_RightOffset_RL { get; set; }
    public Vector2 Right_RightOffset_RL { get; set; }
    public Vector2 Up_RightOffset_RL { get; set; }
    public Vector2 Down_RightOffset_RL { get; set; }
    public Vector2 A_RightOffset_RL { get; set; }
    public Vector2 B_RightOffset_RL { get; set; }
    public Vector2 X_RightOffset_RL { get; set; }
    public Vector2 Y_RightOffset_RL { get; set; }
    public Vector2 Left_LeftOffset_LR { get; set; }
    public Vector2 Right_LeftOffset_LR { get; set; }
    public Vector2 Up_LeftOffset_LR { get; set; }
    public Vector2 Down_LeftOffset_LR { get; set; }
    public Vector2 A_LeftOffset_LR { get; set; }
    public Vector2 B_LeftOffset_LR { get; set; }
    public Vector2 X_LeftOffset_LR { get; set; }
    public Vector2 Y_LeftOffset_LR { get; set; }
    public Vector2 Left_RightOffset_LR { get; set; }
    public Vector2 Right_RightOffset_LR { get; set; }
    public Vector2 Up_RightOffset_LR { get; set; }
    public Vector2 Down_RightOffset_LR { get; set; }
    public Vector2 A_RightOffset_LR { get; set; }
    public Vector2 B_RightOffset_LR { get; set; }
    public Vector2 X_RightOffset_LR { get; set; }
    public Vector2 Y_RightOffset_LR { get; set; }
    public Vector2 Left_LeftOffset_R { get; set; }
    public Vector2 Right_LeftOffset_R { get; set; }
    public Vector2 Up_LeftOffset_R { get; set; }
    public Vector2 Down_LeftOffset_R { get; set; }
    public Vector2 A_LeftOffset_R { get; set; }
    public Vector2 B_LeftOffset_R { get; set; }
    public Vector2 X_LeftOffset_R { get; set; }
    public Vector2 Y_LeftOffset_R { get; set; }
    public Vector2 Left_RightOffset_R { get; set; }
    public Vector2 Right_RightOffset_R { get; set; }
    public Vector2 Up_RightOffset_R { get; set; }
    public Vector2 Down_RightOffset_R { get; set; }
    public Vector2 A_RightOffset_R { get; set; }
    public Vector2 B_RightOffset_R { get; set; }
    public Vector2 X_RightOffset_R { get; set; }
    public Vector2 Y_RightOffset_R { get; set; }
    public Vector2 Left_LeftOffset_L { get; set; }
    public Vector2 Right_LeftOffset_L { get; set; }
    public Vector2 Up_LeftOffset_L { get; set; }
    public Vector2 Down_LeftOffset_L { get; set; }
    public Vector2 A_LeftOffset_L { get; set; }
    public Vector2 B_LeftOffset_L { get; set; }
    public Vector2 X_LeftOffset_L { get; set; }
    public Vector2 Y_LeftOffset_L { get; set; }
    public Vector2 Left_RightOffset_L { get; set; }
    public Vector2 Right_RightOffset_L { get; set; }
    public Vector2 Up_RightOffset_L { get; set; }
    public Vector2 Down_RightOffset_L { get; set; }
    public Vector2 A_RightOffset_L { get; set; }
    public Vector2 B_RightOffset_L { get; set; }
    public Vector2 X_RightOffset_L { get; set; }
    public Vector2 Y_RightOffset_L { get; set; }
    public Vector2 Dpad_LeftOffset { get; set; }
    public Vector2 Dpad_RightOffset { get; set; }
    public Vector2 Face_LeftOffset { get; set; }
    public Vector2 Face_RightOffset { get; set; }
    public Vector2 Dpad_LeftOffset_L { get; set; }
    public Vector2 Dpad_RightOffset_L { get; set; }
    public Vector2 Face_LeftOffset_L { get; set; }
    public Vector2 Face_RightOffset_L { get; set; }
    public Vector2 Dpad_LeftOffset_R { get; set; }
    public Vector2 Dpad_RightOffset_R { get; set; }
    public Vector2 Face_LeftOffset_R { get; set; }
    public Vector2 Face_RightOffset_R { get; set; }
    public Vector2 Dpad_LeftOffset_LR { get; set; }
    public Vector2 Dpad_RightOffset_LR { get; set; }
    public Vector2 Face_LeftOffset_LR { get; set; }
    public Vector2 Face_RightOffset_LR { get; set; }
    public Vector2 Dpad_LeftOffset_RL { get; set; }
    public Vector2 Dpad_RightOffset_RL { get; set; }
    public Vector2 Face_LeftOffset_RL { get; set; }
    public Vector2 Face_RightOffset_RL { get; set; }


    // Bar Elements
    public Vector2 PadlockOffset       { get; set; } = new(0);
    public Vector2 SetTextOffset       { get; set; } = new(0);
    public Vector2 ChangeSetOffset     { get; set; } = new(0);
    public bool HidePadlock            { get; set; }
    public bool HideSetText            { get; set; }
    public bool HideTriggerText        { get; set; }
    public bool HideUnassigned         { get; set; }

    // Colors
    public Vector3 SelectColorMultiply { get; set; } = Preset.MultiplyNeutral;
    public int SelectBlend             { get; set; } //0: Normal, 2: Dodge
    public int SelectStyle             { get; set; } //0: Solid, 1: Frame, 2: Hide
    public Vector3 GlowA               { get; set; } = Preset.White;
    public Vector3 GlowB               { get; set; } = Preset.White;
    public Vector3 TextColor           { get; set; } = Preset.White;
    public Vector3 TextGlow            { get; set; } = Preset.TextGlow;
    public Vector3 BorderColor         { get; set; } = Preset.White;

    // Separate Expanded Hold Controls
    public bool SepExBar               { get; set; }
    public Vector2 LRpos               { get; set; } = new(-214, -88);
    public Vector2 RLpos               { get; set; } = new(214, -88);
    public bool OnlyOneEx              { get; set; }

    // Combat Fader
    public bool CombatFadeInOut        { get; set; }
    public int TranspOutOfCombat       { get; set; } = 100;
    public int TranspInCombat          { get; set; }

    public ConfigProfile() { }

    /// <summary>Copy-constructor for cloning an existing profile</summary>
    public ConfigProfile(ConfigProfile original)
    {
        SplitOn             = original.SplitOn;
        SplitDist           = original.SplitDist;
        CenterPoint         = original.CenterPoint;
        PadlockOffset       = original.PadlockOffset;
        SetTextOffset       = original.SetTextOffset;
        ChangeSetOffset     = original.ChangeSetOffset;
        HidePadlock         = original.HidePadlock;
        HideSetText         = original.HideSetText;
        HideTriggerText     = original.HideTriggerText;
        HideUnassigned      = original.HideUnassigned;
        SelectColorMultiply = original.SelectColorMultiply;
        SelectBlend         = original.SelectBlend;
        SelectStyle         = original.SelectStyle;
        GlowA               = original.GlowA;
        GlowB               = original.GlowB;
        TextColor           = original.TextColor;
        TextGlow            = original.TextGlow;
        BorderColor         = original.BorderColor;
        SepExBar            = original.SepExBar;
        LRpos               = original.LRpos;
        RLpos               = original.RLpos;
        OnlyOneEx           = original.OnlyOneEx;
        CombatFadeInOut     = original.CombatFadeInOut;
        TranspOutOfCombat   = original.TranspOutOfCombat;
        TranspInCombat      = original.TranspInCombat;

        // SlotConfig Vars
        ConfigureSlots      = original.ConfigureSlots;
        PSConfig            = original.PSConfig;
        Left_LeftOffset     = original.Left_LeftOffset;
        Right_LeftOffset    = original.Right_LeftOffset;
        Up_LeftOffset       = original.Up_LeftOffset;
        Down_LeftOffset     = original.Down_LeftOffset;
        A_LeftOffset        = original.A_LeftOffset;
        B_LeftOffset        = original.B_LeftOffset;
        X_LeftOffset        = original.X_LeftOffset;
        Y_LeftOffset        = original.Y_LeftOffset;
        Left_RightOffset    = original.Left_RightOffset;
        Right_RightOffset   = original.Right_RightOffset;
        Up_RightOffset      = original.Up_RightOffset;
        Down_RightOffset    = original.Down_RightOffset;
        A_RightOffset       = original.A_RightOffset;
        B_RightOffset       = original.B_RightOffset;
        X_RightOffset       = original.X_RightOffset;
        Y_RightOffset       = original.Y_RightOffset;
        Dpad_LeftOffset     = original.Dpad_LeftOffset;
        Dpad_RightOffset    = original.Dpad_RightOffset;
        Face_LeftOffset     = original.Face_LeftOffset;
        Face_RightOffset    = original.Face_RightOffset;
        Dpad_LeftOffset_LR = original.Dpad_LeftOffset_LR;
        Dpad_RightOffset_LR = original.Dpad_RightOffset_LR;
        Face_LeftOffset_LR = original.Face_LeftOffset_LR;
        Face_RightOffset_LR = original.Face_RightOffset_LR;
        Dpad_LeftOffset_R = original.Dpad_LeftOffset_R;
        Dpad_RightOffset_R = original.Dpad_RightOffset_R;
        Face_LeftOffset_R = original.Face_LeftOffset_R;
        Face_RightOffset_R = original.Face_RightOffset_R;
        Dpad_LeftOffset_LR = original.Dpad_LeftOffset_LR;
        Dpad_RightOffset_LR = original.Dpad_RightOffset_LR;
        Face_LeftOffset_LR = original.Face_LeftOffset_LR;
        Face_RightOffset_LR = original.Face_RightOffset_LR;
        Dpad_LeftOffset_RL = original.Dpad_LeftOffset_RL;
        Dpad_RightOffset_RL = original.Dpad_RightOffset_RL;
        Face_LeftOffset_RL = original.Face_LeftOffset_RL;
        Face_RightOffset_RL = original.Face_RightOffset_RL;

    }
}