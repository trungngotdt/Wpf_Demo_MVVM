using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SinhVien
    {
        private int id;

        public int Id { get => id; set => id = value; }
        public string Ten { get => ten; set => ten = value; }

        private string ten;
    }
}
