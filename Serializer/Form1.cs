using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Serializer
{
    public partial class Form1 : Form
    {
        Person viewdPerson = null;
        int viewdPersonNumber = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Person addPerson = new Person();
            addPerson.Name = txtBoxName.Text;
            addPerson.Adress = txtBoxAdress.Text;
            addPerson.PhoneNumber = txtBoxPhone.Text;
            addPerson.Serialize();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                showPerson(1);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    

        void showPerson(int num)
        {
            try
            {
                viewdPerson = Person.Deserialize(num);
                txtBoxName.Text = viewdPerson.Name;
                txtBoxAdress.Text = viewdPerson.Adress;
                txtBoxPhone.Text = viewdPerson.PhoneNumber; 
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                txtBoxName.Text = "";
                txtBoxAdress.Text = "";
                txtBoxPhone.Text = "";
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            showPerson(++viewdPersonNumber);  
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (viewdPersonNumber > 1)
            {
                showPerson(--viewdPersonNumber);
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            showPerson(getLastFileIndex());
        }

        int getLastFileIndex()
        {
            List<FileInfo> personFiles = new List<FileInfo>();
            FileInfo[] currentFiles = new DirectoryInfo(Environment.CurrentDirectory).GetFiles();
            foreach(FileInfo file in currentFiles)
            {
                if (Regex.IsMatch(file.Name, @"^Person\w{1,2}\.dat$"))
                {
                    personFiles.Add(file);
                }
            }
            return personFiles.Count;
        }
    }
}
