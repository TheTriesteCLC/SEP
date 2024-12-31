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
            //Application.Run(new TableList());
            //Application.Exit();

            Application.Run(new Login());
            Application.Exit();
        }
    }
}