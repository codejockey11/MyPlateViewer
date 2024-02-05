using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace MyPlateViewer
{
    public partial class Form1 : Form
    {
        private String userprofileFolder = Environment.GetEnvironmentVariable("USERPROFILE");
        
        private String tempFolder = Environment.GetEnvironmentVariable("TEMP");

        private String airportName;
        
        private Int32 alnum;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonGetPlates_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = this.textBox1.Text.ToUpper().Trim();
            
            if (this.textBox1.Text.Length == 0)
            {
                return;
            }

            this.listBox1.Items.Clear();

            LoadSupp(this.textBox1.Text);
            
            LoadPlates(this.textBox1.Text);
            
            LoadCmpr(this.textBox1.Text);
        }

        public void LoadPlates(String apt)
        {
            this.alnum = -1;

            String[] fileEntries = Directory.GetFiles(this.userprofileFolder + "\\Downloads\\", "DDTPPE_*.zip");

            ZipArchive archive = ZipFile.OpenRead(fileEntries[0]);
            
            ZipArchiveEntry zae = archive.GetEntry("d-TPP_Metafile.xml");

            XmlDocument xdcDocument = new XmlDocument();
            
            xdcDocument.Load(zae.Open());
            
            XmlElement xmlRoot = xdcDocument.DocumentElement;
            
            XmlNodeList xmlNodes = xmlRoot.SelectNodes("state_code/city_name/airport_name");

            List<String> dtpp = new List<String>();

            foreach (XmlNode xn in xmlNodes)
            {
                if (String.Compare(xn.Attributes.GetNamedItem("apt_ident").Value, apt) == 0)
                {
                    this.airportName = xn.Attributes.GetNamedItem("ID").Value;

                    this.alnum = Convert.ToInt32(xn.Attributes.GetNamedItem("alnum").Value);

                    XmlNodeList records = xn.SelectNodes("record");

                    foreach (XmlNode p in records)
                    {
                        String name = "";
                        String pdf = "";
                        String chartCode = "";
                        
                        foreach (XmlNode cn in p.ChildNodes)
                        {
                            switch(cn.Name)
                            {
                                case "chart_name":
                                {
                                    name = new String(cn.InnerXml.ToCharArray());
                                    
                                    break;
                                }

                                case "chart_code":
                                {
                                    chartCode = new String(cn.InnerXml.ToCharArray());
                                    
                                    break;
                                }

                                case "pdf_name":
                                {
                                    pdf = cn.InnerXml;
                                    
                                    dtpp.Add(chartCode + "-" + name + ":" + pdf);
                                    
                                    break;
                                }

                                default:
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            dtpp.Sort();
            
            foreach(String s in dtpp)
            {
                this.listBox1.Items.Add(s);
            }
        }

        private void LoadSupp(String apt)
        {
            String[] fileEntries = Directory.GetFiles(this.userprofileFolder + "\\Downloads\\", "DCS_*.zip");

            ZipArchive archive = ZipFile.OpenRead(fileEntries[0]);

            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.Name.Length > 0)
                {
                    String[] p = entry.Name.Split('.');
                    
                    if (String.Compare(p[1], "xml") == 0)
                    {
                        XmlDocument xdcDocument = new XmlDocument();
                        
                        xdcDocument.Load(entry.Open());
                        
                        XmlElement xmlRoot = xdcDocument.DocumentElement;
                        
                        XmlNodeList xmlNodes = xmlRoot.SelectNodes("location/airport");

                        String aptid = null;
                        String pdf = null;
                        String navidname = null;

                        foreach (XmlNode xn in xmlNodes)
                        {
                            foreach (XmlNode cn in xn.ChildNodes)
                            {
                                if (String.Compare(cn.Name, "aptname") == 0)
                                {
                                    this.airportName = cn.InnerXml;
                                }
                                
                                if (String.Compare(cn.Name, "aptid") == 0)
                                {
                                    aptid = cn.InnerXml;
                                }
                                
                                if (String.Compare(cn.Name, "pages") == 0)
                                {
                                    pdf = cn.InnerText;
                                    
                                    String[] fnp = pdf.Split('.');
                                    
                                    pdf = fnp[0].ToUpper() + "." + fnp[1];
                                }
                                
                                if (String.Compare(cn.Name, "navidname") == 0)
                                {
                                    navidname = cn.InnerXml;
                                }
                            }
                            
                            if (String.Compare(aptid, apt) == 0)
                            {
                                String[] pdfp = pdf.Split('_');
                                
                                this.listBox1.Items.Add("DCS-" + "Front:" + pdfp[0] + "_front_" + pdfp[2]);
                                this.listBox1.Items.Add("DCS-" + aptid + ":" + pdf);
                                this.listBox1.Items.Add("DCS-" + "Rear:" + pdfp[0] + "_rear_" + pdfp[2]);

                                break;
                            }
                            
                            if (String.Compare(navidname, apt) == 0)
                            {
                                String[] pdfp = pdf.Split('_');
                                
                                this.listBox1.Items.Add("DCS-" + "Front:" + pdfp[0] + "_front_" + pdfp[2]);
                                this.listBox1.Items.Add("DCS-" + navidname + ":" + pdf);
                                this.listBox1.Items.Add("DCS-" + "Rear:" + pdfp[0] + "_rear_" + pdfp[2]);

                                break;
                            }
                        }

                        break;
                    }
                }
            }
        }

        private void LoadCmpr(String apt)
        {
            String[] fileEntries = Directory.GetFiles(this.userprofileFolder + "\\Downloads\\", "DDTPPE_*.zip");

            ZipArchive archive = ZipFile.OpenRead(fileEntries[0]);
            
            ZipArchiveEntry zae = archive.GetEntry("d-TPP_Metafile.xml");

            XmlDocument xdcDocument = new XmlDocument();
            
            xdcDocument.Load(zae.Open());
            
            XmlElement xmlRoot = xdcDocument.DocumentElement;
            
            XmlNodeList xmlNodes = xmlRoot.SelectNodes("state_code/city_name/airport_name");

            foreach (XmlNode xn in xmlNodes)
            {
                if (String.Compare(xn.Attributes.GetNamedItem("apt_ident").Value, apt) == 0)
                {
                    this.airportName = xn.Attributes.GetNamedItem("ID").Value;
                    
                    this.alnum = Convert.ToInt32(xn.Attributes.GetNamedItem("alnum").Value);
                    
                    break;
                }
            }

            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.Name != "")
                {
                    String sn = new String(entry.Name.ToCharArray(0, 5));
                    
                    String alnums = this.alnum.ToString("D5");
                    
                    if (alnums == sn)
                    {
                        this.listBox1.Items.Add("CMPR-" + entry.Name);
                    }
                }
            }
        }

        private void ViewPlates(String pdf, String name)
        {
            String[] fileEntries = Directory.GetFiles(this.userprofileFolder + "\\Downloads\\", "DDTPP*.zip");

            foreach (String fn in fileEntries)
            {
                ZipArchive archive = ZipFile.OpenRead(fn);
                
                ZipArchiveEntry zae = archive.GetEntry(pdf);
                
                if (zae != null)
                {
                    zae.ExtractToFile(this.tempFolder + "\\temp.pdf", true);

                    Form2 f2 = new Form2();

                    f2.Text = this.airportName + " " + name;
                    
                    f2.axAcroPDF1.LoadFile(this.tempFolder + "\\temp.pdf");
                    
                    f2.axAcroPDF1.setZoom(100.0F);
                    f2.axAcroPDF1.setShowToolbar(false);
                    
                    f2.WindowState = FormWindowState.Normal;
                    
                    f2.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Right - f2.Size.Width - 6), Screen.PrimaryScreen.Bounds.Top + 6);
                    
                    f2.Show();
                }
            }
        }

        private void ViewSupp(String pdf)
        {
            String[] fileEntries = Directory.GetFiles(this.userprofileFolder + "\\Downloads\\", "DCS*.zip");
            
            ZipArchive archive = ZipFile.OpenRead(fileEntries[0]);

            foreach (ZipArchiveEntry zae in archive.Entries)
            {
                if (String.Compare(zae.Name, pdf) == 0)
                {
                    zae.ExtractToFile(this.tempFolder + "\\temp.pdf", true);

                    Form2 f2 = new Form2();
                    
                    f2.Text = this.airportName + " SUPPLEMENT";
                    
                    f2.axAcroPDF1.LoadFile(this.tempFolder + "\\temp.pdf");
                    
                    f2.axAcroPDF1.setZoom(100.0F);
                    f2.axAcroPDF1.setShowToolbar(false);
                    
                    f2.WindowState = FormWindowState.Normal;
                    
                    f2.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Right - f2.Size.Width - 6), Screen.PrimaryScreen.Bounds.Top + 6);
                    
                    f2.Show();
                }
            }
        }

        private void ViewCmpr(String pdf)
        {
            String[] fileEntries = Directory.GetFiles(this.userprofileFolder + "\\Downloads\\", "DDTPPE*.zip");

            foreach (String fn in fileEntries)
            {
                ZipArchive archive = ZipFile.OpenRead(fn);
                
                ZipArchiveEntry zae = archive.GetEntry("compare_pdf/" + pdf);
                
                if (zae != null)
                {
                    zae.ExtractToFile(this.tempFolder + "\\temp.pdf", true);

                    Form2 f2 = new Form2();
                    
                    f2.Text = this.airportName + " COMPARISON";
                    
                    f2.axAcroPDF1.LoadFile(this.tempFolder + "\\temp.pdf");
                    
                    f2.axAcroPDF1.setZoom(100.0F);
                    f2.axAcroPDF1.setShowToolbar(false);
                    
                    f2.WindowState = FormWindowState.Maximized;
                    
                    f2.Show();
                }
            }
        }

        private void buttonViewDoc_Click(object sender, EventArgs e)
        {
            foreach (String si in this.listBox1.SelectedItems)
            {
                String[] scn = si.Split(':');
                
                String[] type = scn[0].Split('-');

                switch(type[0])
                {
                    case "APD":
                    case "DAU":
                    case "DP":
                    case "HOT":
                    case "IAP":
                    case "LAH":
                    case "MIN":
                    case "STAR":
                    {
                        ViewPlates(scn[1], type[1]);
                        
                        break;
                    }

                    case "DCS":
                    {
                        ViewSupp(scn[1]);
                        
                        break;
                    }

                    case "CMPR":
                    {
                        ViewCmpr(type[1]);
                        
                        break;
                    }
                }
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.textBox1.TextLength > 2)
                {
                    e.Handled = e.SuppressKeyPress = true;
                    
                    this.buttonGetPlates_Click(sender, e);
                }
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.buttonViewDoc_Click(sender, e);
        }

    }
}
