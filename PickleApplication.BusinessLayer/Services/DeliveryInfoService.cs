using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PickleApplication.BusinessLayer.Models;
using PickleApplication.DataLayer.Repository;

namespace PickleApplication.BusinessLayer.Services
{
    public class DeliveryInfoService : IDeliveryInfoService
    {
        public DeliveryInfoService(IDeliveryInfoRepository deliveryInfoRepository)
        {
            _deliveryInfoRepository = deliveryInfoRepository;
        }
        public async Task<bool> CreateDeliveryInfo(DeliveryInfo deliveryInfo)
        {
            return await _deliveryInfoRepository.Create(new DataLayer.Models.DeliveryInfo {
                Id = deliveryInfo.DeliveryInfoId,
                Address = deliveryInfo.Address,
                CityId = deliveryInfo.Region.City.CityId,
                CountryId = deliveryInfo.Region.City.Country.CountryId,
                RegionId = deliveryInfo.Region.RegionId
            });
        }

        public async Task<int> CreateDeliveryInfoAndReturnDeliveryInfoId(DeliveryInfo deliveryInfo)
        {
            return await _deliveryInfoRepository.CreateDeliveryInfoAndReturnDeliveryInfoId(new DataLayer.Models.DeliveryInfo
            {
                Id = deliveryInfo.DeliveryInfoId,
                Address = deliveryInfo.Address,
                CityId = deliveryInfo.Region.City.CityId,
                CountryId = deliveryInfo.Region.City.Country.CountryId,
                RegionId = deliveryInfo.Region.RegionId
            });
        }

        private readonly IDeliveryInfoRepository _deliveryInfoRepository;
    }
}
