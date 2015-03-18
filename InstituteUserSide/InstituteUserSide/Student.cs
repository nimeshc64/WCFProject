﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InstituteUserSide
{
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
            GetlastID();
            GridClassDetais();
            
        }

        string pic;
        string gender=null;
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string picloca = dlg.FileName.ToString();
                pic = picloca;
                stuImage.ImageLocation = picloca;
            }        
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            GenderSet();
            InstituteServices.StudentServicesClient client = new InstituteServices.StudentServicesClient();
            if (txtstuid.Text == null ||
                txtfname.Text == null ||
                txtlname.Text == null ||
                txtadd.Text == null ||
                txtcont.Text == null ||
                txtcourseid.Text == null ||
                gender == null)
            {
                MessageBox.Show("Please Fill All Fields ");
            }
            else
            {

                InstituteServices.Student student = new InstituteServices.Student()
                {
                    stuid = Convert.ToInt32(txtstuid.Text),
                    stuCourseID = Convert.ToInt32(txtcourseid.Text),
                    stuFName = txtfname.Text,
                    stuLName = txtlname.Text,
                    stuAddr = txtadd.Text,
                    stuGender = gender,
                    stuContact = Convert.ToInt32(txtcont.Text),
                    stuPhoto = pic
                };

                int chk = client.SaveStudent(student);
                if (chk == 1)
                {
                    MessageBox.Show("Student saved", "Message");
                    GetlastID();
                    clear();
                }
                else
                    MessageBox.Show("Not Saved");
            }
        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {
            InstituteServices.StudentServicesClient client = new InstituteServices.StudentServicesClient();
            if (txtstuid.Text == null ||
                txtfname.Text == null ||
                txtlname.Text == null ||
                txtadd.Text == null ||
                txtcont.Text == null ||
                txtcourseid.Text == null ||
                gender == null)
            {
                MessageBox.Show("Please Fill All Fields ");
            }
            else 
            { 
            InstituteServices.Student student = new InstituteServices.Student()
            {
                stuid=Convert.ToInt32(txtstuid.Text),
                stuFName = txtfname.Text,
                stuLName = txtlname.Text,
                stuAddr = txtadd.Text,
                stuGender = gender,
                stuContact = Convert.ToInt32(txtcont.Text)
            };

            int chk = client.UpdateStudent(student);
            if (chk == 1)
            {
                MessageBox.Show("Student Updates", "Message");
                GetlastID();
                clear();
            }
            else
                MessageBox.Show("Not Updated","Error");
                
            }
        }

        private void Clearbtn_Click(object sender, EventArgs e)
        {
            clear();
        }

        public void GenderSet()
        {
            if(radioGenF.Checked==true)
            {
                gender = "F";
            }
            else
            {
                gender = "M";
            }
        }

        public void clear()
        {
            Savebtn.Enabled = true;
            txtfname.Clear();
            txtlname.Clear();
            txtadd.Clear();
            txtcont.Clear();
            radioGenF.Checked = false;
            radioGenM.Checked = false;
            GetlastID();
        }

        public void GetlastID()
        {
            InstituteServices.StudentServicesClient client = new InstituteServices.StudentServicesClient();
            InstituteServices.Student student = new InstituteServices.Student();
            txtstuid.Text = client.studentlastid().ToString();       
        }

        public void GridClassDetais()
        {
            InstituteServices.StudentServicesClient client = new InstituteServices.StudentServicesClient();           
            DataSet set = client.GetStuCourseData();
            DataTable table = set.Tables[0];
            StuClassGrid.DataSource = table;
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            Savebtn.Enabled=false;
            InstituteServices.StudentServicesClient client = new InstituteServices.StudentServicesClient();

            InstituteServices.Student student = new InstituteServices.Student();
            student = client.SearchStudentDate(int.Parse(txtstuid.Text));

            txtfname.Text =student.stuFName;
            txtlname.Text = student.stuLName;
            txtadd.Text =student.stuAddr;
            gender =student.stuGender;
            if(gender=="F")
            {
                radioGenF.Checked = true;
            }
            else
            {
                radioGenM.Checked = true;
            }
            txtcont.Text =Convert.ToString(student.stuContact);
        }

      
        private void tabPage4_Click(object sender, EventArgs e)
        {           
        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {
            InstituteServices.StudentServicesClient client = new InstituteServices.StudentServicesClient();
            DataSet set = client.GetAllStudentData();
            DataTable table = set.Tables[0];
            StuAllGrid.DataSource = table;
        }
    }
}
