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
    public partial class ForgotPasswordPage : ContentPage
    {
        UserRepository _userRepository = new UserRepository();
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        private async void ButtonSendLink_Clicked(object sender, EventArgs e)
        {
            string email = TxtEmail.Text;
            if(string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Alerta", "Porfavor ingresa tu correo", "Ok");
                return;
            }

            bool isSend =await _userRepository.ResetPassword(email);
            if(isSend)
            {
                await DisplayAlert("Recuperar contraseña", "El link se envio a tu correo.", "Ok");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Recuperar contraseña", "El link no se envio", "Ok");
            }
        }
    }
}