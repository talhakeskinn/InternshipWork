using System.ServiceModel;

namespace CalculatorProject.Services.Abstract
{
    [ServiceContract]
    public interface ICalculatorService
    {
        [OperationContract]
        Task<string> Add();

        [OperationContract]
        Task<string> Substract();

        [OperationContract]
        Task<string> Divide();

        [OperationContract]
        Task<string> Multiply();
    }
}
