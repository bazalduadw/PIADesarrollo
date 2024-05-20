using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFirebaseApp.Views.Reseñas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReseñaEdit : ContentPage
    {
        MediaFile file;
        ReseñaRepository reseñaRepository = new ReseñaRepository();
        public ReseñaEdit(ReseñaModel student)
        {
            InitializeComponent();
            TxtDesc.Text = student.Descripcion;
            TxtHoras.Text = student.Horas;
            TxtPlataforma.Text = student.Plataforma;
            TxtName.Text = student.Name;
            TxtId.Text = student.Id;

        }

        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            try
            {
                string name = TxtName.Text;
                string plataforma = TxtPlataforma.Text;
                string horas = TxtHoras.Text;
                string desc = TxtDesc.Text;

                if (string.IsNullOrEmpty(name))
                {
                    await DisplayAlert("Alerta", "Porfavor ingresa un nombre", "Cancelar");
                }
                if (string.IsNullOrEmpty(plataforma))
                {
                    await DisplayAlert("Alerta", "Porfavor ingresa la plataforma", "Cancelar");
                }
                if (string.IsNullOrEmpty(horas))
                {
                    await DisplayAlert("Alerta", "Porfavor ingresa las horas", "Cancelar");
                }
                if (string.IsNullOrEmpty(desc))
                {
                    await DisplayAlert("Alerta", "Porfavor ingresa una descripcion", "Cancelar");
                }

                ReseñaModel student = new ReseñaModel();
                student.Name = name;
                student.Plataforma = plataforma;
                student.Horas = horas;
                student.Descripcion = desc;
                if (file != null)
                {
                    string image = await reseñaRepository.Upload(file.GetStream(), Path.GetFileName(file.Path));
                    student.Image = image;
                }
                bool isUpdated = await reseñaRepository.Update(student);
                if (isUpdated)
                {
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Update failed.", "Cancel");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async void ImageTap_Tapped(object sender, EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file != null)
                {
                    StudentImage.Source = ImageSource.FromStream(() =>
                    {
                        return file.GetStream();
                    });
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}