using Nop.Data.Mapping;
using Nop.Plugin.Uga.Partners.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Uga.Partners.Data
{
    public partial class PartnersRecordMap : NopEntityTypeConfiguration<PartnersRecord>
    {
        public PartnersRecordMap()
        {
            this.ToTable("Partners");
            this.HasKey(x => x.Id);
        }
    }
}
