using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace yltl.UIComponent
{
    public partial class Form1 : yltl.UIComponent.ViewForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            OnAdd = new Action(() =>
            {
                EditForm ef = new EditForm();
                ef.EditFormMode = eEditFormMode.View;
                ef.Show();
            });
        }
    }
}
