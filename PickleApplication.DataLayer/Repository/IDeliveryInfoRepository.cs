using PickleApplication.DataLayer.Core;
using PickleApplication.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickleApplication.DataLayer.Repository
{
    public interface IDeliveryInfoRepository : IRepository<DeliveryInfo>
    {
        Task<int> CreateDeliveryInfoAndReturnDeliveryInfoId(DeliveryInfo deliveryInfo);
    }
}
