using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmadaMotors.Domain.Configurations
{
    public class Filter : PaginationParams
    {
        /// <summary>
        /// Custom text
        /// </summary>
        public string Text { get; set; }

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
        public long? CategoryId { get; set; }
    }
}
