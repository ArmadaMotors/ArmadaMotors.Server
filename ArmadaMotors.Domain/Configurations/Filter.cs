using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArmadaMotors.Domain.Enums;

namespace ArmadaMotors.Domain.Configurations
{
    public class Filter : PaginationParams
    {
        /// <summary>
        /// Custom text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Currency type
        /// </summary>
        /// <value></value>
        public CurrencyType? CurrencyType { get; set; }

        /// <summary>
        /// Price from
        /// </summary>
        public decimal? From { get; set; }

        /// <summary>
        /// Price to
        /// </summary>
        public decimal? To { get; set; }
        
        /// <summary>
        /// Category name
        /// </summary>
        public long[] CategoryIds { get; set; }
    }
}
