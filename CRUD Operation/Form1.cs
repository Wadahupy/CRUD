using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace CRUD_Operation
{
    public partial class Form1 : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader dr;

        int i = 0;
        dbconnection dbconn = new dbconnection();
        public Form1()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.dbconnect());
            LoadRecord();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void LoadRecord()
        {
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new MySqlCommand("SELECT `Name`, `Age`, `Gender` FROM `crud`", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, dr["Name"].ToString(), dr["Age"].ToString(), dr["Gender"].ToString());
            }
            dr.Close();
            conn.Close();
        }


        public void clear() 
        {
            TxtName.Clear();
            TxtAge.Clear();
            TxtGender.Clear();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if ((TxtName.Text == string.Empty) || (TxtAge.Text == string.Empty) || (TxtGender.Text == string.Empty))
            {
                MessageBox.Show("WARNING: Please Fill out the Field", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                conn.Open();
                cmd = new MySqlCommand("INSERT INTO `crud`(`Name`, `Age`, `Gender`) VALUES (@Name, @Age, @Gender)", conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", TxtName.Text);
                cmd.Parameters.AddWithValue("@Age", TxtAge.Text);
                cmd.Parameters.AddWithValue("@Gender", TxtGender.Text);

                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Record Successfully Added!", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to Add Records!", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                conn.Close();
                LoadRecord();
                clear();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new MySqlCommand("UPDATE `crud` SET `Name` = @NewName, `Age` = @Age, `Gender` = @Gender WHERE `Name` = @OldName", conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@NewName", TxtName.Text);
            cmd.Parameters.AddWithValue("@Age", TxtAge.Text);
            cmd.Parameters.AddWithValue("@Gender", TxtGender.Text);
            cmd.Parameters.AddWithValue("@OldName", TxtName.Text); // Assuming 'Name' is the primary key

            i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Record Successfully Updated!", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to Update Records!", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            conn.Close();
            LoadRecord();
            clear();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new MySqlCommand("DELETE FROM `crud` WHERE `Name` = @Name", conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Name", TxtName.Text); // Assuming 'Name' is the primary key

            i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Record Successfully Deleted!", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to Delete Record!", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            conn.Close();
            LoadRecord();
            clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            TxtAge.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            TxtGender.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new MySqlCommand("SELECT `Name`, `Age`, `Gender` FROM `crud` WHERE `Name` LIKE @Search OR `Age` LIKE @Search OR `Gender` LIKE @Search OR `Gender` LIKE @Search ", conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Search", "%" + txtSearch.Text + "%");

            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, dr["Name"].ToString(), dr["Age"].ToString(), dr["Gender"].ToString());
            }
            dr.Close();
            conn.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
