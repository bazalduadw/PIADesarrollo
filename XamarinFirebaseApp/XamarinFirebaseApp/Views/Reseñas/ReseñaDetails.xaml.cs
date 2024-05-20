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
    public partial class ReseñaDetails : ContentPage
    {
        public ReseñaDetails(ReseñaModel student)
        {
            InitializeComponent();
            LabelName.Text = student.Name;
            LabelPlataforma.Text = student.Plataforma;
            LabelHoras.Text = student.Horas;
            LabelDesc.Text = student.Descripcion;
        }
    }
}