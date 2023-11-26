using System;
using CrossUp.Commands;
using CrossUp.Features;
using CrossUp.Features.Layout;
using CrossUp.Game.Hooks;
using CrossUp.Utility;
using Dalamud.Interface;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Components;
using FFXIVClientStructs.FFXIV.Common.Math;
using ImGuiNET;
using static CrossUp.CrossUp;
using static CrossUp.Utility.Service;

namespace CrossUp.UI.Tabs;

internal class SlotConfig
{
    public static void DrawTab()
    {
        if (!ImGui.BeginTabItem(Strings.SlotConfig.TabTitle)) return;

        ImGui.Spacing();

        if (ImGui.BeginTabBar("SlotConfigSubTabs"))
        {
            if (ImGui.BeginTabItem(Strings.SlotConfig.Config))
            {

                ImGui.Spacing();
                ImGui.Indent(10);

                if (ImGui.BeginTable("Layout", 6, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.ScrollX))
                {
                    ImGui.TableSetupColumn("labels", ImGuiTableColumnFlags.WidthFixed);
                    ImGui.TableSetupColumn("controls", ImGuiTableColumnFlags.WidthFixed);
                    ImGui.TableSetupColumn("reset", ImGuiTableColumnFlags.WidthFixed);
                    ImGui.TableSetupColumn("labels2", ImGuiTableColumnFlags.WidthFixed);
                    ImGui.TableSetupColumn("controls2", ImGuiTableColumnFlags.WidthFixed);
                    ImGui.TableSetupColumn("reset2", ImGuiTableColumnFlags.WidthFixed);

                    ImGui.TableNextRow();
                    ImGui.TableNextColumn();
                    Rows.MainSettings();
                    Rows.LeftHeld();
                    Rows.RightHeld();
                    Rows.LeftToRight();
                    Rows.RightToLeft();

                    ImGui.EndTable();
                }

                ImGui.EndTabItem();
            }

            ImGui.EndTabBar();
        }

        HudOptions.ProfileIndicator();
        ImGui.EndTabItem();
    }

    private static class Rows
    {
        public static void MainSettings()
        {
            var psConfig = Profile.PSConfig;

            // Idle

            ImGui.TextColored(Helpers.DimColor, Strings.SlotConfig.idle.ToUpper());
            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            ImGui.TextColored(Helpers.DimColor, Strings.SlotConfig.LeftHotbar);
                ImGui.TableNextColumn();
                ImGui.TableNextColumn();
                ImGui.TableNextColumn();
                ImGui.TextColored(Helpers.DimColor, Strings.SlotConfig.RightHotbar);
                ImGui.TableNextRow();
                ImGui.TableNextColumn();

            GroupSettings(Profile.Dpad_LeftOffset, "idle", "left", "dpad");
            GroupSettings(Profile.Dpad_RightOffset, "idle", "right", "dpad");
            GroupSettings(Profile.Face_LeftOffset, "idle", "left", "face", psConfig);
            GroupSettings(Profile.Face_RightOffset, "idle", "right", "face", psConfig);

                ImGui.TableNextRow();
                ImGui.TableNextColumn();
        }

        public static void LeftHeld()
        {
            var psConfig = Profile.PSConfig;

            // Left Held

            ImGui.TextColored(Helpers.DimColor, Strings.SlotConfig.leftheld.ToUpper());
            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            ImGui.TextColored(Helpers.DimColor, Strings.SlotConfig.LeftHotbar);
                ImGui.TableNextColumn();
                ImGui.TableNextColumn();
                ImGui.TableNextColumn();
                ImGui.TextColored(Helpers.DimColor, Strings.SlotConfig.RightHotbar);
                ImGui.TableNextRow();
                ImGui.TableNextColumn();

            GroupSettings(Profile.Dpad_LeftOffset_L, "leftheld", "left", "dpad");
            GroupSettings(Profile.Dpad_RightOffset_L, "leftheld", "right", "dpad");
            GroupSettings(Profile.Face_LeftOffset_L, "leftheld", "left", "face", psConfig);
            GroupSettings(Profile.Face_RightOffset_L, "leftheld", "right", "face", psConfig);

            ImGui.TableNextRow();
                ImGui.TableNextColumn();
        }

        public static void RightHeld()
        {
            var psConfig = Profile.PSConfig;

            // Right Held

            ImGui.TextColored(Helpers.DimColor, Strings.SlotConfig.rightheld.ToUpper());
            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            ImGui.TextColored(Helpers.DimColor, Strings.SlotConfig.LeftHotbar);
                ImGui.TableNextColumn();
                ImGui.TableNextColumn();
                ImGui.TableNextColumn();
                ImGui.TextColored(Helpers.DimColor, Strings.SlotConfig.RightHotbar);
                ImGui.TableNextRow();
                ImGui.TableNextColumn();

            GroupSettings(Profile.Dpad_LeftOffset_R, "rightheld", "left", "dpad");
            GroupSettings(Profile.Dpad_RightOffset_R, "rightheld", "right", "dpad");
            GroupSettings(Profile.Face_LeftOffset_R, "rightheld", "left", "face", psConfig);
            GroupSettings(Profile.Face_RightOffset_R, "rightheld", "right", "face", psConfig);

            ImGui.TableNextRow();
                ImGui.TableNextColumn();
        }

