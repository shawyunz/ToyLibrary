using Demo0607.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo0607.Services
{
    class CloudDBHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://xamarinlibrary-a2d6a.firebaseio.com/");

        public async Task<List<User>> GetAllPersons()
        {
            return (await firebase
              .Child("User")
              .OnceAsync<User>()).Select(item => new User
              {
                  user_id = item.Object.user_id,
                  first_name = item.Object.first_name,
                  last_name = item.Object.last_name,
                  email = item.Object.email,
                  password = item.Object.password,
              }).ToList();
        }

        public async Task AddPerson(int personId, string name)
        {

            await firebase
              .Child("User")
              .PostAsync(null);
        }

        public async Task<User> GetPerson(int personId)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("User")
              .OnceAsync<User>();
            return allPersons.Where(a => a.user_id == personId).FirstOrDefault();
        }

        public async Task UpdatePerson(int personId, string name)
        {
            var toUpdatePerson = (await firebase
              .Child("User")
              .OnceAsync<User>()).Where(a => a.Object.user_id == personId).FirstOrDefault();

            await firebase
              .Child("User")
              .Child(toUpdatePerson.Key)
              .PutAsync(null);
        }

        public async Task DeletePerson(int personId)
        {
            var toDeletePerson = (await firebase
              .Child("User")
              .OnceAsync<User>()).Where(a => a.Object.user_id == personId).FirstOrDefault();
            await firebase.Child("User").Child(toDeletePerson.Key).DeleteAsync();

        }
    }
}
