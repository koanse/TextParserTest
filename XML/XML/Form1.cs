using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace XML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Process pr = Process.Start("bin\\wrf.exe", "i:readme.txt o:sample_output1.xml xml");
            pr.WaitForExit();
            FileStream fs = new FileStream("sample_output1.xml", FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);
            string s = "";
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        s += string.Format("<{0}>", reader.Name);
                        break;
                    case XmlNodeType.Text:
                        s += string.Format(reader.Value);
                        break;
                    case XmlNodeType.CDATA:
                        s += string.Format("<![CDATA[{0}]]>", reader.Value);
                        break;
                    case XmlNodeType.ProcessingInstruction:
                        s += string.Format("<?{0} {1}?>", reader.Name, reader.Value);
                        break;
                    case XmlNodeType.Comment:
                        s += string.Format("<!--{0}-->", reader.Value);
                        break;
                    case XmlNodeType.XmlDeclaration:
                        s += string.Format("<?xml version='1.0'?>");
                        break;
                    case XmlNodeType.Document:
                        break;
                    case XmlNodeType.DocumentType:
                        s += string.Format("<!DOCTYPE {0} [{1}]", reader.Name, reader.Value);
                        break;
                    case XmlNodeType.EntityReference:
                        s += string.Format(reader.Name);
                        break;
                    case XmlNodeType.EndElement:
                        s += string.Format("</{0}>", reader.Name);
                        break;
                }
            }
            textBox1.Text = s;
        }
    }
}
