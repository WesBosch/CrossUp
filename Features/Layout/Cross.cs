using CrossUp.Game.Hotbar;
using CrossUp.Utility;
using Dalamud.Interface.Utility;
using FFXIVClientStructs.FFXIV.Common.Math;
using FFXIVClientStructs.FFXIV.Component.GUI;
using System;
using static CrossUp.CrossUp;
using static CrossUp.Utility.Service;

namespace CrossUp.Features.Layout
{
    /// <summary>Methods for rearranging the main Cross Hotbar</summary>
    internal static class Cross
    {
        /// <summary>Arranges all elements of the main Cross Hotbar based on current selection status and other factors</summary>
        public static void Arrange(ActionCrossSelect select, ActionCrossSelect previous, float scale, int split, bool mixBar, bool arrangeEx, (int, int, int, int) coords, bool forceArrange, bool resetAll)
        {
            if (Profile.SplitOn) Recenter(scale);

            // Custom Offsets (I'm so sorry)  (( Like, REALLY sorry ))

            Vector2 l_LO, r_LO, u_LO, d_LO, a_LO, b_LO, x_LO, y_LO,
                    l_RO, r_RO, u_RO, d_RO, a_RO, b_RO, x_RO, y_RO,

                    l_LO_L, r_LO_L, u_LO_L, d_LO_L, a_LO_L, b_LO_L, x_LO_L, y_LO_L,
                    l_RO_L, r_RO_L, u_RO_L, d_RO_L, a_RO_L, b_RO_L, x_RO_L, y_RO_L,

                    l_LO_R, r_LO_R, u_LO_R, d_LO_R, a_LO_R, b_LO_R, x_LO_R, y_LO_R,
                    l_RO_R, r_RO_R, u_RO_R, d_RO_R, a_RO_R, b_RO_R, x_RO_R, y_RO_R,

                    a_LO_LR, b_LO_LR, x_LO_LR, y_LO_LR,
                    l_RO_LR, r_RO_LR, u_RO_LR, d_RO_LR,

                    a_LO_RL, b_LO_RL, x_LO_RL, y_LO_RL,
                    l_RO_RL, r_RO_RL, u_RO_RL, d_RO_RL,

                    dpad_LO, dpad_RO, dpad_LO_L, dpad_RO_L, dpad_LO_R, dpad_RO_R,
                    dpad_LO_LR, dpad_RO_LR, dpad_LO_RL, dpad_RO_RL,
                    face_LO, face_RO, face_LO_L, face_RO_L, face_LO_R, face_RO_R,
                    face_LO_LR, face_RO_LR, face_LO_RL, face_RO_RL;

                    l_LO = r_LO = u_LO = d_LO = a_LO = b_LO = x_LO = y_LO =
                    l_RO = r_RO = u_RO = d_RO = a_RO = b_RO = x_RO = y_RO =

                    l_LO_L = r_LO_L = u_LO_L = d_LO_L = a_LO_L = b_LO_L = x_LO_L = y_LO_L =
                    l_RO_L = r_RO_L = u_RO_L = d_RO_L = a_RO_L = b_RO_L = x_RO_L = y_RO_L =

                    l_LO_R = r_LO_R = u_LO_R = d_LO_R = a_LO_R = b_LO_R = x_LO_R = y_LO_R =
                    l_RO_R = r_RO_R = u_RO_R = d_RO_R = a_RO_R = b_RO_R = x_RO_R = y_RO_R =

                    a_LO_LR = b_LO_LR = x_LO_LR = y_LO_LR =
                    l_RO_LR = r_RO_LR = u_RO_LR = d_RO_LR =

                    a_LO_RL = b_LO_RL = x_LO_RL = y_LO_RL =
                    l_RO_RL = r_RO_RL = u_RO_RL = d_RO_RL = 

                    dpad_LO = dpad_RO = dpad_LO_L = dpad_RO_L = dpad_LO_R = dpad_RO_R =
                    dpad_LO_LR = dpad_RO_LR = dpad_LO_RL = dpad_RO_RL =
                    face_LO = face_RO = face_LO_L = face_RO_L = face_LO_R = face_RO_R =
                    face_LO_LR = face_RO_LR = face_LO_RL = face_RO_RL = Vector2.Zero;

            if (Profile.ConfigureSlots)
            {
                l_LO = Profile.Left_LeftOffset;
                r_LO = Profile.Right_LeftOffset;
                u_LO = Profile.Up_LeftOffset;
                d_LO = Profile.Down_LeftOffset;
                a_LO = Profile.A_LeftOffset;
                b_LO = Profile.B_LeftOffset;
                x_LO = Profile.X_LeftOffset;
                y_LO = Profile.Y_LeftOffset;
                l_RO = Profile.Left_RightOffset;
                r_RO = Profile.Right_RightOffset;
                u_RO = Profile.Up_RightOffset;
                d_RO = Profile.Down_RightOffset;
                a_RO = Profile.A_RightOffset;
                b_RO = Profile.B_RightOffset;
                x_RO = Profile.X_RightOffset;
                y_RO = Profile.Y_RightOffset;

                l_LO_L = Profile.Left_LeftOffset_L;
                r_LO_L = Profile.Right_LeftOffset_L;
                u_LO_L = Profile.Up_LeftOffset_L;
                d_LO_L = Profile.Down_LeftOffset_L;
                a_LO_L = Profile.A_LeftOffset_L;
                b_LO_L = Profile.B_LeftOffset_L;
                x_LO_L = Profile.X_LeftOffset_L;
                y_LO_L = Profile.Y_LeftOffset_L;
                l_RO_L = Profile.Left_RightOffset_L;
                r_RO_L = Profile.Right_RightOffset_L;
                u_RO_L = Profile.Up_RightOffset_L;
                d_RO_L = Profile.Down_RightOffset_L;
                a_RO_L = Profile.A_RightOffset_L;
                b_RO_L = Profile.B_RightOffset_L;
                x_RO_L = Profile.X_RightOffset_L;
                y_RO_L = Profile.Y_RightOffset_L;

                l_LO_R = Profile.Left_LeftOffset_R;
                r_LO_R = Profile.Right_LeftOffset_R;
                u_LO_R = Profile.Up_LeftOffset_R;
                d_LO_R = Profile.Down_LeftOffset_R;
                a_LO_R = Profile.A_LeftOffset_R;
                b_LO_R = Profile.B_LeftOffset_R;
                x_LO_R = Profile.X_LeftOffset_R;
                y_LO_R = Profile.Y_LeftOffset_R;
                l_RO_R = Profile.Left_RightOffset_R;
                r_RO_R = Profile.Right_RightOffset_R;
                u_RO_R = Profile.Up_RightOffset_R;
                d_RO_R = Profile.Down_RightOffset_R;
                a_RO_R = Profile.A_RightOffset_R;
                b_RO_R = Profile.B_RightOffset_R;
                x_RO_R = Profile.X_RightOffset_R;
                y_RO_R = Profile.Y_RightOffset_R;

                a_LO_LR = Profile.A_LeftOffset_LR;
                b_LO_LR = Profile.B_LeftOffset_LR;
                x_LO_LR = Profile.X_LeftOffset_LR;
                y_LO_LR = Profile.Y_LeftOffset_LR;
                l_RO_LR = Profile.Left_RightOffset_LR;
                r_RO_LR = Profile.Right_RightOffset_LR;
                u_RO_LR = Profile.Up_RightOffset_LR;
                d_RO_LR = Profile.Down_RightOffset_LR;

                a_LO_RL = Profile.A_LeftOffset_RL;
                b_LO_RL = Profile.B_LeftOffset_RL;
                x_LO_RL = Profile.X_LeftOffset_RL;
                y_LO_RL = Profile.Y_LeftOffset_RL;
                l_RO_RL = Profile.Left_RightOffset_RL;
                r_RO_RL = Profile.Right_RightOffset_RL;
                u_RO_RL = Profile.Up_RightOffset_RL;
                d_RO_RL = Profile.Down_RightOffset_RL;

                dpad_LO = Profile.Dpad_LeftOffset;
                dpad_RO = Profile.Dpad_RightOffset;
                face_LO = Profile.Face_LeftOffset;
                face_RO = Profile.Face_RightOffset;
                dpad_LO_L = Profile.Dpad_LeftOffset_L;
                dpad_RO_L = Profile.Dpad_RightOffset_L;
                face_LO_L = Profile.Face_LeftOffset_L;
                face_RO_L = Profile.Face_RightOffset_L;
                dpad_LO_R = Profile.Dpad_LeftOffset_R;
                dpad_RO_R = Profile.Dpad_RightOffset_R;
                face_LO_R = Profile.Face_LeftOffset_R;
                face_RO_R = Profile.Face_RightOffset_R;
                dpad_LO_LR = Profile.Dpad_LeftOffset_LR;
                dpad_RO_LR = Profile.Dpad_RightOffset_LR;
                face_LO_LR = Profile.Face_LeftOffset_LR;
                face_RO_LR = Profile.Face_RightOffset_LR;
                dpad_LO_RL = Profile.Dpad_LeftOffset_RL;
                dpad_RO_RL = Profile.Dpad_RightOffset_RL;
                face_LO_RL = Profile.Face_LeftOffset_RL;
                face_RO_RL = Profile.Face_RightOffset_RL;
            }

            Bars.Cross.Root.SetPos(Bars.Cross.Base.X - split * scale, Bars.Cross.Base.Y)
                           .SetSize((ushort)(float)(588 + split * 2), 210);

            ushort? lineSize = split != 0 || SeparateEx.Ready && !resetAll ? 0 : null;
            Bars.Cross.VertLine.SetSize(lineSize, lineSize);
            Bars.Cross.Padlock.SetRelativePos(Profile.PadlockOffset.X + split, Profile.PadlockOffset.Y);
            Bars.Cross.Padlock[2u].SetVis(!Profile.HidePadlock);
            Bars.Cross.SetDisplay.SetVis(!Profile.HideSetText)
                                 .SetRelativePos(Profile.SetTextOffset.X + split, Profile.SetTextOffset.Y);
            Bars.Cross.ChangeSetDisplay.Container.SetRelativePos(Profile.ChangeSetOffset.X + split, Profile.ChangeSetOffset.Y);
            Bars.Cross.LTtext.SetScale(Profile.HideTriggerText ? 0F : 1F);
            Bars.Cross.RTtext.SetScale(Profile.HideTriggerText ? 0F : 1F);

            UnassignedSlotVis(resetAll || !Profile.HideUnassigned);

            if (!forceArrange && select == previous) return;

            var (lrX, lrY, rlX, rlY) = coords;
            var miniSize = (ushort)(Profile.SelectStyle == 2 || (mixBar && split > 0) ? 0 : 166);
            switch (select)
            {
                case ActionCrossSelect.None:
                case ActionCrossSelect.DoubleCrossLeft:
                case ActionCrossSelect.DoubleCrossRight:
                default:
                    Bars.Cross.Container.SetRelativePos();
                    Bars.Cross.LTtext.SetRelativePos();
                    Bars.Cross.RTtext.SetRelativePos(split * 2);

                    Bars.Cross.Buttons[0].ChildVis(true).SetRelativePos(dpad_LO.X, dpad_LO.Y);
                    Bars.Cross.Buttons[1].ChildVis(true).SetRelativePos(face_LO.X, face_LO.Y);
                    Bars.Cross.Buttons[2].ChildVis(true).SetRelativePos((split * 2) + dpad_RO.X, dpad_RO.Y);
                    Bars.Cross.Buttons[3].ChildVis(true).SetRelativePos((split * 2) + face_RO.X, face_RO.Y);
                    SetButtonLocations(0, 0, d_LO.X, d_LO.Y); // Down
                    SetButtonLocations(0, 1, r_LO.X, r_LO.Y); // Right
                    SetButtonLocations(0, 2, u_LO.X, u_LO.Y); // Up
                    SetButtonLocations(0, 3, l_LO.X, l_LO.Y); // Left
                    SetButtonLocations(1, 0, a_LO.X, a_LO.Y); // A
                    SetButtonLocations(1, 1, b_LO.X, b_LO.Y); // B 
                    SetButtonLocations(1, 2, y_LO.X, y_LO.Y); // Y
                    SetButtonLocations(1, 3, x_LO.X, x_LO.Y); // X
                    SetButtonLocations(2, 0, d_RO.X, d_RO.Y); // Down
                    SetButtonLocations(2, 1, r_RO.X, r_RO.Y); // Right
                    SetButtonLocations(2, 2, u_RO.X, u_RO.Y); // Up
                    SetButtonLocations(2, 3, l_RO.X, l_RO.Y); // Left
                    SetButtonLocations(3, 0, a_RO.X, a_RO.Y); // A
                    SetButtonLocations(3, 1, b_RO.X, b_RO.Y); // B 
                    SetButtonLocations(3, 2, y_RO.X, y_RO.Y); // Y
                    SetButtonLocations(3, 3, x_RO.X, x_RO.Y); // X
                    break;
                case ActionCrossSelect.Left:
                    Bars.Cross.Container.SetRelativePos();
                    Bars.Cross.LTtext.SetRelativePos();
                    Bars.Cross.RTtext.SetRelativePos(split * 2);

                    var fromLR = previous == ActionCrossSelect.LR && arrangeEx;
                    Bars.Cross.Buttons[0].ChildVis(true).SetRelativePos(dpad_LO_L.X, dpad_LO_L.Y);
                    Bars.Cross.Buttons[1].ChildVis(!fromLR || !mixBar).SetRelativePos(face_LO_L.X, face_LO_L.Y);
                    Bars.Cross.Buttons[2].ChildVis(!fromLR || mixBar).SetRelativePos((split * 2) + dpad_RO_L.X, dpad_RO_L.Y);
                    Bars.Cross.Buttons[3].ChildVis(!fromLR).SetRelativePos((split * 2) + face_RO_L.X, face_RO_L.Y);
                    SetButtonLocations(0, 0, d_LO_L.X, d_LO_L.Y); // Down
                    SetButtonLocations(0, 1, r_LO_L.X, r_LO_L.Y); // Right
                    SetButtonLocations(0, 2, u_LO_L.X, u_LO_L.Y); // Up
                    SetButtonLocations(0, 3, l_LO_L.X, l_LO_L.Y); // Left
                    SetButtonLocations(1, 0, a_LO_L.X, a_LO_L.Y); // A
                    SetButtonLocations(1, 1, b_LO_L.X, b_LO_L.Y); // B 
                    SetButtonLocations(1, 2, y_LO_L.X, y_LO_L.Y); // Y
                    SetButtonLocations(1, 3, x_LO_L.X, x_LO_L.Y); // X
                    SetButtonLocations(2, 0, d_RO_L.X, d_RO_L.Y); // Down
                    SetButtonLocations(2, 1, r_RO_L.X, r_RO_L.Y); // Right
                    SetButtonLocations(2, 2, u_RO_L.X, u_RO_L.Y); // Up
                    SetButtonLocations(2, 3, l_RO_L.X, l_RO_L.Y); // Left
                    SetButtonLocations(3, 0, a_RO_L.X, a_RO_L.Y); // A
                    SetButtonLocations(3, 1, b_RO_L.X, b_RO_L.Y); // B 
                    SetButtonLocations(3, 2, y_RO_L.X, y_RO_L.Y); // Y
                    SetButtonLocations(3, 3, x_RO_L.X, x_RO_L.Y); // X

                    Bars.Cross.MiniSelectL.SetSize(miniSize, 140);
                    Bars.Cross.MiniSelectR.SetSize(miniSize, 140);
                    break;

                case ActionCrossSelect.Right:
                    Bars.Cross.Container.SetRelativePos(split * 2);
                    Bars.Cross.LTtext.SetRelativePos(-split * 2);
                    Bars.Cross.RTtext.SetRelativePos();

                    var fromRL = previous == ActionCrossSelect.RL && arrangeEx;
                    Bars.Cross.Buttons[0].ChildVis(!fromRL).SetRelativePos((-split * 2) + dpad_LO_R.X, dpad_LO_R.Y);
                    Bars.Cross.Buttons[1].ChildVis(!fromRL || mixBar).SetRelativePos((-split * 2) + face_LO_R.X, face_LO_R.Y);
                    Bars.Cross.Buttons[2].ChildVis(!fromRL || !mixBar).SetRelativePos(dpad_RO_R.X, dpad_RO_R.Y);
                    Bars.Cross.Buttons[3].ChildVis(true).SetRelativePos(face_RO_R.X, face_RO_R.Y);
                    SetButtonLocations(0, 0, d_LO_R.X, d_LO_R.Y); // Down
                    SetButtonLocations(0, 1, r_LO_R.X, r_LO_R.Y); // Right
                    SetButtonLocations(0, 2, u_LO_R.X, u_LO_R.Y); // Up
                    SetButtonLocations(0, 3, l_LO_R.X, l_LO_R.Y); // Left
                    SetButtonLocations(1, 0, a_LO_R.X, a_LO_R.Y); // A
                    SetButtonLocations(1, 1, b_LO_R.X, b_LO_R.Y); // B 
                    SetButtonLocations(1, 2, y_LO_R.X, y_LO_R.Y); // Y
                    SetButtonLocations(1, 3, x_LO_R.X, x_LO_R.Y); // X
                    SetButtonLocations(2, 0, d_RO_R.X, d_RO_R.Y); // Down
                    SetButtonLocations(2, 1, r_RO_R.X, r_RO_R.Y); // Right
                    SetButtonLocations(2, 2, u_RO_R.X, u_RO_R.Y); // Up
                    SetButtonLocations(2, 3, l_RO_R.X, l_RO_R.Y); // Left
                    SetButtonLocations(3, 0, a_RO_R.X, a_RO_R.Y); // A
                    SetButtonLocations(3, 1, b_RO_R.X, b_RO_R.Y); // B 
                    SetButtonLocations(3, 2, y_RO_R.X, y_RO_R.Y); // Y
                    SetButtonLocations(3, 3, x_RO_R.X, x_RO_R.Y); // X

                    Bars.Cross.MiniSelectL.SetSize(miniSize, 140);
                    Bars.Cross.MiniSelectR.SetSize(miniSize, 140);
                    break;

                case ActionCrossSelect.LR:
                    Bars.Cross.Container.SetRelativePos(lrX + split, lrY);
                    Bars.Cross.LTtext.SetRelativePos(-lrX - split, -lrY);
                    Bars.Cross.RTtext.SetRelativePos(-lrX + split, -lrY);

                    Bars.Cross.Buttons[0].ChildVis(false).SetRelativePos(dpad_LO_LR.X, dpad_LO_LR.Y);
                    Bars.Cross.Buttons[1].ChildVis(true).SetRelativePos(face_LO_LR.X, face_LO_LR.Y);
                    Bars.Cross.Buttons[2].ChildVis(true).SetRelativePos(dpad_RO_LR.X, dpad_RO_LR.Y);
                    Bars.Cross.Buttons[3].ChildVis(false).SetRelativePos(face_RO_LR.X, face_RO_LR.Y);
                    SetButtonLocations(1, 0, a_LO_LR.X, a_LO_LR.Y); // A
                    SetButtonLocations(1, 1, b_LO_LR.X, b_LO_LR.Y); // B 
                    SetButtonLocations(1, 2, y_LO_LR.X, y_LO_LR.Y); // Y
                    SetButtonLocations(1, 3, x_LO_LR.X, x_LO_LR.Y); // X
                    SetButtonLocations(2, 0, d_RO_LR.X, d_RO_LR.Y); // Down
                    SetButtonLocations(2, 1, r_RO_LR.X, r_RO_LR.Y); // Right
                    SetButtonLocations(2, 2, u_RO_LR.X, u_RO_LR.Y); // Up
                    SetButtonLocations(2, 3, l_RO_LR.X, l_RO_LR.Y); // Left
                    break;

                case ActionCrossSelect.RL:
                    Bars.Cross.Container.SetRelativePos(rlX + split, rlY);
                    Bars.Cross.LTtext.SetRelativePos(-rlX - split, -rlY);
                    Bars.Cross.RTtext.SetRelativePos(-rlX + split, -rlY);

                    Bars.Cross.Buttons[0].ChildVis(false).SetRelativePos(dpad_LO_RL.X, dpad_LO_RL.Y);
                    Bars.Cross.Buttons[1].ChildVis(true).SetRelativePos(face_LO_RL.X, face_LO_RL.Y);
                    Bars.Cross.Buttons[2].ChildVis(true).SetRelativePos(dpad_RO_RL.X, dpad_RO_RL.Y);
                    Bars.Cross.Buttons[3].ChildVis(false).SetRelativePos(face_RO_RL.X, face_RO_RL.Y);
                    SetButtonLocations(1, 0, a_LO_RL.X, a_LO_RL.Y); // A
                    SetButtonLocations(1, 1, b_LO_RL.X, b_LO_RL.Y); // B 
                    SetButtonLocations(1, 2, y_LO_RL.X, y_LO_RL.Y); // Y
                    SetButtonLocations(1, 3, x_LO_RL.X, x_LO_RL.Y); // X
                    SetButtonLocations(2, 0, d_RO_RL.X, d_RO_RL.Y); // Down
                    SetButtonLocations(2, 1, r_RO_RL.X, r_RO_RL.Y); // Right
                    SetButtonLocations(2, 2, u_RO_RL.X, u_RO_RL.Y); // Up
                    SetButtonLocations(2, 3, l_RO_RL.X, l_RO_RL.Y); // Left
                    break;
            }
        }