        public static void LeftToRight()
        {
            var psConfig = Profile.PSConfig;

            // Left to Right

            ImGui.TextColored(Helpers.DimColor, Strings.SlotConfig.lefttoright.ToUpper());
            ImGui.TableNextRow();
            ImGui.TableNextColumn();

            // Square is so silly
            GroupSettings(Profile.Face_LeftOffset_LR, "lefttoright", "left", "dpad");            // Dpad
            GroupSettings(Profile.Dpad_RightOffset_LR, "lefttoright", "left", "face", psConfig); // Face

            ImGui.TableNextRow();
                ImGui.TableNextColumn();
        }

        public static void RightToLeft()
        {
            var psConfig = Profile.PSConfig;

            // Right to Left

            ImGui.TextColored(Helpers.DimColor, Strings.SlotConfig.righttoleft.ToUpper());
            ImGui.TableNextRow();
            ImGui.TableNextColumn();

            // Square is so silly
            GroupSettings(Profile.Face_LeftOffset_RL, "righttoleft", "left", "dpad");            // Dpad
            GroupSettings(Profile.Dpad_RightOffset_RL, "righttoleft", "left", "face", psConfig); // Face

            ImGui.TableNextRow();
                ImGui.TableNextColumn();
        }

        public static void GroupSettings(Vector2 value, string status, string hotbar, string slot, bool ps = false)
        {
            var offset = new System.Numerics.Vector2(0, 0); // why..?
            var reset = new System.Numerics.Vector2(0, 0);
            offset = value;

            switch (slot)
            {
                case "dpad":
                    ImGui.TextColored(Helpers.HighlightColor, Strings.SlotConfig.Dpad);
                    break;
                case "face":
                    if (ps) { ImGui.TextColored(Helpers.HighlightColor, Strings.SlotConfig.FacePS); }
                    else { ImGui.TextColored(Helpers.HighlightColor, Strings.SlotConfig.Face); }
                    break;

                case "left":
                    ImGui.TextColored(Helpers.HighlightColor, Strings.SlotConfig.Left_Dpad);
                    break;
                case "right":
                    ImGui.TextColored(Helpers.HighlightColor, Strings.SlotConfig.Right_Dpad);
                    break;
                case "up":
                    ImGui.TextColored(Helpers.HighlightColor, Strings.SlotConfig.Up_Dpad);
                    break;
                case "down":
                    ImGui.TextColored(Helpers.HighlightColor, Strings.SlotConfig.Down_Dpad);
                    break;

                case "a":
                    if (ps) { ImGui.TextColored(Helpers.HighlightColor, Strings.SlotConfig.Ex_Button); }
                    else { ImGui.TextColored(Helpers.HighlightColor, Strings.SlotConfig.A_Button); }
                    break;
                case "b":
                    if (ps) { ImGui.TextColored(Helpers.HighlightColor, Strings.SlotConfig.Cir_Button); }
                    else { ImGui.TextColored(Helpers.HighlightColor, Strings.SlotConfig.B_Button); }
                    break;
                case "x":
                    if (ps) { ImGui.TextColored(Helpers.HighlightColor, Strings.SlotConfig.Squ_Button); }
                    else { ImGui.TextColored(Helpers.HighlightColor, Strings.SlotConfig.X_Button); }
                    break;
                case "y":
                    if (ps) { ImGui.TextColored(Helpers.HighlightColor, Strings.SlotConfig.Tri_Button); }
                    else { ImGui.TextColored(Helpers.HighlightColor, Strings.SlotConfig.Y_Button); }
                    break;
            }

            ImGui.TableNextColumn();

            ImGui.PushID($"Reset##{status}_{hotbar}_{slot}");
            if (ImGuiComponents.IconButton(FontAwesomeIcon.UndoAlt)) InternalCmd.SaveSlot(status, hotbar, slot, reset);
            ImGui.PopID();

            ImGui.TableNextColumn();
            ImGui.SetNextItemWidth(90 * Helpers.Scale);
            if (ImGui.DragFloat2($"##{status}_{hotbar}_{slot}", ref offset, 1, -9999, 9999, "%g")) InternalCmd.SaveSlot(status, hotbar, slot, offset);

            //ImGui.TableNextRow();
            ImGui.TableNextColumn();

        }
    }
}
