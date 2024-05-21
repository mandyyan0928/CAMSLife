using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaliphWeb.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [JsonProperty("Username")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [JsonProperty("PW")]
        public string Password { get; set; }
    }
}