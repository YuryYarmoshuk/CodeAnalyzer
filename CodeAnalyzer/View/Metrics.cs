using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace CodeAnalyzer.View
{
    public partial class Metrics : Form
    {
        public Metrics()
        {
            InitializeComponent();
        }

        private void Metrics_Load(object sender, EventArgs e)
        {
            MetricsImage.Image = Properties.Resources.MetricsRe;
        }
    }
}
