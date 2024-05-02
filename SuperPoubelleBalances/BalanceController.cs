using Microsoft.AspNetCore.Mvc;
using UnitsNet;

[ApiController]
[Route("[controller]")]
public class BalanceController : ControllerBase
{
    [HttpGet(Name = "Balance Reports")]
    public IActionResult Get()
    {
        var ret = new List<WeightReport>();
        for(int i = 0; i < Program.Balances.Count; i++){
            ret.Add(new WeightReport{BalanceIdentifier= i.ToString(), Grams = Program.Balances[i].GetWeight().Grams});
        }        
        return Ok(ret);
    }

    [HttpPost("{id}/calibrate")]
    public IActionResult CalibrateBalance(int id, [FromBody] CalibrationBody body)
    {
        Program.Balances[id].SetCalibration(Mass.FromGrams(body.Grams));
        return NoContent();
    }
}

public class WeightReport
{
    public string BalanceIdentifier { get; set; } = string.Empty;
    public double Grams { get; set; }
}

public class CalibrationBody
{
    public double Grams { get; set; }
}