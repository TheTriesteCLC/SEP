using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using MongoDB.Driver;
using System.Windows.Forms;
using SEP.ClientDatabase;
using SEP.Interfaces;

namespace SEP
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //try
            //{

            //    Microsoft.Data.SqlClient.SqlConnection con = new Microsoft.Data.SqlClient.SqlConnection(Constants.testSQLConenctionString2);
            //    con.Open();

            //    List<string> result = new List<string>();
            //    Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("SELECT name FROM sys.Tables", con);
            //    Microsoft.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        result.Add(reader["name"].ToString());
            //    }
            //    reader.Close();

            //    foreach (string s in result)
            //    {
            //        System.Diagnostics.Debug.WriteLine(s);

            //        // Sử dụng GetSchema từ SqlConnection để lấy schema tables
            //        DataTable t = con.GetSchema("Tables");
            //        DisplayData(t);
            //    }


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    System.Diagnostics.Debug.WriteLine(ex.Message);
            //}

            //IDatabase clientDatabase = new ClientMongoDB("mongodb+srv://vmtriet21:X5djNq4hX4KKN3yR@sep.lpk2b.mongodb.net/", "SEP");
            //clientDatabase.GetAllCollections().Wait();


            Application.Run(new TableList());
            Application.Exit();

            //Application.Run(new Login());
            //Application.Exit();
        }

        

        private static void DisplayData(System.Data.DataTable table)
        {
            foreach (System.Data.DataRow row in table.Rows)
            {
                foreach (System.Data.DataColumn col in table.Columns)
                {
                    System.Diagnostics.Debug.WriteLine("{0} = {1}", col.ColumnName, row[col]);
                }
                System.Diagnostics.Debug.WriteLine("============================");
            }
        }
    }
}