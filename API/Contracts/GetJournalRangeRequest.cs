using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TreeNodeApi;


public class GetJournalRangeRequest
{
    [Required,FromQuery]
    public int Skip { get; set; }
    [Required, FromQuery]
    public int Take { get; set; }

    [Required, FromBody] public Filter Filter { get; set; }
}