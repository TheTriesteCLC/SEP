namespace SEP
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Application.Run(new Login());
            Application.Exit();
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