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

namespace Task_eFILECompany
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SelectRoot();
        }

        void SelectRoot()
        {
            //Clear TreeView
            treeView1.Nodes.Clear();

            //Loop For Get Drives
            foreach (System.IO.DriveInfo myDrives in System.IO.DriveInfo.GetDrives())
            {
                TreeNode myDrivesNode = treeView1.Nodes.Add(myDrives.Name);
                //Adding "Expand" string is use for Add Expand "[+]" option on Drives
                myDrivesNode.Nodes.Add("Expand");
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode MyExistNode = e.Node;
            //Clear TreeNode
            MyExistNode.Nodes.Clear();

            try
            {
                string mypath = MyExistNode.FullPath;

                //Loop For Get Folders
                foreach (string myFolders in System.IO.Directory.GetDirectories(mypath))
                {
                    TreeNode FldrNode = MyExistNode.Nodes.Add(System.IO.Path.GetFileName(myFolders));
                    //Here, Expand is use for add Expanding option "[+]" on folder
                    FldrNode.Nodes.Add("Expand");
                }

                //Loop For Get Files
                foreach (string MyFiles in System.IO.Directory.GetFiles(mypath))
                {
                    var fullPath = System.IO.Path.GetFileName(MyFiles);
                    var pathSplit = fullPath.Split('.');
                    var extention = pathSplit[pathSplit.Length - 1].ToString();

                    if (extention.ToUpper() == "JPG" || extention.ToUpper() == "PNG" || extention.ToUpper() == "GIF" || extention.ToUpper() == "BMP")
                    {
                        TreeNode FLNode = MyExistNode.Nodes.Add(System.IO.Path.GetFileName(MyFiles));
                    }
                    

                }

            }
            catch (Exception FlErr)
            {
                MessageBox.Show(FlErr.ToString());
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode MySelectedNode = e.Node;
            //Get Selected Node Path

            var fullPath = MySelectedNode.FullPath;
            var pathSplit = fullPath.Split('.');
            var extention = pathSplit[pathSplit.Length -1].ToString();
            
            if (extention.ToUpper() == "JPG" || extention.ToUpper() == "PNG" || extention.ToUpper() == "GIF" || extention.ToUpper() == "BMP")
            {
                img_Logo.Image = Image.FromFile(fullPath);
            }
            else
            {
                MessageBox.Show("Please Select Image: ملفات صور |*.JPG; *.PNG; *.Gif; *.BMP;");
            }
        }

        /// <summary>
        /// Add ImageDetail to Db Sql Async
        /// </summary>
        /// <returns></returns>
        async Task<int> Add()
        {
            try
            {
                using var context = new eEIleContextFactory().CreateDbContext();
                {
                    var imageDetail = new ImageDetail
                    {
                        Name = txt_Name.Text,
                        Address = txt_Address.Text,
                        Email = txt_Email.Text,
                        Img = ImageWork.ToByteArray(img_Logo.Image)
                    };
                    await context.ImageDetails.AddAsync(imageDetail);
                    return await context.SaveChangesAsync();
                }
                
            }
            catch { MessageBox.Show("حدث خطاً غير متوقع برجاء مراجعة البيانات جيداً"); }
            return 0;
        }

        /// <summary>
        /// check if TextBox Is Not Empty
        /// </summary>
        /// <returns>Bool value if Valid Check</returns>
        bool checkValid()
        {
            bool result = true;

            if (string.IsNullOrWhiteSpace(txt_Name.Text))
            {
                MessageBox.Show("Please Enter Your Name"); return false;
            }
            if (string.IsNullOrWhiteSpace(txt_Address.Text))
            {
                MessageBox.Show("Please Enter Your Address"); return false;
            }
            if (string.IsNullOrWhiteSpace(txt_Email.Text))
            {
                MessageBox.Show("Please Enter Your Email"); return false;
            }
            if (img_Logo.Image == null)
            {
                MessageBox.Show("Please Choose Your Image"); return false;
            }

            return result;
        }

        /// <summary>
        /// check if TextBox Is Not Empty
        /// </summary>
        /// <returns>Bool value if Valid Check</returns>
        void msg(bool check, string work)
        {
            if (check)
            {
                MessageBox.Show($"تم {work} بنجاح");
            }
            else
            {
                MessageBox.Show($"حدث خطاُ اثناء عملية {work}");
            }
        }

        /// <summary>
        /// clear textBox On All Data
        /// </summary>
        /// <returns>clear TextBox</returns>
        void clear()
        {
            txt_Address.Clear();
            txt_Email.Clear();
            txt_Name.Clear();
            img_Logo.Image = null;
        }
        private async void btn_Save_Click(object sender, EventArgs e)
        {
            if (checkValid())
            {
                var res = await Add() != 0 ? true : false;
                if (res) { clear(); }
                msg(res, "الحفظ");
            }
        }
    }
}
