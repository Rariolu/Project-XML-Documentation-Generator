using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortfolioGeneratorBackend
{
    public partial class LogAlert : Form
    {
        private LogAlert(string text)
        {
            InitializeComponent();
            rtb.Text = text;
        }
        public static void Show(string text)
        {
            new LogAlert(text).Show();
        }
    }
}