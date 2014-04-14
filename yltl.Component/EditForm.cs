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
    public partial class EditForm : Form
    {
        public EditForm()
        {
            InitializeComponent();
        }
        private eEditFormMode _EditFormMode;
        public eEditFormMode EditFormMode
        {
            get { return _EditFormMode; }
            set
            {
                _EditFormMode = value;
                switch (_EditFormMode)
                {
                    case eEditFormMode.View:
                        btnSave.Visible = false;
                        btnAdd.Visible = true;
                        btnUpdate.Visible = true;
                        break;
                    case eEditFormMode.New:
                    case eEditFormMode.Edit:
                        btnAdd.Visible = false;
                        btnUpdate.Visible = false;
                        btnSave.Visible = true;
                        break;
                }
            }
        }

        private Dictionary<string, string> _dicField_value;

        public event Action<bool> OnInfoChanged;

        private void EditForm_Load(object sender, EventArgs e)
        {
        }

        #region 按钮
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.EditFormMode = eEditFormMode.New;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.EditFormMode = eEditFormMode.Edit;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.EditFormMode = eEditFormMode.View;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        protected void InfoChanged(bool isChanged)
        {
            if (OnInfoChanged != null) OnInfoChanged(isChanged);
        }

        protected virtual Dictionary<string, Control> FieldControlsMapping { get; set; }

        protected virtual void Bind(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;

            var dic = FieldControlsMapping;
            if (dic == null || dic.Count == 0) return;
            if (_dicField_value == null) _dicField_value = new Dictionary<string, string>();

            foreach (string key in dic.Keys)
            {
                if (dt.Columns[key] == null) continue;
                dic[key].Text = dt.Rows[0][key].ToString();

                if (_dicField_value.ContainsKey(key))
                {
                    _dicField_value[key] = dic[key].Text;
                }
                else
                {
                    _dicField_value.Add(key, dic[key].Text);
                }
            }
        }

        protected bool IsValuesChanged()
        {
            var dic = FieldControlsMapping;
            if (dic == null || dic.Count == 0) return false;
            if (_dicField_value == null) return false;

            foreach (string key in dic.Keys)
            {
                if (_dicField_value[key] != dic[key].Text) return true;
            }
            return false;
        }
    }

    public enum eEditFormMode
    {
        View,
        New,
        Edit
    }
}
