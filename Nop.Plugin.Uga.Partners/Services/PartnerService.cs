using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Plugin.Uga.Partners.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Uga.Partners.Services
{
    public partial class PartnerService : IPartnerService
    {
        #region Constants
        private const string PARTNER_ALL_KEY = "Nop.partners.all-{0}-{1}";
        private const string PARTNER_PATTERN_KEY = "Nop.partners.";
        #endregion

        private readonly IRepository<PartnersRecord> _pRepository;
        private readonly ICacheManager _cacheManager;

        #region Ctor

        public PartnerService(ICacheManager cacheManager,
            IRepository<PartnersRecord> pRepository)
        {
            this._cacheManager = cacheManager;
            this._pRepository = pRepository;
        }

        #endregion

        #region Methods

        public virtual void DeletePartners()
        {
          
            var partners = this.GetAll();

            foreach(PartnersRecord r in partners)
                _pRepository.Delete(r);

        }

        public virtual IPagedList<PartnersRecord> GetAll(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            string key = string.Format(PARTNER_ALL_KEY, pageIndex, pageSize);
            return _cacheManager.Get(key, () =>
            {
                var query = from sbw in _pRepository.Table
                            orderby sbw.Name
                            select sbw;

                var records = new PagedList<PartnersRecord>(query, pageIndex, pageSize);
                return records;
            });
        }


		public virtual void DeletePartner(PartnersRecord record) {

			var partners = this.GetAll().Where(p=>p.Id==record.Id);

			foreach (PartnersRecord r in partners)
				_pRepository.Delete(r);

		}

		public virtual void InsertPartnerRecord(PartnersRecord record)
        {
            if (record == null)
                throw new ArgumentNullException("PartnersRecord");

            _pRepository.Insert(record);

            _cacheManager.RemoveByPattern(PARTNER_PATTERN_KEY);
        }

        public virtual void UpdatePartnerRecord(PartnersRecord record)
        {
            if (record == null)
                throw new ArgumentNullException("PartnersRecord");

            _pRepository.Update(record);
			
            _cacheManager.RemoveByPattern(PARTNER_PATTERN_KEY);
        }

        #endregion
    }
}
