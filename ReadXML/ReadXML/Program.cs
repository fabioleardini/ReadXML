using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace ReadXML
{
    class Program
    {
        static string path = @"C:\XML";

        static void Main(string[] args)
        {
            string[] filePath = Directory.GetFiles(path, "*.xml");

            foreach(string p in filePath)
                readXml(p);

            Console.ReadLine();
        }

        public static void readXml(string path)
        {
            XmlTextReader reader = new XmlTextReader(path);

            try
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element: // The node is an element.
                            Console.Write("<" + reader.Name);
                            Console.WriteLine(">");
                            break;
                        case XmlNodeType.CDATA: //Display the text in each element.
                            Console.WriteLine(reader.Value);
                            break;
                        case XmlNodeType.EndElement: //Display the end of the element.
                            Console.Write("</" + reader.Name);
                            Console.WriteLine(">");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public void updateDB(string tag, string id)
        {
            SqlConnection con = new SqlConnection("Application Name=Converge BPM;Persist Security Info=True;User ID=usrsml;pwd=c0nV3rG3r;Initial Catalog=DBConvergeBPM;Data Source=192.168.10.233;Pooling=true;Min Pool Size=0;Max Pool Size=400;");

            SqlCommand cmd = new SqlCommand(string.Format("UPDATE tabelaX SET campoX = {0} WHERE campoY = {1}", tag, id),  con);
            cmd.CommandType = CommandType.Text;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            finally
            {
                con.Close();
            }
        }
    }
}
