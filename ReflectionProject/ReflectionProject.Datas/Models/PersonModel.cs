using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionProject.Datas.Models
{
    public class PersonModel
    {
        public class Person
        {
            public int Id { get; set; }
            public string Ad { get; set; }
            public string Soyad { get; set; }
            public byte Yas { get; set; }
        }
    }
}
