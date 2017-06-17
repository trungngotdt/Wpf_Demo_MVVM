using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ViewModel
{
    public class SinhVienViewModel
    {
        private ObservableCollection<SinhVien> listSinhVien;

        public ObservableCollection<SinhVien> ListSinhVien { get => listSinhVien; set => listSinhVien = value; }
        public SinhVienViewModel()
        {
            listSinhVien = new ObservableCollection<SinhVien>
            {
                new SinhVien() { Id = 1, Ten = "A" },
                new SinhVien() { Id = 2, Ten = "B" },
                new SinhVien() { Id = 3, Ten = "C" },
                new SinhVien() { Id = 4, Ten = "D" }
            };
            DeleteCommad = new RayCommand<object>((p) => p != null, (p) => { ListSinhVien.Remove(p as SinhVien); });
            AddCommad = new RayCommand<UIElementCollection>((p) => true, (p) =>
              {
                  int id = 0;
                  string ten = "";
                  bool IsIntId = false;
                  
                  foreach (var item in p)
                  {
                      TextBox a= item as TextBox;
                      if (String.IsNullOrEmpty( a.Name))
                      {
                          continue;
                      }
                      switch (a.Name)
                      {
                          case "Txb_ID":
                              IsIntId = Int32.TryParse(a.Name, out id);
                              break;
                          case "Txb_Ten":
                              ten = a.Text;
                              break;
                          
                      }
                      if (IsIntId)
                      {
                          return;
                      }
                  }
                  SinhVien sv = new SinhVien()
                  {
                      Id = id,
                      Ten = ten
                  };
                  ListSinhVien.Add(sv);
              });
        }
        public ICommand DeleteCommad { get; set; }
        public ICommand AddCommad { get; set; }
    }
}
