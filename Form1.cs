using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Simple_Text_Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonBold_Click(object sender, EventArgs e)
        {
            Font oldFont;
            Font newFont;

            // 首先获取之前的字体对象
            oldFont = this.richTextBoxText.SelectionFont;

            // 判断如果之前的字体已经是粗体，则新字体为正常，否则新字体为粗体
            if (oldFont.Bold)
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Bold);
            else
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Bold);

            //将新字体赋给richText中的选中的文本
            this.richTextBoxText.SelectionFont = newFont;
            this.richTextBoxText.Focus();
        }

        private void buttonUnderline_Click(object sender, EventArgs e)
        {
            Font oldFont;
            Font newFont;

            // 首先获取之前的字体对象
            oldFont = this.richTextBoxText.SelectionFont;

            // 判断如果之前的字体已经是下划线，则新字体为正常，否则新字体为下划线
            if (oldFont.Underline)
            {
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Underline);
            }
            else
            {
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Underline);
            }

            //将新字体赋给richText中的选中的文本
            this.richTextBoxText.SelectionFont = newFont;
            this.richTextBoxText.Focus();
        }

        private void buttonItalic_Click(object sender, EventArgs e)
        {
            Font oldFont;
            Font newFont;

            oldFont = this.richTextBoxText.SelectionFont;

            if (oldFont.Italic)
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Italic);
            else
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Italic);

            this.richTextBoxText.SelectionFont = newFont;
            this.richTextBoxText.Focus();
        }

        private void buttonCenter_Click(object sender, EventArgs e)
        {
            if (this.richTextBoxText.SelectionAlignment == HorizontalAlignment.Center)
                this.richTextBoxText.SelectionAlignment = HorizontalAlignment.Left;
            else
                this.richTextBoxText.SelectionAlignment = HorizontalAlignment.Center;
            this.richTextBoxText.Focus();
        }

        private void textBoxSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只允许输入 数字、删除、回车
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
                                                   e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
            else if (e.KeyChar == 13)
            {
                // 点击回车后，将字体大小应用
                TextBox txt = (TextBox)sender;

                if (txt.Text.Length > 0)
                    ApplyTextSize(txt.Text);
                e.Handled = true;
                this.richTextBoxText.Focus();
            }
        }

        private void textBoxSize_Validated(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            ApplyTextSize(txt.Text);
            this.richTextBoxText.Focus();
        }

        private void ApplyTextSize(string textSize)
        {
            
            float newSize = Convert.ToSingle(textSize);
            FontFamily currentFontFamily;
            Font newFont;

            // 首先获取当前选中文本的字体族
            currentFontFamily = this.richTextBoxText.SelectionFont.FontFamily;
            // 设置为新大小
            newFont = new Font(currentFontFamily, newSize);
      
         
            this.richTextBoxText.SelectionFont = newFont;
        }

        private void richTextBoxText_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            // 载入当前目录下的rtf文件
            try
            {
                richTextBoxText.LoadFile("Test.rtf");
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("No file to load yet");
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // 保存为rtf文件
            try
            {
                richTextBoxText.SaveFile("Test.rtf");
            }
            catch (System.Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Color userColor = colorDialog.Color;
                richTextBoxText.SelectionColor = userColor;
            }            
        }

        private void buttonFont_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                Font userFont = fontDialog.Font;
                this.richTextBoxText.SelectionFont = userFont;
                this.richTextBoxText.Focus();
            }
        }
    }
}
