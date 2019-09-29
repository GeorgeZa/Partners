using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Uga.Partners.Domain
{
    public partial class PartnersRecord : BaseEntity
    {
        public Guid? UniqueID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
		public string Latitude { get; set; }
		public string Longtitude { get; set; }
		public string Title { get; set; }
	}
}
