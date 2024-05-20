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
    public partial class ReseñaEntry : ContentPage
    {
        ReseñaRepository repository = new ReseñaRepository();
        public ReseñaEntry()
        {
            InitializeComponent();
        }

        private async void ButtonSave_Clicked(object sender, EventArgs e)
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

            var isSaved=await repository.Save(student);
            if(isSaved)
            {
                await DisplayAlert("Informacion", "La reseña se guardo", "Ok");
                Clear();
            }
            else
            {
                await DisplayAlert("Error", "La reseña no se guardo", "Ok");
            }

        }

        public void Clear()
        {
            TxtName.Text = string.Empty;
            TxtPlataforma.Text = string.Empty;
            TxtHoras.Text = string.Empty;
            TxtDesc.Text = string.Empty;
        }
    }
}