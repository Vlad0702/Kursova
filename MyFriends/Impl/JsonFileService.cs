using MyFriends.Data;
using MyFriends.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace MyFriends.ViewModels
{
    public class JsonFileService : IFileService
    {
        public List<Friend> Open(string filename)
        {
            List<Friend> phonesList = new List<Friend>();
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(List<Friend>));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                phonesList = jsonFormatter.ReadObject(fs) as List<Friend>;
            }

            return phonesList;
        }

        public void Save(string filename, List<Friend> friendsList)
        {
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(List<Friend>));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, friendsList);
            }
        }
    }
}
