using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CalculatorProject.Services.Abstract;
using CalculatorSoapServiceReferans;

namespace CalculatorProject.Services.Concrete
{
    public class CalculatorService : ICalculatorService
    {
        private readonly RequestModel _requestModel;
        private readonly CalculatorSoapClient _client;
        private readonly XmlSerializer _requestSerializer;
        private readonly XmlSerializer _responseSerializer;

        public CalculatorService(RequestModel requestModel, CalculatorSoapClient client)
        {
            _requestModel = requestModel;
            _client = client;
            _responseSerializer = new XmlSerializer(typeof(ResponseModel));
        }

        public async Task<string> Add()
        {
            var result = await _client.AddAsync(_requestModel.A, _requestModel.B);
            var responseModel = new ResponseModel { Result = result.ToString() };
            return SerializeToXml<ResponseModel>(responseModel);
        }

        public async Task<string> Divide()
        {
            var result = await _client.DivideAsync(_requestModel.A, _requestModel.B);
            var responseModel = new ResponseModel { Result = result.ToString() };
            return SerializeToXml<ResponseModel>(responseModel);
        }

        public async Task<string> Multiply()
        {
            var result = await _client.MultiplyAsync(_requestModel.A, _requestModel.B);
            var responseModel = new ResponseModel { Result = result.ToString() };
            return SerializeToXml<ResponseModel>(responseModel);
        }

        public async Task<string> Substract()
        {
            var result = await _client.SubtractAsync(_requestModel.A, _requestModel.B);
            var responseModel = new ResponseModel { Result = result.ToString() };
            return SerializeToXml<ResponseModel>(responseModel);
        }

        private string SerializeToXml<T>(T obj)
        {
            using (var stringWriter = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }
    }
}
