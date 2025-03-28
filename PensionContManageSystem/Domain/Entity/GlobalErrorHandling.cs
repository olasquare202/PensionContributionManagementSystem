using Newtonsoft.Json;

namespace PensionContManageSystem.Domain.Entity
{
    public class GlobalErrorHandling
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
