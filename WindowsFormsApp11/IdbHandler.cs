using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp11
{
    interface IdbHandler
    {
        void ReadAll();
        void InsertOne(User oneUser);
        void DeleteOne(User oneUser);

        void DeleteAll();
        void UpdateOne(User oneUser);
    }
}
