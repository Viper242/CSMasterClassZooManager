using System;
using System.Windows;
using System.Windows.Controls;
using ZooManagerLibrary;
using ZooManagerLibrary.Models;

namespace CSMasterClassZooManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            

            DbConnectionManager.InitConnection();
           
            ShowZooList();
            ShowAllAnimals();

        }
        private void ShowZooList()
        {
                    listBoxZoo.DisplayMemberPath = "Location";
                    listBoxZoo.SelectedValuePath = "Id";
                    listBoxZoo.ItemsSource = DbConnectionManager.PouplateZooList().DefaultView;
        }

        private void ShowAnimalsList()
        {
            listBoxAnimal.DisplayMemberPath = "Animal";
            listBoxAnimal.SelectedValuePath = "Id";
            listBoxAnimal.ItemsSource = DbConnectionManager.PopulateZooAnimalList(listBoxZoo).DefaultView;

        }

        private void ShowAllAnimals()
        {

                    listBoxAllAnimals.DisplayMemberPath = "Animal";
                    listBoxAllAnimals.SelectedValuePath = "Id";
                    listBoxAllAnimals.ItemsSource = DbConnectionManager.PopulateAllAnimalsList().DefaultView;

        }

        private void UpdateAnimalTextBox()
        {
            
                textAnimal.Text = DbConnectionManager.UpdateAnimalTextBox(listBoxAllAnimals).Rows[0]["Animal"].ToString();

            }

        private void UpdateZooTextBox()
        {
            
                textZoo.Text = DbConnectionManager.UpdateZooTextBox(listBoxZoo).Rows[0]["Location"].ToString();

        }

        private void ListBoxZoo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxZoo.SelectedItem != null)
            {
                ShowAnimalsList();
                UpdateZooTextBox();
            }

        }

        private void ListBoxAllAnimals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxAllAnimals.SelectedItem != null)
            {
                UpdateAnimalTextBox();
            }
            
        }

        private void AddZoo_Clicked(object sender, RoutedEventArgs e)
        {
            
            ZooModel zooModel = new ZooModel();
            zooModel.Location = textZoo.Text;

            try
            {
                DbConnectionManager.AddZoo(zooModel);
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                textZoo.Text = "";
                ShowZooList();
            }
        }

        private void DeleteZoo_Clicked(object sender, RoutedEventArgs e)
        {
            
            if (listBoxZoo.SelectedItem != null)
            {
                try
                {
                    DbConnectionManager.DeleteZoo(listBoxZoo);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    ShowZooList();
                    
                    textZoo.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Please select a zoo to delete");
            }

        }

        private void AddAnimal_Clicked(object sender, RoutedEventArgs e)
        {
            AnimalModel animalModel = new AnimalModel();
            animalModel.Animal = textAnimal.Text;
            try
            {
                DbConnectionManager.AddAnimal(animalModel);
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                textAnimal.Text = "";
                ShowAllAnimals();
            }

        }

        private void DeleteAnimal_Clicked(object sender, RoutedEventArgs e)
        {

            if (listBoxAllAnimals.SelectedItem != null)
            {

                try
                {
                    
                    DbConnectionManager.DeleteAnimal(listBoxAllAnimals);
                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    ShowAllAnimals();
                    textAnimal.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Please select animal to delete");
            }

        }

        private void AddAnimalToZoo_Clicked(object sender, RoutedEventArgs e)
        {
            
            if (listBoxZoo.SelectedItem != null && listBoxAllAnimals.SelectedItem != null)
            {
                try
                {
                    DbConnectionManager.AddAnimalToZoo(listBoxZoo,listBoxAllAnimals);
                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    ShowAnimalsList();
                }
            }
            else
            {
                MessageBox.Show("Please select a zoo and animal first");
            }
        }

        private void RemoveAnimalFromZoo_Clicked(object sender, RoutedEventArgs e)
        {
            //string query = @"delete from ZooAnimal where AnimalID = @AnimalID";
            if (listBoxZoo.SelectedItem != null && listBoxAnimal.SelectedItem != null)
            {
                try
                {
                    DbConnectionManager.RemoveAnimalFromZoo(listBoxZoo,listBoxAnimal);
                    //SqlCommand removeAnimalCommand = new SqlCommand(query, sqlConnection);

                    //using (removeAnimalCommand)
                    //{
                    //    removeAnimalCommand.Parameters.AddWithValue("@ZooID", listBoxZoo.SelectedValue);
                    //    removeAnimalCommand.Parameters.AddWithValue("@AnimalID", listBoxAnimal.SelectedValue);
                    //    removeAnimalCommand.Connection.Open();
                    //    removeAnimalCommand.ExecuteScalar();
                    //    removeAnimalCommand.Connection.Close();
                    //}
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    ShowAnimalsList();
                }

            }
            else
            {
                MessageBox.Show("Select a zoo and animal first");
            }
        }

        private void UpdateAnimal_Clicked(object sender, RoutedEventArgs e)
        {
            AnimalModel animalModel = new AnimalModel();
            animalModel.Animal = textAnimal.Text;
            try
            {
                DbConnectionManager.UpdateAnimal(listBoxAllAnimals,animalModel);
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                ShowAllAnimals();
                textAnimal.Text = "";
            }

        }

        private void UpdateZoo_Clicked(object sender, RoutedEventArgs e)
        {
            
            ZooModel zooModel = new ZooModel();
            zooModel.Location = textZoo.Text;
            try
            {
                DbConnectionManager.UpdateZoo(listBoxZoo,zooModel);
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (listBoxZoo.SelectedItem != null)
                {
                    ShowZooList();
                    textZoo.Text = "";
                }
            }

        }
    }
}
