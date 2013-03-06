using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace FIFAUltimateTeamBot
{
    public static class Extensions
    {
        public static void AppendRTFText(this RichTextBox box, string text)
        {
            AppendRTFText(box, text, box.ForeColor);
        }
        public static void AppendRTFText(this RichTextBox box, string text, Color color)
        {
            //Place the marker.
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            //Set the color.
            box.SelectionColor = color;

            //Append the RTF text.
            box.SelectedRtf = text;

            //Revert to default color.
            box.SelectionColor = box.ForeColor;
        }
    }
}
