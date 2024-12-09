using System;
using MongoDB.Driver;

namespace SEP
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Application.Run(new Registration());
            Application.Exit();
        }
    }
}