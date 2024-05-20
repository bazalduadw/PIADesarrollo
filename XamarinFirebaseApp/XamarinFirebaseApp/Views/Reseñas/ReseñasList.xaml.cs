using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFirebaseApp.Views.Reseñas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReseñasList : ContentPage
    {
        ReseñaRepository reseñaRepository = new ReseñaRepository();
        public ReseñasList()
        {
            InitializeComponent();

            StudentListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }

        protected override async void OnAppearing()
        {
            var students= await reseñaRepository.GetAll();
            StudentListView.ItemsSource = null;
            StudentListView.ItemsSource = students;
            StudentListView.IsRefreshing = false;

        }

        private void AddToolBarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ReseñaEntry());
        }

        private void StudentListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            var studnet=e.Item as ReseñaModel;
            Navigation.PushModalAsync(new ReseñaDetails(studnet));
            ((ListView)sender).SelectedItem = null;
                
        }

        

        private async void DeleteTapp_Tapped(object sender, EventArgs e)
        {
           
           var response=await DisplayAlert("Eliminar", "Deseas eliminar?","Si", "No");
            if(response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
               bool isDelete= await reseñaRepository.Delete(id);
                if(isDelete)
                {
                    await DisplayAlert("Informacion", "La reseña se elimino", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "La eliminacion de la reseña fallo", "Ok");
                }
            }
        }

        private async void EditTap_Tapped(object sender, EventArgs e)
        {
            //DisplayAlert("Edit", "Do you want to Edit?", "Ok");

            string id = ((TappedEventArgs)e).Parameter.ToString();
            var student = await reseñaRepository.GetById(id);
            if(student==null)
            {
               await DisplayAlert("Warning", "Data not found.", "Ok");
            }
            student.Id = id;
            await Navigation.PushModalAsync(new ReseñaEdit(student));

        }

        private async void TxtSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if(!String.IsNullOrEmpty(searchValue))
            {
                var students =await reseñaRepository.GetAllByName(searchValue);
                StudentListView.ItemsSource = null;
                StudentListView.ItemsSource = students;
            }
            else
            {
                OnAppearing();
            }
        }

        private async void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var students = await reseñaRepository.GetAllByName(searchValue);
                StudentListView.ItemsSource = null;
                StudentListView.ItemsSource = students;
            }
            else
            {
                OnAppearing();
            }
        }

        private async void EditMenuItem_Clicked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var student = await reseñaRepository.GetById(id);
            if (student == null)
            {
                await DisplayAlert("Warning", "Data not found.", "Ok");
            }
            student.Id = id;
            await Navigation.PushModalAsync(new ReseñaEdit(student));
        }

        private async void DeleteMenuItem_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Eliminar", "Deseas eliminar?", "Si", "No");
            if (response)
            {
                string id = ((MenuItem)sender).CommandParameter.ToString();
                bool isDelete = await reseñaRepository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Informacion", "La reseña se elimino", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "La eliminacion de la reseña fallo", "Ok");
                }
            }
        }

        private async void EditSwipeItem_Invoked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var student = await reseñaRepository.GetById(id);
            if (student == null)
            {
                await DisplayAlert("Warning", "Data not found.", "Ok");
            }
            student.Id = id;
            await Navigation.PushModalAsync(new ReseñaEdit(student));
        }
    }
}