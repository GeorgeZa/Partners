using System;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Uga.Partners.Models
{
    [System.Diagnostics.DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PartnersModel : BaseNopEntityModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
		public string Latitude { get; set; }
		public string Longtitude { get; set; }
		public string Title { get; set; }
		public string Email { get; set; }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return ToString(); }
        }
    }
}