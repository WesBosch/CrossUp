using CrossUp.Game.Hotbar;
using Dalamud.Interface.Utility;
using FFXIVClientStructs.FFXIV.Common.Math;
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

            // Custom Offsets (I'm so sorry)

            Vector2 l_LO, r_LO, u_LO, d_LO, a_LO, b_LO, x_LO, y_LO,
                    l_RO, r_RO, u_RO, d_RO, a_RO, b_RO, x_RO, y_RO,
                    dpad_LO, dpad_RO, dpad_LO_L, dpad_RO_L, dpad_LO_R, dpad_RO_R,
                    dpad_LO_LR, dpad_RO_LR, dpad_LO_RL, dpad_RO_RL,
                    face_LO, face_RO, face_LO_L, face_RO_L, face_LO_R, face_RO_R,
                    face_LO_LR, face_RO_LR, face_LO_RL, face_RO_RL;

                    l_LO = r_LO = u_LO = d_LO = a_LO = b_LO = x_LO = y_LO =
                    l_RO = r_RO = u_RO = d_RO = a_RO = b_RO = x_RO = y_RO =
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
                    break;

                case ActionCrossSelect.RL:
                    Bars.Cross.Container.SetRelativePos(rlX + split, rlY);
                    Bars.Cross.LTtext.SetRelativePos(-rlX - split, -rlY);
                    Bars.Cross.RTtext.SetRelativePos(-rlX + split, -rlY);

                    Bars.Cross.Buttons[0].ChildVis(false).SetRelativePos(dpad_LO_RL.X, dpad_LO_RL.Y);
                    Bars.Cross.Buttons[1].ChildVis(true).SetRelativePos(face_LO_RL.X, face_LO_RL.Y);
                    Bars.Cross.Buttons[2].ChildVis(true).SetRelativePos(dpad_RO_RL.X, dpad_RO_RL.Y);
                    Bars.Cross.Buttons[3].ChildVis(false).SetRelativePos(face_RO_RL.X, face_RO_RL.Y);
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