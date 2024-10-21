using StopajHesapAPI.Entities;
using StopajHesapAPI.Models;
using StopajHesapAPI.Services.Abstraction;

namespace StopajHesapAPI.Services.Concerete
{
    public class StopajHesapService : IStopajHesapService
    {
        private readonly ResponseStopaj _parametersModels;

        public StopajHesapService(ResponseStopaj parametersModels)
        {
            _parametersModels = parametersModels;
        }
        public ResponseStopaj Hesapla(RequestStopaj parametersEntity)
        {
            if (parametersEntity.oran <= 0)
                throw new ApplicationException("Oran Küçük olamaz");
            if (parametersEntity.isNet)
            {
                _parametersModels.newMatrah = parametersEntity.matrah / (1 - parametersEntity.oran / 10000);
                _parametersModels.Stopaj = parametersEntity.matrah * (parametersEntity.oran / 100);
                return _parametersModels;
            }

            else
            {
                _parametersModels.Stopaj = parametersEntity.matrah * (parametersEntity.oran / 100);
                return _parametersModels;
            }
        }
    }
}