        /// <summary>Sets the visibility of empty slots on the Cross Hotbar</summary>
        public static void UnassignedSlotVis(bool show)
        {
            var scale = show ? 1F : 0F;

            for (var set = 0; set < 4; set++)
                for (uint bID = 2; bID <= 5; bID++)
                {
                    Bars.Cross.Buttons[set][bID][3u].SetScale(scale);
                    Bars.WXHB.Buttons[set][bID][3u].SetScale(scale);
                }
        }

        public static unsafe void SetButtonLocations(int group, int button, float x, float y)
        {
            var defaultPos = Vector2.Zero;

            /*if (button == 0) { defaultPos = new Vector2(42, 53); }
            if (button == 1) { defaultPos = new Vector2(84, 29); }
            if (button == 2) { defaultPos = new Vector2(42, 5); }
            if (button == 3) { defaultPos = new Vector2(0, 29); }*/


            Bars.Cross.Buttons[group][button].SetPos(defaultPos.X + x, defaultPos.Y + y);

        }

        /// <summary>Overrides HUD settings to horizontally recenter Cross Hotbar at user-selected position</summary>
        private static void Recenter(float scale)
        {
            var baseX = (short)((ImGuiHelpers.MainViewport.Size.X - 588 * scale) / 2 + Profile.CenterPoint);
            var misalign = Bars.Cross.Base.X - baseX;
            if (Math.Abs(misalign) < 1) return;

            Bars.Cross.Base.X = baseX;
            Log.Verbose($"Realigning Cross Hotbar to Center Point {Profile.CenterPoint} (was off by {misalign})");
        }

        /// <summary>Restores everything back to default</summary>
        public static void Reset()
        {
            Bars.Cross.VertLine.SetSize();
            Bars.Cross.Padlock.SetRelativePos();
            Bars.Cross.Padlock[2u].SetVis(true);
            Bars.Cross.SetDisplay.SetVis(true).SetRelativePos();
            Bars.Cross.ChangeSetDisplay.Container.SetRelativePos();
            Bars.Cross.LTtext.SetScale();
            Bars.Cross.RTtext.SetScale();
            Bars.Cross.MiniSelectL.SetSize();
            Bars.Cross.MiniSelectR.SetSize();
        }
    }
}