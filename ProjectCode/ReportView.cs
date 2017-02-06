using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class ReportView : Form
    {
        public ReportView()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            //LinkReport(CrystalDecisions.CrystalReports.Engine.ReportDocument cr);   
        }
        public void LinkReport(CrystalDecisions.CrystalReports.Engine.ReportDocument cr)
        {
            crystalReportViewer1.ReportSource = cr;
        }
    }
}
