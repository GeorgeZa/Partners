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
    [System.Diagnostics.DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PartnerService : IPartnerService
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

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string DebuggerDisplay => ToString();

        #endregion

        #region Methods

        public virtual void DeletePartners()
        {
            var partners = this.GetAll();

            RepoDelete(partners);
        }

        public virtual IPagedList<PartnersRecord> GetAll(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var key = string.Format(PARTNER_ALL_KEY, pageIndex, pageSize);
            return _cacheManager.Get(key, () =>
            {
                var query = from sbw in _pRepository.Table
                            orderby sbw.Name
                            select sbw;

                var records = new PagedList<PartnersRecord>(query, pageIndex, pageSize);
                return records;
            });
        }

        public virtual void DeletePartner(PartnersRecord record)
        {
            IEnumerable<PartnersRecord> partners = GetAll().Where(p => p.Id == record.Id);

            RepoDelete(partners);
        }

        private void RepoDelete(IEnumerable<PartnersRecord> partners)
        {
            foreach (var r in partners)
            {
                _pRepository.Delete(r);
            }
        }

        public virtual void InsertPartnerRecord(PartnersRecord record)
        {

            _pRepository.Insert(record);

            _cacheManager.RemoveByPattern(PARTNER_PATTERN_KEY);
        }

        public virtual void UpdatePartnerRecord(PartnersRecord record)
        {
           

            _pRepository.Update(record);
            _cacheManager.RemoveByPattern(PARTNER_PATTERN_KEY);
        }

       

        #endregion
    }
}
