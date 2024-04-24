using Refit;

namespace SuperPoubelle.BalanceCommunication
{
    public interface IBalanceRestClient
    {
        [Get("/balance")]
        Task<IEnumerable<BalanceReport>> GetBalanceReports();

        [Post("/balance/{id}/calibrate")]
        Task Calibrate([AliasAs("id")] string balanceId, [Body] CalibrationBody body);
    }

    public class BalanceReport
    {
        public string BalanceIdentifier { get; set; }
        public double Grams { get; set; }
    }

    public class CalibrationBody
    {
        public double Grams { get; set; }
    }
}
