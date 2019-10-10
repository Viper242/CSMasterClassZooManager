using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using ZooManagerLibrary.Models;


namespace ZooManagerLibrary
{
    public class DbConnectionManager
    {
        private static SqlConnection sqlConnection;
        public static void InitConnection()
        {
            string connectionString = @"Server=desktop-3OQG4CG\mysqlserver;Database=CSMasterClassDB;User Id=sa;Password=Fem5lingar";
            sqlConnection = new SqlConnection(connectionString);
        }

        public static DataTable PouplateZooList()
        {
            var zooTable = new DataTable();
            try
            {
                string query = "select * from Zoo";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);

                using (sqlDataAdapter)
                {
                    
                    sqlDataAdapter.Fill(zooTable);
                    
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }

            return zooTable;
        }

        public static DataTable PopulateZooAnimalList(ListBox listBox)
        {
            DataTable animalTable = new DataTable();
            try
            {
                string query = "select * from Animal a inner join ZooAnimal za on a.Id = za.AnimalID where za.ZooID = @ZooID";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                using (sqlDataAdapter)
                {
                    sqlCommand.Parameters.AddWithValue("@ZooID", listBox.SelectedValue);
                    
                    sqlDataAdapter.Fill(animalTable);


                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }

            return animalTable;
        }

        public static DataTable PopulateAllAnimalsList()
        {
            DataTable allAnimalTable = new DataTable();
            try
            {
                string query = "select * from Animal";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);

                using (sqlDataAdapter)
                {
                    

                    sqlDataAdapter.Fill(allAnimalTable);

                    
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }

            return allAnimalTable;
        }

        public static DataTable UpdateAnimalTextBox(ListBox listBox)
        {
            string query = @"Select Animal from Animal where Id = @Id";
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand(query,sqlConnection);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);

            using (sqlAdapter)
            {
                sqlCommand.Parameters.AddWithValue("@Id", listBox.SelectedValue);
                sqlAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public static DataTable UpdateZooTextBox(ListBox listBox)
        {
            string query = @"Select Location from Zoo where Id = @Id";
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand(query,sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            using (sqlDataAdapter)
            {
                sqlCommand.Parameters.AddWithValue("@Id", listBox.SelectedValue);
                sqlDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public static void AddZoo(ZooModel zooModel)
        {
            string addZooRecord = @"insert into Zoo (Location) values (@Location)";

            
                SqlCommand addZooCommand = new SqlCommand(addZooRecord, sqlConnection);

                using (addZooCommand)
                {
                    addZooCommand.Parameters.AddWithValue("@Location", zooModel.Location);
                    addZooCommand.Connection.Open();
                    addZooCommand.ExecuteScalar();
                    addZooCommand.Connection.Close();
                }
            
        }

        public static void DeleteZoo(ListBox listBox)
        {
            string deleteRecordQuery = @"delete from Zoo where Id = @ZooID";
            SqlCommand deleteZooCommand = new SqlCommand(deleteRecordQuery, sqlConnection);
            deleteZooCommand.Connection.Open();
            deleteZooCommand.Parameters.AddWithValue("@ZooID", listBox.SelectedValue);
            deleteZooCommand.ExecuteScalar();
            deleteZooCommand.Connection.Close();
        }

        public static void AddAnimal(AnimalModel animalModel)
        {
            string insertRecord = @"insert into Animal (Animal) values (@Animal)";
            SqlCommand addAnimalCommand = new SqlCommand(insertRecord, sqlConnection);
            //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(addAnimalCommand);

            using (addAnimalCommand)
            {
                addAnimalCommand.Parameters.AddWithValue("@Animal", animalModel.Animal);
                addAnimalCommand.Connection.Open();
                addAnimalCommand.ExecuteScalar();
                addAnimalCommand.Connection.Close();
            }
        }

        public static void DeleteAnimal(ListBox listBox)
        {
            string deleteAnimalRecord = @"Delete from Animal where Id = @AnimalID";

            SqlCommand deleteAnimalRecordCommand = new SqlCommand(deleteAnimalRecord,sqlConnection);

            using (deleteAnimalRecordCommand)
            {
                deleteAnimalRecordCommand.Parameters.AddWithValue("AnimalID", listBox.SelectedValue);
                deleteAnimalRecordCommand.Connection.Open();
                deleteAnimalRecordCommand.ExecuteScalar();
                deleteAnimalRecordCommand.Connection.Close();
            }
        }

        public static void AddAnimalToZoo(ListBox listBoxZoo, ListBox listBoxAllAnimals)
        {
            string query = @"Insert into ZooAnimal values (@ZooID, @AnimalID)";
            SqlCommand addCommand = new SqlCommand(query,sqlConnection);

            using (addCommand)
            {
                addCommand.Parameters.AddWithValue("ZooID", listBoxZoo.SelectedValue);
                addCommand.Parameters.AddWithValue("AnimalID", listBoxAllAnimals.SelectedValue);
                addCommand.Connection.Open();
                addCommand.ExecuteScalar();
                addCommand.Connection.Close();
            }
        }

        public static void RemoveAnimalFromZoo(ListBox listBoxZoo, ListBox listBoxAnimal)
        {
            string query = @"Delete from ZooAnimal where AnimalID = @AnimalID";
            SqlCommand removeCommand = new SqlCommand(query,sqlConnection);

            using (removeCommand)
            {
                removeCommand.Parameters.AddWithValue("@ZooID", listBoxZoo.SelectedValue);
                removeCommand.Parameters.AddWithValue("@AnimalID", listBoxAnimal.SelectedValue);
                removeCommand.Connection.Open();
                removeCommand.ExecuteScalar();
                removeCommand.Connection.Close();
            }
        }

        public static void UpdateZoo(ListBox listBoxZoo, ZooModel zooModel)
        {
            string query = @"Update Zoo set Location = @Location where Id = @ZooID";
            
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            using (sqlCommand)
            {
                sqlCommand.Parameters.AddWithValue("@ZooID", listBoxZoo.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@Location", zooModel.Location);
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteScalar();
                sqlCommand.Connection.Close();
            }
        }

        public static void UpdateAnimal(ListBox listBoxAnimal, AnimalModel animalModel)
        {
            string query = @"Update Animal set Animal = @Animal where Id = @AnimalID";
            SqlCommand sqlCommand = new SqlCommand(query,sqlConnection);

            using (sqlCommand)
            {
                sqlCommand.Parameters.AddWithValue("@AnimalID", listBoxAnimal.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@Animal", animalModel.Animal);
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteScalar();
                sqlCommand.Connection.Close();
            }
        }
    }
}
