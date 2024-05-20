using Firebase.Database;
using Firebase.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFirebaseApp
{
    public class ReseñaRepository
    {
       
        FirebaseClient firebaseClient = new FirebaseClient("https://crudxamarin-68d46-default-rtdb.firebaseio.com/");
        FirebaseStorage firebaseStorage = new FirebaseStorage("crudxamarin-68d46.appspot.com");
        public async Task<bool> Save(ReseñaModel student)
        {
           var data=await firebaseClient.Child(nameof(ReseñaModel)).PostAsync(JsonConvert.SerializeObject(student));
            if(!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        public async Task<List<ReseñaModel>>GetAll()
        {
            return (await firebaseClient.Child(nameof(ReseñaModel)).OnceAsync<ReseñaModel>()).Select(item => new ReseñaModel
            {
                Plataforma = item.Object.Plataforma,
                Horas = item.Object.Horas,
                Descripcion = item.Object.Descripcion,
                Name = item.Object.Name,
                Image = item.Object.Image,
                Id = item.Key
            }).ToList();
        }

        public async Task<List<ReseñaModel>>GetAllByName(string name)
        {
            return (await firebaseClient.Child(nameof(ReseñaModel)).OnceAsync<ReseñaModel>()).Select(item => new ReseñaModel
            {
                Plataforma = item.Object.Plataforma,
                Horas = item.Object.Horas,
                Descripcion = item.Object.Descripcion,
                Name = item.Object.Name,
                Image = item.Object.Image,
                Id = item.Key
            }).Where(c=>c.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        public async Task<ReseñaModel> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(ReseñaModel) + "/" + id).OnceSingleAsync<ReseñaModel>());
        }

        public async Task<bool> Update(ReseñaModel student)
        {            
            await firebaseClient.Child(nameof(ReseñaModel) + "/" + student.Id).PutAsync(JsonConvert.SerializeObject(student));
            return true;
        }

        public async Task<bool>Delete(string id)
        {
            await firebaseClient.Child(nameof(ReseñaModel) + "/" + id).DeleteAsync();
            return true;
        }

        public async Task<string> Upload(Stream img, string fileName)
        {
            try
            {
                var image = await firebaseStorage.Child("Images").Child(fileName).PutAsync(img);
                return image;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading image: {ex.Message}");
                return null; // o cualquier otro valor que indique que la subida falló
            }
        }

    }
}
