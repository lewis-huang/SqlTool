using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using System.Threading.Tasks;

namespace SqlTool
{
    
    public partial class Form1 : Form
    {

        string instanceconnectionstring;
        public Server dbserver;
        public DataSet localset;
        public DataTable tblServerProperties;
        public Form1()
        {
            InitializeComponent();
            localset = new DataSet("localset");
            tblServerProperties = localset.Tables.Add("ServerProperties");
            tblServerProperties.Columns.Add("ServerPropertyName");
            tblServerProperties.Columns["ServerPropertyName"].DataType = typeof(string);
            tblServerProperties.Columns.Add("ServerPropertyValue");
            tblServerProperties.Columns["ServerPropertyValue"].DataType = typeof(string);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            instanceconnectionstring = textBox1.Text;
             dbserver = new Server(instanceconnectionstring);
            
            
            
            dgvServerProperties.DataSource = localset;
            dgvServerProperties.DataMember = localset.Tables["ServerProperties"].ToString();
          
            Task search = new Task( searchDBProperties);
            search.Start();

            
           

        }

        public void searchDBProperties()
        {
            foreach (Database mydb in dbserver.Databases)
            {
                tblServerProperties.Rows.Add(mydb.Name.ToString());
                foreach (Property dbpro in mydb.Properties)
                {
                    tblServerProperties.Rows.Add(dbpro.Name.ToString(),dbpro.Value.ToString());
                }
            }
        }
            
    }
}
