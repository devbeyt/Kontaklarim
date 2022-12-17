using System;
using System.Collections.Generic;
using System.Text;

namespace Kontaklar.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
    }
}
