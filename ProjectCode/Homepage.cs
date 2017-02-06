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
    public partial class Homepage : Form
    {
        public Homepage()
        {
            InitializeComponent();
        }

        private void branchMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BranchMaster bmfrm = new BranchMaster();
            bmfrm.Show();
        }

        private void admissionFeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ADMISSION_FEE af = new ADMISSION_FEE();
            af.Show();
        }

        private void shareContributionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SHARE_CONTRIBUTION sf = new SHARE_CONTRIBUTION();
            sf.Show();
        }

        private void mBFContributionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MBF_CONTRIBUTION mbf = new MBF_CONTRIBUTION();
            mbf.Show();
        
        }

        private void mRFContributionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MRF_CONTRIBUTION mrf = new MRF_CONTRIBUTION();
            mrf.Show();
        }

        private void membershipFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Membership_Form mrsf = new Membership_Form();
            mrsf.Show();
        }

        private void mARFContributionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            marf_contribtion marf = new marf_contribtion();
            marf.Show();
        }

        private void subscriptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            subscriptions sub = new subscriptions();
            sub.Show();
        }

        private void memberLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemberLedger ml = new MemberLedger();
            ml.Show();
        }

        private void shortTermLoanEligibilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            short_term_loan_eligibility stle = new short_term_loan_eligibility();
            stle.Show();
        }

        private void loanApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoanApplication la = new LoanApplication();
            la.Show();
        }

        private void lToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoanVerification lv = new LoanVerification();
            lv.Show();
        }

        private void shortTermLoanIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            St_loanIssue sl = new St_loanIssue();
            sl.Show();
        }

        private void loanApplicationReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoanApplicationReport lar = new LoanApplicationReport();
            lar.Show();
        }

        private void temporaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Total_Loans tl = new Total_Loans();
            tl.Show();
        }

        private void issuedLoanDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IssuedLoanDetails ild = new IssuedLoanDetails();
            ild.Show();
        }

        private void deathApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeathApplication da = new DeathApplication();
            da.Show();
        }

        private void mBFEligibilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MBF_ELIGIBILITY me = new MBF_ELIGIBILITY();
            me.Show();
        }

        private void settlementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settlement st = new settlement();
            st.Show();
        }    
    }
}
