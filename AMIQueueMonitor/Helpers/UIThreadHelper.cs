using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace AMIQueueMonitor.Helpers
{
    public static class UIThreadHelper
    {
        delegate void SetFormControlTextDelegate(ISynchronizeInvoke syncObject, Control ctrl, string text, Color? textColor);
        delegate void AppendTextBoxTextDelegate(ISynchronizeInvoke syncObject, RichTextBox textBox, string text, Color? textColor);
        delegate void SetToolStripBoxLabelTextDelegate(ISynchronizeInvoke syncObject, ToolStripLabel toolStripLabel, string text, Color? textColor);
        delegate void ShowFormDelegate(ISynchronizeInvoke syncObject, Form form);

        public static void SetFormControlText(ISynchronizeInvoke syncObject, Control control, string text, Color? textColor)
        {
            if (control.InvokeRequired)
            {
                SetFormControlTextDelegate d = SetFormControlText;
                syncObject.Invoke(d, new object[] { syncObject, control, text, textColor });
            }
            else
            {
                control.Text = text;
                if (textColor != null) control.ForeColor = textColor.Value;
            }
        }

        public static void AppendTextBoxText(ISynchronizeInvoke syncObject, RichTextBox textBox, string text, Color? textColor)
        {
            if (textBox.InvokeRequired)
            {
                AppendTextBoxTextDelegate d = AppendTextBoxText;
                syncObject.Invoke(d, new object[] { syncObject, textBox, text, textColor });
            }
            else
            {
                textBox.SelectionStart = textBox.Text.Length;
                var oldcolor = textBox.SelectionColor;
                if (textColor != null) textBox.SelectionColor = textColor.Value;
                textBox.AppendText(text + Environment.NewLine);
                textBox.SelectionColor = oldcolor;
                textBox.ScrollToCaret();
            }
        }

        public static void SetToolStripBoxLabelText(ISynchronizeInvoke syncObject, ToolStripLabel toolStripLabel, string text, Color? textColor)
        {
            if (toolStripLabel.GetCurrentParent().InvokeRequired)
            {
                SetToolStripBoxLabelTextDelegate d = SetToolStripBoxLabelText;
                syncObject.Invoke(d, new object[] { syncObject, toolStripLabel, text, textColor });
            }
            else
            {
                toolStripLabel.Text = text;
                if (textColor != null) toolStripLabel.ForeColor = textColor.Value;
            }
        }

        public static void ShowForm(ISynchronizeInvoke syncObject, Form form)
        {
            if (form.InvokeRequired)
            {
                ShowFormDelegate d = ShowForm;
                syncObject.Invoke(d, new object[] { syncObject, form });
            }
            else
            {
                if (form.WindowState == FormWindowState.Minimized)
                {
                    form.Show();
                    form.WindowState = FormWindowState.Normal;
                }
            }
        }
    }
}
