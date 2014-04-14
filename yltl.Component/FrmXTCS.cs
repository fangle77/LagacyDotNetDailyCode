using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace yltl.UIComponent
{
    public partial class FrmXTCS : Form
    {
        public FrmXTCS()
        {
            InitializeComponent();
            InitDicBind();
        }

        private Dictionary<string, Control> _DicBind;
        private void InitDicBind()
        {
            _DicBind = new Dictionary<string, Control>()
            {
                {"XTMC",txtXTMC},
                {"DYSJCZBCQX",numDYSJCZBCQX},
                {"CDRZBCQX",numCDRZBCQX},
                {"XTRZBCQX",numXTRZBCQX},
                {"GGBCQX",numGGBCQX},
                {"SSSJBCQX",numSSSJBCQX},
                {"DBBFLJ",txtDBBFLJ},
                {"DBBFZJ",numDBBFZJ},
                {"BFTIME",dtpBFTIME},
                {"QCTIME",dtpQCTIME},
                {"BFCPZXSYKJ",numBFCPZXSYKJ},
                {"BFLASTTIME",txtBFLASTTIME},
                {"YCCDCGL",numYCCDCGL},
                {"ZQCJCGL",numZQCJCGL},
                {"WML",numWML}
            };
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {

        }

        private void btnChoosePath_Click(object sender, EventArgs e)
        {

        }

        private void dtpBFTIME_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
