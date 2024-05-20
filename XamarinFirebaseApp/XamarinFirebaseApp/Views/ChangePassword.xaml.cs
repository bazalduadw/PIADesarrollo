using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFirebaseApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePassword : ContentPage
    {
        UserRepository _userRepository = new UserRepository();
        public ChangePassword()
        {
            InitializeComponent();
        }

        private async void BtnChangePassword_Clicked(object sender, EventArgs e)
        {
            try
            {
                string password = TxtPassword.Text;
                string confirmPass = TxtConfirm.Text;
                if(string.IsNullOrEmpty(password))
                {
                   await DisplayAlert("Cambiar contraseña", "Porfavor ingresa una contraseña","Ok");
                    return;
                }
                if (string.IsNullOrEmpty(confirmPass))
                {
                  await  DisplayAlert("Cambiar contraseña", "Porfavor confirma tu contraseña", "Ok");
                    return;
                }
                if(password!=confirmPass)
                {
                    await DisplayAlert("Cambiar contraseña", "Confirm password not match.", "Ok");
                    return;
                }
                string token = Preferences.Get("token","");
                string newToken =await _userRepository.ChangePassword(token, password);
                if(!string.IsNullOrEmpty(newToken))
                {
                    await DisplayAlert("Cambiar contraseña", "Password has been changed.", "Ok");
                    Preferences.Set("token", newToken);
                    //Preferences.Clear();
                    //await Navigation.PushAsync(new LoginPage());
                }
                else
                {
                    await DisplayAlert("Cambiar contraseña", "Password Change failed.", "Ok");
                }
            }
            catch(Exception exception)
            {

            }
        }
    }
}