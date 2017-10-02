using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Uga.Partners.Services
{
    public partial interface IPartnerService
    {
        void DeletePartners();
		void DeletePartner(Partners.Domain.PartnersRecord record);

		IPagedList<Partners.Domain.PartnersRecord> GetAll(int pageIndex = 0, int pageSize = int.MaxValue);

        void InsertPartnerRecord(Partners.Domain.PartnersRecord record);

        void UpdatePartnerRecord(Partners.Domain.PartnersRecord record);
    }
}
