using NbaRosManagementTool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NbaRosManagementTool.ViewModels
{
    public class OfferViewModel
    {
        public List<KeyValuePair<Offer,FreeAgent>> Offers { get; set; }
    }
}
