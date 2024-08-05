using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnccfChords.Models
{
    public class PaginationResponse<T>
    {
        public int CurrentPage { get; set; }
        public int ElementsPerPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalElements { get; set; }
        public ObservableCollection<object> Results { get; set; }
    }
}
