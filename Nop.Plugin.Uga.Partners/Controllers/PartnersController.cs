using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Uga.Partners.Infrastructure.Cache;
using Nop.Plugin.Uga.Partners.Models;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Security;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Services.Common;
using Nop.Core.Data;
using Nop.Core.Domain.Common;
using System.Collections;
using Nop.Plugin.Uga.Partners.Services;
using Nop.Plugin.Uga.Partners.Domain;

namespace Nop.Plugin.Uga.Partners.Controllers
{
    [NopHttpsRequirement(SslRequirement.NoMatter)]
    public class PartnersController : BasePluginController
    {
        #region Fields

      
        private readonly IWorkContext _workContext;
        private readonly IPartnerService _pService;
        private readonly IRepository<PartnersRecord> _pRepository;
        

        #endregion

        #region Ctor

        public PartnersController(IPartnerService pService,
            IWorkContext workContext,
            IRepository<PartnersRecord> pRepository
           
            )
        {
            this._pService = pService;
            this._workContext = workContext;
            this._pRepository = pRepository;
        }

		#endregion

		#region Utilities

		#endregion

		#region Methods

		[ChildActionOnly]
		public ActionResult Configure() {
			return View("~/Plugins/Uga.Partners/Views/Partners/Configure.cshtml");
		}

		[HttpPost]
		public ActionResult Configure(DataSourceRequest command) {
		
			var partnerModels = new List<PartnersModel>();
			foreach (var partner in _pService.GetAll())
				partnerModels.Add(new PartnersModel()
                {
					 Address =partner.Address,
					 City=partner.City,
					 Description=partner.Description,
					 Email=partner.Email,
					 Latitude=partner.Latitude,
					 Longtitude=partner.Longtitude,
					 Name=partner.Name,
					 PostCode=partner.PostCode,
					 Telephone=partner.Telephone,
					 Title=partner.Title,
					 Id=partner.Id
				});

            DataSourceResult gridModel = new DataSourceResult()
            {
				Data = partnerModels,
				Total = partnerModels.Count
			};
			return Json(gridModel);
		}

		[HttpPost]
		[AdminAntiForgery]
		public ActionResult PartnersUpdate(PartnersModel model) {
	
			int partnerId = model.Id;
			PartnersRecord rec = new PartnersRecord();
			rec.Id = model.Id;
			rec.Latitude = model.Latitude;
			rec.Longtitude = model.Longtitude;
			rec.Name = model.Name;
			rec.PostCode = model.PostCode;
			rec.Telephone = model.Telephone;
			rec.Title = model.Title;
			rec.Address = model.Address;
			rec.City = model.City;
			rec.Description = model.Description;
			rec.Email = model.Email;

            if (rec.Id == 0)
            {
                _pService.InsertPartnerRecord(rec);
            }
            else
            {
                _pService.UpdatePartnerRecord(rec);
            }

            //_pService.SetSetting(string.Format("ShippingRateComputationMethod.FixedRate.Rate.ShippingMethodId{0}", shippingMethodId), rate);

            return new NullJsonResult();
		}


		[HttpPost]
		[AdminAntiForgery]
		public ActionResult PartnersCreate(PartnersModel model) {

			int partnerId = model.Id;
			PartnersRecord rec = new PartnersRecord();
			rec.Id = model.Id;
			rec.Latitude = model.Latitude;
			rec.Longtitude = model.Longtitude;
			rec.Name = model.Name;
			rec.PostCode = model.PostCode;
			rec.Telephone = model.Telephone;
			rec.Title = model.Title;
			rec.Address = model.Address;
			rec.City = model.City;
			rec.Description = model.Description;
			rec.Email = model.Email;

			if (rec.Id == 0)
				_pService.InsertPartnerRecord(rec);
			else
				_pService.UpdatePartnerRecord(rec);

			//_pService.SetSetting(string.Format("ShippingRateComputationMethod.FixedRate.Rate.ShippingMethodId{0}", shippingMethodId), rate);

			return new NullJsonResult();
		}

		[HttpPost]
		[AdminAntiForgery]
		public ActionResult PartnersDelete(PartnersModel model) {


			PartnersRecord rec = new PartnersRecord();
			rec.Id = model.Id;

			_pService.DeletePartner(rec);


			//_pService.SetSetting(string.Format("ShippingRateComputationMethod.FixedRate.Rate.ShippingMethodId{0}", shippingMethodId), rate);

			return new NullJsonResult();
		}



        public ActionResult Index()
        {
            return View("~/Plugins/Uga.Partners/Views/Partners/Index.cshtml");
        }


        [HttpPost]
        public ActionResult PartnersGridList(DataSourceRequest request, string id) {
            IList<PartnersModel> partners;
            if (!string.IsNullOrEmpty(id))
            {
                partners = (whereReduce(id)).Distinct().ToList();
            }
            else {
                 partners = (from c in _pRepository.Table.AsEnumerable()
                             select getPartnerDetails(c)).Distinct().ToList();
            }

            var gridModel = new DataSourceResult {
                Data = partners,
                Total = partners.Count()
            };

            return Json(gridModel, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<PartnersModel> whereReduce(string id)
        {
            return from c in _pRepository.Table.AsEnumerable()
                   where (c.City.ToUpper().Contains(id.ToUpper()) || c.PostCode.ToUpper().Contains(id.ToUpper()))
                   select getPartnerDetails(c);
        }

        public ActionResult PartnersList(string id)
        {
            IList<PartnersModel> partners;
            if (!string.IsNullOrEmpty(id))
            {
                partners = (whereReduce(id)).Distinct().ToList();

            }
            else {
                partners = (from c in _pRepository.Table.AsEnumerable()
                            select getPartnerDetails(c)).Distinct().ToList();
            }
           

            return Json(partners, JsonRequestBehavior.AllowGet);
        }

        private static PartnersModel getPartnerDetails(PartnersRecord c)
        {
            return new PartnersModel
            {
                Id = c.Id,
                Name = c.Name,
                Title = c.Description,
                Email = c.Email,
                Description = c.Description,
                Telephone = c.Telephone,
                PostCode = c.PostCode,
                Address = c.Address,
                City = c.City,
                Latitude = c.Latitude,
                Longtitude = c.Longtitude
            };
        }

        #endregion
    }
}   