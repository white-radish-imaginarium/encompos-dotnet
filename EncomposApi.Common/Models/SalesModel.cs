using System;
using EncomposApi.Types.Optional;

namespace EncomposApi.Models;

public class SalesModel
{
    public Optional<string> ProductCode { get; set; }
    public Optional<DateTimeOffset> Date { get; set; }
    public decimal Qty { get; set; }
    public decimal Cost { get; set; }
    public decimal Retail { get; set; }
}
