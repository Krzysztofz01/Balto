using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Balto.Web.ViewModels
{
    public class EntryOrder
    {
        [Required]
        public IList<long> Order { get; set; }
    }
}
