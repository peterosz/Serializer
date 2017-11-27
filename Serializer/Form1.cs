using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serializer
{
    public partial class Form1 : Form
    {
        Person viewdPerson = new Person();
        static int viewdPersonNumber = 1;

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
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            viewdPersonNumber++;
            txtBoxName.Text = "";
            txtBoxAdress.Text = "";
            txtBoxPhone.Text = "";
            showPerson(viewdPersonNumber);  
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (viewdPersonNumber > 1)
            {
                viewdPersonNumber--;
                showPerson(viewdPersonNumber);
            }
        }
    }
}
