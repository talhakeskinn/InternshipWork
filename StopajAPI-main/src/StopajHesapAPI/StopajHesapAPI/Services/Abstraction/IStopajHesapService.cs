using StopajHesapAPI.Entities;
using StopajHesapAPI.Models;
using System.Reflection.Metadata;

namespace StopajHesapAPI.Services.Abstraction
{
    public interface IStopajHesapService
    {
        ResponseStopaj Hesapla(RequestStopaj T);
    }
}
