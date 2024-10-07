using System;
using static Villa_Utility.SD;

namespace Villa_VillaWEB.Models;

public class APIRequest
{
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string Url { get; set; }
    public object data { get; set; }
}
