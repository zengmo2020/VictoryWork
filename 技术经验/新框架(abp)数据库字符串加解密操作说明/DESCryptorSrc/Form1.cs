using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESCryptor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                var input = txtInput.Text;
                if (string.IsNullOrEmpty(input))
                {
                    txtOutput.Text = "请输入明/密文！";
                    return;
                }

                txtOutput.Text = DESCryptor.EncryptString(input);
            }
            catch (Exception ex)
            {
                txtOutput.Text = "加密失败！异常信息：" + ex.Message;
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                var input = txtInput.Text;
                if (string.IsNullOrEmpty(input))
                {
                    txtOutput.Text = "请输入明/密文！";
                    return;
                }

                txtOutput.Text = DESCryptor.DecryptString(input);
            }
            catch (Exception ex)
            {
                txtOutput.Text = "解密失败！异常信息：" + ex.Message;
            }
        }
    }
}
