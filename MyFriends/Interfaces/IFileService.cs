using MyFriends.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFriends.Interfaces
{
    public interface IFileService
    {
        List<Friend> Open(string filename);
        void Save(string filename, List<Friend> phonesList);
    }
}
