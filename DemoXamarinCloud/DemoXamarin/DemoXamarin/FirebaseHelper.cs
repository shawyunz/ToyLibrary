using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoXamarin
{
    class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://xamarinlibrary-a2d6a.firebaseio.com/");

        public async Task<List<User>> GetAllPersons()
        {
            return (await firebase
              .Child("User")
              .OnceAsync<User>()).Select(item => new User
              {
                  Name = item.Object.Name,
                  ID = item.Object.ID
              }).ToList();
        }

        public async Task AddPerson(int personId, string name)
        {

            await firebase
              .Child("User")
              .PostAsync(new User() { ID = personId, Name = name });
        }

        public async Task<User> GetPerson(int personId)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("User")
              .OnceAsync<User>();
            return allPersons.Where(a => a.ID == personId).FirstOrDefault();
        }

        public async Task UpdatePerson(int personId, string name)
        {
            var toUpdatePerson = (await firebase
              .Child("User")
              .OnceAsync<User>()).Where(a => a.Object.ID == personId).FirstOrDefault();

            await firebase
              .Child("User")
              .Child(toUpdatePerson.Key)
              .PutAsync(new User() { ID = personId, Name = name });
        }

        public async Task DeletePerson(int personId)
        {
            var toDeletePerson = (await firebase
              .Child("User")
              .OnceAsync<User>()).Where(a => a.Object.ID == personId).FirstOrDefault();
            await firebase.Child("User").Child(toDeletePerson.Key).DeleteAsync();

        }

    }
}
