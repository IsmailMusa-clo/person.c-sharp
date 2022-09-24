using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace person
{
    public partial class person : Form
    {
        public person()
        {
            InitializeComponent();
        }
        ErrorProvider er = new ErrorProvider();

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void person_Load(object sender, EventArgs e)
        {
            this.Icon = Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.FriendlyName);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {


            try
            {
                StreamReader sr = new StreamReader("Data.txt");
                string allData = sr.ReadToEnd();
                sr.Close();

             
                
                if (allData.Contains(txtId.Text))
                {
            
                    er.SetError(txtId, "the id is visable try again");
                    //MessageBox.Show("the id is visable try again");
                }
                else
                {

                    StreamWriter sW = new StreamWriter("Data.txt", true);
                    if (txtId.Text != null && txtName.Text != null&&pic.Image!=null)
                    {
                        string data = txtId.Text + ";" +
                                     txtName.Text + ";" +
                                     txtCity.Text + ";";
                        sW.WriteLine(data);
                         sW.Close();
                    
                        if(!Directory.Exists("img"))
                        Directory.CreateDirectory("img");
                        pic.Image.Save("img/" + txtId.Text + ".jpg");
                        MessageBox.Show("the person data is added");
                    }
                        else
                        {
                        MessageBox.Show("Enter Your Data");
                        }
                   
                    txtId.Focus();
                    txtId.SelectAll();
                    txtName.Text = "";
                    txtCity.Text = "";
                    pic.Image = new PictureBox().Image;
                }


        }catch(Exception exe)
            {
                MessageBox.Show("Error");
            }


}
        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txtId.Text!="")
            {
                StreamReader sr = new StreamReader("Data.txt");
                string dataFind = "";
                bool found = false;
                /////////////////////////
                do
                {
                    dataFind = sr.ReadLine();
                    if (dataFind != null) 
                        { 
                          string[] df = dataFind.Split(';');
                          if (df[0] == txtId.Text)
                          { 
                            txtId.Text = df[0];
                            txtName.Text= df[1];
                            txtCity.Text = df[2];

                            String myPath = "img/" + df[0] +".jpg" ;
                            if (File.Exists(myPath))
                                pic.Image = Image.FromFile(myPath);

                            found = true;
                            MessageBox.Show("your found");
                            break;
                        }

                        
                    }  else
                        {
                        er.SetError(txtId, "the id is not Found");
                            //MessageBox.Show("the id is not Found");
                        }

                } while (dataFind != null);
                sr.Close();
                txtId.Focus();
                txtId.SelectAll();
            }
            else
            {
               MessageBox.Show("please Enter the id number");
                txtId.Focus();
                
            }
           

        }
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            Form ShowAllData = new Form();
            ShowAllData.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            ShowAllData.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ShowAllData.AutoSize = true;
            ShowAllData.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            ShowAllData.ClientSize = new System.Drawing.Size(509, 420);
            ShowAllData.Show();
            ShowAllData.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ShowAllData.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            ShowAllData.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            ShowAllData.MaximizeBox = false;
            ShowAllData.Name = "person";
            ShowAllData.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            TextBox txt = new TextBox();
            txt.Dock = System.Windows.Forms.DockStyle.Fill;
            txt.Multiline = true;
            ShowAllData.Controls.Add(txt);
            StreamReader sr = new StreamReader("Data.txt");
            string allShow = sr.ReadToEnd();
            sr.Close();
            txt.Text = allShow;
            ShowAllData.Text = "Person Data";
        }
        private void btnChooseThePic_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "JPG image |*.jpg|png image|*.png|Gif image|*.gif |Jpg image|*.jpg |JPEG image|*.JPEG";
            of.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (of.ShowDialog() == DialogResult.OK)
            { 
            pic.Image = Image.FromFile(of.FileName);
            }
        }
        private void btnShowImage_Click(object sender, EventArgs e)
        {
            Form ShowAllpic = new Form();
            ShowAllpic.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            ShowAllpic.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ShowAllpic.AutoSize = true;
            ShowAllpic.AutoScroll = true;
            ShowAllpic.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            ShowAllpic.ClientSize = new System.Drawing.Size(509, 420);
            ShowAllpic.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ShowAllpic.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            ShowAllpic.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            ShowAllpic.MaximizeBox = false;
            ShowAllpic.Name = "ShowAllpic";
            ShowAllpic.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          
            StreamReader sr = new StreamReader("Data.txt");
            string line = "";
            int mytop = 10;
            do
            {
                line = sr.ReadLine();

                if (line != null)
                {
                    TextBox txt = new TextBox();
                    PictureBox pics = new PictureBox();
                    txt.Top = mytop;     
                    txt.Text = line;
                    txt.Width = 300;
                    pics.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    pics.Left = 320;
                    pics.Top = mytop;    
                    pics.Size = new Size(100, 100);
                   
                    String mypath = "img/" + line.Split(';')[0] + ".jpg";
                    if(File.Exists(mypath))
                       pics.Image = Image.FromFile(mypath);
                    
                    ShowAllpic.Controls.Add(txt);
                    ShowAllpic.Controls.Add(pics);
                     mytop += 110;

                   



                }




            }
            while (line != null);

            sr.Close();
            ShowAllpic.Show();


        }
        private void txtId_TextChanged(object sender, EventArgs e)
        {
            er.Clear();
            
        }
    }
}
