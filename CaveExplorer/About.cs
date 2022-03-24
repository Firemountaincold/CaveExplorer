using System;
using System.Reflection;
using System.Windows.Forms;

namespace CaveExplorer
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            //载入软件版本
            Assembly ass = Assembly.GetEntryAssembly();
            AssemblyName an = ass.GetName();
            Version ver = an.Version;
            label3.Text += ver.ToString();
        }

        private void buttonok_Click(object sender, EventArgs e)
        {
            //确认按钮
            DialogResult = DialogResult.OK;
        }
    }
}
