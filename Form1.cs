using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace YouTubePlayerAndDownloader
{
    public partial class form1 : Form
    {
        private OleDbConnection connection = new OleDbConnection();

        public form1()
        {
            InitializeComponent();

            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\josep\Desktop\YouTubeURLs.accdb;Persist Security Info=False;";
        }

        private void form1_Load(object sender, EventArgs e)
        {
            connection.Open();

            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "select VideoName from YouTube_URL";
            command.CommandText = query;

            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                urlListBox.Items.Add(reader["VideoName"].ToString());
            }

            connection.Close();
        }

        private void urlListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string url = "";

            connection.Open();

            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "select * from YouTube_URL where VideoName = '" + urlListBox.SelectedItem + "'";
            command.CommandText = query;

            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                url = reader["YouTubeURL"].ToString();
            }

            axShockwaveFlash.Movie = url;
            axShockwaveFlash.Play();

            connection.Close();
        }
    }
}
