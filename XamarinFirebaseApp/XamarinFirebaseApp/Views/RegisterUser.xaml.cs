using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFirebaseApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterUser : ContentPage
    {

        UserRepository _userRepository = new UserRepository();
        public RegisterUser()
        {
            InitializeComponent();
        }

        private async void ButtonRegister_Clicked(object sender, EventArgs e)
        {
            try
            {
                string email = TxtEmail.Text;
                string password = TxtPassword.Text;
                string confirmPassword = TxtConfirmPass.Text;

                if (String.IsNullOrEmpty(email))
                {
                    await DisplayAlert("Alerta", "Escribe un correo", "Ok");
                    return;
                }
                if (password.Length<6)
                {
                    await DisplayAlert("Alerta", "La contraseña debe tener almenos 6 digitos.", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Alerta", "Escibre una contraseña", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(confirmPassword))
                {
                    await DisplayAlert("Alerta", "Confrima tu contraseña", "Ok");
                    return;
                }
                if (password != confirmPassword)
                {
                    await DisplayAlert("Alerta", "Password not match.", "Ok");
                    return;
                }

                bool isSave = await _userRepository.Register(email, password);
                if (isSave)
                {
                    await DisplayAlert("Registro", "Registro completado", "Ok");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Registro", "Registro fallido", "Ok");
                }
            }
            catch(Exception exception)
            {
               if(exception.Message.Contains("EMAIL_EXISTS"))
                {
                   await DisplayAlert("Alerta", "Este correo ya existe", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", exception.Message, "Ok");
                }
                
            }
            

        }   
    }
}