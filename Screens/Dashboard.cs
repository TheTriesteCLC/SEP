﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using SEP.CustomClassBuilder;
using SEP.Screens;
using SEP.CurrUser;

namespace SEP
{
    public partial class Dashboard : Form
    {
        private IMongoDatabase database;
        private string collectionName;

        public Dashboard(string collectionName)
        {
            InitializeComponent();
            database = CurrUserInfo.getUserDB();
            this.collectionName = collectionName;
            LoadCollectionData();
        }
        private void LoadCollectionData()
        {
            var collection = database.GetCollection<BsonDocument>(collectionName);
            var documents = collection.Find(FilterDefinition<BsonDocument>.Empty).ToList();

            DataTable dataTable = new DataTable();
            foreach (var doc in documents)
            {
                if (dataTable.Columns.Count == 0)
                {
                    foreach (var element in doc.Elements)
                    {
                        dataTable.Columns.Add(element.Name);
                    }
                }

                var row = new object[dataTable.Columns.Count];
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    row[i] = doc[dataTable.Columns[i].ColumnName].ToString();
                }
                dataTable.Rows.Add(row);
            }

            dataGridView1.DataSource = dataTable;
        }

        private void button2_Enter(object sender, EventArgs e)
        {
            AddNewDocument addNewDocument = new AddNewDocument(collectionName);
            addNewDocument.ShowDialog();
            //this.Close();
        }
    }
}
